using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCount : MonoBehaviour
{
    public TMP_Text states;

    private void LateUpdate()
    {
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach(KeyValuePair<string, int> s in worldstates)
        {
            states.text += s.Key + ": " + s.Value + "\n";
        }
    }
}
