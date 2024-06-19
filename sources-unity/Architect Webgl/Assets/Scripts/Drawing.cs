using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour
{
    public LineRenderer CurrentLine = new LineRenderer();
    public Material Mat;
    public Camera Camera;
    public GameObject LineObject;
    public EventSystem UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ResetLine();
    }

    void ResetLine()
    {
        LineObject = new GameObject();
        LineObject.name = "Room";
        CurrentLine = LineObject.AddComponent<LineRenderer>();
        CurrentLine.material = Mat;
        CurrentLine.startWidth = 0.1f;
        CurrentLine.endWidth = 0.1f;
        CurrentLine.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI.IsPointerOverGameObject())
        {
            CurrentLine.positionCount++;
            var Position = Camera.ScreenToWorldPoint(Input.mousePosition);
            Position = SnapPosition(Position);
            CurrentLine.SetPosition(CurrentLine.positionCount - 1, Position);
            if (IsStart(Position, CurrentLine.GetPosition(0)) && CurrentLine.positionCount != 1)
            {
                FinishLineObject(CurrentLine);
            }
        }
    }

    Vector3 SnapPosition(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z + 5));
    }

    bool IsStart(Vector3 Current, Vector3 Start)
    {
        if (Current == Start)
            return true;
        return false;
    }

    Vector3 AvgPositions(Vector3[] Positions)
    {
        Vector3 Average = Vector3.zero;
        foreach (var Position in Positions)
        {
            Average += Position;
        }
        Average -= Positions[0]; // If we need position 0  to be both the last and the first position added we should only include it once in our average calulations
        return Average / Positions.Length;
    }

    public void FinishLineObject(LineRenderer CurrentLine)
    {
        Vector3[] PositionArray = new Vector3[CurrentLine.positionCount];
        CurrentLine.GetPositions(PositionArray);
        var Center = AvgPositions(PositionArray);

        var ParentObject = new GameObject();
        ParentObject.transform.position = Center;
        ParentObject.name = "Master_Room_Object";

        var CanvasObj = ParentObject.AddComponent<Canvas>();
        CanvasObj.renderMode = RenderMode.WorldSpace;
        CanvasObj.transform.localScale = new Vector3(0.05f, 0.05f, 1);

        CanvasObj.AddComponent<GraphicRaycaster>();

        var Text = CanvasObj.AddComponent<Text>();
        Text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        Text.color = Color.green;
        Text.alignment = TextAnchor.MiddleCenter;

        var Input = CanvasObj.AddComponent<InputField>();
        Input.textComponent = Text;
        Input.text = "Type Room ID";
        Input.ActivateInputField();
        Input.interactable = true;

        LineObject.transform.SetParent(ParentObject.transform);
        ParentObject.tag = "Room";
        ResetLine();
    }

    public void FinishLineObject(LineRenderer CurrentLine, string DefaultID)
    {
        Vector3[] PositionArray = new Vector3[CurrentLine.positionCount];
        CurrentLine.GetPositions(PositionArray);
        var Center = AvgPositions(PositionArray);

        var ParentObject = new GameObject();
        ParentObject.transform.position = Center;
        ParentObject.name = "Master_Room_Object";

        var CanvasObj = ParentObject.AddComponent<Canvas>();
        CanvasObj.renderMode = RenderMode.WorldSpace;
        CanvasObj.transform.localScale = new Vector3(0.05f, 0.05f, 1);

        CanvasObj.AddComponent<GraphicRaycaster>();

        var Text = CanvasObj.AddComponent<Text>();
        Text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        Text.color = Color.green;
        Text.alignment = TextAnchor.MiddleCenter;

        var Input = CanvasObj.AddComponent<InputField>();
        Input.textComponent = Text;
        Input.text = DefaultID;
        Input.ActivateInputField();
        Input.interactable = true;

        LineObject.transform.SetParent(ParentObject.transform);
        ParentObject.tag = "Room";
        ResetLine();
    }
}
