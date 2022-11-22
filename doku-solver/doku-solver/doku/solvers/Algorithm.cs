using doku_solver.doku.solvers.algorithms;

namespace doku_solver.doku.solvers;

public class Algorithm{

    public static readonly Algorithm SlotPerSlot = new(typeof(SlotPerSlot));
    public static readonly Algorithm BrutForce = new(typeof(BrutForce));


    private readonly Type _type;

    private Algorithm(Type type){
        _type = type;
    }

    private Solver GetClass(){
        return (Solver) Activator.CreateInstance(_type)! ?? throw new InvalidOperationException();
    }

    public int[,] Solve(int[,] tab, int maxIterations = 100){
        return GetClass().Solve(tab, maxIterations);
    }

}