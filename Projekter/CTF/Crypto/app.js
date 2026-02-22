// Kryptografi CTF - JavaScript Logic
class CryptoCTF {
    constructor() {
        this.challenges = {
            caesar: {
                name: 'Caesar Cipher',
                solved: false,
                correctFlag: 'CTF{caesar_cipher}',
                description: 'Dekrypter Caesar cipher med shift 3'
            },
            base64: {
                name: 'Base64 Encoding',
                solved: false,
                correctFlag: 'CTF{base64_decoded}',
                description: 'Dekod Base64 streng'
            },
            rot13: {
                name: 'ROT13',
                solved: false,
                correctFlag: 'CTF{rot13_text}',
                description: 'Dekrypter ROT13 tekst'
            },
            hex: {
                name: 'Hexadecimal',
                solved: false,
                correctFlag: 'CTF{hex_converted}',
                description: 'Konverter hex til tekst'
            },
            vigenere: {
                name: 'Vigenère Cipher',
                solved: false,
                correctFlag: 'CTF{vigenere_solved}',
                description: 'Dekrypter Vigenère cipher med nøgle SECRET'
            }
        };
        
        this.totalChallenges = Object.keys(this.challenges).length;
        this.solvedCount = 0;
        
        this.init();
    }

    init() {
        this.updateProgress();
        this.setupEventListeners();
        this.loadProgress();
    }

    setupEventListeners() {
        // Enter key support for flag inputs
        document.querySelectorAll('.flag-input').forEach(input => {
            input.addEventListener('keypress', (e) => {
                if (e.key === 'Enter') {
                    const challengeId = e.target.getAttribute('data-challenge');
                    this.checkFlag(challengeId);
                }
            });
        });

        // Auto-focus on first unsolved challenge
        this.focusNextUnsolved();
    }

    checkFlag(challengeId) {
        const input = document.querySelector(`input[data-challenge="${challengeId}"]`);
        const resultDiv = document.getElementById(`result-${challengeId}`);
        const card = document.querySelector(`[data-challenge="${challengeId}"]`).closest('.challenge-card');
        const submitBtn = card.querySelector('.submit-btn');
        
        // Debug logging
        console.log('Challenge ID:', challengeId);
        console.log('Input element:', input);
        console.log('Result div:', resultDiv);
        console.log('Card:', card);
        console.log('Submit btn:', submitBtn);
        
        if (!input) {
            console.error('Input element not found for challenge:', challengeId);
            return;
        }
        
        const userInput = input.value.trim();
        const challenge = this.challenges[challengeId];
        
        if (!userInput) {
            this.showResult(resultDiv, 'Indtast et flag først!', 'error');
            return;
        }

        // Disable button during check
        submitBtn.disabled = true;
        submitBtn.textContent = 'Tjekker...';

        // Simulate checking delay for better UX
        setTimeout(() => {
            if (this.validateFlag(userInput, challenge.correctFlag)) {
                if (!challenge.solved) {
                    challenge.solved = true;
                    this.solvedCount++;
                    this.updateProgress();
                    this.saveProgress();
                    
                    // Mark card as solved
                    card.classList.add('solved');
                    submitBtn.disabled = true;
                    submitBtn.textContent = '✅ Løst!';
                    
                    this.showResult(resultDiv, '🎉 Korrekt! Godt klaret!', 'success');
                    
                    // Check if all challenges are solved
                    if (this.solvedCount === this.totalChallenges) {
                        this.showCompletionMessage();
                    }
                } else {
                    this.showResult(resultDiv, '✅ Dette flag er allerede løst!', 'success');
                }
            } else {
                this.showResult(resultDiv, '❌ Forkert flag. Prøv igen!', 'error');
                submitBtn.disabled = false;
                submitBtn.textContent = 'Tjek Flag';
            }
        }, 500);
    }

    validateFlag(userInput, correctFlag) {
        // Normalize input: convert to lowercase and trim
        const normalizedInput = userInput.toLowerCase().trim();
        const normalizedCorrect = correctFlag.toLowerCase().trim();
        
        return normalizedInput === normalizedCorrect;
    }

    showResult(element, message, type) {
        element.innerHTML = `<div class="${type}">${message}</div>`;
        element.style.display = 'block';
        
        // Auto-hide success messages after 3 seconds
        if (type === 'success') {
            setTimeout(() => {
                element.style.display = 'none';
            }, 3000);
        }
    }

    updateProgress() {
        const progressFill = document.getElementById('progressFill');
        const solvedCountElement = document.getElementById('solvedCount');
        const totalCountElement = document.getElementById('totalCount');
        
        const percentage = (this.solvedCount / this.totalChallenges) * 100;
        
        progressFill.style.width = `${percentage}%`;
        solvedCountElement.textContent = this.solvedCount;
        totalCountElement.textContent = this.totalChallenges;
    }

    showCompletionMessage() {
        const completionMessage = document.getElementById('completionMessage');
        completionMessage.style.display = 'block';
        
        // Scroll to completion message
        completionMessage.scrollIntoView({ behavior: 'smooth' });
        
        // Confetti effect (simple)
        this.createConfetti();
    }

