/**
 * Web 区域编辑器 — 对齐 Desktop RegionEditorViewModel
 * 支持矩形/多边形绘制、预定义区域、自定义区域、删除
 */
function rt(key) {
    return (typeof LuxI18n !== 'undefined' && LuxI18n.t) ? LuxI18n.t(key) : key;
}
function fmtR(template, ...args) {
    return String(template).replace(/\{(\d+)\}/g, (_, i) => args[Number(i)] ?? '');
}

const regionEditor = {
    canvas: null,
    ctx: null,
    bgImage: null,
    scale: 1,
    offsetX: 0,
    offsetY: 0,
    imgW: 0,
    imgH: 0,

    // 区域定义（来自算法）
    requiredRegions: [],
    // 已绘制区域: { name, displayName, color, points: [{x,y},...] }
    drawnRegions: [],
    // 当前选中的目标
    selectedTarget: null,
    customName: '',

    // 绘制状态
    mode: 'none', // 'none' | 'rect' | 'polygon'
    isDrawing: false,
    rectStart: null,
    rectEnd: null,
    polyPoints: [],

    // 回调
    onSave: null,

    /**
     * 打开区域编辑器
     * @param {Object} opts
     * @param {string} opts.algorithmType - 算法类型
     * @param {Object} opts.videoSource - 视频源配置 {type, source, ...}
     * @param {string} opts.configId - 配置 ID（运行中时用快照）
     * @param {Array}  opts.existingRegions - 已有区域 JSON
     * @param {Function} opts.onSave - 保存回调 (regionsJson)
     */
    async open(opts) {
        this.onSave = opts.onSave || null;
        this.drawnRegions = [];
        this.mode = 'none';
        this.isDrawing = false;
        this.polyPoints = [];
        this.rectStart = null;
        this.customName = '';

        // 获取算法的区域定义
        try {
            const resp = await fetch(`/api/algorithms/${opts.algorithmType}/regions`);
            this.requiredRegions = resp.ok ? await resp.json() : [];
        } catch { this.requiredRegions = []; }

        // 加载已有区域
        if (opts.existingRegions && opts.existingRegions.length > 0) {
            for (const r of opts.existingRegions) {
                const def = this.requiredRegions.find(d => d.name === r.name);
                this.drawnRegions.push({
                    name: r.name,
                    displayName: r.displayName || def?.displayName || r.name,
                    color: def?.color || 'rgba(0,255,0,1)',
                    points: r.points || []
                });
            }
        }

        // 自动选中第一个未绘制的必需区域
        this.autoSelectNext();

        // 显示 Modal
        document.getElementById('regionEditorModal').classList.add('active');
        this.renderSidebar();
        this.setStatus(rt('WebRegion_LoadingFrame'));

        // 获取帧
        await this.loadFrame(opts);
    },

    async loadFrame(opts) {
        let imageUrl = null;

        // 优先从运行中的会话取快照
        if (opts.configId) {
            const resp = await fetch(`/api/snapshot/${opts.configId}`);
            if (resp.ok) {
                const blob = await resp.blob();
                imageUrl = URL.createObjectURL(blob);
            }
        }

        // 备选：从视频源直接捕获
        if (!imageUrl && opts.videoSource && opts.videoSource.source) {
            try {
                const resp = await fetch('/api/capture-frame', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(opts.videoSource)
                });
                if (resp.ok) {
                    const blob = await resp.blob();
                    imageUrl = URL.createObjectURL(blob);
                }
            } catch {}
        }

        if (!imageUrl) {
            this.setStatus(rt('WebRegion_NoFrame'));
            return;
        }

        const img = new Image();
        img.onload = () => {
            this.bgImage = img;
            this.imgW = img.naturalWidth;
            this.imgH = img.naturalHeight;
            this.initCanvas();
            this.draw();
            this.setStatus(fmtR(rt('WebRegion_LoadedFmt'), this.imgW, this.imgH));
        };
        img.src = imageUrl;
    },

    initCanvas() {
        this.canvas = document.getElementById('regionCanvas');
        this.ctx = this.canvas.getContext('2d');

        const container = this.canvas.parentElement;
        const maxW = container.clientWidth;
        const maxH = container.clientHeight;

        this.scale = Math.min(maxW / this.imgW, maxH / this.imgH, 1);
        const cw = Math.floor(this.imgW * this.scale);
        const ch = Math.floor(this.imgH * this.scale);

        this.canvas.width = cw;
        this.canvas.height = ch;
        this.offsetX = 0;
        this.offsetY = 0;

        // 事件绑定
        this.canvas.onmousedown = e => this.onMouseDown(e);
        this.canvas.onmousemove = e => this.onMouseMove(e);
        this.canvas.onmouseup = e => this.onMouseUp(e);
        this.canvas.oncontextmenu = e => { e.preventDefault(); this.onRightClick(e); };
    },

    toImageCoords(e) {
        const rect = this.canvas.getBoundingClientRect();
        return {
            x: Math.round((e.clientX - rect.left) / this.scale),
            y: Math.round((e.clientY - rect.top) / this.scale)
        };
    },

    // ═══ Drawing ═══

    draw() {
        if (!this.ctx || !this.bgImage) return;
        const ctx = this.ctx;
        const s = this.scale;

        ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
        ctx.drawImage(this.bgImage, 0, 0, this.imgW * s, this.imgH * s);

        // 已绘制区域
        for (const region of this.drawnRegions) {
            this.drawRegion(ctx, region, s);
        }

        // 当前绘制预览
        if (this.mode === 'polygon' && this.polyPoints.length > 0) {
            this.drawPolyPreview(ctx, s);
        }

        if (this.mode === 'rect' && this.isDrawing && this.rectStart && this.rectEnd) {
            this.drawRectPreview(ctx, s);
        }
    },

    drawRegion(ctx, region, s) {
        const pts = region.points;
        if (pts.length < 2) return;

        ctx.save();

        // 填充
        ctx.beginPath();
        ctx.moveTo(pts[0].x * s, pts[0].y * s);
        for (let i = 1; i < pts.length; i++) ctx.lineTo(pts[i].x * s, pts[i].y * s);
        ctx.closePath();
        ctx.fillStyle = this.withAlpha(region.color, 0.15);
        ctx.fill();

        // 边框
        ctx.strokeStyle = region.color;
        ctx.lineWidth = 2;
        ctx.stroke();

        // 顶点
        for (const p of pts) {
            ctx.beginPath();
            ctx.arc(p.x * s, p.y * s, 4, 0, Math.PI * 2);
            ctx.fillStyle = region.color;
            ctx.fill();
        }

        // 标签
        const label = region.displayName || region.name;
        const tx = pts[0].x * s;
        const ty = pts[0].y * s - 8;
        ctx.font = 'bold 13px sans-serif';
        const metrics = ctx.measureText(label);
        ctx.fillStyle = 'rgba(0,0,0,0.6)';
        ctx.fillRect(tx - 2, ty - 14, metrics.width + 6, 18);
        ctx.fillStyle = region.color;
        ctx.fillText(label, tx, ty);

        ctx.restore();
    },

    drawPolyPreview(ctx, s) {
        const color = this.getTargetColor();
        ctx.save();

        // 顶点
        for (const p of this.polyPoints) {
            ctx.beginPath();
            ctx.arc(p.x * s, p.y * s, 5, 0, Math.PI * 2);
            ctx.fillStyle = color;
            ctx.fill();
        }

        // 连线
        if (this.polyPoints.length >= 2) {
            ctx.beginPath();
            ctx.moveTo(this.polyPoints[0].x * s, this.polyPoints[0].y * s);
            for (let i = 1; i < this.polyPoints.length; i++)
                ctx.lineTo(this.polyPoints[i].x * s, this.polyPoints[i].y * s);
            ctx.strokeStyle = color;
            ctx.lineWidth = 2;
            ctx.setLineDash([6, 3]);
            ctx.stroke();
        }

        ctx.restore();
    },

    drawRectPreview(ctx, s) {
        const color = this.getTargetColor();
        const x1 = Math.min(this.rectStart.x, this.rectEnd.x) * s;
        const y1 = Math.min(this.rectStart.y, this.rectEnd.y) * s;
        const w = Math.abs(this.rectEnd.x - this.rectStart.x) * s;
        const h = Math.abs(this.rectEnd.y - this.rectStart.y) * s;

        ctx.save();
        ctx.fillStyle = this.withAlpha(color, 0.12);
        ctx.fillRect(x1, y1, w, h);
        ctx.strokeStyle = color;
        ctx.lineWidth = 2;
        ctx.setLineDash([6, 3]);
        ctx.strokeRect(x1, y1, w, h);
        ctx.restore();
    },

    // ═══ Mouse Events ═══

    onMouseDown(e) {
        if (e.button !== 0) return;
        const p = this.toImageCoords(e);

        if (this.mode === 'rect') {
            this.isDrawing = true;
            this.rectStart = p;
            this.rectEnd = p;
        } else if (this.mode === 'polygon') {
            this.polyPoints.push(p);
            this.draw();
            this.setStatus(fmtR(rt('WebRegion_PolyPointsFmt'), this.polyPoints.length));
        }
    },

    onMouseMove(e) {
        if (this.mode === 'rect' && this.isDrawing) {
            this.rectEnd = this.toImageCoords(e);
            this.draw();
        }
    },

    onMouseUp(e) {
        if (e.button !== 0) return;
        if (this.mode === 'rect' && this.isDrawing) {
            this.isDrawing = false;
            this.rectEnd = this.toImageCoords(e);
            this.finishRect();
        }
    },

    onRightClick(e) {
        if (this.mode === 'polygon' && this.polyPoints.length >= 3) {
            this.finishPolygon();
        }
    },

    // ═══ Finish Drawing ═══

    finishRect() {
        const x1 = Math.min(this.rectStart.x, this.rectEnd.x);
        const y1 = Math.min(this.rectStart.y, this.rectEnd.y);
        const x2 = Math.max(this.rectStart.x, this.rectEnd.x);
        const y2 = Math.max(this.rectStart.y, this.rectEnd.y);

        if (x2 - x1 < 10 || y2 - y1 < 10) {
            this.setStatus(rt('WebRegion_RectTooSmall'));
            this.rectStart = null;
            this.rectEnd = null;
            this.draw();
            return;
        }

        const target = this.resolveTarget();
        if (!target) return;

        const points = [
            { x: x1, y: y1 }, { x: x2, y: y1 },
            { x: x2, y: y2 }, { x: x1, y: y2 }
        ];

        this.addRegion(target.name, target.displayName, target.color, points);
        this.rectStart = null;
        this.rectEnd = null;
    },

    finishPolygon() {
        if (this.polyPoints.length < 3) {
            this.setStatus(rt('WebRegion_PolyMinPoints'));
            return;
        }

        const target = this.resolveTarget();
        if (!target) {
            this.polyPoints = [];
            this.draw();
            return;
        }

        this.addRegion(target.name, target.displayName, target.color, [...this.polyPoints]);
        this.polyPoints = [];
    },

    resolveTarget() {
        if (!this.selectedTarget) {
            this.setStatus(rt('WebRegion_SelectTargetFirst'));
            return null;
        }

        if (this.selectedTarget.name === '__custom__') {
            const name = document.getElementById('reCustomName')?.value?.trim();
            if (!name) { this.setStatus(rt('WebRegion_EnterCustomName')); return null; }
            return { name, displayName: name, color: 'rgba(0,200,0,1)' };
        }

        return {
            name: this.selectedTarget.name,
            displayName: this.selectedTarget.displayName,
            color: this.selectedTarget.color
        };
    },

    addRegion(name, displayName, color, points) {
        const existing = this.drawnRegions.findIndex(r => r.name === name);
        if (existing >= 0) this.drawnRegions.splice(existing, 1);

        this.drawnRegions.push({ name, displayName, color, points });
        this.autoSelectNext();
        this.renderSidebar();
        this.draw();
        this.setStatus(fmtR(rt('WebRegion_AddedFmt'), displayName, points.length));
    },

    deleteRegion(name) {
        this.drawnRegions = this.drawnRegions.filter(r => r.name !== name);
        this.autoSelectNext();
        this.renderSidebar();
        this.draw();
        this.setStatus(rt('WebRegion_Deleted'));
    },

    autoSelectNext() {
        if (this.requiredRegions.length === 0) return;
        const drawnNames = new Set(this.drawnRegions.map(r => r.name));
        const next = this.requiredRegions.find(r => !drawnNames.has(r.name));
        this.selectedTarget = next || this.requiredRegions[0];
    },

    // ═══ Mode Controls ═══

    setMode(mode) {
        this.mode = mode;
        this.isDrawing = false;
        this.polyPoints = [];
        this.rectStart = null;
        this.rectEnd = null;
        this.draw();

        const msgs = { rect: rt('WebRegion_ModeRect'), polygon: rt('WebRegion_ModePoly'), none: rt('WebRegion_ModeNone') };
        this.setStatus(msgs[mode] || '');
        this.renderSidebar();
    },

    clearCurrent() {
        this.polyPoints = [];
        this.isDrawing = false;
        this.rectStart = null;
        this.rectEnd = null;
        this.draw();
        this.setStatus(rt('WebRegion_Cleared'));
    },

    // ═══ Sidebar Rendering ═══

    renderSidebar() {
        const drawnNames = new Set(this.drawnRegions.map(r => r.name));

        // 目标区域列表
        let targetsHtml = '';
        for (const r of this.requiredRegions) {
            const isDrawn = drawnNames.has(r.name);
            const isSelected = this.selectedTarget?.name === r.name;
            const mark = isDrawn ? '✓' : (r.required ? '*' : '');
            targetsHtml += `<div class="re-target ${isSelected ? 'active' : ''} ${isDrawn ? 'drawn' : ''}"
                onclick="regionEditor.selectTarget('${r.name}')">
                <span class="re-target-mark">${mark}</span>
                <span>${esc(r.displayName)}</span>
            </div>`;
        }

        // 自定义区域选项
        const isCustom = this.selectedTarget?.name === '__custom__';
        targetsHtml += `<div class="re-target ${isCustom ? 'active' : ''}"
            onclick="regionEditor.selectTarget('__custom__')">
            <span class="re-target-mark">+</span>
            <span>${esc(rt('WebRegion_CustomLabel'))}</span>
        </div>`;

        if (isCustom) {
            targetsHtml += `<input type="text" id="reCustomName" class="re-custom-input"
                placeholder="${esc(rt('WebRegion_WmCustomName'))}" value="${esc(this.customName)}"
                oninput="regionEditor.customName=this.value">`;
        }

        document.getElementById('reTargets').innerHTML = targetsHtml;

        // 已绘制列表
        if (this.drawnRegions.length === 0) {
            document.getElementById('reDrawnList').innerHTML =
                `<div style="text-align:center;padding:12px;color:var(--text-muted);font-size:12px;">${esc(rt('WebRegion_NoDrawnYet'))}</div>`;
        } else {
            document.getElementById('reDrawnList').innerHTML = this.drawnRegions.map(r =>
                `<div class="re-drawn-item">
                    <span style="color:${r.color};">●</span>
                    <span style="flex:1;">${esc(r.displayName)} (${r.points.length}点)</span>
                    <button class="btn-link" style="font-size:11px;color:var(--danger);"
                        onclick="regionEditor.deleteRegion('${r.name}')">${esc(rt('Web_ActionDelete'))}</button>
                </div>`
            ).join('');
        }

        // 模式按钮高亮
        document.getElementById('reBtnRect')?.classList.toggle('active-mode', this.mode === 'rect');
        document.getElementById('reBtnPoly')?.classList.toggle('active-mode', this.mode === 'polygon');

        // 描述
        const desc = this.selectedTarget && this.selectedTarget.name !== '__custom__'
            ? this.selectedTarget.description || '' : '';
        document.getElementById('reTargetDesc').textContent = desc;
    },

    selectTarget(name) {
        if (name === '__custom__') {
            this.selectedTarget = { name: '__custom__', displayName: rt('WebRegion_CustomLabel'), color: 'rgba(0,200,0,1)' };
        } else {
            this.selectedTarget = this.requiredRegions.find(r => r.name === name) || null;
        }
        this.renderSidebar();
    },

    // ═══ Save / Close ═══

    save() {
        // 检查必需区域
        const drawnNames = new Set(this.drawnRegions.map(r => r.name));
        const missing = this.requiredRegions.filter(r => r.required && !drawnNames.has(r.name));
        if (missing.length > 0) {
            this.setStatus(fmtR(rt('WebRegion_MissingRequiredFmt'), missing.map(r => r.displayName).join('、')));
            return;
        }

        const result = this.drawnRegions.map(r => ({
            name: r.name,
            displayName: r.displayName,
            description: '',
            points: r.points,
            color: this.rgbaToHex(r.color),
            required: this.requiredRegions.find(d => d.name === r.name)?.required || false,
            visible: true
        }));

        if (this.onSave) this.onSave(JSON.stringify(result, null, 2));
        this.close();
    },

    close() {
        document.getElementById('regionEditorModal').classList.remove('active');
        if (this.canvas) {
            this.canvas.onmousedown = null;
            this.canvas.onmousemove = null;
            this.canvas.onmouseup = null;
            this.canvas.oncontextmenu = null;
        }
    },

    // ═══ Helpers ═══

    setStatus(text) {
        const el = document.getElementById('reStatus');
        if (el) el.textContent = text;
    },

    getTargetColor() {
        return this.selectedTarget?.color || 'rgba(255,0,0,1)';
    },

    rgbaToHex(color) {
        if (color.startsWith('#') && /^#[0-9A-Fa-f]{6}$/.test(color)) return color;
        const m = color.match(/rgba?\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)/);
        if (!m) return '#00FF00';
        return '#' + [m[1], m[2], m[3]]
            .map(c => parseInt(c).toString(16).padStart(2, '0').toUpperCase())
            .join('');
    },

    withAlpha(color, alpha) {
        if (color.startsWith('rgba')) {
            return color.replace(/,\s*[\d.]+\)/, `,${alpha})`);
        }
        return color;
    }
};
