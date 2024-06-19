using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shape_Selection_Buttons : MonoBehaviour
{
    public GameObject panel;
    public Button Rectangle;
    public Button Circle;
    public Camera cam;
    public GameObject Object = null;
    // Start is called before the first frame update
    void Start()
    {
        panel = transform.gameObject;
        Rectangle = panel.transform.GetChild(1).GetComponent<Button>();
        Circle = panel.transform.GetChild(0).GetComponent<Button>();
        Rectangle.onClick.AddListener(() => MakeShape(shape: PrimitiveType.Cube));
        Circle.onClick.AddListener(() => MakeShape(shape: PrimitiveType.Cylinder));
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Object)
        {
            var NormalPos = (Input.mousePosition);
            Object.transform.position = cam.ScreenToWorldPoint(new Vector3(NormalPos.x, NormalPos.y, 30));
            if (Input.GetKey(KeyCode.Tab))
            {
                Object.GetComponent<Renderer>().material.color = Color.white;
                Object.AddComponent<WebRequester>();
                var CanvasObj = new GameObject("CanvasObj");
                CanvasObj.transform.SetParent(Object.transform, false);
                CanvasObj.AddComponent<Canvas>();
                CanvasObj.GetComponent<Canvas>().AddComponent<Text>();
                CanvasObj.GetComponent<Canvas>().GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                CanvasObj.GetComponent<Canvas>().GetComponent<Text>().color = Color.black;
                CanvasObj.transform.Rotate(90, 180, 180);
                CanvasObj.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y + 10, Object.transform.position.z);
                CanvasObj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                Object = null;
            }
            else if (Input.GetKey(KeyCode.Return))
            {
                GameObject.Destroy(Object);
                Object = null;
            }
        }
    }

    void MakeShape(PrimitiveType shape)
    {
        GameObject.Destroy(Object);
        Object = GameObject.CreatePrimitive(shape);
        Object.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.33f);
    }
}
