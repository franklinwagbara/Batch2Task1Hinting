// GameTests.cs
using NUnit.Framework;
using System;

[TestFixture]
public class GameTests
{
    [Test]
    public void Game_Instance_ReturnsSameInstance()
    {
        // Arrange & Act
        var instance1 = Game.Instance;
        var instance2 = Game.Instance;

        // Assert
        Assert.That(instance1, Is.SameAs(instance2));
    }

    [Test]
    public void InitializeWorld_WithValidDimensions_CreatesWorldInstance()
    {
        // Arrange
        int width = 5;
        int height = 10;
        var game = Game.Instance;

        // Act
        game.InitializeWorld(width, height);

        // Assert
        Assert.That(game.CurrentWorld, Is.Not.Null);
        Assert.That(game.CurrentWorld.Width, Is.EqualTo(width));
        Assert.That(game.CurrentWorld.Height, Is.EqualTo(height));
    }

    [Test]
    public void InitializeWorld_WithNonPositiveWidth_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 0;
        int height = 10;
        var game = Game.Instance;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => game.InitializeWorld(width, height));
    }

    [Test]
    public void InitializeWorld_WithNonPositiveHeight_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = -1;
        var game = Game.Instance;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => game.InitializeWorld(width, height));
    }
}

[TestFixture]
public class WorldTests
{
    [Test]
    public void World_Constructor_InitializesGridWithCorrectDimensions()
    {
        // Arrange
        int width = 8;
        int height = 12;

        // Act
        var world = new World(width, height);

        // Assert
        Assert.That(world.Width, Is.EqualTo(width));
        Assert.That(world.Height, Is.EqualTo(height));
        Assert.That(world.Grid.GetLength(0), Is.EqualTo(width));
        Assert.That(world.Grid.GetLength(1), Is.EqualTo(height));
    }

    [Test]
    public void World_Constructor_InitializesCellsWithDefaultNameAndCoordinates()
    {
        // Arrange
        int width = 3;
        int height = 2;

        // Act
        var world = new World(width, height);

        // Assert
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Assert.That(world.Grid[x, y].Name, Is.EqualTo("Empty"));
                Assert.That(world.Grid[x, y].X, Is.EqualTo(x));
                Assert.That(world.Grid[x, y].Y, Is.EqualTo(y));
            }
        }
    }

    [Test]
    public void World_Constructor_WithNonPositiveWidth_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 0;
        int height = 5;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(width, height));
    }

    [Test]
    public void World_Constructor_WithNonPositiveHeight_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = 0;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new World(width, height));
    }

    [Test]
    public void GetCell_WithValidCoordinates_ReturnsCorrectCell()
    {
        // Arrange
        int width = 5;
        int height = 5;
        var world = new World(width, height);

        // Act
        var cell = world.GetCell(2, 3);

        // Assert
        Assert.That(cell, Is.Not.Null);
        Assert.That(cell.X, Is.EqualTo(2));
        Assert.That(cell.Y, Is.EqualTo(3));
        Assert.That(cell.Name, Is.EqualTo("Empty"));
    }

    [Test]
    public void GetCell_WithNegativeXCoordinate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = 5;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(-1, 2));
    }

    [Test]
    public void GetCell_WithXCoordinateOutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = 5;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(5, 2));
    }

    [Test]
    public void GetCell_WithNegativeYCoordinate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = 5;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(2, -1));
    }

    [Test]
    public void GetCell_WithYCoordinateOutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 5;
        int height = 5;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.GetCell(2, 5));
    }

    [Test]
    public void SetCellName_WithValidCoordinatesAndName_SetsCellName()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);
        int x = 1;
        int y = 2;
        string newName = "Forest";

        // Act
        world.SetCellName(x, y, newName);

        // Assert
        Assert.That(world.Grid[x, y].Name, Is.EqualTo(newName));
    }

    [Test]
    public void SetCellName_WithNegativeXCoordinate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(-1, 1, "Test"));
    }

    [Test]
    public void SetCellName_WithXCoordinateOutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(3, 1, "Test"));
    }

    [Test]
    public void SetCellName_WithNegativeYCoordinate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(1, -1, "Test"));
    }

    [Test]
    public void SetCellName_WithYCoordinateOutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => world.SetCellName(1, 3, "Test"));
    }

    [Test]
    public void SetCellName_WithNullName_ThrowsArgumentNullException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => world.SetCellName(1, 1, null));
    }

    [Test]
    public void SetCellName_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => world.SetCellName(1, 1, string.Empty));
    }

    [Test]
    public void SetCellName_WithWhitespaceName_ThrowsArgumentException()
    {
        // Arrange
        int width = 3;
        int height = 3;
        var world = new World(width, height);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => world.SetCellName(1, 1, "   "));
    }
}