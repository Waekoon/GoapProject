using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSystem : MonoBehaviour
{
    RobotController lastClicked;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            FireScreenRay();    
        }
    }

    void FireScreenRay()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitObject))
        {
            if (hitObject.collider.gameObject.TryGetComponent<RobotController>(out RobotController robotScript))
            {
                lastClicked?.HideUI();
                lastClicked = robotScript;
                robotScript.ShowUI();
            } else
            {
                lastClicked?.HideUI();
            }
        } else
        {
            lastClicked?.HideUI();
        }
    }
}
