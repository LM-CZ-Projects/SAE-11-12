using doku_solver.doku.tools;

namespace doku_solver.grid;

public class Grid{
    private readonly int[,] _grid;
    public Cursor Cursor{ get; }

    public Grid(int[,] grid){
        _grid = CopyArray(grid);
        Cursor = new Cursor(0, 0, this._grid.GetLength(0));
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

    public int[,] GetGrid(){
        return _grid;
    }

    public int GetLength(){
        return _grid.GetLength(0);
    }

    public void SetOnPosition(Position position, int value){
        _grid[position.Row, position.Column] = value;
    }

    public void SetOnCursor(int value){
        _grid[Cursor.GetPosition().Row, Cursor.GetPosition().Column] = value;
    }
    
    public void SetOnCursor(Cursor cursor, int value){
        _grid[cursor.GetPosition().Row, cursor.GetPosition().Column] = value;
    }
    
    public int GetOnPosition(Position position){
        return _grid[position.Row, position.Column];
    }

    public int GetOnCursor(){
        return _grid[Cursor.GetPosition().Row, Cursor.GetPosition().Column];
    }
    
    public int GetOnCursor(Cursor cursor){
        return _grid[cursor.GetPosition().Row, cursor.GetPosition().Column];
    }
    
}