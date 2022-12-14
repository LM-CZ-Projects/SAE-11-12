using System.Reflection;
using doku_solver.doku.solvers.algorithms;

namespace doku_solver.doku.solvers;

public class Algorithm{

    public static readonly Algorithm SlotPerSlot = new(typeof(SlotPerSlot));
    public static readonly Algorithm BrutForce = new(typeof(BrutForce));
    public static readonly Algorithm Backtrack = new(typeof(BackTrack));
    public static readonly Algorithm OtherBackTrack = new(typeof(OtherBackTrack));

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

    public static List<Algorithm> GetAlgorithms(){
        List<Algorithm> algorithms = new();
        foreach (FieldInfo fieldInfo in typeof(Algorithm).GetFields()){
            if (fieldInfo.GetValue(null) is Algorithm algorithm){
                algorithms.Add(algorithm);
            }
        }
        return algorithms;
    }

}