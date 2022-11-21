namespace doku_solver.solvers;

public abstract class Solver{

    public abstract int[,] Solve(int[,] tab);
    
    protected int[,] Copy(int[,] tab){
        int[,] nTab = new int[tab.GetLength(0), tab.GetLength(1)];
        for (int i = 0; i < tab.GetLength(0); i++){
            for (int j = 0; j < tab.GetLength(1); j++){
                nTab[i, j] = tab[i, j];
            }
        }
        return nTab;
    }

    protected bool IsSolved(int[,] tab){
        bool isSolved = true;
        for (int i = 0; i < tab.GetLength(0) && isSolved; i++){
            for (int j = 0; j < tab.GetLength(1) && isSolved; j++){
                if (tab[i, j] == 0) isSolved = false;
            }
        }
        return isSolved;
    }
    
}