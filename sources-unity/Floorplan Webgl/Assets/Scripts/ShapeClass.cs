using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ShapeClass : MonoBehaviour
{
    public GameObject ShapeHelper;
    // Start is called before the first frame update
    void Start()
    {
        ShapeHelper = GameObject.Find("ShapeHelper");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string ObjectToData(ShapeModel shape)
    {
        return JsonUtility.ToJson(shape);
    }

    public ShapeModel DataToObject(string shape)
    {
        var Class = ShapeHelper.GetComponent<ShapeModel>();
        JsonUtility.FromJsonOverwrite(shape, Class);
        return Class;
    }
}
