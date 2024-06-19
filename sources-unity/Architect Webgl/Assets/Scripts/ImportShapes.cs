using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class ImportShapes : MonoBehaviour
{
    public bool LocalHost = true;
    public string URL;
    // Start is called before the first frame update
    void Start()
    {
        if (!LocalHost)
            URL = "<Removed>";
        else
            URL = "https://localhost:5269/api/Floorspaces/";

        StartCoroutine(DownloadShapes());
    }

    IEnumerator DownloadShapes()
    {
        var ShapeModel = GameObject.Find("ShapeModelHelper").GetComponent<ShapeModel>();
        var ShapeConvert = GameObject.Find("ShapeModelConvert").GetComponent<ShapeClass>();

        List<string> Shapes = new List<string>();
        UnityWebRequest www = UnityWebRequest.Get(URL + "DownloadPoints/" + "4/");
        www.certificateHandler = new CertOverwrite();
        yield return www.Send();

        var Creator = GameObject.Find("DrawingObject").GetComponent<Drawing>();
        var Data = www.downloadHandler.text;

        if (Data.Contains("Exception"))
        {
            yield break;
        }
        
        Shapes = Data.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();

        foreach (var Shape in Shapes)
        {
            var Object = ShapeConvert.DataToObject(Shape);
            Creator.CurrentLine.positionCount = Object.Points.Count();
            Creator.CurrentLine.SetPositions(Object.Points);
            Creator.FinishLineObject(Creator.CurrentLine, Object.RoomID);
        }
    }

    public class CertOverwrite : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
