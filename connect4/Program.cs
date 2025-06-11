using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    public class Player
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }
    }

    public class Board
    {
        public int Height;
        public int Width;
        public string[,] Array;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Array = new string[Width + 1, Height + 1];
        }

        public void CreateBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Array[i, j] = "#";
                }
            }

            for (int i = 0; i < Height; i++)
            {
                Console.Write("|   ");
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(Array[i, j] + " ");
                }
                Console.Write("   |");
                Console.WriteLine(" ");
            }
        }

        public void Choice(int column)
        {

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string i;
            Board board = new Board(6, 7);
            CreatePlayers();
            board.CreateBoard();
            
        }

        static void CreatePlayers()
        {
            Console.Write("Player 1 Choose your name: ");
            string player1Name = Console.ReadLine();
            Player player1 = new Player(player1Name);

            Console.Write("Player 2 Choose your name: ");
            string player2Name = Console.ReadLine();
            Player player2 = new Player(player2Name);
            Console.Write($"your names are {player1.Name} and {player2.Name}\n");

        }
    }
}
