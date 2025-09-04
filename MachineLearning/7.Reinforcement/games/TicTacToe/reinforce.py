## Reinforcement Learning for Tic Tac Toe 
## Her ser I hvordan vi kan bruge en "AI" til at spille Tic Tac Toe 
## Nu har I til opgave at lave en "AI" som kan spille Tic Tac Toe bedre end mit program

import random
from main import TicTacToe

class RandomAI:
    def __init__(self, player_symbol):
        """Initialize AI with X or O as player symbol"""
        self.symbol = player_symbol
    
    def make_move(self, game):
        """Make a random valid move on the game board"""
        # Get all available positions
        available_moves = [
            pos for pos in range(9) 
            if game.board[pos] not in ["X", "O"]
        ]
        
        # Return random choice from available moves
        return random.choice(available_moves)

def play_against_ai():
    game = TicTacToe()
    ai = RandomAI("O")  # AI plays as O
    
    while True:
        game.print_board()
        
        # Human turn (X)
        if game.current_player == "X":
            print("Your turn (X)")
            try:
                position = int(input("Enter position (0-8): "))
                if not (0 <= position <= 8):
                    print("Invalid input! Position must be between 0 and 8")
                    continue
                if not game.make_move(position):
                    print("That position is already taken!")
                    continue
            except ValueError:
                print("Please enter a valid number!")
                continue
        
        # AI turn (O)
        else:
            print("AI's turn (O)")
            position = ai.make_move(game)
            game.make_move(position)
        
        # Check for winner
        winner = game.check_winner()
        if winner:
            game.print_board()
            if winner == "Tie":
                print("Game ended in a tie!")
            else:
                color = game.BLUE if winner == "X" else game.RED
                print(f"Player {color}{winner}{game.RESET} wins!")
            break

if __name__ == "__main__":
    play_against_ai()
