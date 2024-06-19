using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public Button Save;
    public bool LocalHost = true;
    public string URL;
    // Start is called before the first frame update
    void Start()
    {
        if (!LocalHost)
            URL = "<Removed>";
        else
            URL = "https://localhost:5269/api/Floorspaces/";


        Save = GetComponent<Button>();
        Save.onClick.AddListener(SavePoints);
    }

    void SavePoints()
    {
        var ShapeModelHelper = GameObject.Find("ShapeModelHelper").GetComponent<ShapeModel>();
        var ShapeModelConvert = GameObject.Find("ShapeModelConvert").GetComponent<ShapeClass>();
        string JSONDATA = "";

        foreach (var Room in GameObject.FindGameObjectsWithTag("Room"))
        {
            var child = Room.transform.GetChild(0);
            var Lines = child.GetComponent<LineRenderer>();

            var PointArray = new Vector3[Lines.positionCount];
            Lines.GetPositions(PointArray);
            var RoomID = Room.GetComponent<InputField>().text;

            ShapeModelHelper.AddPoints(PointArray);
            ShapeModelHelper.RoomID = RoomID;
            JSONDATA += ShapeModelConvert.ObjectToData(ShapeModelHelper);

            ShapeModelHelper.Points = null;
            ShapeModelHelper.RoomID = null;
            JSONDATA += "|";
        }

        StartCoroutine(SendPoints(JSONDATA));
        return;
    }

    IEnumerator SendPoints(string JSONDATA)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL + "SavePoints/" + 4 + "/" + JSONDATA);
        www.certificateHandler = new CertOverwrite();
        yield return www.SendWebRequest();
    }

    public class CertOverwrite : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
