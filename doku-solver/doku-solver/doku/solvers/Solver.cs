namespace doku_solver.doku.solvers;

public abstract class Solver : Doku{

    public abstract int[,] Solve(int[,] tab, int maxIterations);
    
    protected bool IsInRow(int[,] grid, int number, int row) {
        for (int i = 0; i < grid.GetLength(0); i++)
            if (grid[row, i] == number)
                return true;
        return false;
    }

    protected bool IsInColumn(int[,] grid, int number, int column) {
        for (int i = 0; i < grid.GetLength(0); i++)
            if (grid[i, column] == number)
                return true;
        return false;
    }

    protected bool IsInSection(int[,] grid, int number, int row, int column) {
        int boxSize = (int) Math.Sqrt(grid.GetLength(0));
        int localRow = row - row % boxSize;
        int localColumn = column - column % boxSize;
        
        for (int i = localRow; i < localRow + boxSize; i++)
        for (int j = localColumn; j < localColumn + boxSize; j++)
            if (grid[i, j] == number)
                return true;
        return false;
    }

    protected bool IsValidPlacement(int[,] grid, int number, int row, int column) {
        return !IsInRow(grid, number, row) &&
               !IsInColumn(grid, number, column) &&
               !IsInSection(grid, number, row, column);
    }
}