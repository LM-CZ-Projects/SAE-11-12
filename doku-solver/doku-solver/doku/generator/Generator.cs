using System.Diagnostics.CodeAnalysis;
using doku_solver.doku.solvers;
using doku_solver.doku.solvers.algorithms;
using doku_solver.doku.tools;

namespace doku_solver.doku.generator;

public class Generator : Doku{

    public int[,] Generate(int sectionSize){
        int[,] grid = GenerateGrid(sectionSize);
        Algorithm algorithm = Algorithm.SlotPerSlot;
        Random random = new Random();
        Position lastPosition = new Position(0, 0);
        int lastSlot = 0;
        
        while (IsSolved(algorithm.Solve(grid))){
            List<Position> positions = GetAvailablePositions(grid);
            Position position = positions[random.Next(positions.Count)];
            lastPosition = position;
            lastSlot = grid[position.row, position.column];
            grid[position.row, position.column] = 0;
        }
        grid[lastPosition.row, lastPosition.column] = lastSlot;
        return grid;
    }

    public int[,] GenerateGrid(int sectionSize){
        int gridSize = sectionSize * sectionSize;
        int[,] grid = new int[gridSize, gridSize];
        ZeroFill(grid);
        MergeSection(grid, GenerateSection(sectionSize), 0, 0);
        BackTrack backTrack = new BackTrack();
        return backTrack.Solve(grid, 100);
    }

    private List<Position> GetAvailablePositions(int[,] grid){
        List<Position> availablePositions = new List<Position>();
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                if (grid[i, j] != 0)
                    availablePositions.Add(new Position(i, j));
        return availablePositions;
    }

    private void MergeSection(int[,] grid, int[,] section, int sectionRow, int sectionColumn){
        int sectionSize = section.GetLength(0);
        for (int row = 0; row < sectionSize; row++)
            for (int column = 0; column < sectionSize; column++)
                grid[sectionRow * sectionSize + row, sectionColumn * sectionSize + column] = section[row, column];
    }
    
    private int[,] GenerateSection(int sectionSize){
        int[,] section = new int[sectionSize, sectionSize];
        List<int> numbers = Enumerable.Range(1, sectionSize * sectionSize).ToList();
        Random random = new Random();
        for (int i = 0; i < sectionSize; i++){
            for (int j = 0; j < sectionSize; j++){
                int index = random.Next(numbers.Count);
                section[i, j] = numbers[index];
                numbers.RemoveAt(index);
            }
        }
        return section;
    }

    private void ZeroFill(int[,] grid){
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = 0;
    }

    public void ExportJson(){
        throw new NotImplementedException();
    }

    public int[,] ImportJson(){
        throw new NotImplementedException();
    }
    
}