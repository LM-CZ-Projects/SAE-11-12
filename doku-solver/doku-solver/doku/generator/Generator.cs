using System.Text;
using System.Text.Json;
using doku_solver.doku.solvers;
using doku_solver.doku.tools;

namespace doku_solver.doku.generator;

public class Generator : Doku{

    public int[,] Generate(int sectionSize, int additionalSlotsCount){
        int[,] grid;
        do{
            grid = GenerateGrid(sectionSize);
        } while (grid == null!);
        Algorithm algorithm = Algorithm.SlotPerSlot;
        Random random = new Random();
        List<Position> removedPositions = new List<Position>();
        List<int> removedValues = new List<int>();
        // While the grid is solvable, remove a slot
        while (IsSolved(algorithm.Solve(grid, 1000))){
            List<Position> positions = GetAvailablePositions(grid);
            Position position = positions[random.Next(positions.Count)];
            removedPositions.Add(position);
            removedValues.Add(grid[position.Row, position.Column]);
            grid[position.Row, position.Column] = 0;
        }

        // Reset last deleted position
        Position lastPosition = removedPositions[^1];
        grid[lastPosition.Row, lastPosition.Column] = removedValues[^1];
        // If to many additional slots, return grid
        if (additionalSlotsCount > removedPositions.Count) return grid;
        // Else, reset additional slots
        for (int i = 1; i <= additionalSlotsCount; i++){
            lastPosition = removedPositions[^i];
            grid[lastPosition.Row, lastPosition.Column] = removedValues[^i];
        }
        return grid;
    }

    public int[,] GenerateGrid(int sectionSize){
        int gridSize = sectionSize * sectionSize;
        int[,] grid = new int[gridSize, gridSize];
        ZeroFill(grid);
        GenerateBaseGrid(grid, sectionSize);
        

        Algorithm backTrack = Algorithm.Backtrack;
        // int[,] solvedGrid = backTrack.Solve(grid, (int) Math.Pow(Factorial(sectionSize), 3)); // 5!^3
        int[,] solvedGrid = backTrack.Solve(grid, -1);
        // DokuSolver.DisplayGrid(grid);
        if (!IsSolved(solvedGrid)) return null!;
        return solvedGrid;
    }

    private long Factorial(int n){
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    private List<Position> GetAvailablePositions(int[,] grid){
        List<Position> availablePositions = new List<Position>();
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                if (grid[i, j] != 0)
                    availablePositions.Add(new Position(i, j));
        return availablePositions;
    }

    private void GenerateBaseGrid(int[,] grid, int sectionSize){
        for(int i = 0; i < sectionSize; i++){
            MergeSection(grid, GenerateSection(sectionSize), i, i);
        }
    }

    private void MergeSection(int[,] grid, int[,] section, int sectionRow, int sectionColumn){
        int sectionSize = section.GetLength(0);
        for (int row = 0; row < sectionSize; row++)
            for (int column = 0; column < sectionSize; column++)
                grid[sectionRow * sectionSize + row, sectionColumn * sectionSize + column] = section[row, column];
    }
    
    private int[,] GenerateSection(int sectionSize){
        int[,] section = new int[sectionSize, sectionSize];
        List<int> numbers = Enumerable.Range(1, sectionSize * sectionSize).ToList();
        Random random = new Random();
        for (int i = 0; i < sectionSize; i++){
            for (int j = 0; j < sectionSize; j++){
                int index = random.Next(numbers.Count);
                section[i, j] = numbers[index];
                numbers.RemoveAt(index);
            }
        }
        return section;
    }

    private void ZeroFill(int[,] grid){
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = 0;
    }

    public void ExportJson(int[,] grid){
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonSerialized = JsonSerializer.Serialize(grid, options);
        File.WriteAllText("../../../grids/export/{fileName}.json", jsonSerialized);
    }
    
    public void ExportJson(List<int[,]> grids, string fileName){
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
    
    public void ExportCsv(int[,] grid, string fileName){
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

    public int[,] ImportJson(){
        throw new NotImplementedException();
    }
    
    public List<int[,]> ImportJsonList(string fileName){
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