using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ShapeModel : MonoBehaviour
{
    public Vector3[] Points;
    public int[] Triangles;

    ShapeModel()
    {

    }

    ShapeModel(Vector3[] InputPoints)
    {
        Points = InputPoints;
    }

    public void AddPoints(IEnumerable<Vector3> InputPoints)
    {
        var Points2 = new List<Vector3>();
        if (Points is not null)
        {
            Points2 = Points.ToList();
        }

        foreach (var InputPoint in InputPoints)
        {
            Points2.Add(InputPoint);
        }

        Points = Points2.ToArray();
    }

    public void ExtrudePoints()
    {
        Vector3[] Extrude = Points.Select(Original => new Vector3(Original.x, Original.y + 1, Original.z)).ToArray();
        Points = Points.Concat(Extrude).ToArray();
    }

    public void Stitch()
    {
        for (int i = 0; i != (Points.Length / 2) - 1; i++)
        {
            int CurrentIndex = i;
            int NextIndex = i + 1 % (Points.Length / 2);
            int TopIndex = CurrentIndex + (Points.Length / 2);
            int NextTopIndex =  (i + 1 % (Points.Length / 2)) + Points.Length / 2;
            AddTriangle(CurrentIndex, NextIndex, TopIndex);
            AddTriangle(TopIndex, NextTopIndex, NextIndex);
        }
    }

    public void AddTriangle(int Index1, int Index2, int Index3)
    {
        var Triangles2 = new List<int>();
        if (Triangles is not null)
        {
            Triangles2 = Triangles.ToList();
        }
        //Spin Clockwise
        Triangles2.Add(Index1);
        Triangles2.Add(Index2);
        Triangles2.Add(Index3);

        //Spin Counter Clockwise
        Triangles2.Add(Index3);
        Triangles2.Add(Index2);
        Triangles2.Add(Index1);

        Triangles = Triangles2.ToArray();
    }
}

