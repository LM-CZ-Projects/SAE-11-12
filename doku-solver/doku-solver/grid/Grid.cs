using doku_solver.doku.tools;

namespace doku_solver.grid;

public class Grid{
    private int[,] SquareGrid { get; }
    private Cursor Cursor{ get; }

    public Grid(int[,] grid){
        SquareGrid = CopyArray(grid);
        Cursor = new Cursor(0, 0, this.SquareGrid.GetLength(0));
    }

    private int[,] CopyArray(int[,] array){
        int[,] newArray = new int[array.GetLength(0), array.GetLength(1)];
        for(int i = 0; i < array.GetLength(0); i++){
            for(int j = 0; j < array.GetLength(1); j++){
                newArray[i, j] = array[i, j];
            }
        }
        return newArray;
    }
    
}