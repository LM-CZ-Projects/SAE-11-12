using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.doku.tools;
using doku_solver.grid;

namespace doku_solver.doku;

public class Tester{
    public void TestGeneratorPerformances(int iterationsCount, int sectionSize, bool generateFullGrid, bool displayProgression){
        Generator generator = new Generator();
        DokuTimer timer = new DokuTimer();
        timer.Start();
        for(int i = 0; i < iterationsCount; i++){
            if (displayProgression) Console.WriteLine($"Generating {i+1}/{iterationsCount}...");
            if(generateFullGrid) generator.GenerateGrid(sectionSize);
            else generator.Generate(sectionSize, 0);
        }
        timer.Stop();
        Console.WriteLine($"Generation done in {timer.GetResult()}s for {iterationsCount} iterations.");
    }

    public void TestAlgorithmPerformances(Algorithm algorithm, int iterationsCount, int sectionSize, bool displayProgression){
        List<int[,]> grids = GridIO.ImportJsonGrids($"unit_tests_{sectionSize}x{sectionSize}");
        DokuTimer timer = new DokuTimer();
        timer.Start();
        for(int i = 0; i < iterationsCount; i++){
            if (displayProgression) Console.WriteLine($"Generating {i+1}/{iterationsCount}...");
            TestAlgorithm(algorithm, false, grids[i]);
        }
        timer.Stop();
        Console.WriteLine($"Generation done in {timer.GetResult()}s for {iterationsCount} iterations.");
    }

    public void TestAlgorithm(Algorithm algorithm, bool displayResult, int sectionSize){
        TestAlgorithm(algorithm, displayResult, new Generator().Generate(sectionSize, 0));
    }
    
    public void TestAlgorithm(Algorithm algorithm, bool displayResult, int[,] grid){
        int[,] solvedGrid = algorithm.Solve(grid);
        DokuSolver.DisplayGrid(new Grid(solvedGrid));
    }
}