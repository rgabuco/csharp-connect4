using System;

namespace Connect4
{
    public abstract class Player
    {
        public string Name { get; set; }
        public char Disc { get; set; }
        
        public Player( string name, char disc)
        {
            Name = name;
            Disc = disc;
        }

        public abstract int PlayerMove();
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, char disc) : base( name, disc) { }

        public override int PlayerMove()
        {
            int dropChoice;
            Console.WriteLine($"{Name}'s Turn");
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7: ");
                dropChoice = Convert.ToInt32(Console.ReadLine());
            
            } while (dropChoice < 1 || dropChoice > 7);

            return dropChoice;
        }
    }
    public class GameBoard
    {
        private char[,] board;
        private const int Rows = 6; 
        private const int Columns = 7;

        public GameBoard()
        {
            board = new char[Rows + 1, Columns + 1];
            InitializeBoard();
        }
        /*
        Initialize the board 2D array. 
        In each iteration, it assigns the character '*' to the corresponding cell in the board array.
        */
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
        /*
        Method to print the board game in the console.
        */
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

        public bool IsFull()
        {
            //TODO: Define logic for checking if board is full
            return false;
        }

        public bool DropDisc(int column, char playerDisc)
        {
            //TODO: Define logic for dropping disc
            return false;
        }

        public bool CheckWin(char playerDisc)
        {
            //TODO: Define logic for checking win
            return false;
        }
    }
    public class Connect4Game
    {
        private Player playerOne;
        private Player playerTwo;
        private GameBoard board;

        public Connect4Game()
        {
            board = new GameBoard();
        }

        public void InitializePlayers()
        {
            //All human players for now
            Console.WriteLine("Let's Play Connect 4!");
            Console.WriteLine("Player One please enter your name: ");
            playerOne = new HumanPlayer(Console.ReadLine(), 'X');
            Console.WriteLine("Player Two please enter your name: ");
            playerTwo = new HumanPlayer(Console.ReadLine(), 'O');
        }

        public void PlayGame()
        {
            //TODO: Define game logic
            board.DisplayBoard();
        }

        private bool RestartGame()
        {
            //TODO: Define logic for restarting game

            return true;
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
