class AudioPlayer {
    constructor() {
        // 确保全局只有一个 AudioPlayer 实例（单例模式）
        if (AudioPlayer.instance) {
            return AudioPlayer.instance;
        }
        AudioPlayer.instance = this;
        // 1. 初始化 Web Audio API 上下文
        const AudioContext = window.AudioContext || window.webkitAudioContext;
        this.audioCtx = new AudioContext();
        // 创建全局音量控制节点 (GainNode)
        this.gainNode = this.audioCtx.createGain();
        // 创建唯一的 Audio 实例
        this.audio = new Audio();
        // 2. 将 audio 元素连接到 gainNode，而不是直接输出到 destination
        this.mediaSource = this.audioCtx.createMediaElementSource(this.audio);
        this.mediaSource.connect(this.gainNode);
        this.gainNode.connect(this.audioCtx.destination); // 连接到扬声器
        this.audio.crossOrigin = "anonymous"; //防止跨域污染导致 Web Audio API 失效

        this.queue = []; // 播放队列，存放待播放的 src 路径
        this.isPlaying = false; // 标记当前是否正在播放中
        this.currentPlayingUrl = null; //记录当前正在播放的音频 URL

        // 监听音频播放结束事件，自动播放下一首
        this.audio.addEventListener('ended', () => {
            this.releaseCurrentUrl(); // 【新增】播放结束时释放当前 URL
            this.playNext();
        });

        // 监听播放出错事件，跳过当前错误的音频，继续播放下一首
        this.audio.addEventListener('error', (e) => {
            this.releaseCurrentUrl();
            this.playNext();
        });
    }

    /**
   * 释放当前 Blob URL 的方法
   */
    releaseCurrentUrl() {
        if (this.currentPlayingUrl) {
            URL.revokeObjectURL(this.currentPlayingUrl);
            this.currentPlayingUrl = null; // 释放后置空
        }
    }

    /**
     * 将音频 src 加入播放队列
     * @param {string} src - 音频文件的 URL 路径
     */
    enqueue(src) {
        if (!src) return;

        // 将新的 src 推入队列尾部
        this.queue.push(src);

        // 如果当前没有在播放，立即触发播放逻辑
        if (!this.isPlaying) {
            this.playNext();
        }
    }

    /**
     * 内部方法：播放队列中的下一个音频
     */
    playNext() {
        // 如果队列为空，说明所有音频已播放完毕，清空状态
        if (this.queue.length === 0) {
            this.isPlaying = false;
            return;
        }

        this.isPlaying = true;

        // 取出队列头部的第一个 src（出队），并从原数组中删除
        const nextSrc = this.queue.shift();
        //在设置新 src 之前，先释放掉上一个残留的 URL（双重保险）
        this.releaseCurrentUrl();
        // 动态配置 Audio 的 url 并记录到 currentPlayingUrl 中
        this.currentPlayingUrl = nextSrc;
        this.audio.src = nextSrc;
        this.audio.load();

        // 解决浏览器自动播放策略限制，确保 AudioContext 处于运行状态
        if (this.audioCtx.state === 'suspended') { this.audioCtx.resume(); }

        // 调用 play()，注意处理浏览器自动播放策略可能抛出的异常
        this.audio.play().catch(err => {
            console.warn('播放被浏览器拦截，请确保在用户交互后调用:', err);
        });
    }

    /**
     * 强制清空整个播放队列并停止当前播放
     */
    clearQueue() {
        this.releaseCurrentUrl();
        this.queue = [];
        this.audio.pause();
        this.audio.currentTime = 0;
        this.isPlaying = false;
    }

    /**
     * 一个平滑改变音量的函数（淡入淡出效果）
     * @param {any} targetVol
     * @param {any} duration
     * @returns
     */
    fadeVolume(targetVol, duration = 500) {
        const now = this.audioCtx.currentTime;
        const currentVol = this.gainNode.gain.value;

        // 取消当前所有的自动化音量变化，避免冲突
        this.gainNode.gain.cancelScheduledValues(now);
        // 设置当前起始音量
        this.gainNode.gain.setValueAtTime(currentVol, now);
        // 线性渐变到目标音量
        this.gainNode.gain.linearRampToValueAtTime(targetVol, now + duration / 1000);
    }
}