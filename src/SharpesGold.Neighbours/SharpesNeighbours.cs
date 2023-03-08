namespace SharpesGold.Neighbours;

/// <summary>
/// Class <c>SharpesNeighbours</c> models request of potential grid neighbours.
/// </summary>
public class SharpesNeighbours
{
    private int Size;
    private bool[] Unvisited;
    public int UnvisitedCount;

    public SharpesNeighbours(int size)
    {
        this.Size = size;
        this.Unvisited = Enumerable.Repeat(true, size*size).ToArray();
        this.UnvisitedCount = size*size;
    }
    
    // ------------------------------------------------------------
    // For the specified node index, return all valid neighbours.
    // In this case, valid means nodes that are unvisited, inside
    // bounds and not selected (i.e. blocked).
    // ------------------------------------------------------------
    public int[] Calculate(int index)
    {
        List<int> neighbors = new List<int>();
        
        // NORTH?
        if (index > this.Size) {
            int indexNorth = index - this.Size;
            if (IsUnvisited(indexNorth)) {
                neighbors.Add(indexNorth);
            }

            // NORTH-EAST?
            if ((index % this.Size) != (this.Size - 1)) {
                int indexNorthEast = indexNorth + 1;
                if (IsUnvisited(indexNorthEast)) {
                    neighbors.Add(indexNorthEast);
                }
            }
        }

        //
        // Complete me!
        // Uncomment code blocks & write missing code between <<< >>>
        //

        // EAST?
        // if (<<< Check index is not rightmost in grid (hint, NORTH-EAST?) >>>) {
        //     int indexEast = index + 1;
        //     if (IsUnvisited(indexEast)) {
        //         neighbors.Add(<<< What am I adding here? >>>);
        //     }
        // }

        // SOUTHERN?
        if (index < ((this.Size * this.Size) - this.Size)) {
            int indexSouth = index + this.Size;

            // SOUTH EAST
            // if (<<< Check index is not rightmost in grid >>>) {
            //     int indexSouthEast = <<< To the index directly to the right of that due south >>>;
            //     <<<
            //       Add index for neighbour representing south east,
            //       only if that index is unobstructed/unvisited
            //     >>>
            // }

            // SOUTH?
            // <<<
            //   Add index for neighbour representing due south,
            //   only if that index is unobstructed/unvisited
            // >>>

            // SOUTH-WEST?
            // <<< Include any unobstructed/unvisited neighbour to the south west >>>
        }

        // WEST?
        if ((index % this.Size) != 0) {
            int indexWest = index - 1;
            if (IsUnvisited(indexWest)) {
                neighbors.Add(indexWest);
            }

            // NORTH-WEST?
            if (index > this.Size) {
                int indexNorthWest = index - (this.Size + 1);
                if (IsUnvisited(indexNorthWest)) {
                    neighbors.Add(indexNorthWest);
                }
            }
        }

        return neighbors.ToArray();
    }
    
    public bool IsUnvisited(int index)
    {
        // Is the node of the specified index still unvisited?
        return (this.Unvisited[index] == true);
    }

    public void Obstructed(int index)
    {
        // Convienence wrapper
        Visited(index);
    }

    public void Visited(int index)
    {
        this.Unvisited[index] = false;
        this.UnvisitedCount--;
    }
}
