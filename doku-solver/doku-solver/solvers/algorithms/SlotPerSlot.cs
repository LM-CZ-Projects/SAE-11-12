namespace doku_solver.solvers.algorithms;

public class SlotPerSlot : Solver{
    public override int[,] Solve(int[,] tab, int maxIterations){
        int[,] result = Copy(tab);
        int iterations = 0;
        while(!IsSolved(result) && iterations < maxIterations){
            for (int i = 0; i < result.GetLength(0); i++){
                for (int j = 0; j < result.GetLength(1); j++){
                    if (result[i, j] == 0){
                        List<int> possibilities = GetCasePossibilities(result, i, j);
                        if (possibilities.Count == 1){
                            result[i, j] = possibilities[0];
                        }
                    }
                }
            }
            iterations++;
        }
        return result;
    }
}