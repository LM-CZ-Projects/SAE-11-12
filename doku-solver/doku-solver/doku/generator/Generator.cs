using System.Diagnostics.CodeAnalysis;

namespace doku_solver.doku.generator;

public class Generator : Doku{

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public int[,] Generate(int sectionSize){
        int gridSize = sectionSize * sectionSize;
        int[,] grid = new int[gridSize, gridSize];
        return grid;
    }

    public void ExportJson(){
        throw new NotImplementedException();
    }

    public int[,] ImportJson(){
        throw new NotImplementedException();
    }
    
}