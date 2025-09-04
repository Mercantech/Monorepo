import tkinter as tk
import time
import os
import threading
import random
import sys

# Choose the appropriate input method based on operating system
if os.name == 'nt':  # For Windows
    import msvcrt
else:  # For Unix/Linux/MacOS
    import tty
    import termios

class PongGame:
    def __init__(self):
        self.WIDTH = 80
        self.HEIGHT = 24
        self.ball_x = self.WIDTH // 2
        self.ball_y = self.HEIGHT // 2
        self.ball_dx = 0.5
        self.ball_dy = 0.5
        self.paddle1_y = self.HEIGHT // 2
        self.paddle2_y = self.HEIGHT // 2
        self.PADDLE_HEIGHT = 5
        self.score1 = 0
        self.score2 = 0
        self.game_running = True
    
    def move_ball(self):
        self.ball_x += self.ball_dx
        self.ball_y += self.ball_dy
        
        # Bounce off top and bottom
        if self.ball_y <= 0 or self.ball_y >= self.HEIGHT - 1:
            self.ball_dy *= -1
        
        # Check for paddle hits
        if self.ball_x <= 2 and (self.paddle1_y <= self.ball_y <= self.paddle1_y + self.PADDLE_HEIGHT):
            self.ball_dx *= -1
            self.ball_dy = random.randint(-1, 1)  # Add some randomness
        
        if self.ball_x >= self.WIDTH - 3 and (self.paddle2_y <= self.ball_y <= self.paddle2_y + self.PADDLE_HEIGHT):
            self.ball_dx *= -1
            self.ball_dy = random.randint(-1, 1)  # Add some randomness
        
        # Score points
        if self.ball_x <= 0:
            self.score2 += 1
            self.reset_ball()
        elif self.ball_x >= self.WIDTH:
            self.score1 += 1
            self.reset_ball()
    
    def reset_ball(self):
        self.ball_x = self.WIDTH // 2
        self.ball_y = self.HEIGHT // 2
        self.ball_dx = random.choice([-0.5, 0.5])
        self.ball_dy = random.choice([-0.5, 0.5])
    
    def move_paddle(self, paddle_num, direction):
        if paddle_num == 1:
            self.paddle1_y = max(0, min(self.HEIGHT - self.PADDLE_HEIGHT, self.paddle1_y + direction))
        else:
            self.paddle2_y = max(0, min(self.HEIGHT - self.PADDLE_HEIGHT, self.paddle2_y + direction))

class ConsoleGame:
    def __init__(self):
        self.game = PongGame()
        self.running = True
        if os.name != 'nt':
            self.old_settings = termios.tcgetattr(sys.stdin)
    
    def get_key_windows(self):
        if msvcrt.kbhit():
            key = msvcrt.getch().decode('utf-8').lower()
            self.process_key(key)
    
    def get_key_unix(self):
        tty.setraw(sys.stdin.fileno())
        key = sys.stdin.read(1).lower()
        termios.tcsetattr(sys.stdin, termios.TCSADRAIN, self.old_settings)
        self.process_key(key)
    
    def process_key(self, key):
        if key == 'w':
            self.game.move_paddle(1, -1)
        elif key == 's':
            self.game.move_paddle(1, 1)
        elif key == 'i':
            self.game.move_paddle(2, -1)
        elif key == 'k':
            self.game.move_paddle(2, 1)
        elif key == 'q':
            self.running = False
    
    def draw_game(self):
        os.system('cls' if os.name == 'nt' else 'clear')
        
        # Create the game board
        board = [[' ' for _ in range(self.game.WIDTH)] for _ in range(self.game.HEIGHT)]
        
        # Add ball
        board[int(self.game.ball_y)][int(self.game.ball_x)] = '●'
        
        # Add paddles
        for i in range(self.game.PADDLE_HEIGHT):
            board[int(self.game.paddle1_y) + i][1] = '█'
            board[int(self.game.paddle2_y) + i][self.game.WIDTH - 2] = '█'
        
        # Print the game
        print(f"Score: {self.game.score1} - {self.game.score2}")
        print('=' * self.game.WIDTH)
        for row in board:
            print(''.join(row))
        print('=' * self.game.WIDTH)
    
    def run_game(self):
        try:
            while self.running:
                if os.name == 'nt':
                    self.get_key_windows()
                else:
                    self.get_key_unix()
                self.game.move_ball()
                self.draw_game()
                time.sleep(0.15)
        finally:
            # Restore terminal settings for Unix systems
            if os.name != 'nt':
                termios.tcsetattr(sys.stdin, termios.TCSADRAIN, self.old_settings)

class PongGUI:
    def __init__(self, master):
        self.game = PongGame()
        self.master = master
        self.master.title("Pong")
        
        # Configure window
        self.master.configure(bg='black')
        self.master.resizable(False, False)
        
        # Create canvas
        self.canvas = tk.Canvas(
            master,
            width=800,
            height=400,
            bg='black',
            highlightthickness=0
        )
        self.canvas.pack(padx=20, pady=20)
        
        # Create score display
        self.score_display = tk.Label(
            master,
            text="0 - 0",
            font=('Arial', 24, 'bold'),
            bg='black',
            fg='white'
        )
        self.score_display.pack()
        
        # Bind keys
        self.master.bind('<w>', lambda e: self.game.move_paddle(1, -2))
        self.master.bind('<s>', lambda e: self.game.move_paddle(1, 2))
        self.master.bind('<Up>', lambda e: self.game.move_paddle(2, -2))
        self.master.bind('<Down>', lambda e: self.game.move_paddle(2, 2))
        
        self.update_game()
    
    def draw_game(self):
        self.canvas.delete("all")
        
        # Draw middle line
        for y in range(0, 400, 20):
            self.canvas.create_line(400, y, 400, y + 10, fill='white', width=2)
        
        # Draw paddles
        paddle1_pos = self.game.paddle1_y * (400 / self.game.HEIGHT)
        paddle2_pos = self.game.paddle2_y * (400 / self.game.HEIGHT)
        self.canvas.create_rectangle(20, paddle1_pos, 30, paddle1_pos + 50, fill='white')
        self.canvas.create_rectangle(770, paddle2_pos, 780, paddle2_pos + 50, fill='white')
        
        # Draw ball
        ball_x = self.game.ball_x * (800 / self.game.WIDTH)
        ball_y = self.game.ball_y * (400 / self.game.HEIGHT)
        self.canvas.create_oval(ball_x-5, ball_y-5, ball_x+5, ball_y+5, fill='white')
        
        # Update score
        self.score_display.config(text=f"{self.game.score1} - {self.game.score2}")
    
    def update_game(self):
        if self.game.game_running:
            self.game.move_ball()
            self.draw_game()
            self.master.after(25, self.update_game)

def play_console():
    print("Controls:")
    print("Player 1: W/S")
    print("Player 2: I/K")
    print("Quit: Q")
    input("Press Enter to start...")
    
    game = ConsoleGame()
    game.run_game()

def play_gui():
    root = tk.Tk()
    app = PongGUI(root)
    root.mainloop()

if __name__ == "__main__":
    while True:
        choice = input("Choose game mode:\n1. Console\n2. GUI\n3. Exit\nYour choice (1-3): ")
        if choice == "1":
            play_console()
        elif choice == "2":
            play_gui()
        elif choice == "3":
            break
        else:
            print("Invalid choice! Please enter 1, 2, or 3.")
