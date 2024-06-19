using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Right_Menu_Button : MonoBehaviour
{
    public GameObject Panel;
    public GameObject button;
    public bool Panel_Extended;
    // Start is called before the first frame update
    void Start()
    {
        Panel_Extended = false;
        Panel = GameObject.Find("RightPanel");
        button = transform.gameObject;
        button.GetComponent<Button>().onClick.AddListener(ButtonHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonHandler()
    {
        Panel_Extended = !Panel_Extended;
        if (Panel_Extended)
        {
            Panel.transform.position = new Vector3(Panel.transform.position.x - 200, Panel.transform.position.y);
            button.transform.position = new Vector3(button.transform.position.x - 200, button.transform.position.y, button.transform.position.z);
        }
        else
        {
            Panel.transform.position = new Vector3(Panel.transform.position.x + 200, Panel.transform.position.y);
            button.transform.position = new Vector3(button.transform.position.x + 200, button.transform.position.y, button.transform.position.z);
        }
    }
}
