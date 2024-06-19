using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class PointReciving : MonoBehaviour
{
    public int InstanceID;
    public string ShapeData;
    public List<string> Shapes;
    public GameObject ShapeHelper;
    public bool LocalHost = true;
    public string URL;

    // Start is called before the first frame update
    void Start()
    {
        if (!LocalHost)
            URL = "<Removed>";
        else
            URL = "https://localhost:5269/api/Floorspaces/";

        ShapeHelper = GameObject.Find("ShapeHelper");
        InstanceID = 4; // Replace with JS call to check company Instance ID
        StartCoroutine(GetPoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetPoints()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL + "DownloadPoints/" + "4/");
        www.certificateHandler = new CertOverwrite();
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log("Network Error Getting Points");
        }
        else
        {
            ShapeData = www.downloadHandler.text;
            Shapes = ShapeData.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        foreach(var shape in Shapes)
        {
            GameObject.Find("ShapeMaker").GetComponent<ShapezScriptIntrepter>().CreateShape(ShapeHelper.GetComponent<ShapeClass>().DataToObject(shape));
        }
    }
}

    public class CertOverwrite : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }

    }
