// Keyboard event handling for quiz navigation
let quizComponent = null;

window.addKeyboardListener = function (component) {
    quizComponent = component;
    
    // Add event listener for keydown events
    document.addEventListener('keydown', handleKeyDown);
};

window.removeKeyboardListener = function () {
    quizComponent = null;
    document.removeEventListener('keydown', handleKeyDown);
};

function handleKeyDown(event) {
    // Handle Enter key and answer keys (A, B, C, D, E, F)
    const validKeys = ['Enter', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F'];
    
    if (validKeys.includes(event.key)) {
        // Prevent default form submission behavior
        event.preventDefault();
        
        // Don't trigger if user is typing in an input field
        if (event.target.tagName === 'INPUT' || event.target.tagName === 'TEXTAREA') {
            return;
        }
        
        // Call the C# method
        if (quizComponent) {
            quizComponent.invokeMethodAsync('HandleKeyPress', event.key);
        }
    }
}
