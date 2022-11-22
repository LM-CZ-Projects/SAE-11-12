namespace doku_solver.doku.solvers.algorithms;

public class BackTrack : Solver{
    
    public override int[,] Solve(int[,] tab, int maxIterations){
        int[,] grid = Copy(tab);
        Backtrack(grid, 0, 0);
        return grid;
    }

    /// <summary>
    /// Recursive method that use the Backtrack algorithm to solve the Sudoku
    /// </summary>
    /// <param name="grid">The sudoku grid</param>
    /// <param name="row">Row to check</param>
    /// <param name="column">Column to check</param>
    /// <returns>True if backtrack success, false otherwise</returns>
    private bool Backtrack(int[,] grid, int row, int column){
        if (column >= grid.GetLength(0)){
            column = 0;
            row++;
        }
        if (row >= grid.GetLength(1)) return true;

        if (grid[row, column] != 0)
            return Backtrack(grid, row, column + 1);
        
        List<int> availableValues = GetSlotPossibilities(grid, row, column);
        for (int k = 1; k <= grid.GetLength(0); k++){
            if (availableValues.Contains(k)){
                grid[row, column] = k;
                if (Backtrack(grid, row, column + 1)) return true;
            }
        }
        grid[row, column] = 0;
        return false;
    }
}