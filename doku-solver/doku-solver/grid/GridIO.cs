using System.Text;
using System.Text.Json;
using doku_solver.doku.generator;

namespace doku_solver.grid;

public class GridIO{
    
    protected readonly short[,] _grid;
    
    // Constructors \\
    
    public GridIO(int sectionSize){
        Generator generator = new Generator();
        _grid = GetGridAsShort(generator.Generate(sectionSize, 0));
    }

    public GridIO(short[,] grid){
        _grid = grid;
    }
    
    // Grid Management Methods \\
    
    [Obsolete("Will be removed to force usage of short tabs")]
    public GridIO(int[,] grid){
        _grid = CopyArray(grid);
    }
    
    [Obsolete("Will be removed to force usage of short tabs")]
    protected int[,] GetGridAsInt(){
        int[,] grid = new int[_grid.GetLength(0), _grid.GetLength(1)];
        for(int i = 0; i < _grid.GetLength(0); i++){
            for(int j = 0; j < _grid.GetLength(1); j++){
                grid[i, j] = _grid[i, j];
            }
        }
        return grid;
    }
    
    [Obsolete("Will be removed to force usage of short tabs")]
    protected short[,] GetGridAsShort(int[,] grid){
        short[,] shortGrid = new short[grid.GetLength(0), grid.GetLength(1)];
        for(int i = 0; i < grid.GetLength(0); i++){
            for(int j = 0; j < grid.GetLength(1); j++){
                shortGrid[i, j] = (short) grid[i, j];
            }
        }
        return shortGrid;
    }
    
    protected short[,] CopyArray(short[,] array){
        short[,] newArray = new short[array.GetLength(0), array.GetLength(1)];
        for(int i = 0; i < array.GetLength(0); i++){
            for(int j = 0; j < array.GetLength(1); j++){
                newArray[i, j] = array[i, j];
            }
        }
        return newArray;
    }
    
    [Obsolete("Will be removed to force usage of short tabs")]
    protected short[,] CopyArray(int[,] array){
        short[,] newArray = new short[array.GetLength(0), array.GetLength(1)];
        for(int i = 0; i < array.GetLength(0); i++){
            for(int j = 0; j < array.GetLength(1); j++){
                newArray[i, j] = (short) array[i, j];
            }
        }
        return newArray;
    }
    
    [Obsolete("Will be removed to force usage of short tabs")]
    public int[,] GetGrid(){
        return GetGridAsInt();
    }
    
    public short[,] GetShortGrid(){
        return _grid;
    }
    
    // IO Methods \\
    
    public static void ExportJsonGrid(int[,] grid){
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonSerialized = JsonSerializer.Serialize(grid, options);
        File.WriteAllText("../../../grids/export/{fileName}.json", jsonSerialized);
    }

    public static void GenerateExportJsonGrids(int count, int sectionSize, string fileName){
        List<int[,]> grids = new List<int[,]>();
        Generator generator = new Generator();
        for (int i = 0; i < count; i++){
            grids.Add(generator.Generate(sectionSize, 0));
        }
        ExportJsonGrids(grids, fileName);
    }
    
    public static void ExportJsonGrids(List<int[,]> grids, string fileName){
        List<int[]> flattenedGrids = new List<int[]>();
        foreach (var grid in grids){
            int[] flattenedGrid = new int[grid.Length];
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    flattenedGrid[i * grid.GetLength(0) + j] = grid[i, j];
            flattenedGrids.Add(flattenedGrid);
        }
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonSerialized = JsonSerializer.Serialize(flattenedGrids, options);
        File.WriteAllText($"../../../grids/export/{fileName}.json", jsonSerialized);
    }
    
    public static void ExportCsvGrid(int[,] grid, string fileName){
        List<string> lines = new List<string>();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < grid.GetLength(0); i++){
            for (int j = 0; j < grid.GetLength(1); j++){
                if(grid[i, j] == 0) builder.Append(" " + ";");
                else builder.Append(grid[i, j] + ";");
            }
            lines.Add(builder.ToString());
            builder.Clear();
        }
        String final = String.Join(Environment.NewLine, lines);
        File.WriteAllText($"../../../grids/export/{fileName}.csv", final);
    }

    public static int[,] ImportJsonGrid(string fileName){
        int[]? flattenedGrid = JsonSerializer.Deserialize<int[]>(File.ReadAllText($"../../../grids/export/{fileName}.json"));
        int[,] grid = new int[(int) Math.Sqrt(flattenedGrid!.Length), (int) Math.Sqrt(flattenedGrid.Length)];
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = flattenedGrid[i * grid.GetLength(0) + j];
        return grid;
    }
    
    public static List<int[,]> ImportJsonGrids(string fileName){
        List<int[]>? flattenedGrids = JsonSerializer.Deserialize<List<int[]>>(File.ReadAllText($"../../../grids/export/{fileName}.json"));
        List<int[,]> grids = new List<int[,]>();
        foreach (var flattenedGrid in flattenedGrids!){
            int[,] grid = new int[(int) Math.Sqrt(flattenedGrid.Length), (int) Math.Sqrt(flattenedGrid.Length)];
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    grid[i, j] = flattenedGrid[i * grid.GetLength(0) + j];
            grids.Add(grid);
        }
        return grids;
    }
}