    createConfetti() {
        // Simple confetti effect
        const colors = ['#FFD700', '#FFA500', '#4CAF50', '#2196F3', '#9C27B0'];
        const confettiCount = 50;
        
        for (let i = 0; i < confettiCount; i++) {
            setTimeout(() => {
                const confetti = document.createElement('div');
                confetti.style.position = 'fixed';
                confetti.style.left = Math.random() * 100 + 'vw';
                confetti.style.top = '-10px';
                confetti.style.width = '10px';
                confetti.style.height = '10px';
                confetti.style.backgroundColor = colors[Math.floor(Math.random() * colors.length)];
                confetti.style.borderRadius = '50%';
                confetti.style.pointerEvents = 'none';
                confetti.style.zIndex = '1000';
                confetti.style.animation = 'confettiFall 3s linear forwards';
                
                document.body.appendChild(confetti);
                
                setTimeout(() => {
                    confetti.remove();
                }, 3000);
            }, i * 50);
        }
    }

    focusNextUnsolved() {
        const unsolvedInputs = document.querySelectorAll('.flag-input:not([disabled])');
        if (unsolvedInputs.length > 0) {
            unsolvedInputs[0].focus();
        }
    }

    saveProgress() {
        const progress = {
            challenges: this.challenges,
            solvedCount: this.solvedCount
        };
        localStorage.setItem('cryptoCTFProgress', JSON.stringify(progress));
    }

    loadProgress() {
        const saved = localStorage.getItem('cryptoCTFProgress');
        if (saved) {
            try {
                const progress = JSON.parse(saved);
                this.challenges = progress.challenges || this.challenges;
                this.solvedCount = progress.solvedCount || 0;
                
                // Update UI to reflect loaded progress
                Object.keys(this.challenges).forEach(challengeId => {
                    if (this.challenges[challengeId].solved) {
                        const card = document.querySelector(`[data-challenge="${challengeId}"]`).closest('.challenge-card');
                        const submitBtn = card.querySelector('.submit-btn');
                        
                        card.classList.add('solved');
                        submitBtn.disabled = true;
                        submitBtn.textContent = '✅ Løst!';
                    }
                });
                
                this.updateProgress();
                
                if (this.solvedCount === this.totalChallenges) {
                    this.showCompletionMessage();
                }
            } catch (e) {
                console.log('Could not load progress:', e);
            }
        }
    }

    resetProgress() {
        Object.keys(this.challenges).forEach(challengeId => {
            this.challenges[challengeId].solved = false;
        });
        this.solvedCount = 0;
        this.updateProgress();
        localStorage.removeItem('cryptoCTFProgress');
        
        // Reset UI
        document.querySelectorAll('.challenge-card').forEach(card => {
            card.classList.remove('solved');
            const submitBtn = card.querySelector('.submit-btn');
            submitBtn.disabled = false;
            submitBtn.textContent = 'Tjek Flag';
        });
        
        document.querySelectorAll('.flag-input').forEach(input => {
            input.value = '';
        });
        
        document.getElementById('completionMessage').style.display = 'none';
    }
}

// Global functions for HTML onclick events
function checkFlag(challengeId) {
    if (window.cryptoCTF) {
        window.cryptoCTF.checkFlag(challengeId);
    }
}

function toggleHint(challengeId) {
    const hintElement = document.getElementById(`hint-${challengeId}`);
    const hintBtn = document.querySelector(`[onclick="toggleHint('${challengeId}')"]`);
    
    if (hintElement.style.display === 'none' || hintElement.style.display === '') {
        hintElement.style.display = 'block';
        hintBtn.textContent = '💡 Skjul Hint';
        hintBtn.style.background = 'rgba(255, 215, 0, 0.4)';
    } else {
        hintElement.style.display = 'none';
        hintBtn.textContent = '💡 Vis Hint';
        hintBtn.style.background = 'rgba(255, 215, 0, 0.2)';
    }
}

function goHome() {
    window.location.href = '../index.html';
}

// Initialize CTF when page loads
document.addEventListener('DOMContentLoaded', function() {
    window.cryptoCTF = new CryptoCTF();
    
    // Add confetti animation CSS
    const style = document.createElement('style');
    style.textContent = `
        @keyframes confettiFall {
            0% {
                transform: translateY(-100vh) rotate(0deg);
                opacity: 1;
            }
            100% {
                transform: translateY(100vh) rotate(720deg);
                opacity: 0;
            }
        }
    `;
    document.head.appendChild(style);
});

// Add some utility functions for debugging (remove in production)
if (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1') {
    window.resetCryptoCTF = () => {
        if (window.cryptoCTF) {
            window.cryptoCTF.resetProgress();
        }
    };
    
    window.showAllFlags = () => {
        if (window.cryptoCTF) {
            Object.keys(window.cryptoCTF.challenges).forEach(challengeId => {
                console.log(`${challengeId}: ${window.cryptoCTF.challenges[challengeId].correctFlag}`);
            });
        }
    };
}
