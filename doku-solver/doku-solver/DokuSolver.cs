using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.doku.tools;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        // List<int[,]> grids = new Generator().ImportJsonList("unit_tests");
        // DisplayGrid(Algorithm.SlotPerSlot.Solve(grids[0]));
        GenerateCsv(5, "5x5");
    }

    private static void RunTest(){
        DokuTimer timer = new DokuTimer();
        timer.Start();
        RunGeneratorTest(100); // Current : 4.05s
        timer.Stop();
        Console.WriteLine($"Time for operations: {timer.GetResult()}s");
    }
    
    public static void DisplayGrid(int[,] grid){
        Console.WriteLine("-----------------");
        for(int i = 0; i < grid.GetLength(0); i++){
            for(int j = 0; j < grid.GetLength(1); j++){
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    private static void RunGeneratorTest(int count){
        int iterationsCount = count;
        Generator generator = new Generator();
        for (int i = 0; i < iterationsCount; i++){
            // Console.WriteLine($"Generating {i+1}/{iterationsCount}");
            int[,] grid = generator.Generate(3, 0);
            if (grid == null!) i--;
        }
    }

    private static void GenerateJson(int sectionSize, int count, string fileName){
        int iterationsCount = count;
        List<int[,]> grids = new();
        for(int i = 0; i < iterationsCount; i++){
            Console.WriteLine($"Generating {i+1}/{iterationsCount}");
            int[,] tab = new Generator().Generate(sectionSize, 0);
            grids.Add(tab);
        }
        new Generator().ExportJson(grids, fileName);
    }
    
    private static void GenerateCsv(int sectionSize, string fileName){
        int[,] tab = new Generator().GenerateGrid(sectionSize);
        new Generator().ExportCsv(tab, fileName);
    }

    private static void RunAlgorithmTest(string fileName){
        List<int[,]> grids = new Generator().ImportJsonList(fileName);
        double startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1D / 1000;
        foreach (int[,] grid in grids){
            RunAlgorithm(grid, Algorithm.Backtrack);
        }
        double endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1D / 1000;
        Console.WriteLine(Math.Round(endTime - startTime, 3) + "s");
    }
    
    private static void RunAlgorithm(int[,] grid, Algorithm algorithm){
        algorithm.Solve(grid);
    }
}
