using doku_solver.doku;
using doku_solver.grid;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        GridIO.GenerateExportJsonGrids(100000, 3, "tests_10k_3x3");
        // new Tester().TestGeneratorPerformances(100000, 3, false, true); // 12.7s
    }
    
    public static void DisplayGrid(Grid grid){
        Console.WriteLine("-----------------");
        grid.Cursor.Reset();
        while (grid.Cursor.HasNext()){
            if (grid.Cursor.GetPosition().Column == 0) Console.WriteLine();
            Console.Write(grid.GetOnCursor() + " ");
            grid.Cursor.Next();
        }
        Console.WriteLine(grid.GetOnCursor() + " ");
    }
}
