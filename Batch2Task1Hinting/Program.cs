// Game.cs
using System;

public sealed class Game
{
    private static Game _instance;
    private static readonly object _lock = new();

    public World World { get; private set; }

    private Game() { }

    public static Game Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Game();
                }
                return _instance;
            }
        }
    }

    public void InitializeWorld(int width, int height)
    {
        try
        {
            World = new World(width, height);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error initializing world: {ex.Message}");
            throw;
        }
    }
}

public class Cell
{
    public string Name { get; set; }
    public int X { get; init; }
    public int Y { get; init; }

    public Cell(string name, int x, int y)
    {
        Name = name;
        X = x;
        Y = y;
    }
}

public class World
{
    public Cell[,] Grid { get; }
    public int Width { get; }
    public int Height { get; }

    /// <summary>
    /// Initializes a new instance of the World class with a grid of specified width and height.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    public World(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero.");
        }
        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero.");
        }

        Width = width;
        Height = height;
        Grid = new Cell[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Grid[x, y] = new Cell("Empty", x, y);
            }
        }
    }

    /// <summary>
    /// Retrieves the cell at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <returns>The Cell at the specified (x, y) coordinates.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the coordinates are out of bounds.</exception>
    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Coordinates are out of bounds.");
        }
        return Grid[x, y];
    }

    /// <summary>
    /// Sets the name of the cell at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="name">The name to assign to the cell.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the coordinates are out of bounds.</exception>
    public void SetCellName(int x, int y, string name)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Coordinates are out of bounds.");
        }
        Grid[x, y].Name = name;
    }

    public class Program
    {
        public static void Main()
        {
            var game = Game.Instance;
            game.InitializeWorld(10, 5);
            var world = game.World;
            Console.WriteLine($"World dimensions: {world.Width} x {world.Height}");
        }
    }
}