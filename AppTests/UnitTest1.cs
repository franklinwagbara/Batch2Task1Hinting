// GameTests.cs
using NUnit.Framework;
using System;

[TestFixture]
public class GameTests
{
    [Test]
    public void GameInstance_IsSingleton()
    {
        var instance1 = Game.Instance;
        var instance2 = Game.Instance;
        Assert.That(instance1, Is.SameAs(instance2));
    }

    [Test]
    public void InitializeWorld_ValidDimensions_WorldIsInitialized()
    {
        Game.Instance.InitializeWorld(5, 10);
        Assert.That(Game.Instance.World, Is.Not.Null);
        Assert.That(Game.Instance.World.Width, Is.EqualTo(5));
        Assert.That(Game.Instance.World.Height, Is.EqualTo(10));
    }

    [Test]
    public void InitializeWorld_InvalidWidth_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Game.Instance.InitializeWorld(0, 10));
        Assert.Throws<ArgumentOutOfRangeException>(() => Game.Instance.InitializeWorld(-5, 10));
    }

    [Test]
    public void InitializeWorld_InvalidHeight_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Game.Instance.InitializeWorld(5, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Game.Instance.InitializeWorld(5, -10));
    }
}

[TestFixture]
public class WorldTests
{
    [Test]
    public void WorldConstructor_ValidDimensions_GridIsInitializedWithEmptyCells()
    {
        int width = 3;
        int height = 4;
        var world = new World(width, height);
        Assert.That(world.Width, Is.EqualTo(width));
        Assert.That(world.Height, Is.EqualTo(height));
        Assert.That(world.Grid.GetLength(0), Is.EqualTo(width));
        Assert.That(world.Grid.GetLength(1), Is.EqualTo(height));

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Assert.That(world.Grid[x, y], Is.Not.Null);
                Assert.That(world.Grid[x, y].Name, Is.EqualTo("Empty"));
                Assert.That(world.Grid[x, y].X, Is.EqualTo(x));
                Assert.That(world.Grid[x, y].Y, Is.EqualTo(y));
            }
        }
    }

    [Test]
    public void WorldConstructor_InvalidWidth_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(0, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(-2, 5));
    }

    [Test]
    public void WorldConstructor_InvalidHeight_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(5, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(5, -3));
    }

    [Test]
    public void GetCell_ValidCoordinates_ReturnsCorrectCell()
    {
        int width = 2;
        int height = 2;
        var world = new World(width, height);
        var cell = world.GetCell(1, 0);
        Assert.That(cell, Is.Not.Null);
        Assert.That(cell.X, Is.EqualTo(1));
        Assert.That(cell.Y, Is.EqualTo(0));
    }

    [Test]
    public void GetCell_OutOfBoundsCoordinates_ThrowsArgumentOutOfRangeException()
    {
        var world = new World(2, 2);
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(2, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(0, 2));
    }

    [Test]
    public void SetCellName_ValidCoordinates_SetsCellName()
    {
        int width = 3;
        int height = 3;
        var world = new World(width, height);
        string newName = "Forest";
        world.SetCellName(1, 1, newName);
        Assert.That(world.Grid[1, 1].Name, Is.EqualTo(newName));
    }

    [Test]
    public void SetCellName_OutOfBoundsCoordinates_ThrowsArgumentOutOfRangeException()
    {
        var world = new World(2, 2);
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(-1, 0, "Test"));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(2, 0, "Test"));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(0, -1, "Test"));
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(0, 2, "Test"));
    }

    [Test]
    public void World_LargeGrid_InitializesCorrectly()
    {
        int width = 100;
        int height = 100;
        var world = new World(width, height);
        Assert.That(world.Width, Is.EqualTo(width));
        Assert.That(world.Height, Is.EqualTo(height));
        Assert.That(world.Grid.GetLength(0), Is.EqualTo(width));
        Assert.That(world.Grid.GetLength(1), Is.EqualTo(height));
    }
}

[TestFixture]
public class CellTests
{
    [Test]
    public void CellConstructor_SetsPropertiesCorrectly()
    {
        string name = "Mountain";
        int x = 5;
        int y = 10;
        var cell = new Cell(name, x, y);
        Assert.That(cell.Name, Is.EqualTo(name));
        Assert.That(cell.X, Is.EqualTo(x));
        Assert.That(cell.Y, Is.EqualTo(y));
    }

    [Test]
    public void CellProperties_CanBeModified()
    {
        var cell = new Cell("Initial", 0, 0);
        cell.Name = "Updated";
        Assert.That(cell.Name, Is.EqualTo("Updated"));
        // X and Y are init-only, so they cannot be modified after initialization.
    }
}