import tkinter as tk
from tkinter import messagebox

class ConnectFour:
    def __init__(self):
        self.ROWS = 6
        self.COLS = 7
        self.board = [[" " for _ in range(self.COLS)] for _ in range(self.ROWS)]
        self.current_player = "X"
        # ANSI color codes
        self.BLUE = "\033[94m"
        self.RED = "\033[91m"
        self.RESET = "\033[0m"
    
    def make_move(self, col):
        """Make a move in the selected column. Returns row position if valid, -1 if invalid."""
        # Find the first empty spot from bottom
        for row in range(self.ROWS-1, -1, -1):
            if self.board[row][col] == " ":
                self.board[row][col] = self.current_player
                self.current_player = "O" if self.current_player == "X" else "X"
                return row
        return -1
    
    def check_winner(self):
        """Check if there's a winner. Returns X, O, Tie, or None."""
        # Check horizontal
        for row in range(self.ROWS):
            for col in range(self.COLS-3):
                if (self.board[row][col] != " " and
                    self.board[row][col] == self.board[row][col+1] == 
                    self.board[row][col+2] == self.board[row][col+3]):
                    return self.board[row][col]
        
        # Check vertical
        for row in range(self.ROWS-3):
            for col in range(self.COLS):
                if (self.board[row][col] != " " and
                    self.board[row][col] == self.board[row+1][col] == 
                    self.board[row+2][col] == self.board[row+3][col]):
                    return self.board[row][col]
        
        # Check diagonal (positive slope)
        for row in range(self.ROWS-3):
            for col in range(self.COLS-3):
                if (self.board[row][col] != " " and
                    self.board[row][col] == self.board[row+1][col+1] == 
                    self.board[row+2][col+2] == self.board[row+3][col+3]):
                    return self.board[row][col]
        
        # Check diagonal (negative slope)
        for row in range(3, self.ROWS):
            for col in range(self.COLS-3):
                if (self.board[row][col] != " " and
                    self.board[row][col] == self.board[row-1][col+1] == 
                    self.board[row-2][col+2] == self.board[row-3][col+3]):
                    return self.board[row][col]
        
        # Check for tie
        if all(self.board[0][col] != " " for col in range(self.COLS)):
            return "Tie"
        
        return None
    
    def print_board(self):
        """Print the current state of the board"""
        print("\n")
        # Print column numbers
        print(" ", end=" ")
        for col in range(self.COLS):
            print(f" {col}  ", end="")
        print("\n")
        
        # Print board
        for row in self.board:
            print("|", end="")
            for cell in row:
                if cell == "X":
                    print(f" {self.BLUE}X{self.RESET} |", end="")
                elif cell == "O":
                    print(f" {self.RED}O{self.RESET} |", end="")
                else:
                    print("   |", end="")
            print("\n" + "-" * (self.COLS * 4 + 1))

class ConnectFourGUI:
    def __init__(self, master):
        self.game = ConnectFour()
        self.master = master
        self.master.title("Connect Four")
        
        # Configure window
        self.master.configure(bg='#2C3E50')
        self.master.resizable(False, False)
        
        # Create main frame
        self.main_frame = tk.Frame(master, bg='#2C3E50', padx=20, pady=20)
        self.main_frame.pack(expand=True)
        
        # Add title
        self.title_label = tk.Label(
            self.main_frame,
            text="Connect Four",
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
            bg='#34495E',
            padx=10,
            pady=10
        )
        self.board_frame.pack()
        
        # Create buttons for column selection
        self.col_buttons = []
        for col in range(self.game.COLS):
            button = tk.Button(
                self.board_frame,
                text="↓",
                font=('Arial', 16),
                width=3,
                command=lambda x=col: self.button_click(x)
            )
            button.grid(row=0, column=col, padx=2)
            self.col_buttons.append(button)
        
        # Create cells
        self.cells = []
        for row in range(self.game.ROWS):
            row_cells = []
            for col in range(self.game.COLS):
                cell = tk.Label(
                    self.board_frame,
                    width=3,
                    height=1,
                    relief="sunken",
                    bg='white',
                    font=('Arial', 24, 'bold')
                )
                cell.grid(row=row+1, column=col, padx=2, pady=2)
                row_cells.append(cell)
            self.cells.append(row_cells)
    
    def button_click(self, col):
        row = self.game.make_move(col)
        if row != -1:
            # Update cell
            color = "#3498DB" if self.game.board[row][col] == "X" else "#E74C3C"
            self.cells[row][col].config(
                text="●",
                fg=color
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
                
                if messagebox.askyesno("Play Again?", "Would you like to play another game?"):
                    self.reset_game()
                else:
                    self.master.quit()
    
    def reset_game(self):
        self.game = ConnectFour()
        for row in self.cells:
            for cell in row:
                cell.config(text="", bg='white')
        self.turn_label.config(text="Player X's turn")

def play_console():
    game = ConnectFour()
    
    while True:
        game.print_board()
        print(f"Player {game.current_player}'s turn")
        
        try:
            col = int(input(f"Enter column (0-{game.COLS-1}): "))
            
            if 0 <= col < game.COLS:
                if game.make_move(col) != -1:
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
                    print("That column is full!")
            else:
                print(f"Invalid input! Column must be between 0 and {game.COLS-1}")
        except ValueError:
            print("Please enter a valid number!")

def play_gui():
    root = tk.Tk()
    app = ConnectFourGUI(root)
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
