public class Game
{
    private static Game _instance;
    private static readonly object _lock = new object();

    public World World { get; private set; }

    private Game() { }

    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Game();
                    }
                }
            }
            return _instance;
        }
    }

    public void InitializeWorld(int width, int height)
    {
        World = new World(width, height);
    }
}

public record Cell
{
    public string Name { get; init; }
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
    public Cell[,] Grid { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    /// <summary>
    /// Initializes a new instance of the World class with a grid of specified width and height.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when width or height is not a positive integer.</exception>
    public World(int width, int height)
    {
        if (width <= 0 || height <= 0 || width == int.MaxValue || height == int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(width <= 0 ? nameof(width) : nameof(height), "Width and height must be positive integers less than Int32.MaxValue.");
        }

        Width = width;
        Height = height;
        Grid = new Cell[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Grid[y, x] = new Cell("Empty", x, y);
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
            throw new ArgumentOutOfRangeException(x < 0 || x >= Width ? nameof(x) : nameof(y), "Coordinates are out of bounds.");
        }
        return Grid[y, x];
    }

    /// <summary>
    /// Sets the name of the cell at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="name">The name to assign to the cell.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the coordinates are out of bounds.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the name is null.</exception>
    public void SetCellName(int x, int y, string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name), "Cell name cannot be null.");
        }
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(x < 0 || x >= Width ? nameof(x) : nameof(y), "Coordinates are out of bounds.");
        }
        Grid[y, x] = Grid[y, x] with { Name = name };
    }
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