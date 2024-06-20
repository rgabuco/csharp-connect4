using System;

namespace Connect4
{
    public abstract class Player
    {
        public string Name { get; set; }
        public char Disc { get; set; }

        public Player(string name, char disc)
        {
            Name = name;
            Disc = disc;
        }

        public abstract int PlayerMove();
    }
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, char disc) : base(name, disc) { }

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

    //Todo: Add Computer Player class


    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
