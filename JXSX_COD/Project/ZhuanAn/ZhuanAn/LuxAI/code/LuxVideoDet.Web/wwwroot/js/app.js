function t(key) {
    return (typeof LuxI18n !== 'undefined' && LuxI18n.t) ? LuxI18n.t(key) : key;
}

/** 与 .NET string.Format 类似，仅支持 {0}、{1}… */
function fmt(template, ...args) {
    return String(template).replace(/\{(\d+)\}/g, (_, i) => args[Number(i)] ?? '');
}

const app = {
    onLocaleApplied() {
        this.populateAlgoTypeSelect();
        if (Array.isArray(this.configs)) {
            this.renderTable();
            this.renderStats();
        }
    },
    configs: [],
    editingId: null,
    algorithms: [],     // 当前编辑中的算法列表 [{...}, ...]
    selectedAlgoIdx: -1, // 当前选中的算法索引
    /** 推理设备下拉选项，来自 GET /api/inference/devices（与 Core InferenceDeviceRegistry 一致） */
    inferenceDevices: [],
    /** 算法元数据：type -> { type, defaultClasses, regions }，来自 GET /api/algorithms/types */
    algorithmTypeMeta: {},

    /** 深拷贝 Desktop/导入 JSON 中的 notification.notifiers，Web 端只改 enabled 时不丢失渠道配置 */
    cloneNotificationNotifiers(arr) {
        if (!Array.isArray(arr) || arr.length === 0) return [];
        try {
            return JSON.parse(JSON.stringify(arr));
        } catch {
            return [];
        }
    },

    async fetchInferenceDevicesForAlgo(device) {
        const cur = device || 'CPU';
        try {
            const r = await fetch(`/api/inference/devices?current=${encodeURIComponent(cur)}`);
            if (r.ok) this.inferenceDevices = await r.json();
        } catch (_) {}
        if (!this.inferenceDevices.length)
            this.inferenceDevices = [{ device: 'CPU', displayName: 'CPU' }];
    },

    buildDeviceOptionsHtml(a) {
        const cur = String(a.device || 'CPU');
        const opts = this.inferenceDevices || [];
        if (!opts.length)
            return `<option value="${esc(cur)}" selected>${esc(cur)}</option>`;
        return opts.map(d => {
            const v = String(d.device != null ? d.device : d.Device);
            const label = d.displayName != null ? d.displayName : d.DisplayName;
            return `<option value="${esc(v)}" ${cur === v ? 'selected' : ''}>${esc(label != null ? String(label) : v)}</option>`;
        }).join('');
    },

    /** 纯算法参数区块（/api/algorithms/types 的 parameterSections） */
    getAlgorithmParameterSectionsForType(type) {
        const t = String(type || '').trim();
        if (!t) return [];
        const entry = this._findAlgorithmTypeMetaEntry(t);
        const u = entry && (entry.parameterSections ?? entry.ParameterSections);
        return Array.isArray(u) ? u : [];
    },

    /** AOI 参数区块（aoiParameterSections；兼容 aoiUsages） */
    getAoiParameterSectionsForType(type) {
        const t = String(type || '').trim();
        if (!t) return [];
        const entry = this._findAlgorithmTypeMetaEntry(t);
        const u = entry && (
            entry.aoiParameterSections ?? entry.AoiParameterSections
            ?? entry.aoiUsages ?? entry.AoiUsages);
        return Array.isArray(u) ? u : [];
    },

    _findAlgorithmTypeMetaEntry(type) {
        const t = String(type || '').trim();
        const meta = this.algorithmTypeMeta || {};
        let entry = meta[t];
        if (!entry) {
            const tl = t.toLowerCase();
            for (const k of Object.keys(meta)) {
                if (k.toLowerCase() === tl) {
                    entry = meta[k];
                    break;
                }
            }
        }
        return entry;
    },

    /** 与 Core DetectionAlgorithmFactory.GetDefaultClasses 一致（来自 /api/algorithms/types），大小写不敏感 */
    getDescriptorClassesForType(type) {
        const t = String(type || '').trim();
        if (!t) return [];
        const meta = this.algorithmTypeMeta || {};
        const entry = meta[t];
        if (entry && Array.isArray(entry.defaultClasses) && entry.defaultClasses.length)
            return [...entry.defaultClasses];
        const tl = t.toLowerCase();
        for (const k of Object.keys(meta)) {
            if (k.toLowerCase() !== tl) continue;
            const dc = meta[k].defaultClasses;
            if (Array.isArray(dc) && dc.length) return [...dc];
        }
        return [];
    },

    async init() {
        await this.loadAppVersion();
        await this.loadAlgorithmTypes();
        await this.refresh();
        // 只轮询运行中会话的状态（FPS/运行时间），不反复拉配置列表
        setInterval(() => this.pollSessions(), 5000);
        this.initImportDrag();
    },

    async loadAppVersion() {
        try {
            const r = await fetch('/api/version');
            if (!r.ok) return;
            const j = await r.json();
            const v = j.version ?? j.Version;
            if (v == null || v === '') return;
            const el = document.getElementById('appVersion');
            if (el) el.textContent = 'v' + String(v);
        } catch (_) {}
    },

    // ═══════════════════════════════
    //  数据
    // ═══════════════════════════════

    /** 静态兜底（API 失败时仍可添加基础算法） */
    getStaticAlgorithmOptions() {
        // 仅作 API 不可用时的兜底；须与 Core 中存在的算法 TypeKey（小写）一致，否则无默认类别无法通过校验
        return [
            { type: 'tearofftab', label: '撕标签检测' },
            { type: 'u7lite', label: 'U7Lite检测' }
        ];
    },

    /** 从服务端获取算法类型 + 默认类/区域元数据，并缓存到 algorithmTypeMeta */
    async loadAlgorithmTypes() {
        try {
            const r = await fetch('/api/algorithms/types');
            if (!r.ok) throw new Error('加载失败');
            const list = await r.json();
            const meta = {};
            for (const item of (Array.isArray(list) ? list : [])) {
                const type = String(item.type ?? item.Type ?? '').trim();
                if (!type) continue;
                meta[type] = {
                    type,
                    defaultClasses: item.defaultClasses ?? item.DefaultClasses ?? [],
                    regions: item.regions ?? item.Regions ?? [],
                    aoiParameterSections: item.aoiParameterSections ?? item.AoiParameterSections
                        ?? item.aoiUsages ?? item.AoiUsages ?? [],
                    parameterSections: item.parameterSections ?? item.ParameterSections ?? []
                };
            }
            this.algorithmTypeMeta = meta;
        } catch (_) {
            // 网络/后端异常时回退到空（由 populateAlgoTypeSelect 使用静态兜底）
            this.algorithmTypeMeta = {};
        }

        this.populateAlgoTypeSelect();
    },

    /** 将算法列表填充到下拉框（动态；无数据则使用静态兜底） */
    populateAlgoTypeSelect(extraTypes = []) {
        const sel = document.getElementById('algoTypeSelect');
        if (!sel) return;

        const staticOpts = this.getStaticAlgorithmOptions();
        const staticLabelMap = {};
        for (const o of staticOpts) staticLabelMap[o.type] = o.label;

        const fromApi = Object.keys(this.algorithmTypeMeta || {});
        const merged = new Set([...fromApi, ...staticOpts.map(o => o.type), ...(extraTypes || [])]);

        const types = Array.from(merged)
            .map(t => String(t || '').trim())
            .filter(Boolean)
            .sort((a, b) => a.localeCompare(b));

        sel.innerHTML = '';
        const ph = document.createElement('option');
        ph.value = '';
        ph.textContent = t('WebEditor_PlaceholderAlgoSelect');
        sel.appendChild(ph);

        for (const type of types) {
            const label = staticLabelMap[type] ? `${staticLabelMap[type]} (${type})` : type;
            const opt = document.createElement('option');
            opt.value = type;
            opt.textContent = label;
            sel.appendChild(opt);
        }
    },

    /** 完整刷新（拉配置列表），仅在用户操作后调用 */
    async refresh() {
        try {
            const resp = await fetch('/api/configs');
            if (!resp.ok) throw new Error('加载失败');
            this.configs = await resp.json();
            this.renderTable();
            this.renderStats();
        } catch (err) {
            this.toast('加载配置失败: ' + err.message, 'error');
        }
    },

    /** 轻量轮询：只更新运行中会话的 FPS 和状态 */
    async pollSessions() {
        try {
            const resp = await fetch('/api/sessions');
            if (!resp.ok) return;
            const sessions = await resp.json();

            const sessionMap = {};
            for (const s of sessions) sessionMap[s.configId] = s;

            let changed = false;
            for (const c of this.configs) {
                const s = sessionMap[c.id];
                const wasRunning = c.isRunning;
                c.isRunning = !!s?.isRunning;
                c.averagePipelineMs = s?.averagePipelineMs ?? 0;
                if (wasRunning !== c.isRunning) changed = true;
            }

            this.renderTable();
            this.renderStats();

            // 如果有会话启停变化，做一次完整刷新以同步新增/删除
            if (changed) await this.refresh();
        } catch { /* 网络波动忽略 */ }
    },

    // ═══════════════════════════════
    //  渲染
    // ═══════════════════════════════

    renderStats() {
        const total = this.configs.length;
        const running = this.configs.filter(c => c.isRunning).length;
        const algos = this.configs.reduce((s, c) => s + c.algorithmCount, 0);
        document.getElementById('statTotal').textContent = total;
        document.getElementById('statRunning').textContent = running;
        document.getElementById('statStopped').textContent = total - running;
        document.getElementById('statAlgos').textContent = algos;
    },

    renderTable() {
        const tbody = document.getElementById('configTable');
        if (this.configs.length === 0) {
            tbody.innerHTML = `<tr><td colspan="6"><div class="empty-state">
                <svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5">
                    <path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/>
                    <polyline points="14 2 14 8 20 8"/></svg>
                <p>${esc(t('Web_EmptyNoConfigs'))}</p>
                <p class="hint">${esc(t('Web_EmptyHintStart'))}</p>
            </div></td></tr>`;
            return;
        }
        const br = esc(t('Web_BadgeRunning'));
        const bs = esc(t('Web_BadgeStopped'));
        const aStop = esc(t('Web_ActionStop'));
        const aView = esc(t('Web_ActionView'));
        const aCopy = esc(t('Web_ActionCopyLink'));
        const aStart = esc(t('Web_ActionStart'));
        const aNg = esc(t('Web_ActionNgReplay'));
        const aEdit = esc(t('Web_ActionEdit'));
        const aDel = esc(t('Web_ActionDelete'));
        const ac = esc(t('Web_AlgoCountFmt'));
        tbody.innerHTML = this.configs.map(c => `<tr>
            <td><div class="cell-name">${esc(c.name)}</div><div class="cell-sub">${esc(c.id).substring(0,8)}...</div></td>
            <td><div class="cell-sub">${esc(c.videoSourceType)}</div><div style="font-size:12px;">${trunc(c.videoSource, 38)}</div></td>
            <td>${fmt(ac, c.algorithmCount)}<div class="cell-sub">${(c.algorithms||[]).join(', ')}</div></td>
            <td>${c.isRunning
                ? `<span class="badge badge-running"><span class="pulse"></span>${br}</span>`
                : `<span class="badge badge-stopped">${bs}</span>`}</td>
            <td>${c.isRunning ? (c.averagePipelineMs ?? 0).toFixed(1) + ' ms' : '-'}</td>
            <td><div class="actions-cell">
                ${c.isRunning
                    ? `<button class="btn btn-danger btn-xs" onclick="app.stopDetection('${c.id}')">${aStop}</button>
                       <a class="btn btn-outline btn-xs" href="/view.html?config=${encodeURIComponent(c.name)}" target="_blank">${aView}</a>
                       <button class="btn btn-outline btn-xs" onclick="app.copyViewUrl('${esc(c.name)}')">${aCopy}</button>`
                    : `<button class="btn btn-success btn-xs" onclick="app.startDetection('${c.id}')">${aStart}</button>`}
                <button class="btn btn-outline btn-xs" onclick="app.openNgReplay('${esc(c.name)}')">${aNg}</button>
                <button class="btn btn-outline btn-xs" onclick="app.openEditModal('${c.id}')" ${c.isRunning?'disabled':''}>${aEdit}</button>
                <button class="btn btn-outline btn-xs" onclick="app.deleteConfig('${c.id}')" ${c.isRunning?'disabled':''} style="color:var(--danger)">${aDel}</button>
            </div></td>
        </tr>`).join('');
    },

    // ═══════════════════════════════
    //  检测控制
    // ═══════════════════════════════

    async startDetection(id) {
        try {
            const r = await fetch(`/api/sessions/${id}/start`, { method: 'POST' });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error);
            this.toast('检测已启动', 'success');
            await this.refresh();
        } catch (e) { this.toast(e.message, 'error'); }
    },

    async stopDetection(id) {
        try {
            await fetch(`/api/sessions/${id}/stop`, { method: 'POST' });
            this.toast('检测已停止', 'success');
            await this.refresh();
        } catch (e) { this.toast(e.message, 'error'); }
    },

    async startAll() {
        const r = await fetch('/api/sessions/start-all', { method: 'POST' });
        const d = await r.json();
        if (d.started.length) this.toast(`已启动 ${d.started.length} 个`, 'success');
        if (d.errors.length) this.toast(`${d.errors.length} 个启动失败`, 'error');
        await this.refresh();
    },

    async stopAll() {
        const r = await fetch('/api/sessions/stop-all', { method: 'POST' });
        const d = await r.json();
        this.toast(d.message, 'success');
        await this.refresh();
    },

    async deleteConfig(id) {
        if (!confirm(t('Web_ConfirmDeleteConfig'))) return;
        try {
            const r = await fetch(`/api/configs/${id}`, { method: 'DELETE' });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error);
            this.toast('配置已删除', 'success');
            await this.refresh();
        } catch (e) { this.toast(e.message, 'error'); }
    },

    // ═══════════════════════════════
    //  配置编辑器 - 打开 / 关闭
    // ═══════════════════════════════

    openCreateModal() {
        this.editingId = null;
        this.algorithms = [];
        this.selectedAlgoIdx = -1;
        this.populateAlgoTypeSelect();
        document.getElementById('editorTitle').textContent = t('WebEditor_TitleNew');
        document.getElementById('cfgName').value = '';
        document.getElementById('cfgVideoType').value = 'Rtsp';
        document.getElementById('cfgVideoSource').value = '';
        document.getElementById('cfgVideoLoop').checked = false;
        document.getElementById('editorStatus').textContent = t('Editor_StatusNewHint');
        this.renderAlgoList();
        this.renderAlgoDetail();
        document.getElementById('editorModal').classList.add('active');
    },

    async openEditModal(id) {
        try {
            const r = await fetch(`/api/configs/${id}`);
            if (!r.ok) throw new Error('加载失败');
            const cfg = await r.json();

            this.editingId = id;
            document.getElementById('editorTitle').textContent = fmt(t('WebEditor_TitleEditFmt'), cfg.name);
            document.getElementById('cfgName').value = cfg.name;
            document.getElementById('cfgVideoType').value = cfg.videoSource.type.toString();
            document.getElementById('cfgVideoSource').value = cfg.videoSource.source;
            document.getElementById('cfgVideoLoop').checked = cfg.videoSource.loop;

            this.algorithms = (cfg.algorithms || []).map((a, i) => ({
                algorithmType: a.algorithmType || '',
                displayName: a.displayName || `${a.algorithmType}_${i+1}`,
                enabled: a.enabled !== false,
                modelPath: a.inference?.modelPath || '',
                device: a.inference?.device || 'CPU',
                modelType: a.inference?.modelType || 'Auto',
                confidenceThreshold: a.inference?.confidenceThreshold ?? 0.5,
                iouThreshold: a.inference?.iouThreshold ?? 0.45,
                inputSize: a.inference?.inputSize?.width ?? 640,
                classes: this.getDescriptorClassesForType(a.algorithmType || '').join('\n'),
                regionsJson: JSON.stringify(a.regions || [], null, 2),
                saveErrorImage: a.storage?.saveErrorImage !== false,
                saveVideo: a.storage?.saveVideo || false,
                videoDuration: a.storage?.videoDuration ?? 10,
                ngVideoUseSourceResolution: !!a.storage?.ngVideoUseSourceResolution,
                retentionDays: a.storage?.retentionDays ?? 7,
                notificationEnabled: a.notification?.enabled || false,
                notificationNotifiers: this.cloneNotificationNotifiers(a.notification?.notifiers),
                args: (() => {
                    try {
                        const src = a.args ?? a.Args;
                        if (src && typeof src === 'object' && !Array.isArray(src))
                            return JSON.parse(JSON.stringify(src));
                    } catch (_) {}
                    return {};
                })()
            }));

            // 兼容：已保存配置里可能包含当前下拉框没有的 algorithmType（例如算法被移除/改名）
            const extraTypes = (this.algorithms || [])
                .map(a => a.algorithmType)
                .filter(Boolean);
            this.populateAlgoTypeSelect(extraTypes);

            this.selectedAlgoIdx = this.algorithms.length > 0 ? 0 : -1;
            this.renderAlgoList();
            await this.renderAlgoDetail();
            document.getElementById('editorModal').classList.add('active');
        } catch (e) { this.toast(e.message, 'error'); }
    },

    closeEditor() {
        document.getElementById('editorModal').classList.remove('active');
    },

    // ═══════════════════════════════
    //  算法池 - 添加/删除/选中
    // ═══════════════════════════════

    async addAlgorithmFromSelect() {
        const sel = document.getElementById('algoTypeSelect');
        const type = sel.value;
        if (!type) return;

        const defaultClasses = this.getDescriptorClassesForType(type);

        const algo = {
            algorithmType: type,
            displayName: `${type}_${this.algorithms.length + 1}`,
            enabled: true,
            modelPath: '',
            device: 'CPU',
            modelType: 'Auto',
            confidenceThreshold: 0.5,
            iouThreshold: 0.45,
            inputSize: 640,
            classes: defaultClasses.length ? defaultClasses.join('\n') : '',
            regionsJson: '[]',
            saveErrorImage: true,
            saveVideo: false,
            videoDuration: 10,
            ngVideoUseSourceResolution: false,
            retentionDays: 7,
            notificationEnabled: false,
            notificationNotifiers: [],
            args: {}
        };

        this.algorithms.push(algo);
        this.selectedAlgoIdx = this.algorithms.length - 1;
        sel.value = '';
        this.renderAlgoList();
        await this.renderAlgoDetail();
        document.getElementById('editorStatus').textContent = fmt(t('Editor_StatusAlgoAdded'), algo.displayName, '');
    },

    async selectAlgorithm(idx) {
        this.syncCurrentAlgo();
        this.selectedAlgoIdx = idx;
        this.renderAlgoList();
        await this.renderAlgoDetail();
    },

    async removeAlgorithm(idx) {
        this.algorithms.splice(idx, 1);
        if (this.selectedAlgoIdx >= this.algorithms.length)
            this.selectedAlgoIdx = this.algorithms.length - 1;
        this.renderAlgoList();
        await this.renderAlgoDetail();
        document.getElementById('editorStatus').textContent = t('Web_EditorPoolAlgoRemoved');
    },

    // ═══════════════════════════════
    //  算法列表渲染 (左面板)
    // ═══════════════════════════════

    renderAlgoList() {
        const panel = document.getElementById('algoListPanel');
        if (this.algorithms.length === 0) {
            panel.innerHTML = `<div style="text-align:center;padding:16px;color:var(--text-muted);font-size:12px;">${esc(t('Web_NoAlgoInPool'))}</div>`;
            return;
        }
        panel.innerHTML = this.algorithms.map((a, i) =>
            `<div class="algo-item ${i === this.selectedAlgoIdx ? 'active' : ''}" onclick="app.selectAlgorithm(${i})">
                <div class="algo-indicator"></div>
                <div class="algo-info">
                    <div class="algo-name">${esc(a.displayName)}</div>
                    <div class="algo-type">${esc(a.algorithmType)}${a.enabled ? '' : esc(t('Web_AlgoDisabledSuffix'))}</div>
                </div>
                <button class="algo-remove" onclick="event.stopPropagation();app.removeAlgorithm(${i})" title="${esc(t('Web_RemoveAlgoTitle'))}">&times;</button>
            </div>`
        ).join('');
    },

    // ═══════════════════════════════
    //  算法详细配置渲染 (右面板)
    //  对齐 Desktop: 推理 / 区域 / AOI / 存储 / 通知
    // ═══════════════════════════════

    getArgFieldDisplayValue(args, f) {
        const name = f.name ?? f.Name;
        const pt = String(f.parameterType ?? f.ParameterType ?? 'string').toLowerCase();
        const defVal = f.defaultValue ?? f.DefaultValue;
        const bag = args || {};
        if (!Object.prototype.hasOwnProperty.call(bag, name) || bag[name] === undefined || bag[name] === null) {
            if (pt === 'bool') return defVal === true || defVal === 'true';
            if (pt === 'string') return defVal != null && defVal !== '' ? String(defVal) : '';
            if (pt === 'int' || pt === 'double')
                return defVal !== undefined && defVal !== null && defVal !== '' ? defVal : '';
        }
        const v = bag[name];
        if (pt === 'bool') return !!v;
        return v;
    },

    collectArgsFromAlgorithmParameterForm(a) {
        const usages = [
            ...this.getAoiParameterSectionsForType(a.algorithmType),
            ...this.getAlgorithmParameterSectionsForType(a.algorithmType)
        ];
        const out = { ...(a.args && typeof a.args === 'object' && !Array.isArray(a.args) ? a.args : {}) };
        for (const u of usages) {
            for (const f of (u.argFields || u.ArgFields || [])) {
                const name = f.name ?? f.Name;
                const id = `ad_arg_${name}`;
                const el = document.getElementById(id);
                if (!el) continue;
                const pt = String(f.parameterType ?? f.ParameterType ?? 'string').toLowerCase();
                const req = f.required ?? f.Required;
                if (pt === 'bool') {
                    out[name] = !!el.checked;
                    continue;
                }
                const raw = String(el.value ?? '').trim();
                if (!raw && !req) {
                    delete out[name];
                    continue;
                }
                if (pt === 'int') {
                    out[name] = parseInt(raw, 10);
                    continue;
                }
                if (pt === 'double') {
                    out[name] = parseFloat(raw);
                    continue;
                }
                if (pt === 'string') {
                    if (!raw && !req) {
                        delete out[name];
                        continue;
                    }
                    if (/^-?\d+$/.test(raw)) {
                        out[name] = parseInt(raw, 10);
                        continue;
                    }
                    if (/^-?\d+\.\d+$|^-?\d*\.\d+$/.test(raw)) {
                        out[name] = parseFloat(raw);
                        continue;
                    }
                    out[name] = raw;
                }
            }
        }
        return out;
    },

    _buildArgsFormCardInnerHtml(a, getUsagesFn) {
        const usages = getUsagesFn.call(this, a.algorithmType);
        if (!usages.length) return '';
        const args = a.args || {};
        let blocks = '';
        for (const u of usages) {
            const title = (u.sectionTitle || u.SectionTitle || '').trim()
                || String(u.aoiDetectorTypeKey || u.AoiDetectorTypeKey || '');
            const desc = u.description || u.Description || '';
            const fields = u.argFields || u.ArgFields || [];
            blocks += `<div style="margin-bottom:16px;">`;
            if (title)
                blocks += `<div style="font-weight:600;margin-bottom:4px;">${esc(title)}</div>`;
            if (desc)
                blocks += `<p style="font-size:11px;color:var(--text-muted);margin:0 0 10px;line-height:1.45;">${esc(desc)}</p>`;
            for (const f of fields) {
                const name = f.name ?? f.Name;
                const label = f.displayName ?? f.DisplayName ?? name;
                const hint = f.description ?? f.Description ?? '';
                const pt = String(f.parameterType ?? f.ParameterType ?? 'string').toLowerCase();
                const id = `ad_arg_${name}`;
                const val = this.getArgFieldDisplayValue(args, f);
                blocks += `<div class="form-group" style="margin-bottom:10px;">`;
                if (pt !== 'bool')
                    blocks += `<label>${esc(label)}</label>`;
                if (hint)
                    blocks += `<p style="font-size:11px;color:var(--text-muted);margin:2px 0 6px;line-height:1.4;">${esc(hint)}</p>`;
                if (pt === 'bool') {
                    blocks += `<div class="form-check"><input type="checkbox" id="${id}" ${val ? 'checked' : ''}>`;
                    blocks += `<label for="${id}" style="margin-left:8px;color:var(--text);">${esc(label)}</label></div>`;
                } else if (pt === 'int') {
                    blocks += `<input type="number" id="${id}" step="1" value="${esc(String(val ?? ''))}">`;
                } else if (pt === 'double') {
                    blocks += `<input type="number" id="${id}" step="0.0001" value="${esc(String(val ?? ''))}">`;
                } else {
                    const ex = f.example ?? f.Example ?? '';
                    blocks += `<input type="text" id="${id}" value="${esc(String(val ?? ''))}" placeholder="${esc(ex)}">`;
                }
                blocks += `</div>`;
            }
            blocks += `</div>`;
        }
        return blocks;
    },

    buildAoiParameterCardHtml(a) {
        const L = t;
        const blocks = this._buildArgsFormCardInnerHtml(a, this.getAoiParameterSectionsForType);
        if (!blocks) return '';
        return `
            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionAoi'))}</div>
                <p style="font-size:11px;color:var(--text-muted);margin:0 0 12px;line-height:1.45;">${esc(L('Editor_HintAoiArgs'))}</p>
                ${blocks}
            </div>`;
    },

    buildAlgorithmParameterCardHtml(a) {
        const L = t;
        const blocks = this._buildArgsFormCardInnerHtml(a, this.getAlgorithmParameterSectionsForType);
        if (!blocks) return '';
        return `
            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionAlgorithmParameters'))}</div>
                <p style="font-size:11px;color:var(--text-muted);margin:0 0 12px;line-height:1.45;">${esc(L('Editor_HintAlgorithmParameters'))}</p>
                ${blocks}
            </div>`;
    },

    async renderAlgoDetail() {
        const hint = document.getElementById('noAlgoHint');
        const detail = document.getElementById('algoDetail');

        if (this.selectedAlgoIdx < 0 || this.selectedAlgoIdx >= this.algorithms.length) {
            hint.style.display = 'flex';
            detail.style.display = 'none';
            return;
        }

        hint.style.display = 'none';
        detail.style.display = 'block';

        const a = this.algorithms[this.selectedAlgoIdx];
        const classLines = this.getDescriptorClassesForType(a.algorithmType);
        const classText = classLines.join('\n');
        await this.fetchInferenceDevicesForAlgo(a.device);
        const deviceOptionsHtml = this.buildDeviceOptionsHtml(a);
        const aoiCardHtml = this.buildAoiParameterCardHtml(a);
        const algoCardHtml = this.buildAlgorithmParameterCardHtml(a);
        const L = t;
        detail.innerHTML = `
            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionInference'))}</div>
                <div class="form-group">
                    <label>${esc(L('Web_AdDisplayName'))}</label>
                    <input type="text" id="ad_display" value="${esc(a.displayName)}" placeholder="${esc(a.algorithmType)}_1">
                </div>
                <div class="form-group">
                    <label>${esc(L('Editor_LblModelPath'))}</label>
                    <input type="text" id="ad_model" value="${esc(a.modelPath)}" placeholder="models/yolov8n.onnx">
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>${esc(L('Editor_LblInferenceDevice'))}</label>
                        <select id="ad_device">
                            ${deviceOptionsHtml}
                        </select>
                    </div>
                    <div class="form-group">
                        <label>${esc(L('Editor_LblModelType'))}</label>
                        <select id="ad_modelType">
                            <option value="Auto" ${a.modelType==='Auto'?'selected':''}>Auto</option>
                            <option value="Detection" ${a.modelType==='Detection'?'selected':''}>Detect</option>
                            <option value="Segmentation" ${a.modelType==='Segmentation'?'selected':''}>Segment</option>
                            <option value="Classification" ${a.modelType==='Classification'?'selected':''}>Classify</option>
                            <option value="Pose" ${a.modelType==='Pose'?'selected':''}>Pose</option>
                            <option value="Obb" ${a.modelType==='Obb'?'selected':''}>Rotated</option>
                            <option value="Track" disabled ${a.modelType==='Track'?'selected':''}>Track (Reserved)</option>
                            <option value="SegmentationTracking" ${a.modelType==='SegmentationTracking'?'selected':''}>Track (Segment)</option>
                            <option value="DetectionTracking" ${a.modelType==='DetectionTracking'?'selected':''}>Track (Detect)</option>
                        </select>
                    </div>
                </div>
                <div class="form-row-3">
                    <div class="form-group">
                        <label>${esc(L('Editor_LblConfidence'))}</label>
                        <input type="number" id="ad_conf" step="0.05" min="0" max="1" value="${a.confidenceThreshold}">
                    </div>
                    <div class="form-group">
                        <label>${esc(L('Editor_LblIou'))}</label>
                        <input type="number" id="ad_iou" step="0.05" min="0" max="1" value="${a.iouThreshold}">
                    </div>
                    <div class="form-group">
                        <label>${esc(L('Editor_LblInputSize'))}</label>
                        <input type="number" id="ad_inputSize" min="320" max="1280" step="32" value="${a.inputSize}">
                    </div>
                </div>
                <div class="form-group">
                    <label>${esc(L('Editor_LblClassesReadonly'))} <span style="font-size:11px;color:var(--text-muted);">${esc(L('Web_AdClassesReadonlyTag'))}</span></label>
                    <textarea id="ad_classes" rows="4" readonly style="opacity:0.92;cursor:not-allowed;" aria-readonly="true">${esc(classText)}</textarea>
                    <p style="font-size:11px;color:var(--text-muted);margin:6px 0 0;line-height:1.45;">${esc(L('Web_AdClassesFooter'))}</p>
                </div>
                <div class="form-check">
                    <input type="checkbox" id="ad_enabled" ${a.enabled?'checked':''}>
                    <label for="ad_enabled" style="margin:0;color:var(--text);">${esc(L('Web_AdEnableAlgo'))}</label>
                </div>
            </div>

            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionRegions'))}</div>
                <button class="btn btn-primary btn-sm" style="margin-bottom:10px;width:100%;" onclick="app.openRegionEditor()">${esc(L('Editor_BtnVisualRegions'))}</button>
                <div class="form-group">
                    <label>${esc(L('Editor_LblRegionsJson'))} <span style="font-size:11px;color:var(--text-muted);">${esc(L('Web_AdRegionsManualHint'))}</span></label>
                    <textarea id="ad_regions" rows="5" placeholder='[{"name":"region1","points":[{"x":0,"y":0},...]}]'>${esc(a.regionsJson)}</textarea>
                </div>
            </div>

            ${aoiCardHtml}
            ${algoCardHtml}

            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionStorage'))}</div>
                <div class="form-check">
                    <input type="checkbox" id="ad_saveErr" ${a.saveErrorImage?'checked':''}>
                    <label for="ad_saveErr" style="margin:0;color:var(--text);">${esc(L('Editor_ChkSaveErrorImage'))}</label>
                </div>
                <div class="form-check">
                    <input type="checkbox" id="ad_saveVideo" ${a.saveVideo?'checked':''}>
                    <label for="ad_saveVideo" style="margin:0;color:var(--text);">${esc(L('Editor_ChkSaveVideo'))}</label>
                </div>
                <div class="form-check">
                    <input type="checkbox" id="ad_ngSrcRes" ${a.ngVideoUseSourceResolution?'checked':''} ${a.saveVideo?'':'disabled'}>
                    <label for="ad_ngSrcRes" style="margin:0;color:var(--text);">${esc(L('Editor_ChkNgVideoSourceRes'))}</label>
                </div>
                <div class="form-row-3">
                    <div class="form-group">
                        <label>${esc(L('Editor_LblVideoDuration'))}</label>
                        <input type="number" id="ad_vidDur" min="1" max="60" value="${a.videoDuration}">
                    </div>
                    <div class="form-group">
                        <label>${esc(L('Web_AdRetentionDays'))}</label>
                        <input type="number" id="ad_retention" min="0" value="${a.retentionDays}">
                    </div>
                </div>
                <p style="font-size:11px;color:var(--text-muted);margin:8px 0 0;line-height:1.5;">${esc(L('Web_AdStorageFpsNote'))}</p>
            </div>

            <div class="config-card">
                <div class="config-card-title">${esc(L('Editor_SectionNotification'))}</div>
                <div class="form-check">
                    <input type="checkbox" id="ad_notify" ${a.notificationEnabled?'checked':''}>
                    <label for="ad_notify" style="margin:0;color:var(--text);">${esc(L('Web_AdNotifyEnable'))}</label>
                </div>
                <p style="font-size:11px;color:var(--text-muted);margin:8px 0 0;line-height:1.5;">
                    ${esc(L('Web_AdNotifyFooter'))}
                </p>
            </div>
        `;
    },

    /** 将右面板表单数据同步回当前选中的算法对象 */
    syncCurrentAlgo() {
        if (this.selectedAlgoIdx < 0 || this.selectedAlgoIdx >= this.algorithms.length) return;
        const el = id => document.getElementById(id);
        if (!el('ad_display')) return;

        const a = this.algorithms[this.selectedAlgoIdx];
        a.displayName = el('ad_display').value;
        a.modelPath = el('ad_model').value;
        a.device = el('ad_device').value;
        a.modelType = el('ad_modelType').value;
        a.confidenceThreshold = parseFloat(el('ad_conf').value) || 0.5;
        a.iouThreshold = parseFloat(el('ad_iou').value) || 0.45;
        a.inputSize = parseInt(el('ad_inputSize').value) || 640;
        a.classes = this.getDescriptorClassesForType(a.algorithmType).join('\n');
        a.enabled = el('ad_enabled').checked;
        a.regionsJson = el('ad_regions').value;
        a.saveErrorImage = el('ad_saveErr').checked;
        a.saveVideo = el('ad_saveVideo').checked;
        if (el('ad_ngSrcRes') && !el('ad_ngSrcRes').disabled)
            a.ngVideoUseSourceResolution = el('ad_ngSrcRes').checked;
        a.videoDuration = parseInt(el('ad_vidDur').value) || 10;
        a.retentionDays = parseInt(el('ad_retention').value) || 7;
        a.notificationEnabled = el('ad_notify').checked;
        a.args = this.collectArgsFromAlgorithmParameterForm(a);
    },

    // ═══════════════════════════════
    //  保存配置
    // ═══════════════════════════════

    async saveConfig() {
        this.syncCurrentAlgo();

        const name = document.getElementById('cfgName').value.trim();
        if (!name) { this.toast(t('Editor_StatusEnterName'), 'error'); return; }
        const source = document.getElementById('cfgVideoSource').value.trim();
        if (!source) { this.toast(t('Editor_StatusSetVideoPath'), 'error'); return; }
        if (this.algorithms.length === 0) { this.toast(t('Editor_StatusAddOneAlgo'), 'error'); return; }

        const config = {
            name,
            enabled: true,
            videoSource: {
                type: document.getElementById('cfgVideoType').value,
                source,
                loop: document.getElementById('cfgVideoLoop').checked,
                reconnectInterval: 5,
                timeout: 10
            },
            algorithms: this.algorithms.map(a => {
                let regions = [];
                try { regions = JSON.parse(a.regionsJson); } catch {}

                return {
                    algorithmType: a.algorithmType,
                    displayName: a.displayName,
                    enabled: a.enabled,
                    inference: {
                        modelPath: a.modelPath,
                        device: a.device,
                        modelType: a.modelType,
                        confidenceThreshold: a.confidenceThreshold,
                        iouThreshold: a.iouThreshold,
                        inputSize: { width: a.inputSize, height: a.inputSize },
                        classes: this.getDescriptorClassesForType(a.algorithmType)
                    },
                    regions,
                    storage: {
                        saveErrorImage: a.saveErrorImage,
                        saveRetrainImage: true,
                        saveVideo: a.saveVideo,
                        videoDuration: a.videoDuration,
                        ngVideoUseSourceResolution: !!a.ngVideoUseSourceResolution,
                        videoCodec: 'mp4v',
                        errorImagePath: 'catch',
                        retrainImagePath: 'retrain',
                        retentionDays: a.retentionDays
                    },
                    notification: {
                        enabled: a.notificationEnabled,
                        notifiers: this.cloneNotificationNotifiers(a.notificationNotifiers)
                    },
                    ...(a.args && typeof a.args === 'object' && Object.keys(a.args).length > 0
                        ? { args: a.args }
                        : {})
                };
            })
        };

        try {
            let r;
            if (this.editingId) {
                r = await fetch(`/api/configs/${this.editingId}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(config)
                });
            } else {
                r = await fetch('/api/configs', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(config)
                });
            }
            const d = await r.json();
            if (!r.ok) throw new Error(d.error || '保存失败');
            this.toast(this.editingId ? '配置已更新' : '配置已创建', 'success');
            this.closeEditor();
            await this.refresh();
        } catch (e) { this.toast(e.message, 'error'); }
    },

    // ═══════════════════════════════
    //  导入 / 导出
    // ═══════════════════════════════

    openImportModal() {
        document.getElementById('importJson').value = '';
        document.getElementById('importFile').value = '';
        document.getElementById('importModal').classList.add('active');
    },

    closeImport() {
        document.getElementById('importModal').classList.remove('active');
    },

    initImportDrag() {
        const zone = document.getElementById('importDropZone');
        if (!zone) return;
        zone.addEventListener('dragover', e => { e.preventDefault(); zone.classList.add('dragover'); });
        zone.addEventListener('dragleave', () => zone.classList.remove('dragover'));
        zone.addEventListener('drop', e => {
            e.preventDefault();
            zone.classList.remove('dragover');
            if (e.dataTransfer.files.length > 0) this.handleImportFiles(e.dataTransfer.files);
        });
        document.getElementById('importFile').addEventListener('change', e => {
            if (e.target.files.length > 0) this.handleImportFiles(e.target.files);
        });
    },

    async handleImportFiles(files) {
        const textarea = document.getElementById('importJson');
        const contents = [];
        for (const f of files) {
            contents.push(await f.text());
        }
        textarea.value = contents.length === 1 ? contents[0] : `[${contents.join(',')}]`;
        this.toast(`已读取 ${files.length} 个文件`, 'info');
    },

    async doImport() {
        const json = document.getElementById('importJson').value.trim();
        if (!json) { this.toast('请选择文件或粘贴 JSON', 'error'); return; }

        try {
            const r = await fetch('/api/configs/import', {
                method: 'POST',
                headers: { 'Content-Type': 'text/plain' },
                body: json
            });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error);
            if (d.imported.length > 0) this.toast(`成功导入 ${d.imported.length} 个配置`, 'success');
            if (d.errors.length > 0) this.toast(`${d.errors.length} 个配置导入失败`, 'error');
            this.closeImport();
            await this.refresh();
        } catch (e) { this.toast(e.message, 'error'); }
    },

    async exportAll() {
        try {
            const r = await fetch('/api/configs/export');
            const json = await r.text();
            const blob = new Blob([json], { type: 'application/json' });
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = `luxvideodet-configs-${new Date().toISOString().slice(0,10)}.json`;
            a.click();
            URL.revokeObjectURL(url);
            this.toast('配置已导出', 'success');
        } catch (e) { this.toast(e.message, 'error'); }
    },

    // ═══════════════════════════════
    //  复制查看链接
    // ═══════════════════════════════

    // ═══════════════════════════════
    //  区域编辑器
    // ═══════════════════════════════

    openRegionEditor() {
        this.syncCurrentAlgo();
        if (this.selectedAlgoIdx < 0) return;

        const a = this.algorithms[this.selectedAlgoIdx];
        const videoSource = {
            type: document.getElementById('cfgVideoType').value,
            source: document.getElementById('cfgVideoSource').value,
            loop: document.getElementById('cfgVideoLoop').checked,
            reconnectInterval: 5,
            timeout: 10
        };

        if (!videoSource.source) {
            this.toast(t('Editor_StatusSetVideoPath'), 'error');
            return;
        }

        let existingRegions = [];
        try { existingRegions = JSON.parse(a.regionsJson); } catch {}

        regionEditor.open({
            algorithmType: a.algorithmType,
            videoSource,
            configId: this.editingId,
            existingRegions,
            onSave: (json) => {
                a.regionsJson = json;
                const el = document.getElementById('ad_regions');
                if (el) el.value = json;
                this.toast('区域已更新', 'success');
            }
        });
    },

    copyViewUrl(configName) {
        const origin = window.location.origin;
        const url = `${origin}/?config=${encodeURIComponent(configName)}`;
        navigator.clipboard.writeText(url).then(() => {
            this.toast(`已复制: ${url}`, 'success');
        }).catch(() => {
            prompt('复制此链接分发给工位一体机:', url);
        });
    },

    // ═══════════════════════════════
    //  NG 回放
    // ═══════════════════════════════

    ngReplayConfig: null,
    ngReplayDates: [],
    ngReplayFiles: [],
    ngSelectedDate: null,

    async openNgReplay(configName) {
        this.ngReplayConfig = configName;
        this.ngReplayDates = [];
        this.ngReplayFiles = [];
        this.ngSelectedDate = null;

        document.getElementById('ngReplayTitle').textContent = fmt(t('Ng_WindowTitle'), configName);
        document.getElementById('ngReplayModal').classList.add('active');
        this.renderNgDateList();
        this.renderNgFileList();
        this.clearNgPlayer();

        try {
            const r = await fetch('/api/ng-replay/' + encodeURIComponent(configName) + '/dates');
            if (r.ok) {
                this.ngReplayDates = await r.json();
                this.renderNgDateList();
                if (this.ngReplayDates.length > 0)
                    this.selectNgDate(this.ngReplayDates[0].date);
            }
        } catch (e) {
            this.toast('加载 NG 回放日期失败: ' + e.message, 'error');
        }
    },

    closeNgReplay() {
        document.getElementById('ngReplayModal').classList.remove('active');
        this.clearNgPlayer();
    },

    async selectNgDate(date) {
        this.ngSelectedDate = date;
        this.renderNgDateList();
        this.clearNgPlayer();

        try {
            const r = await fetch('/api/ng-replay/' + encodeURIComponent(this.ngReplayConfig) + '/dates/' + date + '/files');
            if (r.ok) {
                this.ngReplayFiles = await r.json();
                this.renderNgFileList();
            }
        } catch (e) {
            this.toast('加载文件列表失败', 'error');
        }
    },

    renderNgDateList() {
        const el = document.getElementById('ngDateList');
        if (this.ngReplayDates.length === 0) {
            el.innerHTML = `<div style="padding:16px;text-align:center;color:var(--text-muted);font-size:12px;">${esc(t('Web_NgEmptyDates'))}</div>`;
            return;
        }
        el.innerHTML = this.ngReplayDates.map(d =>
            `<div class="ng-date-item ${d.date === this.ngSelectedDate ? 'active' : ''}" onclick="app.selectNgDate('${d.date}')">
                <span>${d.date}</span>
                <span class="ng-date-count">${esc(fmt(t('Web_NgFileCountFmt'), d.fileCount))}</span>
            </div>`
        ).join('');
    },

    renderNgFileList() {
        const el = document.getElementById('ngFileList');
        if (this.ngReplayFiles.length === 0) {
            el.innerHTML = `<div style="padding:16px;text-align:center;color:var(--text-muted);font-size:12px;">${esc(t('Web_NgSelectDateHint'))}</div>`;
            return;
        }
        el.innerHTML = this.ngReplayFiles.map(f =>
            `<div class="ng-file-item" onclick="app.playNgFile('${esc(f.url)}', '${f.type}', '${esc(f.name)}')">
                <span class="ng-file-icon">${f.type === 'video' ? '🎬' : '🖼️'}</span>
                <span class="ng-file-name">${esc(f.name)}</span>
                <span class="ng-file-size">${this.formatSize(f.size)}</span>
            </div>`
        ).join('');
    },

    playNgFile(url, type, name) {
        const container = document.getElementById('ngPlayerContainer');
        document.getElementById('ngPlayerTitle').textContent = name;

        if (type === 'video') {
            container.innerHTML = `<video id="ngVideo" controls autoplay style="max-width:100%;max-height:100%;border-radius:6px;">
                <source src="${url}" type="video/mp4">
            </video>
            <div class="ng-speed-controls">
                <span style="font-size:12px;color:var(--text-dim);">${esc(t('Web_NgPlaybackRate'))}</span>
                <button class="btn btn-outline btn-xs" onclick="app.setNgSpeed(0.2)">0.2x</button>
                <button class="btn btn-outline btn-xs" onclick="app.setNgSpeed(0.5)">0.5x</button>
                <button class="btn btn-outline btn-xs" onclick="app.setNgSpeed(0.7)">0.7x</button>
                <button class="btn btn-outline btn-xs ng-speed-active" onclick="app.setNgSpeed(1)">1x</button>
            </div>`;
        } else {
            container.innerHTML = `<img src="${url}" style="max-width:100%;max-height:100%;border-radius:6px;" alt="${esc(name)}">`;
        }
    },

    setNgSpeed(speed) {
        const video = document.getElementById('ngVideo');
        if (video) video.playbackRate = speed;
        document.querySelectorAll('.ng-speed-controls .btn').forEach(b => b.classList.remove('ng-speed-active'));
        event.target.classList.add('ng-speed-active');
    },

    clearNgPlayer() {
        const container = document.getElementById('ngPlayerContainer');
        if (container) container.innerHTML = `<div class="ng-player-placeholder">
            <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5">
                <polygon points="5 3 19 12 5 21 5 3"/>
            </svg>
            <p>${esc(t('Ng_PlaceholderSelect'))}</p>
        </div>`;
        const title = document.getElementById('ngPlayerTitle');
        if (title) title.textContent = '';
    },

    formatSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / 1048576).toFixed(1) + ' MB';
    },

    // ═══════════════════════════════════════════════
    //  MinIO 文件同步
    // ═══════════════════════════════════════════════
    _minioSyncing: false,
    _minioConnected: false,
    _minioFileFilter: 'all',
    _minioFileCache: { plugin: [], model: [], video: [], zip: [] },
    _minioLogs: [],
    _minioOnlyImportantLogs: false,

    openMinioModal() {
        document.getElementById('minioModal').classList.add('active');
        this._setMinioConnectionBadge('failed', '未连接');
        this._minioRenderLogs();
        this._minioAddLog('info', '系统', '打开算法插件与资源更新窗口');
        this.minioLoadConfig().then(() => this.minioCheckConnection()).catch(() => this._setMinioConnectionBadge('failed', '未连接'));
    },
    closeMinioModal() { document.getElementById('minioModal').classList.remove('active'); },
    switchMinioTab(tab) {
        const tabs = document.querySelectorAll('.minio-tab');
        tabs.forEach(t => t.classList.remove('active'));
        const tabIds = ['sync', 'config', 'stats', 'logs'];
        const activeIndex = tabIds.indexOf(tab);
        if (activeIndex >= 0 && tabs[activeIndex]) tabs[activeIndex].classList.add('active');
        tabIds.forEach(t => {
            const panel = document.getElementById(`minioTab${t.charAt(0).toUpperCase()}${t.slice(1)}`);
            if (panel) panel.style.display = t === tab ? '' : 'none';
        });
        if (tab === 'stats') this.minioLoadStats();
        else if (tab === 'logs') this._minioRenderLogs();
    },
    async minioLoadConfig() {
        try {
            await this._minioLoadProviderOptions();
            const r = await fetch('/api/minio/config');
            if (!r.ok) throw new Error('加载失败');
            const cfg = await r.json();
            document.getElementById('minioProvider').value = cfg.provider || 'minio-sdk';
            document.getElementById('minioEndpoint').value = cfg.endpoint || '';
            document.getElementById('minioBucket').value = cfg.bucketName || '';
            document.getElementById('minioAccessKey').value = cfg.accessKey || '';
            document.getElementById('minioSecretKey').value = cfg.secretKey || '';
            document.getElementById('minioRemotePluginPath').value = cfg.remotePluginPath || 'plugins/';
            document.getElementById('minioRemoteModelPath').value = cfg.remoteModelPath || 'models/';
            document.getElementById('minioAutoSyncInterval').value = cfg.autoSyncIntervalMinutes ?? 60;
            document.getElementById('minioDownloadTimeout').value = cfg.downloadTimeoutSeconds ?? 300;
            document.getElementById('minioMaxRetry').value = cfg.maxRetryCount ?? 3;
            document.getElementById('minioRetryDelay').value = cfg.retryDelaySeconds ?? 5;
            document.getElementById('minioUseSsl').checked = cfg.useSsl || false;
            document.getElementById('minioSyncOnStartup').checked = cfg.syncOnStartup !== false;
            document.getElementById('minioUseETag').checked = cfg.useETag !== false;
            this._minioAddLog('info', '配置', `配置加载成功：${cfg.endpoint || '-'} / ${cfg.bucketName || '-'}`);
        } catch (e) {
            this._minioAddLog('error', '配置', `配置加载失败: ${e.message}`);
        }
    },
    async minioSaveConfig() {
        const config = {
            enabled: true,
            provider: document.getElementById('minioProvider').value || 'minio-sdk',
            endpoint: document.getElementById('minioEndpoint').value.trim(),
            bucketName: document.getElementById('minioBucket').value.trim(),
            accessKey: document.getElementById('minioAccessKey').value.trim(),
            secretKey: document.getElementById('minioSecretKey').value.trim(),
            remotePluginPath: document.getElementById('minioRemotePluginPath').value.trim() || 'plugins/',
            remoteModelPath: document.getElementById('minioRemoteModelPath').value.trim() || 'models/',
            autoSyncIntervalMinutes: parseInt(document.getElementById('minioAutoSyncInterval').value) || 60,
            downloadTimeoutSeconds: parseInt(document.getElementById('minioDownloadTimeout').value) || 300,
            maxRetryCount: parseInt(document.getElementById('minioMaxRetry').value) || 3,
            retryDelaySeconds: parseInt(document.getElementById('minioRetryDelay').value) || 5,
            useSsl: document.getElementById('minioUseSsl').checked,
            syncOnStartup: document.getElementById('minioSyncOnStartup').checked,
            useETag: document.getElementById('minioUseETag').checked
        };
        try {
            const r = await fetch('/api/minio/config', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(config) });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error || '保存失败');
            this.toast('S3 存储配置已保存', 'success');
        } catch (e) {
            this.toast('保存 S3 存储配置失败: ' + e.message, 'error');
        }
    },
    async minioCheckConnection() {
        this._setMinioBtnsDisabled(true);
        this._setMinioConnectionBadge('checking', '检查中...');
        try {
            const r = await fetch('/api/minio/check-connection', { method: 'POST' });
            const d = await r.json();
            if (d.success) {
                this._minioConnected = true;
                this._setMinioConnectionBadge('connected', '已连接');
                this.toast('S3 存储连接成功', 'success');
                await this.minioRefreshFiles();
            } else {
                this._minioConnected = false;
                this._setMinioConnectionBadge('failed', '未连接');
                this.toast('S3 存储连接失败: ' + d.message, 'error');
            }
        } catch (e) {
            this._minioConnected = false;
            this._setMinioConnectionBadge('failed', '未连接');
            this.toast('S3 存储连接异常: ' + e.message, 'error');
        } finally {
            this._setMinioBtnsDisabled(false);
        }
    },
    async minioSyncAll() { await this._minioSync('/api/minio/force-sync-all', '同步并覆盖本地'); },
    async minioSyncPlugins() { await this._minioSync('/api/minio/sync-plugins', '同步插件'); },
    async minioSyncModels() { await this._minioSync('/api/minio/sync-models', '同步模型'); },
    async minioSyncVideos() { await this._minioSync('/api/minio/sync-videos', '同步视频'); },
    async minioSyncZips() { await this._minioSync('/api/minio/sync-zips', '同步压缩包'); },
    async _minioSync(url, label) {
        if (this._minioSyncing) return;
        this._minioSyncing = true;
        this._setMinioBtnsDisabled(true);
        const syncStatus = document.getElementById('minioSyncStatus');
        const progressArea = document.getElementById('minioProgressArea');
        const progressFill = document.getElementById('minioProgressFill');
        const progressText = document.getElementById('minioProgressText');
        syncStatus.textContent = label + '中...';
        progressArea.style.display = '';
        progressFill.style.width = '0%';
        progressText.textContent = '正在' + label + '...';
        try {
            const r = await fetch(url, { method: 'POST' });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error || '同步失败');
            progressFill.style.width = '100%';
            progressText.textContent = `完成: 下载${d.downloadedCount} 更新${d.updatedCount} 跳过${d.skippedCount} 失败${d.failedCount}`;
            syncStatus.textContent = d.success ? label + '完成' : label + '部分失败';
            await this.minioRefreshFiles();
        } catch (e) {
            progressFill.style.width = '0%';
            progressText.textContent = label + '失败: ' + e.message;
            syncStatus.textContent = label + '失败';
        } finally {
            this._minioSyncing = false;
            this._setMinioBtnsDisabled(false);
        }
    },
    async minioRefreshFiles() {
        await this._minioLoadFileList('plugin', 'minioPluginFiles');
        await this._minioLoadFileList('model', 'minioModelFiles');
        await this._minioLoadFileList('video', 'minioVideoFiles');
        await this._minioLoadFileList('zip', 'minioZipFiles');
        await this.minioLoadStats();
    },
    async minioLoadStats() {
        const all = [...(this._minioFileCache.plugin || []), ...(this._minioFileCache.model || []), ...(this._minioFileCache.video || []), ...(this._minioFileCache.zip || [])];
        const pending = all.filter(f => f.status === 'Pending' || f.status === 'Updated').length;
        const existing = all.filter(f => f.status === 'Skipped' || f.status === 'Downloaded').length;
        const failed = all.filter(f => f.status === 'Failed').length;
        const setText = (id, text) => { const el = document.getElementById(id); if (el) el.textContent = text; };
        setText('minioStatsPending', String(pending));
        setText('minioStatsExisting', String(existing));
        setText('minioStatsFailed', String(failed));
        setText('minioStatsByType', `${this._minioFileCache.plugin.length} / ${this._minioFileCache.model.length} / ${this._minioFileCache.video.length} / ${this._minioFileCache.zip.length}`);
        try {
            const r = await fetch('/api/minio/stats');
            const d = await r.json();
            if (!r.ok) throw new Error(d.error || '加载统计失败');
            setText('minioStatsTotalFiles', String(d.totalFiles ?? 0));
            setText('minioStatsTotalSize', this.formatSize(d.totalSize ?? 0));
            setText('minioStatsLastSync', d.lastSyncTime ? new Date(d.lastSyncTime).toLocaleString() : '-');
        } catch {
            setText('minioStatsTotalFiles', String(all.length));
            setText('minioStatsTotalSize', '-');
            setText('minioStatsLastSync', '-');
        }
    },
    async _minioLoadProviderOptions() {
        const select = document.getElementById('minioProvider');
        if (!select) return;
        try {
            const r = await fetch('/api/minio/providers');
            if (!r.ok) return;
            const providers = await r.json();
            if (!Array.isArray(providers) || providers.length === 0) return;
            select.innerHTML = providers.map(p => `<option value="${esc(p.providerId)}">${esc(p.displayName)}</option>`).join('');
        } catch { }
    },
    async _minioLoadFileList(fileType, tbodyId) {
        const tbody = document.getElementById(tbodyId);
        try {
            const r = await fetch(`/api/minio/files/${fileType}`);
            if (!r.ok) throw new Error('加载失败');
            const files = await r.json();
            this._minioFileCache[fileType] = files || [];
            this._minioRenderFileList(fileType, tbodyId);
        } catch {
            this._minioFileCache[fileType] = [];
            tbody.innerHTML = `<tr><td colspan="4" class="minio-empty">加载失败</td></tr>`;
        }
    },
    _minioRenderFileList(fileType, tbodyId) {
        const tbody = document.getElementById(tbodyId);
        const files = this._minioFileCache[fileType] || [];
        if (!files.length) {
            tbody.innerHTML = `<tr><td colspan="4" class="minio-empty">暂无数据</td></tr>`;
            return;
        }
        tbody.innerHTML = files.map(f => {
            const statusClass = (f.status || '').toLowerCase();
            const statusLabel = this._minioStatusText(f.status);
            const canDownload = f.status === 'Pending' || f.status === 'Downloaded' || f.status === 'Updated' || f.status === 'Failed';
            return `<tr>
                <td title="${esc(f.remotePath)}">${esc(f.fileName)}</td>
                <td>${this.formatSize(f.fileSize || 0)}</td>
                <td><span class="minio-status-badge ${statusClass}">${statusLabel}</span></td>
                <td>${canDownload ? `<button class="btn btn-outline btn-xs" onclick="app.minioDownloadFile('${esc(f.remotePath)}','${esc(f.fileType)}')">下载</button>` : ''}</td>
            </tr>`;
        }).join('');
    },
    _setMinioConnectionBadge(state, text) {
        const statusEl = document.getElementById('minioConnectionStatus');
        if (!statusEl) return;
        statusEl.textContent = text;
        statusEl.style.background = 'rgba(139,143,163,.12)';
        statusEl.style.color = 'var(--text-dim)';
        if (state === 'connected') { statusEl.style.background = 'var(--success-dim)'; statusEl.style.color = 'var(--success)'; }
        else if (state === 'failed') { statusEl.style.background = 'var(--danger-dim)'; statusEl.style.color = 'var(--danger)'; }
        else if (state === 'checking') { statusEl.style.background = 'rgba(245,158,11,.15)'; statusEl.style.color = 'var(--warning)'; }
    },
    async minioDownloadFile(remotePath, fileType) {
        try {
            const r = await fetch('/api/minio/download-file', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ remotePath, fileType }) });
            const d = await r.json();
            if (!r.ok) throw new Error(d.error || '下载失败');
            this.toast(`文件下载成功: ${d.fileName}`, 'success');
            await this.minioRefreshFiles();
        } catch (e) {
            this.toast('下载文件失败: ' + e.message, 'error');
        }
    },
    _minioStatusText(status) {
        const map = { Pending: '待下载', Syncing: '同步中', Success: '已处理', Failed: '失败', Skipped: '已存在', Deleted: '已删除', Updated: '需更新', Downloaded: '已下载' };
        return map[status] || status || '未知';
    },
    _setMinioBtnsDisabled(disabled) {
        const ids = ['minioBtnCheck', 'minioBtnSyncAll', 'minioBtnSyncPlugins', 'minioBtnSyncModels', 'minioBtnSyncVideos', 'minioBtnSyncZips', 'minioBtnRefresh'];
        ids.forEach(id => { const btn = document.getElementById(id); if (btn) btn.disabled = disabled; });
    },
    minioToggleImportantLogs(checked) { this._minioOnlyImportantLogs = !!checked; this._minioRenderLogs(); },
    minioClearLogs() { this._minioLogs = []; this._minioRenderLogs(); },
    _minioAddLog(level, category, message) {
        this._minioLogs.unshift({ time: new Date().toLocaleString(), level: level || 'info', category: category || '系统', message: message || '' });
        if (this._minioLogs.length > 500) this._minioLogs.length = 500;
        this._minioRenderLogs();
    },
    _minioRenderLogs() {
        const list = document.getElementById('minioLogList');
        if (!list) return;
        const visible = this._minioOnlyImportantLogs ? this._minioLogs.filter(x => x.level === 'warn' || x.level === 'error') : this._minioLogs;
        if (!visible.length) { list.innerHTML = '<div class="minio-log-empty">暂无日志</div>'; return; }
        list.innerHTML = visible.map(item => `
            <div class="minio-log-item">
                <span class="minio-log-time">${esc(item.time)}</span>
                <span class="minio-log-level ${esc(item.level)}">${esc(item.level.toUpperCase())}</span>
                <span class="minio-log-category">${esc(item.category)}</span>
                <span class="minio-log-message">${esc(item.message)}</span>
            </div>
        `).join('');
    },

    // ═══════════════════════════════
    //  Toast
    // ═══════════════════════════════
    toast(msg, type = 'info') {
        const c = document.getElementById('toastContainer');
        const el = document.createElement('div');
        el.className = `toast ${type}`;
        el.textContent = msg;
        c.appendChild(el);
        setTimeout(() => el.remove(), 4000);
    }
};

// ── Utilities ──

function esc(str) {
    if (!str) return '';
    const d = document.createElement('div');
    d.textContent = str;
    return d.innerHTML;
}

function trunc(str, len) {
    if (!str) return '';
    const e = esc(str);
    return e.length > len ? e.substring(0, len) + '...' : e;
}

window.app = app;

// ── Init ──
(async function bootstrap() {
    if (typeof LuxI18n !== 'undefined')
        await LuxI18n.init();
    await app.init();
})();
