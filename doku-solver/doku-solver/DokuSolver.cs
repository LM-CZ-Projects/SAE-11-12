using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.grid;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        RunAlgorithmTest();
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
        int[,] tab = new Generator().Generate(3);
        DisplayGrid(tab);
        // int[,] solvedTab = Algorithm.SlotPerSlot.Solve(tab);
        // Console.WriteLine("---------------------");
        // DisplayGrid(solvedTab);
    }

    private static void RunAlgorithmTest(){
        Loader loader = new Loader("grid_3x3_1");
        int[,] tab = Algorithm.Backtrack.Solve(loader.GetGrid());
        DisplayGrid(tab);
    }
}
