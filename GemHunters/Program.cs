
using System;

namespace GemHunters
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class Player
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public int GemCount { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
            GemCount = 0;
        }
        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Position.X--;
                    break;

                case 'D':
                    Position.X++;
                    break;

                case 'L':
                    Position.Y--;
                    break;

                case 'R':
                    Position.Y++;
                    break;

                default:
                    Console.WriteLine("Invalid Move");
                    break;

            }
        }

    }
    class Cell
    {
        public string Occupant { get; set; }
        public Cell(string occupant="-")
        {
            Occupant = occupant;
        }
        
    }
    class Board
    {
        private Cell[,] grid;
        private Random random = new Random();
        public Board()
        {
            grid = new Cell[6, 6];
            InitializeBoard();
        }
        private void InitializeBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
            PlaceObjects('G', 11);
            PlaceObjects('O', 5);
        }
        private void PlaceObjects(char item, int count)
        {
            int placed = 0;
            while (placed < count)
            {
                int x = random.Next(6);
                int y = random.Next(6);
                if (grid[x, y].Occupant == "-")
                {
                    grid[x, y].Occupant = item.ToString();
                    placed++;
                }

            }
        }
        public void Display(Player player1, Player player2)
        {
            Console.Clear();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i == player1.Position.X && j == player1.Position.Y)
                    {
                        Console.Write("P1 ");
                    }
                    else if (i == player2.Position.X && j == player2.Position.Y)
                    {
                        Console.Write("P2 ");
                    }
                    else
                    {
                        Console.Write(grid[i, j].Occupant + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n");
            Console.WriteLine(player1.Name + " Gems: " + player1.GemCount);
            Console.WriteLine(player2.Name + " Gems: " + player2.GemCount);
        }
        public bool IsValidMove(Player player, char direction)
        {
            int newX = player.Position.X;
            int newY = player.Position.Y;

            switch (direction)
            {
                case 'U':
                    newX--;
                    break;

                case 'D':
                    newX++;
                    break;

                case 'L':
                    newY--;
                    break;

                case 'R':
                    newY++;
                    break;

                default:
                    return false;
            }
            return newX >= 0 && newX < 6 && newY >= 0 && newY < 6 && grid[newX, newY].Occupant != "O";
        }
        public void CollectGem(Player player)
        {
            if (grid[player.Position.X, player.Position.Y].Occupant == "G")
            {
                player.GemCount++;
                grid[player.Position.X, player.Position.Y].Occupant = "-";
            }
        }
    }      
    class Game
    {
        public Board Board { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        private Player CurrentTurn;
        private int TotalTurns = 30;
        private int Turnstaken = 0;

        public Game()
        {
            Board = new Board();
            Player1 = new Player("P1", new Position(0, 0));
            Player2 = new Player("P2", new Position(5, 5));
            CurrentTurn = Player1;
        }
                    
        public void Start()
        {
            while (!IsGameOver())
            {               
                Console.WriteLine("\n");
                Board.Display(Player1,Player2);
                Console.WriteLine("\nCurrent Player : " + " " + CurrentTurn.Name);
                Console.WriteLine("\n");
                Console.Write("Choose the direction (U/D/L/R): ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (Board.IsValidMove(CurrentTurn, move))
                {
                    CurrentTurn.Move(move);
                    Board.CollectGem(CurrentTurn);
                    Turnstaken++;
                    SwitchTurn();
                }

                else
                {
                    Console.WriteLine("Invalid move! Try again");
                }              

            }
            Board.Display(Player1 ,Player2);
            AnnounceWinner();

        }
        private void SwitchTurn()
        {
            CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
        }
        private bool IsGameOver()
        {
            return Turnstaken >= TotalTurns;
        }
        private void AnnounceWinner()
        {
           
            if (Player1.GemCount > Player2.GemCount)
            {
                Console.WriteLine("\nGame Over!"+ " " + Player1.Name + " " + "wins with" + " " + Player1.GemCount + " " + "gems.");
            }
            else if (Player2.GemCount > Player1.GemCount)
            {
                Console.WriteLine("\nGame Over!" + " " + Player2.Name + " " + "wins with" + " " + Player2.GemCount + " " + "gems.");
            }
            else
            {
                Console.WriteLine("\nIt's a tie!!");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}

