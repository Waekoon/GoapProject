using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapUI : MonoBehaviour
{
    List<GameObject> currentActions = new List<GameObject>();

    public float halfWidth;
    RectTransform rectTransform;
    Vector2 leftBound;
    Vector2 rightBound;

    GAgent agent;

    public void Setup(GAgent agent)
    {
        this.agent = agent;
        rectTransform = GetComponent<RectTransform>();
        leftBound = new Vector2(-halfWidth + 200, 50);
        rightBound = new Vector2(halfWidth - 200, 50);
    }

    public GAgent GetAgent()
    {
        return agent;
    }

    public void CreateCard(Queue<GAction> actionQueue)
    {
        foreach (GameObject obj in currentActions) Destroy(obj);
        currentActions.Clear();

        int i = 0;
        foreach(GAction action in actionQueue)
        {
            GameObject new_action = Instantiate(action.actionCard, transform);
            float t = (float)i / (actionQueue.Count - 1);
            new_action.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(leftBound, rightBound, t);
            Debug.Log(Vector2.Lerp(leftBound, rightBound, t));
            currentActions.Add(new_action);

            i++;
        }
    }
}
