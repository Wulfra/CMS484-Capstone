using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public int rows = 10;
    public int columns = 10;

    private bool[,] visitedCells;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        visitedCells = new bool[rows, columns];
        DFS(0, 0);
    }

    void DFS(int row, int col)
    {
        visitedCells[row, col] = true;
        Instantiate(wallPrefab, new Vector3(row, 0, col), Quaternion.identity);

        // Possible directions to move: north, east, south, west
        int[] directions = { -1, 1 };
        Shuffle(directions); // Shuffle to randomize the direction

        foreach (int dir in directions)
        {
            if (row + dir >= 0 && row + dir < rows && !visitedCells[row + dir, col])
                DFS(row + dir, col);
            if (col + dir >= 0 && col + dir < columns && !visitedCells[row, col + dir])
                DFS(row, col + dir);
        }
    }

    void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}