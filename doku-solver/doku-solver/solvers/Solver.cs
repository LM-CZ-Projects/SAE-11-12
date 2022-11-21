namespace doku_solver.solvers;

public class Solver{

    private readonly Algorithm _algorithm;

    public Solver(Algorithm algorithm){
        _algorithm = algorithm;
    }

    public int[,] Solve(int[,] tab){
        return _algorithm.GetClass().Solve(tab);
    }
    
}