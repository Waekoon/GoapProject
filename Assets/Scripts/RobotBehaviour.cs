using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{ 
    public GameObject configUI;
    Canvas canva;
    GameObject UIInstance;

    private void Awake()
    {
        canva = GameObject.FindWithTag("Canva").GetComponent<Canvas>();
        UIInstance = Instantiate(configUI, canva.transform);
        //UIInstance.GetComponent<GoapUI>().Setup(this);
        HideUI();
    }


    public void ShowUI()
    {
        UIInstance.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        UIInstance.gameObject.SetActive(false);
    }
}
