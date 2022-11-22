using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.grid;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        RunGeneratorTest();
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

    private static void RunGeneratorTest(){
        int[,] tab = null!;
        int iterationsCount = 1000;
        for(int i = 0; i < iterationsCount; i++){
            Console.WriteLine($"Generating {i+1}/{iterationsCount}");
            tab = new Generator().Generate(5, 0);
        }
        // DisplayGrid(tab);
        // int[,] solvedTab = Algorithm.Backtrack.Solve(tab);
        // DisplayGrid(solvedTab);
    }

    private static void RunAlgorithmTest(){
        Loader loader = new Loader("grid_3x3_1");
        int[,] tab = Algorithm.Backtrack.Solve(loader.GetGrid());
        DisplayGrid(tab);
    }
}
