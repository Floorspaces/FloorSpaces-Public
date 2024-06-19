using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ButtonObject;
    public bool extended;
    // Start is called before the first frame update
    void Start()
    {
        ButtonObject.GetComponent<Button>().onClick.AddListener(PanelToggle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PanelToggle()
    {
        if (!extended)
        {
            Panel.transform.position = new Vector3(Panel.transform.position.x - 200, Panel.transform.position.y);
            ButtonObject.transform.position = new Vector3(ButtonObject.transform.position.x - 200, ButtonObject.transform.position.y);
        }
        else
        {
            Panel.transform.position = new Vector3(Panel.transform.position.x + 200, Panel.transform.position.y);
            ButtonObject.transform.position = new Vector3(ButtonObject.transform.position.x + 200, ButtonObject.transform.position.y);
        }

        extended = !extended;
    }
}
