namespace doku_solver.doku.tools;

public class Cursor{
    private Position Position{ get; }
    private int GridSize{ get; }

    public Cursor(int gridSize){
        GridSize = gridSize;
        Position = new Position();
    }

    public Cursor(int row, int column, int gridSize){
        GridSize = gridSize;
        Position = new Position(row, column);
    }

    public void SetPosition(int row, int column){
        Position.Row = row;
        Position.Column = column;
    }
    
    public void SetPosition(Cursor newCursor){
        Position.Row = newCursor.Position.Row;
        Position.Column = newCursor.Position.Column;
    }

    public Position GetPosition(){
        return Position;
    }

    public bool HasNext(){
        return Position.Row < GridSize - 1 && Position.Column < GridSize;
    }

    public Cursor Next(){
        Position.Row++;
        if (Position.Row != GridSize) return this;
        Position.Row = 0;
        Position.Column++;
        return this;
    }
}