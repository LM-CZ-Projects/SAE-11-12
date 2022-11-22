namespace doku_solver.doku;

public class Doku{
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
                if (GetSlotPossibilities(tab, i, j).Count != 0) isSolved = false;
            }
        }
        return isSolved;
    }
    
    protected List<int> GetSlotPossibilities(int[,] tab, int row, int column){
        List<int> rowPossibilities = GetRowPossibilities(tab, row);
        List<int> columnPossibilities = GetColumnPossibilities(tab, column);
        List<int> sectionPossibilities = GetSectionPossibilities(tab, row, column);
        List<int> possibilities = new List<int>();
        foreach (int val in rowPossibilities){
            if(columnPossibilities.Contains(val) && sectionPossibilities.Contains(val)){
                possibilities.Add(val);
            }
        }
        return possibilities;
    }

    private List<int> GetColumnPossibilities(int[,] tab, int column){
        List<int> possibilities = new List<int>();
        for(int i = 1; i <= tab.GetLength(0); i++){
            possibilities.Add(i);
        }
        for (int i = 0; i < tab.GetLength(0); i++){
            if(possibilities.Contains(tab[i, column])){
                possibilities.Remove(tab[i, column]);
            }
        }
        return possibilities;
    }
    
    private List<int> GetRowPossibilities(int[,] tab, int row){
        List<int> possibilities = new List<int>();
        for(int i = 1; i <= tab.GetLength(1); i++){
            possibilities.Add(i);
        }
        for (int i = 0; i < tab.GetLength(1); i++){
            if(possibilities.Contains(tab[row, i])){
                possibilities.Remove(tab[row, i]);
            }
        }
        return possibilities;
    }
    
    private List<int> GetSectionPossibilities(int[,] tab, int row, int column){
        List<int> possibilities = new List<int>();
        for(int i = 1; i <= tab.GetLength(1); i++){
            possibilities.Add(i);
        }
        int squareSize = (int)Math.Sqrt(tab.GetLength(0));
        int squareRow = row / squareSize;
        int squareColumn = column / squareSize;
        for (int i = squareRow * squareSize; i < squareRow * squareSize + squareSize; i++){
            for (int j = squareColumn * squareSize; j < squareColumn * squareSize + squareSize; j++){
                if(possibilities.Contains(tab[i, j])){
                    possibilities.Remove(tab[i, j]);
                }
            }
        }
        return possibilities;
    }
}