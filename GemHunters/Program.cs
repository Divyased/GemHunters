
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

    }
}

