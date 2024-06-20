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
    //Todo: Add Human Player class

    //Todo: Add Computer Player class
    

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}