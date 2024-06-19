using UnityEngine;

public class FillGround : MonoBehaviour
{
    public GameObject groundPrefab; // Assign your ground tile prefab in the inspector

    public int gridSizeX = 100;
    public int gridSizeY = 100;

    void Start()
    {
        FillGroundWithPrefab();
    }

    void FillGroundWithPrefab()
    {
        // Calculate the offset to center the grid
        float offsetX = gridSizeX / 2f;
        float offsetY = gridSizeY / 2f;

        // Iterate over a grid defined by gridSizeX and gridSizeY
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Calculate the position for the current tile, adjusting for the offset to center the grid
                // Subtracting offsetX and offsetY centers the grid at (0,0)
                Vector3 position = new Vector3(x - offsetX, 0, y - offsetY); // Adjust Y as needed for your scene layout

                // Instantiate the prefab at the calculated position
                // Quaternion.identity means no rotation
                Instantiate(groundPrefab, position, Quaternion.identity, transform); // Parenting to this GameObject
            }
        }
    }
}
