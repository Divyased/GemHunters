
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

