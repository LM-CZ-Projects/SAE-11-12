using doku_solver.doku;
using doku_solver.grid;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        new Tester().TestGeneratorPerformances(1000, 3, false, true); // 12.7s
    }
    
    public static void DisplayGrid(Grid grid){
        Console.WriteLine("-----------------");
        while (grid.Cursor.HasNext()){
            if (grid.Cursor.GetPosition().Column == 0) Console.WriteLine();
            Console.Write(grid.GetOnCursor() + " ");
            grid.Cursor.Next();
        }
        Console.WriteLine(grid.GetOnCursor() + " ");
    }
}
