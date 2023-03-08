namespace SharpesGold.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpesGold.Logic;
using SharpesGold.Maps;
using SharpesGold.Neighbours;

public partial class MainWindow : Window
{
    [DllImport("kernel32.dll")]                         // To ensure Console works in WPF
    static extern bool AttachConsole(uint dwProcessId); // To ensure Console works in WPF
    const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;     // To ensure Console works in WPF
    
    private const string BRICKS = "https://img.icons8.com/fluency/256/brick-wall.png";
    private const string DONE = "https://img.icons8.com/emoji/256/green-circle-emoji.png";
    private const string DOT = "https://img.icons8.com/windows/256/dot-logo.png";
    private const string IMG = "Image_{0}_{1}";
    private const int POTENTIAL_REMOVE = 1; // UI feedback to remove as potential
    private const int POTENTIAL_TARGET = 2; // UI feedback to add as a target
    private const int SIZE = SharpesMaps.SIZE;
    
    private SharpesLogic Logic = new SharpesLogic(SIZE, new SharpesNeighbours(SIZE));
    private SharpesMaps Maps = new SharpesMaps();
    private SharpesNeighbours Neighbours = new SharpesNeighbours(SIZE);
    
    private BackgroundWorker Worker = new BackgroundWorker();

    public MainWindow()
    {
        AttachConsole(ATTACH_PARENT_PROCESS); // To ensure Console works in WPF
        InitializeComponent(); // Standard WPF initializer
        InitializeMaps();

        // 
        // Ensure algorithm loop does not block UI thread
        // 

        this.Worker.DoWork += Worker_Start;
        this.Worker.ProgressChanged += Worker_Update;
        this.Worker.RunWorkerCompleted += Worker_Completed;
        this.Worker.WorkerReportsProgress = true;
    }

    private void InitializeGrid()
    {
        this.Neighbours = new SharpesNeighbours(SIZE);
        ((Button)this.FindName("resetButton")).IsEnabled = false;
        ((Button)this.FindName("executeButton")).IsEnabled = true;
        int map = ((ComboBox)this.FindName("mapsCombo")).SelectedIndex;
        
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (!((row == 0 && col == 0) || (row == SIZE-1 && col == SIZE-1)))
                {
                    int obstacle = this.Maps.GetObstacle(map, row, col);
                    if (obstacle == 0) {
                        SetImage(row, col, "");
                    } else {
                        SetImage(row, col, BRICKS);
                        this.Neighbours.Obstructed((row * SIZE) + col);
                    }
                }
            }
        }
    }

    private void InitializeMaps()
    {
        ComboBox combo = (ComboBox)this.FindName("mapsCombo");
        for (int i = 0; i < this.Maps.GetCount(); i++) {
            combo.Items.Add(this.Maps.GetName(i));
        }
    }

    private void OnChangedMapsCombo(object sender, RoutedEventArgs e)
    {
        InitializeGrid();
    }

    private void OnClickExecuteButton(object sender, RoutedEventArgs e)
    {
        InitializeGrid();
        ((Button)this.FindName("executeButton")).IsEnabled = false;
        this.Worker.RunWorkerAsync();
    }

    private void OnClickResetButton(object sender, RoutedEventArgs e)
    {
        InitializeGrid();
    }

    private void SetImage(int row, int col, string uri)
    {
        bool empty = string.IsNullOrEmpty(uri);
        string name = string.Format(IMG, row, col);
        Image image = (Image)this.FindName(name);

        //
        // According to this source, WPF caches images loaded from URIs:
        // https://stackoverflow.com/questions/30164042/wpf-image-caching
        //

        image.Source = (empty) ? null : new BitmapImage(new Uri(uri));
    }

    private void Worker_Completed(object? sender, RunWorkerCompletedEventArgs? e)
    {
        ((Button)this.FindName("executeButton")).IsEnabled = true;
        ((Button)this.FindName("resetButton")).IsEnabled = true;
    }

    private void Worker_Start(object? sender, DoWorkEventArgs? e)
    {
        BackgroundWorker worker = this.Worker;
        this.Logic = new SharpesLogic(SIZE, this.Neighbours);
        this.Logic.Start((row, col, potential) => worker.ReportProgress(0, new int[] {row, col, potential}));
    }

    private void Worker_Update(object? sender, ProgressChangedEventArgs? e)
    {
        if (e == null || e.UserState == null)
        {
            return;
        }

        int row = ((int[])e.UserState)[0];
        int col = ((int[])e.UserState)[1];
        int potential = ((int[])e.UserState)[2];

        if (row == 0 && col == 0) {
            return;
        }

        SetImage(row, col,
            (potential == POTENTIAL_TARGET) ? DONE :
            (potential == POTENTIAL_REMOVE) ? "" : DOT);
    }
}
