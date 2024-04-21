using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numRows = 9; // Number of rows of cubes
    public int numColumns = 9; // Number of columns of cubes
    public float gridWidth = 5f; // Width of the grid in Unity units
    public float cubeScale = 0.2f; // Scale of each cube

    void Start()
    {
        // Calculate the offset needed to center the grid
        float xOffset = -gridWidth / 2f + cubeScale / 2f;
        float yOffset = -gridWidth / 2f + cubeScale / 2f;

        // Loop through rows and columns to instantiate cubes
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                // Calculate position for the current cube
                Vector3 position = new Vector3(col * cubeScale + xOffset, row * cubeScale + yOffset, 0f);

                // Instantiate the cube at the calculated position
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);

                // Set the name of the cube based on its position
                cube.name = $"{col}-{row}";
            }
        }
    }
}
