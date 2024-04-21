using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MazeControl : MonoBehaviour
{
    public int numRows = 16;
    public int numCols = 16;
    public float obstacleProb = 0.80f;
    public int fullCount;
    public Button[] controls;
    public int numX = 0;
    public int numY = 0;

    void Update()
    {
        if (GameObject.Find($"{14}-{1}").GetComponent<Renderer>().material.color == Color.yellow)
        {
            Start();
        }
    }

    void ButtonClicked(Button button)
    {
        for (int i = 0; i < 16; i++)
        {
            for (int x = 0; x < 16; x++)
            {
                GameObject cell = GameObject.Find($"{i}-{x}");
                if (cell != null && cell.GetComponent<Renderer>() != null && cell.GetComponent<Renderer>().material.color == Color.yellow)
                {
                    numX = i;
                    numY = x;
                }
            }
        }

        if (button.name == "up")
        {
            GameObject upCell = GameObject.Find($"{numX}-{numY + 1}");
            if (upCell != null && upCell.GetComponent<Renderer>() != null && upCell.GetComponent<Renderer>().material.color != Color.black)
            {
                upCell.GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find($"{numX}-{numY}").GetComponent<Renderer>().material.color = Color.white;
            }
        }
        else if (button.name == "down")
        {
            GameObject downCell = GameObject.Find($"{numX}-{numY - 1}");
            if (downCell != null && downCell.GetComponent<Renderer>() != null && downCell.GetComponent<Renderer>().material.color != Color.black)
            {
                downCell.GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find($"{numX}-{numY}").GetComponent<Renderer>().material.color = Color.white;
            }
        }
        else if (button.name == "left")
        {
            GameObject leftCell = GameObject.Find($"{numX - 1}-{numY}");
            if (leftCell != null && leftCell.GetComponent<Renderer>() != null && leftCell.GetComponent<Renderer>().material.color != Color.black)
            {
                leftCell.GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find($"{numX}-{numY}").GetComponent<Renderer>().material.color = Color.white;
            }
        }
        else if (button.name == "right")
        {
            GameObject rightCell = GameObject.Find($"{numX + 1}-{numY}");
            if (rightCell != null && rightCell.GetComponent<Renderer>() != null && rightCell.GetComponent<Renderer>().material.color != Color.black)
            {
                rightCell.GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find($"{numX}-{numY}").GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }


    private void Start()
    {
        foreach (Button button in controls)
        {
            button.onClick.AddListener(() => ButtonClicked(button));
        }
        for (int trial = 0; trial < 5; trial++)
        {
            Debug.Log($"\n\n-----Easy trial {trial + 1}-----");
            fullCount = Main(numRows, numCols, obstacleProb);
        }
        GameObject cube = GameObject.Find($"{2}-{13}");
        if (cube != null)
            cube.GetComponent<Renderer>().material.color = Color.yellow;
        GameObject wallOne = GameObject.Find($"{1}-{14}");
        if (cube != null)
            wallOne.GetComponent<Renderer>().material.color = Color.black;
        GameObject end = GameObject.Find($"{14}-{1}");
        if (cube != null)
            end.GetComponent<Renderer>().material.color = Color.green;
    }

    private int Main(int numRows, int numCols, float obstacleProb)
    {
        int[,] grid = CreateGrid(numRows, numCols, obstacleProb);
        PrintGrid(grid);

        Vector2Int start = new Vector2Int(1, 1);
        Vector2Int goal = new Vector2Int(numRows - 2, numCols - 2);
        grid[start.x, start.y] = -1; // Start position
        grid[goal.x, goal.y] = -2; // Goal position

        PriorityQueue<State> queue = new PriorityQueue<State>();
        State startState = new State(start, goal, grid);
        startState.totalMoves = 0;

        float priority = startState.totalMoves + ManhattanDistance(startState.position, startState.goal);
        queue.Enqueue(priority, startState);

        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        while (queue.Count > 0)
        {
            State currentState = queue.Dequeue().Value;
            Vector2Int currentPosition = currentState.position;

            if (currentPosition == goal)
            {
                // Goal reached, return the number of moves
                return currentState.totalMoves;
            }

            if (visited.Contains(currentPosition))
                continue;

            visited.Add(currentPosition);

            foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int newR = currentPosition.x + dr;
                int newC = currentPosition.y + dc;
                if (newR > 0 && newR < numRows - 1 && newC > 0 && newC < numCols - 1 && currentState.grid[newR, newC] == 0)
                {
                    int[,] newGrid = (int[,])currentState.grid.Clone();
                    newGrid[newR, newC] = -3; // Path position
                    Vector2Int newPosition = new Vector2Int(newR, newC);
                    State newState = new State(newPosition, goal, newGrid);
                    newState.totalMoves = currentState.totalMoves + 1;
                    priority = newState.totalMoves + ManhattanDistance(newState.position, newState.goal);
                    queue.Enqueue(priority, newState);
                }
            }
        }

        // No path found, return a sentinel value (e.g., -1)
        return -1;
    }


    private int[,] CreateGrid(int numRows, int numCols, float obstacleProb)
    {
        int[,] grid = new int[numRows, numCols];

        // Initialize grid with walls
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                grid[r, c] = 1; // 1 represents walls
            }
        }

        // Set start and end points
        grid[1, 1] = 0; // Start point
        grid[numRows - 2, numCols - 2] = 0; // End point

        // Generate maze paths
        GenerateMazePath(grid, 1, 1);

        return grid;
    }

    private void GenerateMazePath(int[,] grid, int startX, int startY)
    {
        List<(int, int)> directions = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
        Shuffle(directions); // Shuffle directions randomly

        foreach (var (dx, dy) in directions)
        {
            int newX = startX + 2 * dx;
            int newY = startY + 2 * dy;

            if (newX >= 1 && newX < grid.GetLength(0) - 1 && newY >= 1 && newY < grid.GetLength(1) - 1 && grid[newX, newY] == 1)
            {
                grid[startX + dx, startY + dy] = 0; // Clear the wall between current cell and next cell
                grid[newX, newY] = 0; // Clear the wall at the next cell
                GenerateMazePath(grid, newX, newY);
            }
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    private void PrintGrid(int[,] grid)
    {
        int numRows = grid.GetLength(0);
        int numCols = grid.GetLength(1);

        for (int r = numRows - 1; r >= 0; r--)
        {
            for (int c = 0; c < numCols; c++)
            {
                if (grid[r, c] == 1)
                {
                    // Cube corresponding to 1 is black
                    GameObject cube = GameObject.Find($"{numRows - r - 1}-{c}");
                    if (cube != null)
                        cube.GetComponent<Renderer>().material.color = Color.black;
                }

                else
                {
                    // Cube corresponding to 0 is white
                    GameObject cube = GameObject.Find($"{numRows - r - 1}-{c}");
                    if (cube != null)
                        cube.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}

public class State
{
    public Vector2Int position;
    public Vector2Int goal;
    public int[,] grid;
    public int totalMoves;

    public State(Vector2Int position, Vector2Int goal, int[,] grid)
    {
        this.position = position;
        this.goal = goal;
        this.grid = grid;
    }
}

public class PriorityQueue<T>
{
    private SortedDictionary<float, Queue<T>> dict = new SortedDictionary<float, Queue<T>>();

    public int Count
    {
        get
        {
            int count = 0;
            foreach (var queue in dict.Values)
                count += queue.Count;
            return count;
        }
    }

    public void Enqueue(float priority, T item)
    {
        if (!dict.ContainsKey(priority))
            dict[priority] = new Queue<T>();
        dict[priority].Enqueue(item);
    }

    public KeyValuePair<float, T> Dequeue()
    {
        var pair = dict.First();
        var item = pair.Value.Dequeue();
        if (pair.Value.Count == 0)
            dict.Remove(pair.Key);
        return new KeyValuePair<float, T>(pair.Key, item);
    }
}
