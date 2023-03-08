namespace SharpesGold.Neighbours.Tests;

public class NeighboursTests
{
    private SharpesNeighbours sharpesNeighbours = new SharpesNeighbours(5);

    [SetUp]
    public void Setup()
    {
        // 
        // Testing against a 5x5 grid
        // (0 - 24 neighbour indexes)
        // --------------
        //  0  1  2  3  4
        //  5  6  7  8  9
        // 10 11 12 13 14
        // 15 16 17 18 19
        // 20 21 22 23 24
        // --------------
        //

        this.sharpesNeighbours = new SharpesNeighbours(5);
    }
    
    [Test]
    public void UnobstructedTest()
    {
        int[] neighbours;
        
        // Top-left
        neighbours = this.sharpesNeighbours.Calculate(0);
        Assert.That(neighbours.Length, Is.EqualTo(3), "Count for top-left");
        Assert.That(neighbours[0], Is.EqualTo(1), "East of top-left");
        Assert.That(neighbours[1], Is.EqualTo(6), "South-east of top-left");
        Assert.That(neighbours[2], Is.EqualTo(5), "South of top-left");

        // Top-middle
        neighbours = this.sharpesNeighbours.Calculate(2);
        Assert.That(neighbours.Length, Is.EqualTo(5), "Count for top-middle");
        Assert.That(neighbours[0], Is.EqualTo(3), "East of top-middle");
        Assert.That(neighbours[1], Is.EqualTo(8), "South-east of top-middle");
        Assert.That(neighbours[2], Is.EqualTo(7), "South of top-middle");
        Assert.That(neighbours[3], Is.EqualTo(6), "South-west of top-middle");
        Assert.That(neighbours[4], Is.EqualTo(1), "West of top-middle");

        // Top-right
        neighbours = this.sharpesNeighbours.Calculate(4);
        Assert.That(neighbours.Length, Is.EqualTo(3), "Count for top-right");
        Assert.That(neighbours[0], Is.EqualTo(9), "South of top-right");
        Assert.That(neighbours[1], Is.EqualTo(8), "South-west of top-right");
        Assert.That(neighbours[2], Is.EqualTo(3), "West of top-right");

        // Middle
        neighbours = this.sharpesNeighbours.Calculate(12);
        Assert.That(neighbours.Length, Is.EqualTo(8), "Count for middle");
        Assert.That(neighbours[0], Is.EqualTo(7), "North of middle");
        Assert.That(neighbours[1], Is.EqualTo(8), "North-east of middle");
        Assert.That(neighbours[2], Is.EqualTo(13), "East of middle");
        Assert.That(neighbours[3], Is.EqualTo(18), "South-east of middle");
        Assert.That(neighbours[4], Is.EqualTo(17), "South of middle");
        Assert.That(neighbours[5], Is.EqualTo(16), "South-west of middle");
        Assert.That(neighbours[6], Is.EqualTo(11), "West of middle");
        Assert.That(neighbours[7], Is.EqualTo(6), "Nort-west of middle");

        // Bottom-left
        neighbours = this.sharpesNeighbours.Calculate(20);
        Assert.That(neighbours.Length, Is.EqualTo(3), "Count for bottom-left");
        Assert.That(neighbours[0], Is.EqualTo(15), "North of bottom-left");
        Assert.That(neighbours[1], Is.EqualTo(16), "North-east of bottom-left");
        Assert.That(neighbours[2], Is.EqualTo(21), "East of bottom-left");

        // Bottom-middle
        neighbours = this.sharpesNeighbours.Calculate(22);
        Assert.That(neighbours.Length, Is.EqualTo(5), "Count for bottom-middle");
        Assert.That(neighbours[0], Is.EqualTo(17), "North of bottom-middle");
        Assert.That(neighbours[1], Is.EqualTo(18), "North-east of bottom-middle");
        Assert.That(neighbours[2], Is.EqualTo(23), "East of bottom-middle");
        Assert.That(neighbours[3], Is.EqualTo(21), "West of bottom-middle");
        Assert.That(neighbours[4], Is.EqualTo(16), "North-west of bottom-middle");

        // Bottom-right
        neighbours = this.sharpesNeighbours.Calculate(24);
        Assert.That(neighbours.Length, Is.EqualTo(3), "Count for bottom-right");
        Assert.That(neighbours[0], Is.EqualTo(19), "North of bottom-right");
        Assert.That(neighbours[1], Is.EqualTo(23), "West of bottom-right");
        Assert.That(neighbours[2], Is.EqualTo(18), "North-west of bottom-right");
    }

    [Test]
    public void ObstructionsTest()
    {
        Assert.That(this.sharpesNeighbours.UnvisitedCount, Is.EqualTo(25));

        this.sharpesNeighbours.Obstructed(6);
        this.sharpesNeighbours.Visited(11);
        
        Assert.That(this.sharpesNeighbours.UnvisitedCount, Is.EqualTo(23));
        Assert.That(this.sharpesNeighbours.IsUnvisited(1), Is.EqualTo(true));
        Assert.That(this.sharpesNeighbours.IsUnvisited(6), Is.EqualTo(false));
        Assert.That(this.sharpesNeighbours.IsUnvisited(11), Is.EqualTo(false));
        Assert.That(this.sharpesNeighbours.IsUnvisited(16), Is.EqualTo(true));
    }
}