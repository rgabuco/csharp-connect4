using System;

namespace Connect4
{
    // Class for debug flags and global variables (if needed)
    public static class Globals
    {
        public static bool Debug = false;
    }

    public abstract class Player
    {
        public string Name { get; set; }
        public char Disc { get; set; }
        protected GameBoard game;

        // Constructor to initialize the player with game board, name, and disc
        public Player(GameBoard game, string name, char disc)
        {
            Name = name;
            Disc = disc;
            this.game = game;
        }

        // Updates the game board reference for the player to the new provided game board
        public void UpdateGameBoard(GameBoard newGameBoard)
        {
            this.game = newGameBoard;
        }

        // Abstract method for player's move, to be implemented by derived classes
        public abstract int PlayerMove();
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(GameBoard game, string name, char disc) : base(game, name, disc) { }

        // Implementation of the PlayerMove method for a human player
        public override int PlayerMove()
        {
            int dropChoice;
            Console.WriteLine($"{Name}'s Turn");
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7, or type 'quit' to exit: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    Environment.Exit(0); // Exit the program
                }
                dropChoice = Convert.ToInt32(Console.ReadLine());
                if (dropChoice < 1 || dropChoice > 7)
                {
                    Console.WriteLine("Invalid column #, please try again.");
                    continue;
                }
                if (!game.IsValidDrop(dropChoice))
                {
                    Console.WriteLine("Column is full, please try again.");
                    continue;
                }
            } while (dropChoice < 1 || dropChoice > 7 || !game.IsValidDrop(dropChoice));

