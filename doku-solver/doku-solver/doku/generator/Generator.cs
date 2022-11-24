using System.Text;
using System.Text.Json;
using doku_solver.doku.solvers;
using doku_solver.doku.tools;
using doku_solver.grid;

namespace doku_solver.doku.generator;

public class Generator : Doku{

    public Grid Generate(int sectionSize, int additionalSlotsCount){
        Grid grid;
        do{
            grid = GenerateSolvedGrid(sectionSize);
        } while (grid == null!);
        Algorithm algorithm = Algorithm.SlotPerSlot;
        Random random = new Random();
        List<Position> removedPositions = new List<Position>();
        List<short> removedValues = new List<short>();
        // While the grid is solvable, remove a slot
        while (IsSolved(algorithm.Solve(grid, 1000))){
            List<Position> positions = GetAvailablePositions(grid);
            Position position = positions[random.Next(positions.Count)];
            removedPositions.Add(position);
            removedValues.Add(grid.GetOnPosition(position));
            grid.SetOnPosition(position, 0);
        }

        // Reset last deleted position
        Position lastPosition = removedPositions[^1];
        grid.SetOnPosition(lastPosition, removedValues[^1]);
        // If to many additional slots, return grid
        if (additionalSlotsCount > removedPositions.Count) return grid;
        // Else, reset additional slots
        for (int i = 1; i <= additionalSlotsCount; i++){
            lastPosition = removedPositions[^i];
            grid.SetOnPosition(lastPosition, removedValues[^i]);
        }
        return grid;
    }

    public Grid GenerateSolvedGrid(int sectionSize){
        int gridSize = sectionSize * sectionSize;
        Grid baseGrid = new Grid(gridSize);
        GenerateBaseGrid(baseGrid, sectionSize);
        

        Algorithm backTrack = Algorithm.Backtrack;
        // int[,] solvedGrid = backTrack.Solve(grid, (int) Math.Pow(Factorial(sectionSize), 3)); // 5!^3
        Grid solvedGrid = backTrack.Solve(baseGrid, -1);
        // DokuSolver.DisplayGrid(grid);
        if (!IsSolved(solvedGrid)) return null!;
        return solvedGrid;
    }

    private long Factorial(int n){
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    private List<Position> GetAvailablePositions(Grid grid){
        List<Position> availablePositions = new List<Position>();
        for (int i = 0; i < grid.GetLength(); i++)
            for (int j = 0; j < grid.GetLength(); j++){
                Position currentPosition = new Position(i, j);
                if (grid.GetOnPosition(currentPosition) != 0)
                    availablePositions.Add(new Position(i, j));
            }
        return availablePositions;
    }

    private void GenerateBaseGrid(Grid grid, int sectionSize){
        for(int i = 0; i < sectionSize; i++){
            MergeSection(grid, GenerateSection(sectionSize), i, i);
        }
    }

    private void MergeSection(Grid grid, short[,] section, int sectionRow, int sectionColumn){
        int sectionSize = section.GetLength(0);
        for (int row = 0; row < sectionSize; row++)
            for (int column = 0; column < sectionSize; column++)
                grid.SetOnPosition(new Position(sectionRow * sectionSize + row, sectionColumn * sectionSize + column), section[row, column]);
    }
    
    private short[,] GenerateSection(int sectionSize){
        short[,] section = new short[sectionSize, sectionSize];
        List<int> numbers = Enumerable.Range(1, sectionSize * sectionSize).ToList();
        Random random = new Random();
        for (int i = 0; i < sectionSize; i++){
            for (int j = 0; j < sectionSize; j++){
                int index = random.Next(numbers.Count);
                section[i, j] = (short) numbers[index];
                numbers.RemoveAt(index);
            }
        }
        return section;
    }
}