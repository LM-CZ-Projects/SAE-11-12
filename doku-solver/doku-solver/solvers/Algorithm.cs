using doku_solver.solvers.algorithms;

namespace doku_solver.solvers;

public class Algorithm{

    public static readonly Algorithm SmartBruteforce = new(typeof(Bruteforce));


    private readonly Type _type;

    private Algorithm(Type type){
        _type = type;
    }

    public ISolver GetClass(){
        return (ISolver) Activator.CreateInstance(_type)! ?? throw new InvalidOperationException();
    }

}