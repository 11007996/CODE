/**
 * 与 LuxVideoDet.Localization 共用键名：GET /api/ui/i18n（受 Cookie / ?culture= 影响）。
 */
(function (global) {
    const STORAGE_KEY = 'luxvideodet.uiCulture';
    const COOKIE_NAME = '.AspNetCore.Culture';

    function readStoredCulture() {
        try {
            const v = localStorage.getItem(STORAGE_KEY);
            return v && v.trim() ? v.trim() : 'zh-CN';
        } catch {
            return 'zh-CN';
        }
    }

    function normalizeCulture(c) {
        if (!c) return 'zh-CN';
        const x = String(c).trim();
        if (x === 'en-US' || x === 'zh-CN' || x === 'vi-VN') return x;
        return 'zh-CN';
    }

    /** @type {Record<string, string>} */
    let strings = {};

    function applyDom() {
        document.querySelectorAll('[data-i18n]').forEach((el) => {
            const k = el.getAttribute('data-i18n');
            if (k && Object.prototype.hasOwnProperty.call(strings, k))
                el.textContent = strings[k];
        });
        document.querySelectorAll('[data-i18n-placeholder]').forEach((el) => {
            const k = el.getAttribute('data-i18n-placeholder');
            if (k && Object.prototype.hasOwnProperty.call(strings, k) && 'placeholder' in el)
                el.placeholder = strings[k];
        });
        if (strings.Web_PageTitle)
            document.title = strings.Web_PageTitle;
        try {
            if (typeof window !== 'undefined' && window.app && typeof window.app.onLocaleApplied === 'function')
                window.app.onLocaleApplied();
        } catch (_) { /* ignore */ }
    }

    function setCultureCookie(culture) {
        const v = encodeURIComponent('c=' + culture + '|uic=' + culture);
        document.cookie = COOKIE_NAME + '=' + v + ';path=/;max-age=31536000;SameSite=Lax';
    }

    async function init() {
        let culture = normalizeCulture(readStoredCulture());
        const q = new URLSearchParams(window.location.search);
        if (q.get('culture')) culture = normalizeCulture(q.get('culture'));
        try {
            localStorage.setItem(STORAGE_KEY, culture);
        } catch (_) { /* ignore */ }
        setCultureCookie(culture);

        const apiQ = new URLSearchParams({ culture, 'ui-culture': culture });
        const r = await fetch('/api/ui/i18n?' + apiQ.toString());
        if (!r.ok) return culture;
        strings = await r.json();
        document.documentElement.lang = culture;
        applyDom();

        const sel = document.getElementById('luxLang');
        if (sel) {
            sel.value = culture;
            sel.addEventListener('change', () => {
                const next = normalizeCulture(sel.value);
                try {
                    localStorage.setItem(STORAGE_KEY, next);
                } catch (_) { /* ignore */ }
                setCultureCookie(next);
                const url = new URL(window.location.href);
                url.searchParams.set('culture', next);
                url.searchParams.set('ui-culture', next);
                window.location.href = url.toString();
            });
        }
        return culture;
    }

    function t(key) {
        return strings[key] ?? key;
    }

    global.LuxI18n = { init, applyDom, t };
})(typeof window !== 'undefined' ? window : globalThis);
