using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalButton : MonoBehaviour
{
    public GameObject playerUI;
    bool is_enabled = false;

    public void Pushed()
    {
        if (!is_enabled)
        {
            playerUI.SetActive(true);
            is_enabled = true;
        } else
        {
            playerUI.SetActive(false);
            is_enabled = false;
        }
    }
}
