using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapUI : MonoBehaviour
{
    public GameObject actionCard;

    public void CreateCard(Queue<GAction> actionQueue)
    {
        foreach(GAction action in actionQueue)
        {
            Instantiate(action.actionCard, transform);
        }
        
    }
}
