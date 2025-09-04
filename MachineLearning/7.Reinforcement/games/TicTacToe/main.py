import tkinter as tk
from tkinter import messagebox

class TicTacToe:
    def __init__(self):
        # Initialize board with numbers 0-8
        self.board = [str(i) for i in range(9)]
        self.current_player = "X"
        # ANSI color codes
        self.BLUE = "\033[94m"
        self.RED = "\033[91m"
        self.RESET = "\033[0m"
    
    def make_move(self, position):
        """Make a move using a single number (0-8)"""
        if self.board[position] not in ["X", "O"]:
            self.board[position] = self.current_player
            self.current_player = "O" if self.current_player == "X" else "X"
            return True
        return False
    
    def check_winner(self):
        """Check if there's a winner. Returns X, O, Tie, or None."""
        # Convert board to 2D for easier checking
        b = [self.board[i:i+3] for i in range(0, 9, 3)]
        
        # Check rows
        for row in b:
            if row.count(row[0]) == 3 and row[0] in ["X", "O"]:
                return row[0]
        
        # Check columns
        for col in range(3):
            if b[0][col] == b[1][col] == b[2][col] and b[0][col] in ["X", "O"]:
                return b[0][col]
        
        # Check diagonals
        if (b[0][0] == b[1][1] == b[2][2]) and b[0][0] in ["X", "O"]:
            return b[0][0]
        if (b[0][2] == b[1][1] == b[2][0]) and b[0][2] in ["X", "O"]:
            return b[0][2]
        
        # Check for tie
        if all(cell in ["X", "O"] for cell in self.board):
            return "Tie"
        
        return None
    
    def print_board(self):
        """Print the board with colored X's and O's"""
        print("\nCurrent board:")
        for i in range(0, 9, 3):
            row = self.board[i:i+3]
            colored_row = []
            for cell in row:
                if cell == "X":
                    colored_row.append(f"{self.BLUE}X{self.RESET}")
                elif cell == "O":
                    colored_row.append(f"{self.RED}O{self.RESET}")
                else:
                    colored_row.append(cell)
            print(f" {colored_row[0]} | {colored_row[1]} | {colored_row[2]} ")
            if i < 6:  # Don't print line after last row
                print("-----------")
        print()

class TicTacToeGUI:
    def __init__(self, master):
        self.game = TicTacToe()
        self.master = master
        self.master.title("Tic Tac Toe")
        
        # Configure window
        self.master.configure(bg='#2C3E50')  # Dark blue background
        self.master.resizable(False, False)   # Fixed window size
        
        # Create main frame with padding
        self.main_frame = tk.Frame(master, bg='#2C3E50', padx=20, pady=20)
        self.main_frame.pack(expand=True)
        
        # Add title label
        self.title_label = tk.Label(
            self.main_frame,
            text="Tic Tac Toe",
            font=('Arial', 24, 'bold'),
            bg='#2C3E50',
            fg='white',
            pady=10
        )
        self.title_label.pack()
        
        # Add turn indicator
        self.turn_label = tk.Label(
            self.main_frame,
            text="Player X's turn",
            font=('Arial', 16),
            bg='#2C3E50',
            fg='white',
            pady=10
        )
        self.turn_label.pack()
        
        # Create game board frame
        self.board_frame = tk.Frame(
            self.main_frame,
            bg='#34495E',  # Lighter blue
            padx=10,
            pady=10
        )
        self.board_frame.pack()
        
        # Create buttons
        self.buttons = []
        for i in range(9):
            button = tk.Button(
                self.board_frame,
                text=str(i),
                font=('Arial', 24, 'bold'),
                width=3,
                height=1,
                bg='#ECF0F1',  # Light gray
                activebackground='#BDC3C7',  # Darker gray when clicked
                command=lambda x=i: self.button_click(x)
            )
            button.grid(
                row=i//3,
                column=i%3,
                padx=3,
                pady=3
            )
            self.buttons.append(button)
    
    def button_click(self, position):
        if self.game.make_move(position):
            # Update button
            color = "#3498DB" if self.game.board[position] == "X" else "#E74C3C"  # Blue for X, Red for O
            self.buttons[position].config(
                text=self.game.board[position],
                fg=color,
                state="disabled",
                disabledforeground=color  # Keep color when disabled
            )
            
            # Update turn label
            next_player = "O" if self.game.current_player == "O" else "X"
            self.turn_label.config(text=f"Player {next_player}'s turn")
            
            # Check for winner
            winner = self.game.check_winner()
            if winner:
                if winner == "Tie":
                    messagebox.showinfo("Game Over", "Game ended in a tie!")
                else:
                    messagebox.showinfo("Game Over", f"Player {winner} wins!")
                
                # Ask if players want to play again
                if messagebox.askyesno("Play Again?", "Would you like to play another game?"):
                    self.reset_game()
                else:
                    self.master.quit()
    
    def reset_game(self):
        """Reset the game to initial state"""
        self.game = TicTacToe()
        for button in self.buttons:
            button.config(
                text=str(self.buttons.index(button)),
                fg='black',
                state="normal",
                bg='#ECF0F1'
            )
        self.turn_label.config(text="Player X's turn")

def play_console():
    game = TicTacToe()
    
    while True:
        game.print_board()
        print(f"Player {game.current_player}'s turn")
        
        try:
            position = int(input("Enter position (0-8): "))
            
            if 0 <= position <= 8:
                if game.make_move(position):
                    winner = game.check_winner()
                    if winner:
                        game.print_board()
                        if winner == "Tie":
                            print("Game ended in a tie!")
                        else:
                            color = game.BLUE if winner == "X" else game.RED
                            print(f"Player {color}{winner}{game.RESET} wins!")
                        break
                else:
                    print("That position is already taken!")
            else:
                print("Invalid input! Position must be between 0 and 8")
        except ValueError:
            print("Please enter a valid number!")

def play_gui():
    root = tk.Tk()
    app = TicTacToeGUI(root)
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
