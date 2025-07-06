using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSystem : MonoBehaviour
{
    GAgent lastClicked;

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
            if (hitObject.collider.gameObject.TryGetComponent<GAgent>(out GAgent agentScript))
            {
                lastClicked?.HideUI();
                lastClicked = agentScript;
                agentScript.ShowUI();
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
