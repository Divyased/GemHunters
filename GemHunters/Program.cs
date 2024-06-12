
using System;

class Position
{
    public int X {  get; set; }
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
            Position.Y--;
            break;

            case 'D':
            Position.Y++;
            break;

            case 'L':
            Position.X--;
            break;

            case 'R':
            Position.X++;
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
    public Cell(string occupant)
    {
        Occupant = occupant;
    }
}
class Board
{
    private Cell[,] grid;
    public Board()
    {
        grid= new Cell[6,6];
        InitializeBoard();
    }
        private void InitializeBoard()
    {
        for (int i = 0; i<6; i++) 
        { 
            for(int j = 0; j<6; j++)
            {
                grid[i, j] = new Cell("-");
            }
        }
        grid[0, 0].Occupant = "P1";
        grid[5, 5].Occupant = "P2";
        InitializingObjects("O", 5);
        InitializingObjects("G", 15);
    }
    private void InitializingObjects(string obj, int count)
    {
        Random random = new Random();
        int gemcnt= 0;
        while(gemcnt<count)
        {
            int x= random.Next(6);
            int y= random.Next(6);
            if (grid[y, x].Occupant == "-")
            {
                grid[y,x].Occupant = obj;
                gemcnt++;
            }

        }
    }
    public void Display()
    {
        for(int i = 0; i<6;i++)
        {
            for(int j=0; j<6; j++)
            {
                Console.Write(grid[i,j].Occupant + " ");
            }
            Console.WriteLine();
        }
    }
    public bool IsValidMove(Player player, char direction)
    {
        int x= player.Position.X;
        int y= player.Position.Y;

        switch (direction)
        {
            case 'U':
            y--;
            break;

            case 'D':
            y++; 
            break;

            case 'L':
            x--;
            break;

            case 'R':
            x++;
            break;

            default:
            break;
        }
        if (x < 0 || x >= 6 || y < 0 || y >= 6)

            return false;

        return grid[y, x].Occupant != "O";  
    }
    public bool CollectGem(Player player)
    {
        if (grid[player.Position.Y, player.Position.X].Occupant == "G")
        {
            player.GemCount++;
            grid[player.Position.Y, player.Position.X].Occupant = "-";
            return true;
        }
        return false;
    }
    public void UpdatePlayer(Player player, Position newPosition)
    {
        grid[player.Position.Y, player.Position.X].Occupant = "-";
        grid[newPosition.Y, newPosition.X].Occupant = player.Name;
        player.Position = newPosition;
    }
}
class Game
{
    private Board Board;
    private Player Player1;
    private Player Player2;
    private Player CurrentTurn;
    private int TotalTurns;

    public Game()
    {
        Board = new Board();
        Player1= new Player("P1", new Position(0,0));
        Player2= new Player("P2", new Position(5,5));
        CurrentTurn = Player1;
        TotalTurns = 0;
    }
    public void Start()
    {
        while(!IsGameOver())
        {
            Console.WriteLine("Welcome to Gem Hunters! Let's get started");
            Console.WriteLine("\n Total Turns : " + " " + TotalTurns);
            Board.Display();
            Console.WriteLine("\n Current Player : " + " " + CurrentTurn.Name);
            Console.Write("\n Choose the direction (U/D/L/R): ");
            char move=Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (Board.IsValidMove(CurrentTurn, move))
            {
                Position newPosition = GetNewPosition(CurrentTurn.Position, move);
                Board.UpdatePlayer(CurrentTurn, newPosition);

                if (Board.CollectGem(CurrentTurn))
                {
                    Console.WriteLine(CurrentTurn.Name + " " + "collected a gem!");
                }
                SwitchTurn();
                TotalTurns++;
            }
            else
            {
                Console.WriteLine("Invalid move! Try again");
            }
            Console.WriteLine();

        }
        AnnounceWinner();

    }
    private Position GetNewPosition(Position currentPosition, char direction)
    {
        int x= currentPosition.X;
        int y= currentPosition.Y;

        switch (direction) 
        {

            case 'U':
                y--;
                break;

            case 'D':
                y++;
                break;


            case 'L':
                x--;
                break;


            case 'R':
                x++;
                break;

            default:
                break;
         
        }
        return new Position(x, y);

    }
    private void SwitchTurn()
    {
        CurrentTurn=(CurrentTurn==Player1) ? Player2 : Player1;
    }
    private bool IsGameOver()
    {
        return TotalTurns >= 30;
    }

    private void AnnounceWinner

}