            return dropChoice;
        }
    }

    public class EasyComputerPlayer : Player
    {
        public EasyComputerPlayer(GameBoard game, string name, char disc) : base(game, name, disc) { }

        // Implementation of the PlayerMove method for an easy computer player
        public override int PlayerMove()
        {
            // Just a simple AI that randomly chooses a column
            Console.WriteLine($"{Name}'s Turn");
            Random random = new Random();
            int dropChoice;
            do
            {
                dropChoice = random.Next(1, 8);
            } while (!game.IsValidDrop(dropChoice));
            Console.WriteLine("Dropping disc in column " + dropChoice);
            return dropChoice;
        }
    }

    public class GameBoard
    {
        private char[,] board;
        private const int Rows = 6;
        private const int Columns = 7;

        // Constructor to initialize the game board
        public GameBoard()
        {
            board = new char[Rows + 1, Columns + 1];
            InitializeBoard();
        }

        // Initialize the board 2D array
        private void InitializeBoard()
        {
            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Columns; j++)
                {
                    board[i, j] = '*';
                }
            }
        }

        // Method to print the board game in the console
        public void DisplayBoard()
        {
            // Print column numbers
            Console.Write(" ");
            for (int j = 1; j <= Columns; j++)
            {
                Console.Write(" " + j + "  ");
            }
            Console.Write("\n");

            // Print board
            for (int i = 1; i <= Rows; i++)
            {
                Console.Write("|");
                for (int j = 1; j <= Columns; j++)
                {
                    if (board[i, j] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (board[i, j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write(" " + board[i, j] + " ");
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.Write("\n");

                // Print separator line
                Console.Write(" ");
                for (int j = 1; j <= Columns; j++)
                {
                    Console.Write("--- ");
                }
                Console.Write("\n");
            }
        }

        // Method to check if the board is full
        public bool IsFull()
        {
            // Iterate through each column in the first row (excluding the 0th index column)
            for (int i = 1; i <= Columns; i++)
            {
                // Check if the current cell is empty (denoted by '*')
                if (board[1, i] == '*')
                {
                    if (Globals.Debug) { Console.WriteLine("Column " + i + " has empty cell"); }
                    return false; // Return false if an empty cell is found
                }
            }
            return true; // Return true if no empty cells are found in the first row
        }

        // Drops a player's disc into the specified column of the game board
        public bool DropDisc(int column, char playerDisc)
        {
            for (int i = Rows; i >= 1; i--)
            {
                if (board[i, column] == '*')
                {
                    board[i, column] = playerDisc;
                    if (Globals.Debug) { Console.WriteLine("Disc " + playerDisc + " dropped in column " + column); }
                    return true;
                }
            }
            return false;
        }

        // Method to check if a player has won
        public bool CheckWin(char playerDisc)
        {
            // Iterate through each row of the board
            for (int i = 1; i <= Rows; i++)
            {
                // Iterate through each column of the board
                for (int j = 1; j <= Columns; j++)
                {
                    // Check if the current position contains the player's disc
                    if (board[i, j] == playerDisc)
                    {
                        // Check for a winning sequence in four possible directions
                        if (CheckDirection(i, j, playerDisc, -1, 1) || // Diagonal from bottom-left to top-right
                           CheckDirection(i, j, playerDisc, 1, 1) ||   // Diagonal from top-left to bottom-right
                           CheckDirection(i, j, playerDisc, 0, 1) ||   // Horizontal from left to right
                           CheckDirection(i, j, playerDisc, 1, 0))     // Vertical from top to bottom
                        {
                            return true; // Return true if any direction has a winning sequence
                        }
                    }
                }
            }
            return false; // Return false if no winning sequence is found
        }

        // Added this method to check if a player has won based on the specified check direction
        private bool CheckDirection(int row, int col, char playerDisc, int rowDir, int colDir)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                int r = row + i * rowDir;
                int c = col + i * colDir;
                if (r > 0 && r <= Rows && c > 0 && c <= Columns && board[r, c] == playerDisc)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count == 4;
        }

        // Method to check if the top cell of the column is empty ('*')
        public bool IsValidDrop(int column)
        {
            return board[1, column] == '*';
        }
    }

    public class Connect4Game
    {
        private Player playerOne;
        private Player playerTwo;
        private GameBoard board;

        // Constructor to initialize the game board
        public Connect4Game()
        {
            board = new GameBoard();
        }

        // Method to initialize players
        public void InitializePlayers()
        {
            Console.WriteLine("Let's Play Connect 4!");
            Console.WriteLine("Player One please enter your name: ");
            playerOne = new HumanPlayer(board, Console.ReadLine(), 'X');
            Console.WriteLine("Do you want to play with an easy computer opponent? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                playerTwo = new EasyComputerPlayer(board, "Computer", 'O');
            }
            else
            {
                // Player Two is a human player
                Console.WriteLine("Player Two please enter your name: ");
                playerTwo = new HumanPlayer(board, Console.ReadLine(), 'O');
            }
        }

        // Method to start and manage the game
        public void PlayGame()
        {
            bool gameOn = true;
            Player currentPlayer = playerOne;

            if (Globals.Debug) { Console.WriteLine("Starting Game..."); }
            while (gameOn)
            {
                board.DisplayBoard();
                int dropChoice = currentPlayer.PlayerMove();
                if (Globals.Debug) { Console.WriteLine($"Player {currentPlayer.Name} dropping {currentPlayer.Disc} on {dropChoice}"); }
                board.DropDisc(dropChoice, currentPlayer.Disc);

                // Check if the current player has won
                if (board.CheckWin(currentPlayer.Disc))
                {
                    board.DisplayBoard();
                    Console.WriteLine($"{currentPlayer.Name} Connected Four, You Win!");
                    gameOn = RestartGame();
                }
                // Check if the board is full
                else if (board.IsFull())
                {
                    board.DisplayBoard();
                    Console.WriteLine("The board is full, it is a draw!");
                    gameOn = RestartGame();
                }
                else
                {
                    // Switch to the other player
                    currentPlayer = currentPlayer == playerOne ? playerTwo : playerOne;
                }
            }
        }

        // Method to handle game restart logic
        private bool RestartGame()
        {
            Console.WriteLine("Would you like to restart? Yes(1) No(2): ");
            int restart = Convert.ToInt32(Console.ReadLine());
            // TODO: Improve the validation logic later, let's keep it simple for now
            if (restart == 1)
            {
                if (Globals.Debug) { Console.WriteLine("Restarting Game..."); }
                board = new GameBoard();
                playerOne.UpdateGameBoard(board); // Update playerOne's GameBoard reference
                playerTwo.UpdateGameBoard(board); // Update playerTwo's GameBoard reference
                return true;
            }
            else
            {
                Console.WriteLine("Goodbye!");
                return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Connect4Game game = new Connect4Game();
            game.InitializePlayers();
            game.PlayGame();
        }
    }
}
