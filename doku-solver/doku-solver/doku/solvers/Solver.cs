namespace doku_solver.doku.solvers;

public abstract class Solver : Doku{

    public abstract int[,] Solve(int[,] tab, int maxIterations);
    
}