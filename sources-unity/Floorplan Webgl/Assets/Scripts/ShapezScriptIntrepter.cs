using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShapezScriptIntrepter : MonoBehaviour
{
    public GameObject ShapeHelper;
    // Start is called before the first frame update
    void Start()
    {
        ShapeHelper = GameObject.Find("ShapeHelper");
    }

    public Mesh Createmesh(ShapeModel Shape)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Shape.Points;
        mesh.uv = new Vector2[Shape.Points.Count()];
        mesh.triangles = Shape.Triangles;
        return mesh;
    }

    public void SwapYZ(ShapeModel shape)
    {
        for (int i = 0; i != shape.Points.Length; i++)
        {
            shape.Points[i] = new Vector3(shape.Points[i].x, 0, shape.Points[i].y);
        }
    }

    public void CreateShape(ShapeModel shape)
    {
        SwapYZ(shape);
        shape.ExtrudePoints();
        shape.Stitch();
        Mesh ShapeMesh = Createmesh(shape);

        var CustomShape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        CustomShape.GetComponent<MeshFilter>().mesh = ShapeMesh;
        CustomShape.name = "CustomShape";

        // Adjust positions & scale to fit the coordinate grid Ground
        // Elevate shapes slightly off ground to avoid clipping
        CustomShape.transform.position += new Vector3(0f, 0.01f, 0f);
        CustomShape.transform.localScale = new Vector3(1f, 1f, 1f);

        AddMessager(CustomShape, shape);
    }

    public void AddMessager(GameObject Object, ShapeModel shape)
    {
        // Create Required SubObjects
        var CanvasObj = new GameObject("CanvasObj");
        CanvasObj.transform.SetParent(Object.transform, true);
        CanvasObj.AddComponent<Canvas>();
        CanvasObj.GetComponent<Canvas>().AddComponent<Text>();
        CanvasObj.GetComponent<Canvas>().GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        CanvasObj.GetComponent<Canvas>().GetComponent<Text>().color = Color.red;
        CanvasObj.transform.localRotation = new Quaternion(0, 0, 0, 0);

        var avg = new Vector3();
        foreach (var point in shape.Points)
        {
            avg += point;
        }
        avg /= shape.Points.Count();

        CanvasObj.transform.position = new Vector3(avg.x, avg.y + 2, avg.z);
        CanvasObj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Object.AddComponent<WebRequester>();
    }
}
