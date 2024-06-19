using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour
{
    public int X;
    public int Y;
    public LineRenderer lineRenderer;
    public Material Material;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;
        lineRenderer.material = Material;
        lineRenderer.positionCount = 2 * X; // 2 Positions for every X line
        for (int i = -X; i < X + 1; i++)
        {
            if (i % 2 == 0)
            {
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
                lineRenderer.SetPosition(i + X, new Vector3(i, -Y));
            }
            else
            {
                lineRenderer.SetPosition(i + X, new Vector3(i, Y));
                lineRenderer.startColor = Color.clear;
                lineRenderer.endColor = Color.clear;
            }
        }
        for (int i = -Y; i < Y + 1; i++)
        {
            if (i % 2 == 0)
            {
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
                lineRenderer.SetPosition(i + Y, new Vector3(-Y, i));
            }
            else
            {
                lineRenderer.SetPosition(i + Y, new Vector3(Y, i));
                lineRenderer.startColor = Color.clear;
                lineRenderer.endColor = Color.clear;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
