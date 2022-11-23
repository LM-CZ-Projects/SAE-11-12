namespace doku_solver.doku.solvers.algorithms; 

public class OtherBackTrack: Solver {
    private int GRID_SIZE;
    public override int[,] Solve(int[,] tab, int maxIterations) {
        GRID_SIZE = tab.GetLength(0);
        SmartSolve(tab);
        return tab;
    }
    
    private bool IsInRow(int[,] grid, int number, int row) {
        for (int i = 0; i < GRID_SIZE; i++)
            if (grid[row, i] == number)
                return true;
        return false;
    }

    private bool IsInColumn(int[,] grid, int number, int column) {
        for (int i = 0; i < GRID_SIZE; i++)
            if (grid[i, column] == number)
                return true;
        return false;
    }

    private bool IsInSection(int[,] grid, int number, int row, int column) {
        int boxSize = (int) Math.Sqrt(GRID_SIZE);
        int localRow = row - row % boxSize;
        int localColumn = column - column % boxSize;
        
        for (int i = localRow; i < localRow + boxSize; i++)
        for (int j = localColumn; j < localColumn + boxSize; j++)
            if (grid[i, j] == number)
                return true;
        return false;
    }

    private bool IsValidPlacement(int[,] grid, int number, int row, int column) {
        return !IsInRow(grid, number, row) &&
               !IsInColumn(grid, number, column) &&
               !IsInSection(grid, number, row, column);
    }

    private bool SmartSolve(int[,] grid) {
        for (int i = 0; i < GRID_SIZE; i++) {
            for (int j = 0; j < GRID_SIZE; j++) {
                if (grid[i, j] == 0) {
                    for (int t = 1; t <= GRID_SIZE; t++) {
                        if (IsValidPlacement(grid, t, i, j)) {
                            grid[i, j] = t;

                            if (SmartSolve(grid))
                                return true;
                            
                            grid[i, j] = 0;
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }
}