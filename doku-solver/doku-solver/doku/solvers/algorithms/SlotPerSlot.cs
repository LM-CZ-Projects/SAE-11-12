using doku_solver.doku.tools;
using doku_solver.grid;

namespace doku_solver.doku.solvers.algorithms;

public class SlotPerSlot : Solver{
    public override int[,] Solve(int[,] tab, int maxIterations){
        Grid grid = new Grid(tab);
        int iterations = 0;
        while(!IsSolved(grid) && iterations < maxIterations){
            while (grid.Cursor.HasNext()){
                Position position = grid.Cursor.GetPosition();
                // Console.WriteLine($"{position.Row}, {position.Column}");
                if (grid.GetOnCursor() == 0){
                    List<int> possibilities = GetSlotPossibilities(grid, position);
                    if (possibilities.Count == 1){
                        grid.SetOnCursor(possibilities[0]);
                    }
                }
                grid.Cursor.Next();
            }
            grid.Cursor.Reset();
            iterations++;
        }
        return grid.GetGrid();
    }
}