namespace SharpesGold.Maps;

using Newtonsoft.Json.Linq;

/// <summary>
/// Class <c>SharpesMaps</c> models the loading of map-based grid JSON.
/// </summary>
public class SharpesMaps
{
    public const string PATH = @"src\SharpesGold.Maps\SharpesMaps.json";
    public const int SIZE = 25;

    private string[] Maps;
    private int[] Obstacles;

    /// <summary>
    /// Method <c>Rooted</c> to ensure solution root found, whether launched
    /// from the UI or automated unit tests.
    /// </summary>
    private string Rooted(string path)
    {
        DirectoryInfo current = new DirectoryInfo(Directory.GetCurrentDirectory());
        if (current.GetFiles("*.sln").Any()) {
            return path; // Must be the solution root!
        }
        
        DirectoryInfo? root = current.Parent?.Parent?.Parent?.Parent?.Parent;
        if (root == null) {
            return path; // TODO: Error handling
        }

        string[] paths = {root.FullName, path};
        return Path.Combine(paths).ToString();
    }
    
    public SharpesMaps(string path = PATH) {
        string current = Directory.GetCurrentDirectory();
        string rooted = Rooted(path);
        JArray arr = JArray.Parse(File.ReadAllText(rooted));

        this.Maps = new string[arr.Count];
        this.Obstacles = new int[arr.Count*SIZE*SIZE];

        int indexMaps = 0;
        int indexObstacles = 0;

        foreach (JObject obj in arr.Children<JObject>())
        {
            if (obj == null) {
                continue; // TODO: Error handling
            }

            JToken? name = obj.GetValue("name");
            JArray? grid = (JArray?)obj.GetValue("grid");

            if (name == null || grid == null) {
                continue; // TODO: Eror handling
            }

            this.Maps[indexMaps++] = name.ToString();
            foreach (JArray row in grid.Children<JArray>())
            {
                foreach (int value in row)
                {
                    this.Obstacles[indexObstacles++] = value;
                }
            }
        }
    }

    public int GetCount()
    {
        return this.Maps.Count();
    }

    public string GetName(int map)
    {
        return this.Maps[map];
    }

    public int GetObstacle(int map, int row, int col)
    {
        return this.Obstacles[(map*SIZE*SIZE)+(row*SIZE)+col];
    }
}
