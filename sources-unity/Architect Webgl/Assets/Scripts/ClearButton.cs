using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    public string URL;
    public bool LocalHost = true;
    public Button Clear;
    // Start is called before the first frame update
    void Start()
    {
        if (!LocalHost)
            URL = "<Removed>";
        else
            URL = "https://localhost:5269/api/Floorspaces/";

        Clear = GetComponent<Button>();
        Clear.onClick.AddListener(ClearPoints);
    }

    void ClearPoints()
    {
        UnityWebRequest www = UnityWebRequest.Delete(URL + "DeletePoints/" + "4");
        www.certificateHandler = new CertOverwrite();
        www.SendWebRequest();
        ReloadScene();
    }
    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public class CertOverwrite : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
