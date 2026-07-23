class TextToSpeech {
    constructor(options = {}) {
        // 检查浏览器是否支持 Web Speech API
        if (!window.speechSynthesis) {
            console.error("当前浏览器不支持 Web Speech API");
            this.isSupported = false;
            return;
        }
        this.isSupported = true;

        // 默认配置
        this.defaults = {
            rate: options.rate || 1,     // 语速，0.1 - 10
            pitch: options.pitch || 1,   // 音调，0 - 2
            volume: options.volume || 1, // 音量，0 - 1
            lang: options.lang || 'zh-CN' // 语言，如 'zh-CN' 中文, 'en-US' 英文
        };

        // 新增：对外暴露的生命周期回调
        this.onStartCallback = null;
        this.onEndCallback = null;

        this.isSpeaking = false;
    }

    /**
     * 设置语音参数
     * @param {Object} options - 配置项
     */
    setOptions(options) {
        Object.assign(this.defaults, options);
    }

    // 新增：绑定 TTS 开始和结束的回调
    on(event, callback) {
        if (event === 'start') this.onStartCallback = callback;
        if (event === 'end') this.onEndCallback = callback;
    }

    /**
     * 获取可用的语音列表
     * @returns {Array} 语音列表
     */
    getVoices() {
        return new Promise((resolve) => {
            const voices = speechSynthesis.getVoices();
            if (voices.length) {
                resolve(voices);
            } else {
                // 有时语音列表需要一点时间加载
                setTimeout(() => {
                    resolve(speechSynthesis.getVoices());
                }, 100);
            }
        });
    }

    /**
     * 朗读文本
     * @param {String} text - 要朗读的文本
     * @param {Object} options - 临时覆盖配置
     */
    async speak(text, options = {}) {
        if (!this.isSupported) return;

        // 取消当前正在播放的语音
        this.stop();

        // 合并默认配置和临时配置
        const config = { ...this.defaults, ...options };

        // 获取语音实例
        let utterance = new SpeechSynthesisUtterance(text);
        utterance.rate = config.rate;
        utterance.pitch = config.pitch;
        utterance.volume = config.volume;
        utterance.lang = config.lang;

        // 可选：自动选择中文语音（如果可用）
        const voices = await this.getVoices();
        const zhVoice = voices.find(voice => voice.lang.startsWith('zh'));
        if (zhVoice) {
            utterance.voice = zhVoice;
        }

        // 事件监听
        utterance.onstart = () => {
            this.isSpeaking = true;
            if (this.onStartCallback) this.onStartCallback();
        };

        utterance.onend = () => {
            this.isSpeaking = false;
            if (this.onEndCallback) this.onEndCallback();
        };

        utterance.onerror = (event) => {
            this.isSpeaking = false;
            if (this.onEndCallback) this.onEndCallback();
            console.error('朗读出错', event);
        };

        // 开始朗读
        speechSynthesis.speak(utterance);
    }

    /**
     * 暂停朗读
     */
    pause() {
        if (this.isSupported && speechSynthesis.speaking) {
            speechSynthesis.pause();
        }
    }

    /**
     * 恢复朗读
     */
    resume() {
        if (this.isSupported && speechSynthesis.paused) {
            speechSynthesis.resume();
        }
    }

    /**
     * 停止朗读
     */
    stop() {
        if (this.isSupported) {
            speechSynthesis.cancel();
            this.isSpeaking = false;
        }
    }
}