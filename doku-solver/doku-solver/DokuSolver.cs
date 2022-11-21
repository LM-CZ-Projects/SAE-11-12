using doku_solver.grid;
using doku_solver.solvers;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        Loader loader = new Loader("grid_3x3_1");
        int[,] tab = Algorithm.SlotPerSlot.Solve(loader.GetGrid());
        for(int i = 0; i < tab.GetLength(0); i++){
            for(int j = 0; j < tab.GetLength(1); j++){
                Console.Write(tab[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
