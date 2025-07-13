using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    public Transform content;
    public GoapUI goapUI;

    int newCardSiblingIndex;
    List<WorldStates> statesSection;
    Queue<GAction> actionQueue;


    private void Awake()
    {
        actionQueue = new Queue<GAction>();
        statesSection = new List<WorldStates>();
        WorldStates FirstStates = new WorldStates();
        statesSection.Add(FirstStates);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ActionCard card = eventData.pointerDrag.GetComponent<ActionCard>();
        GAction action = eventData.pointerDrag.GetComponent<GAction>();
        if (card == null || action == null) return;

        newCardSiblingIndex = CheckSiblingIndex(card.transform);
        if (ArePreConditionsMet(newCardSiblingIndex, action)) 
        {        
            card.SetOriginalParent(content);
            card.siblingIndex = newCardSiblingIndex;
            card.transform.SetSiblingIndex(newCardSiblingIndex);
            
            ApplyEffect(newCardSiblingIndex, action);
        } else
        {
            Debug.Log("Conditions are not met");
        }
    }

    int CheckSiblingIndex(Transform card)
    {
        int closestIndex = 0;
        bool isOnTheLeft = true;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < content.childCount; i++)
        {
            Transform sibling = content.GetChild(i);
            if (sibling == card) continue;

            float dist = card.position.x - sibling.position.x;
            if (Mathf.Abs(dist) < closestDistance)
            {
                closestDistance = dist;
                closestIndex = i;

                if (dist < 0)
                {
                    isOnTheLeft = true;
                }
                else
                {
                    isOnTheLeft = false;
                }
            }
        }

        if (isOnTheLeft == true)
        {
            return closestIndex;

        }
        else
        {
            return closestIndex + 1;
        }
    }

    public void ConfirmAction()
    {
        foreach (Transform child in content)
        {
            GAction action = child.GetComponent<GAction>();          
            actionQueue.Enqueue(action);         
        }

        if (actionQueue.Count != 0)
        {
            goapUI.GetAgent().ManualAssignActions(actionQueue);
        } else
        {
            Debug.Log("No action");
        }
        
    }

    bool ArePreConditionsMet(int siblingIndex ,GAction action)
    {
        foreach (KeyValuePair<string, int> condition in action.preconditions)
        {
            Debug.Log("Has condition: " + condition.Key + ": " + condition.Value);
            Debug.Log("statesSection[" + siblingIndex + "]:");
            foreach (var pair in statesSection[siblingIndex].GetStates())
            {
                Debug.Log(pair.Key);
            }
            if (!statesSection[siblingIndex].HasState(condition.Key)) { return false; }
        }

        return true;
    }

    void ApplyEffect(int siblingIndex, GAction action)
    {
        if (siblingIndex + 1 >= statesSection.Count)
        {
            WorldStates previousStates = statesSection[siblingIndex];
            WorldStates current = new WorldStates(previousStates);
            statesSection.Add(current);
        }

        foreach (KeyValuePair<string, int> effect in action.effects)
        {
            Debug.Log(effect.Key);
            statesSection[siblingIndex + 1].ModifyState(effect.Key, effect.Value);
        }
    }
}
