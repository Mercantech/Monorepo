// Initialize Prism.js syntax highlighting
(function() {
    function highlightAllCode() {
        if (typeof Prism !== 'undefined') {
            // Highlight all code blocks on the page
            Prism.highlightAll();
            
            // Also try to highlight code within dynamically loaded content
            document.querySelectorAll('pre code[class*="language-"]').forEach(function(element) {
                if (!element.classList.contains('language-none') && !element.hasAttribute('data-prism-processed')) {
                    Prism.highlightElement(element);
                }
            });
        }
    }

    // Initial highlight
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', highlightAllCode);
    } else {
        // DOM already loaded
        highlightAllCode();
    }

    // Function to highlight all code blocks (can be called from Blazor)
    window.highlightCode = highlightAllCode;

    // Listen for Blazor navigation events (if using Blazor Server)
    if (window.Blazor) {
        // Override DOMContentLoaded might not fire on navigation, so we use MutationObserver
        const observer = new MutationObserver(function(mutations) {
            let shouldHighlight = false;
            mutations.forEach(function(mutation) {
                if (mutation.addedNodes.length > 0) {
                    mutation.addedNodes.forEach(function(node) {
                        if (node.nodeType === 1) { // Element node
                            // Check if any code blocks were added
                            if (node.querySelectorAll && (
                                node.querySelectorAll('pre code[class*="language-"]').length > 0 ||
                                node.tagName === 'CODE' ||
                                node.tagName === 'PRE'
                            )) {
                                shouldHighlight = true;
                            }
                        }
                    });
                }
            });
            
            if (shouldHighlight) {
                // Debounce highlighting
                clearTimeout(window._prismHighlightTimeout);
                window._prismHighlightTimeout = setTimeout(highlightAllCode, 100);
            }
        });

        // Start observing when DOM is ready
        if (document.body) {
            observer.observe(document.body, {
                childList: true,
                subtree: true
            });
        } else {
            document.addEventListener('DOMContentLoaded', function() {
                if (document.body) {
                    observer.observe(document.body, {
                        childList: true,
                        subtree: true
                    });
                }
            });
        }

        // Also listen for Blazor navigation events
        document.addEventListener('DOMContentLoaded', function() {
            // Re-highlight when navigation completes
            setTimeout(highlightAllCode, 500);
            
            // Periodic check (fallback)
            setInterval(function() {
                const unprocessed = document.querySelectorAll('pre code[class*="language-"]:not([data-prism-processed])');
                if (unprocessed.length > 0) {
                    highlightAllCode();
                }
            }, 1000);
        });
    }
})();

