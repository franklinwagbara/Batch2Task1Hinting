using NUnit.Framework;
using System;

[TestFixture]
public class GameTests
{
    [Test]
    public void Game_Instance_ReturnsSameInstance()
    {
        Game instance1 = Game.Instance;
        Game instance2 = Game.Instance;
        Assert.That(instance1, Is.SameAs(instance2));
    }

    [Test]
    public void Game_InitializeWorld_CreatesWorldInstance()
    {
        Game game = Game.Instance;
        game.InitializeWorld(10, 5);
        Assert.That(game.World, Is.Not.Null);
        Assert.That(game.World.Width, Is.EqualTo(10));
        Assert.That(game.World.Height, Is.EqualTo(5));
    }
}

[TestFixture]
public class WorldTests
{
    [Test]
    public void World_Constructor_ValidDimensions_InitializesGrid()
    {
        int width = 5;
        int height = 3;
        World world = new World(width, height);
        Assert.That(world.Width, Is.EqualTo(width));
        Assert.That(world.Height, Is.EqualTo(height));
        Assert.That(world.Grid.GetLength(0), Is.EqualTo(height));
        Assert.That(world.Grid.GetLength(1), Is.EqualTo(width));
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Assert.That(world.Grid[y, x].Name, Is.EqualTo("Empty"));
                Assert.That(world.Grid[y, x].X, Is.EqualTo(x));
                Assert.That(world.Grid[y, x].Y, Is.EqualTo(y));
            }
        }
    }

    [TestCase(0, 5)]
    [TestCase(5, 0)]
    [TestCase(-1, 5)]
    [TestCase(5, -1)]
    [TestCase(int.MaxValue, 5)]
    [TestCase(5, int.MaxValue)]
    public void World_Constructor_InvalidDimensions_ThrowsArgumentOutOfRangeException(int width, int height)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(width, height));
    }

    [Test]
    public void World_GetCell_ValidCoordinates_ReturnsCorrectCell()
    {
        int width = 5;
        int height = 3;
        World world = new World(width, height);
        Cell cell = world.GetCell(2, 1);
        Assert.That(cell, Is.Not.Null);
        Assert.That(cell.X, Is.EqualTo(2));
        Assert.That(cell.Y, Is.EqualTo(1));
        Assert.That(cell.Name, Is.EqualTo("Empty"));
    }

    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(5, 0)]
    [TestCase(0, 3)]
    [TestCase(int.MinValue, 0)]
    [TestCase(0, int.MinValue)]
    [TestCase(int.MaxValue, 0)]
    [TestCase(0, int.MaxValue)]
    public void World_GetCell_InvalidCoordinates_ThrowsArgumentOutOfRangeException(int x, int y)
    {
        World world = new World(5, 3);
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(x, y));
    }

    [Test]
    public void World_SetCellName_ValidCoordinatesAndName_SetsCellName()
    {
        World world = new World(5, 3);
        world.SetCellName(1, 2, "Forest");
        Cell cell = world.GetCell(1, 2);
        Assert.That(cell.Name, Is.EqualTo("Forest"));
        Assert.That(cell.X, Is.EqualTo(1));
        Assert.That(cell.Y, Is.EqualTo(2));
    }

    [TestCase(-1, 0, "Mountain")]
    [TestCase(0, -1, "Mountain")]
    [TestCase(5, 0, "Mountain")]
    [TestCase(0, 3, "Mountain")]
    public void World_SetCellName_InvalidCoordinates_ThrowsArgumentOutOfRangeException(int x, int y, string name)
    {
        World world = new World(5, 3);
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(x, y, name));
    }

    [Test]
    public void World_SetCellName_NullName_ThrowsArgumentNullException()
    {
        World world = new World(5, 3);
        Assert.Throws<ArgumentNullException>(() => world.SetCellName(1, 1, null));
    }

    [Test]
    public void World_SetCellName_EmptyName_SetsCellName()
    {
        World world = new World(5, 3);
        world.SetCellName(2, 0, "");
        Cell cell = world.GetCell(2, 0);
        Assert.That(cell.Name, Is.EqualTo(""));
    }
}