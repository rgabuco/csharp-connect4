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
    public class Connect4Game
    {
        private Player playerOne;
        private Player playerTwo;
        //TODO: We can define another GameBoard class to keep track of game state

        public Connect4Game()
        {
            //TODO: We can initialize the GameBoard here
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
        }

        private bool RestartGame()
        {
            //TODO: Define logic for restarting game

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Connect4Game game = new Connect4Game();
            game.InitializePlayers();
        }
    }
}
