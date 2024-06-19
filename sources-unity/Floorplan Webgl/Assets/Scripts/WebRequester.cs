using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequester : MonoBehaviour
{
    public Transform Output;
    public string RoomID;
    public bool LocalHost = true;
    public string URL;

    // Start is called before the first frame update
    void Start()
    {
        if (!LocalHost)
            URL = "<Removed>";
        else
            URL = "https://localhost:5269/api/Floorspaces/";

        Output = gameObject.transform.GetChild(0);
        RoomID = gameObject.name;
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL + "GetLiveMessage/" + "4/" + RoomID);
        www.certificateHandler = new CertOverwrite();
        yield return www.Send();

        if (www.isNetworkError)
        {
            Output.GetComponent<Text>().text = "";
        }
        else
        {
            Output.GetComponent<Text>().text = www.downloadHandler.text;
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(GetText());
    }

    public class CertOverwrite : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
