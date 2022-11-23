using doku_solver.doku.tools;

namespace doku_solver.grid;

public class Grid : GridIO{
    public Cursor Cursor{ get; }

    public Grid(int sectionSize) : base(sectionSize){
        Cursor = new Cursor(0, 0, _grid.GetLength(0));
    }

    [Obsolete("Will be removed to force usage of short tabs")]
    public Grid(int[,] grid) : base(grid){
        Cursor = new Cursor(0, 0, _grid.GetLength(0));
    }
    
    public Grid(short[,] grid) : base(grid){
        Cursor = new Cursor(0, 0, _grid.GetLength(0));
    }

    public int GetLength(){
        return _grid.GetLength(0);
    }

    public void SetOnPosition(Position position, int value){
        _grid[position.Row, position.Column] = (short) value;
    }

    public void SetOnCursor(int value){
        _grid[Cursor.GetPosition().Row, Cursor.GetPosition().Column] = (short) value;
    }
    
    public void SetOnCursor(Cursor cursor, int value){
        _grid[cursor.GetPosition().Row, cursor.GetPosition().Column] = (short) value;
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