using doku_solver.doku.tools;

namespace doku_solver.grid;

public class Grid{
    private int[,] SquareGrid { get; }
    private Cursor Cursor{ get; }

    public Grid(int[,] grid){
        this.SquareGrid = grid;
        Cursor = new Cursor(0, 0, this.SquareGrid.GetLength(0));
    }
    
    
    
    
}