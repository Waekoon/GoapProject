using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public GameObject configUI;
    GameObject UIInstance;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Canvas canva = GameObject.FindWithTag("Canva").GetComponent<Canvas>();
        UIInstance = Instantiate(configUI, canva.transform);

        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        
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
