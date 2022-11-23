using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.doku.tools;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        // List<int[,]> grids = new Generator().ImportJsonList("unit_tests");
        // DisplayGrid(grids[0]);
        // DisplayGrid(Algorithm.Backtrack.Solve(grids[0]));
        // RunGeneratorTest(10000, 3);
        RunTest();
    }

    private static void RunTest(){
        RunGeneratorTest(100, 3, false); // Current : 4.05s
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

    private static void RunGeneratorTest(int count, int sectionSize, bool display = false){
        DokuTimer timer = new DokuTimer();
        int iterationsCount = count;
        Generator generator = new Generator();
        timer.Start();
        for (int i = 0; i < iterationsCount; i++){
            if(display) Console.WriteLine($"Generating {i+1}/{iterationsCount}");
            int[,] grid = generator.Generate(sectionSize, 0);
            if (grid == null!) i--;
        }
        timer.Stop();
        Console.WriteLine($"The test took {timer.GetResult()}s for {iterationsCount} iterations");
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

    private static void RunAlgorithmTest(string fileName, Algorithm algorithm){
        List<int[,]> grids = new Generator().ImportJsonList(fileName);
        double startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1D / 1000;
        foreach (int[,] grid in grids){
            RunAlgorithm(grid, algorithm);
        }
        double endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() * 1D / 1000;
        Console.WriteLine(Math.Round(endTime - startTime, 3) + "s");
    }
    
    private static int[,] RunAlgorithm(int[,] grid, Algorithm algorithm){
        return algorithm.Solve(grid);
    }
}
