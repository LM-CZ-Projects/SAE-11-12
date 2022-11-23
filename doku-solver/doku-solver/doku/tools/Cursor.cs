namespace doku_solver.doku.tools;

public class Cursor{
    private Position position;
    public int GridSize{ get; }

    public Cursor(int gridSize){
        GridSize = gridSize;
        position = new Position();
    }
    
    public Cursor(Cursor cursor, int gridSize){
        GridSize = gridSize;
        position = new Position(cursor.GetPosition());
    }

    public Cursor(int row, int column, int gridSize){
        GridSize = gridSize;
        position = new Position(row, column);
    }

    public Cursor SetPosition(Position newPosition){
        position.Row = newPosition.Row;
        position.Column = newPosition.Column;
        return this;
    }
    
    public void SetPosition(Cursor newCursor){
        position.Row = newCursor.position.Row;
        position.Column = newCursor.position.Column;
    }

    public Position GetPosition(){
        return position;
    }

    public bool HasNext(){
        return position.Row < GridSize - 1 || position.Column < GridSize - 1;
    }

    public Cursor Next(){
        position.Row++;
        if (position.Row != GridSize) return this;
        position.Row = 0;
        position.Column++;
        return this;
    }

    public Cursor Previous(){
        position.Row--;
        if (position.Row != -1) return this;
        position.Row = GridSize - 1;
        position.Column--;
        return this;
    }

    public void Reset(){
        position.Row = 0;
        position.Column = 0;
    }
    
    public Cursor GetCopy(){
        return new Cursor(this, GridSize);
    }
}