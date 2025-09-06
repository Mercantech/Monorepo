// Scroll Position Manager
window.scrollManager = {
    saveScrollPosition: function() {
        const scrollX = window.scrollX || window.pageXOffset;
        const scrollY = window.scrollY || window.pageYOffset;
        
        // Save to sessionStorage
        sessionStorage.setItem('scrollPosition', JSON.stringify({
            x: scrollX,
            y: scrollY,
            timestamp: Date.now()
        }));
    },
    
    restoreScrollPosition: function() {
        try {
            const saved = sessionStorage.getItem('scrollPosition');
            if (saved) {
                const position = JSON.parse(saved);
                const now = Date.now();
                
                // Only restore if saved within last 5 minutes
                if (now - position.timestamp < 300000) {
                    window.scrollTo(position.x, position.y);
                }
            }
        } catch (e) {
            console.warn('Could not restore scroll position:', e);
        }
    },
    
    clearScrollPosition: function() {
        sessionStorage.removeItem('scrollPosition');
    }
};

// Global functions for Blazor to call
window.saveScrollPosition = function() {
    window.scrollManager.saveScrollPosition();
};

window.restoreScrollPosition = function() {
    window.scrollManager.restoreScrollPosition();
};

window.clearScrollPosition = function() {
    window.scrollManager.clearScrollPosition();
};

// Auto-save scroll position on scroll (throttled)
let scrollTimeout;
window.addEventListener('scroll', function() {
    clearTimeout(scrollTimeout);
    scrollTimeout = setTimeout(function() {
        window.scrollManager.saveScrollPosition();
    }, 100);
});

// Clear scroll position on page unload
window.addEventListener('beforeunload', function() {
    window.scrollManager.clearScrollPosition();
});
