namespace SharpesGold.Logic;

using System.Threading;

using SharpesGold.Neighbours;

/// <summary>
/// Class <c>SharpesLogic</c> models the high-level flow of Dijkstra's
/// algorithm to find the shortest path to Sharpe's gold.
/// </summary>
public class SharpesLogic
{
    //
    // For UI callbacks to indicate feedback
    //

    private const int POTENTIAL_INSERT = 0;      
    private const int POTENTIAL_REMOVE = 1;
    private const int POTENTIAL_TARGET = 2;

    private Action<int, int, int> UpdatePotential = (row, col, potential) => {};

    //
    // Algorithm working variables
    //

    private bool Complete;
    private int[] Distances;
    private int[] Indexes;
    private int Size;
    private int Stop;

    //
    // Helper component to request potential neighbours
    //
    
    private SharpesNeighbours Neighbours;

    /// <summary>
    /// Constructor for <c>SharpesLogic</c> requires a grid size and helper to
    /// request potential neighbours.
    /// </summary>
    public SharpesLogic(int size, SharpesNeighbours neighbours)
    {
        this.Complete = false;
        this.Distances = Enumerable.Repeat(size*size, size*size).ToArray();
        this.Indexes = Enumerable.Repeat(-1, size*size).ToArray();
        this.Neighbours = neighbours;
        this.Size = size;   
        this.Stop = ((size * size) - 1);
    }

    private int ToCol(int index)
    {
        return index % this.Size;
    }

    private int ToRow(int index)
    {
        return (int)Math.Floor((decimal)(index / this.Size));
    }

    /// <summary>
    /// Method <c>Start</c> initiates algorithm loop and requires a UI callback
    /// to indicate feedback.
    /// </summary>
    public void Start(Action<int, int, int> updatePotential)
    {
        this.UpdatePotential = updatePotential;
        VisitPotentials(new int[] {0}, 1);

        if (this.Complete)
        {
            int previous = this.Indexes[this.Stop];
            while (previous != -1)
            {
                this.UpdatePotential(ToRow(previous), ToCol(previous), POTENTIAL_TARGET);
                previous = this.Indexes[previous];
            }
        }
    }

    // ------------------------------------------------------------
    // Step to mark applicable neigbors with smaller distances.
    // Forms a core part of the algorithm with the following logic:
    // 1. Get valid neighbours of the current node index
    // 2. Update neighbours distance if smaller than currently set
    // 3. Mark neighbours as new potentials to display to user
    // 4. Mark current index as now visited (no longer potential)
    // ------------------------------------------------------------
    private int[] VisitNode(int currentIndex, int distance)
    {
        int[] neighbors = this.Neighbours.Calculate(currentIndex);
        List<int> potentials = new List<int>();

        foreach (int index in neighbors) {
            if (distance < this.Distances[index]) {
                this.Distances[index] = distance;
                this.Indexes[index] = currentIndex;

                if (index == this.Stop) {
                    this.Complete = true;
                } else {
                    this.UpdatePotential(ToRow(index), ToCol(index), POTENTIAL_INSERT);
                    potentials.Add(index);
                }
            }
        }

        this.Neighbours.Visited(currentIndex);
        this.UpdatePotential(ToRow(currentIndex), ToCol(currentIndex), POTENTIAL_REMOVE);
        return potentials.ToArray();
    }

    private void VisitPotentials(int[] potentials, int distance)
    {
        List<int> neighbors = new List<int>();
        foreach (int potential in potentials)
        {
            neighbors.AddRange(VisitNode(potential, distance));
        }

        if (neighbors.Count > 0) {
            Thread.Sleep(100); // Slow it down for the human eye 
            VisitPotentials(neighbors.ToArray(), distance + 1);
        }
    }
}
