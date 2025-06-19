using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace connect4 //Ignore commented out code please.
{
    //==========================Player Object==============================================
    public class Player
    {
        private string Name { get; set; }
        public int Wins;
        public char Token { get; set; } //assigned X or O in the game creation method.

        public Player(string name)
        {
            Name = name;
            Wins = 0;
        }
        /*public void CreatePlayers()
        {
            Console.Write("Player 1 Choose your name: ");
            string player1Name = Console.ReadLine();
            Player player1 = new Player(player1Name);

            Console.Write("Player 2 Choose your name: ");
            string player2Name = Console.ReadLine();
            Player player2 = new Player(player2Name);
            Console.Write($"your names are {player1.Name} and {player2.Name}\n");
        }*/

    }
//================================Board Object================================================
    public class Board
    {
        private int Height = 7;
        private int Width = 8;
        public string[,] Array = new string[8,7];
        public Board()
        {
            Array = new string[Width + 1, Height + 1];
        }
//===============================Board Creation method==========================================
        public Board CreateBoard()
        {


            for (int i = 0; i < Height; i++) //This assigns all the squares in the array to '#' meaning empty
            {
                for (int j = 0; j < Width; j++)
                {
                    Array[i, j] = "#";
                }
            }

            /*for (int i = 0; i < Height; i++) //Prints the board with the current values in the array                                 | DEBUG|
            {
                Console.Write("|   "); //When the line starts, prints the board border
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(Array[i, j] + " ");//Adds a space between each cell
                }
                Console.Write("   |"); //When the line ends, print the board border
                Console.WriteLine(" ");
            }*/

            Console.Write("|   ");
            for(int i = 0; i < Width; i++)
            {
                Console.Write(i + " "); //Prints the column number underneath the board
            }
            Console.WriteLine("   |");
            return this; //
        }
        //======Board print method=========================
        public void PrintBoard() //This method reprints the board after every move. board.PrintBoard should be invoked after EVERYMOVE
        {
            for(int i = 0;i < Height; i++)
            {
                Console.WriteLine("|   ");
                for(int j = 0; j < Width; j++)
                {
                    Console.WriteLine(Array[i, j] + " ");
                }
                Console.WriteLine("   |");
            }
        }

        /*public void Turn()
        {
            if(PlayerTurn == true)
            {
                PlayerCard = 'X';
                Console.WriteLine("Select a column");

                PlayerTurn = !PlayerTurn;
            }
            else
            {
                PlayerCard = 'O';
                Console.WriteLine("Select a column");
                PlayerTurn = !PlayerTurn;
            }
        }*/
    }
//============================================Game Creation and Gameplay============================================================
    public class CreateGame
    {
        private Board Arena;
        private Player Player1;
        private Player Player2;
        public CreateGame(Board board, Player player1, Player player2)
        {
            Arena = board;
            Player1 = player1;
            Player2 = player2;
        }

        public void GameStart() //Still trying to figure out how we want to do this part. We could pass the board to a turn object to make the moves and then send it back to a method in here?
        {
            List<Player> playerList = new List<Player>();
            playerList.Add(Player1);
            playerList.Add(Player2);

            playerList[0].Token = 'X';
            playerList[1].Token = 'O';
            Arena.PrintBoard();
        }

        public void GamePlay()
        {

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to connect 4\nPlease input 2 for 2 player mode, please input 1 to face off against an AI\nAI not implemented yet");
            //board.CreateBoard(); //DEBUG
            string command = Console.ReadLine();

            while(command != "exit" || command != "Exit")
            {
                if(command == "1")
                {
                    //List<Player> list = new List<Player>(); //DEBUG
                    Board board = new Board();

                    Console.WriteLine("Please choose player 1's name");
                    string player1 = Console.ReadLine();
                    Player p1 = new Player(player1);

                    Console.WriteLine("Please choose player 2's name");
                    string player2 = Console.ReadLine();
                    Player p2 = new Player(player2);

                    CreateGame game = new CreateGame(board, p1, p2);

                }
                command = Console.ReadLine();
            }
            
        }

    }
}
