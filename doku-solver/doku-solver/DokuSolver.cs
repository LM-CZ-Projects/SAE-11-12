using doku_solver.doku;
using doku_solver.doku.generator;
using doku_solver.doku.solvers;
using doku_solver.grid;

namespace doku_solver;

public static class DokuSolver{
    public static void Main(){
        // Grid grid = GridIO.ImportJsonGrids("unit_tests_3x3")[0];
        Grid grid = new Generator().Generate(3, 0);
        DisplayGrid(new Grid(grid));
        new Tester().TestAlgorithm(Algorithm.SlotPerSlot, true, grid);
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
