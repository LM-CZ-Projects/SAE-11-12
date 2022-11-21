namespace doku_solver.solvers.algorithms;

public class Algorithm{

    public static Algorithm Bruteforce = new Algorithm(typeof(Bruteforce));


    private readonly Type _type;

    private Algorithm(Type type){
        this._type = type;
    }

    public ISolver GetClass(){
        return (ISolver) Activator.CreateInstance(_type)! ?? throw new InvalidOperationException();
    }

}