// Simple syntax highlighting for C# code
window.syntaxHighlighting = {
    highlightCSharp: function(element) {
        if (!element) return;
        
        // Check if already highlighted to avoid double processing
        if (element.innerHTML !== element.textContent) return;
        
        const code = element.textContent;
        if (!code) return;
        
        // Keywords - different categories for better highlighting
        const accessModifiers = ['public', 'private', 'protected', 'internal'];
        const modifiers = ['static', 'virtual', 'override', 'abstract', 'sealed', 'readonly', 'const', 'async'];
        const controlFlow = ['if', 'else', 'for', 'while', 'foreach', 'do', 'switch', 'case', 'default', 'break', 'continue', 'return'];
        const exceptionHandling = ['try', 'catch', 'finally', 'throw'];
        const declarations = ['using', 'namespace', 'class', 'interface', 'struct', 'enum', 'var', 'new'];
        const context = ['this', 'base', 'await', 'void'];
        
        // Types
        const primitiveTypes = ['int', 'string', 'bool', 'char', 'double', 'float', 'decimal', 'long', 'short', 'byte', 'sbyte', 'uint', 'ulong', 'ushort'];
        const referenceTypes = ['object', 'dynamic', 'List', 'Dictionary', 'Array', 'Task', 'Action', 'Func', 'IEnumerable', 'ICollection'];
        
        // Methods and properties
        const consoleMethods = ['Console.WriteLine', 'Console.ReadLine', 'Console.Write', 'Console.Read'];
        const objectMethods = ['ToString', 'Equals', 'GetHashCode', 'GetType'];
        const stringMethods = ['Length', 'Substring', 'IndexOf', 'Replace', 'Split', 'Trim'];
        const arrayMethods = ['Length', 'Count', 'Add', 'Remove', 'Contains', 'Clear'];
        
        let highlighted = code;
        
        // Highlight strings first (to avoid highlighting inside strings)
        highlighted = highlighted.replace(/"([^"\\]|\\.)*"/g, '<span class="string">$&</span>');
        highlighted = highlighted.replace(/'([^'\\]|\\.)*'/g, '<span class="string">$&</span>');
        
        // Highlight comments
        highlighted = highlighted.replace(/\/\/.*$/gm, '<span class="comment">$&</span>');
        highlighted = highlighted.replace(/\/\*[\s\S]*?\*\//g, '<span class="comment">$&</span>');
        
        // Highlight numbers
        highlighted = highlighted.replace(/\b\d+(\.\d+)?\b/g, '<span class="number">$&</span>');
        
        // Highlight access modifiers (purple)
        accessModifiers.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="access-modifier">${keyword}</span>`);
        });
        
        // Highlight modifiers (blue)
        modifiers.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="modifier">${keyword}</span>`);
        });
        
        // Highlight control flow (orange)
        controlFlow.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="control-flow">${keyword}</span>`);
        });
        
        // Highlight exception handling (red)
        exceptionHandling.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="exception">${keyword}</span>`);
        });
        
        // Highlight declarations (green)
        declarations.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="declaration">${keyword}</span>`);
        });
        
        // Highlight context keywords (yellow)
        context.forEach(keyword => {
            const regex = new RegExp(`\\b${keyword}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="context">${keyword}</span>`);
        });
        
        // Highlight primitive types (cyan)
        primitiveTypes.forEach(type => {
            const regex = new RegExp(`\\b${type}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="primitive-type">${type}</span>`);
        });
        
        // Highlight reference types (light blue)
        referenceTypes.forEach(type => {
            const regex = new RegExp(`\\b${type}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="reference-type">${type}</span>`);
        });
        
        // Highlight console methods (yellow)
        consoleMethods.forEach(method => {
            const regex = new RegExp(`\\b${method.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="console-method">${method}</span>`);
        });
        
        // Highlight object methods (light yellow)
        objectMethods.forEach(method => {
            const regex = new RegExp(`\\b${method}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="object-method">${method}</span>`);
        });
        
        // Highlight string methods (light green)
        stringMethods.forEach(method => {
            const regex = new RegExp(`\\b${method}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="string-method">${method}</span>`);
        });
        
        // Highlight array methods (light cyan)
        arrayMethods.forEach(method => {
            const regex = new RegExp(`\\b${method}\\b(?![^<]*>)`, 'g');
            highlighted = highlighted.replace(regex, `<span class="array-method">${method}</span>`);
        });
        
        // Highlight method names (function definitions)
        highlighted = highlighted.replace(/\b([a-zA-Z_][a-zA-Z0-9_]*)\s*\(/g, (match, methodName) => {
            // Don't highlight if it's already highlighted or if it's a keyword
            if (match.includes('<span') || 
                [...accessModifiers, ...modifiers, ...controlFlow, ...exceptionHandling, ...declarations, ...context, ...primitiveTypes, ...referenceTypes].includes(methodName)) {
                return match;
            }
            return `<span class="method-name">${methodName}</span>(`;
        });
        
        // Highlight variable names (after = or in parameter lists)
        highlighted = highlighted.replace(/\b([a-zA-Z_][a-zA-Z0-9_]*)\s*[=;]/g, (match, varName) => {
            // Don't highlight if it's already highlighted or if it's a keyword
            if (match.includes('<span') || 
                [...accessModifiers, ...modifiers, ...controlFlow, ...exceptionHandling, ...declarations, ...context, ...primitiveTypes, ...referenceTypes].includes(varName)) {
                return match;
            }
            return `<span class="variable">${varName}</span>${match.substring(varName.length)}`;
        });
        
        // Highlight operators (avoid already highlighted content)
        highlighted = highlighted.replace(/([+\-*/%=<>!&|^~])(?![^<]*>)/g, '<span class="operator">$1</span>');
        
        // Highlight punctuation (avoid already highlighted content)
        highlighted = highlighted.replace(/([{}();,])(?![^<]*>)/g, '<span class="punctuation">$1</span>');
        
        // Update the element's innerHTML
        element.innerHTML = highlighted;
    }
};
