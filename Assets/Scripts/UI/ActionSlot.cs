using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    public Transform content;
    public GoapUI goapUI;

    int droppedCardSiblingIndex;
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

        droppedCardSiblingIndex = CheckSiblingIndex(card.transform);
        bool is_new_card = false;

        if ((card.originalParent == content) == false) is_new_card = true;

        if (is_new_card)
        {
            if (ArePreConditionsMet(droppedCardSiblingIndex, action))
            {
                card.originalParent = content;
                card.transform.SetParent(content);
                card.siblingIndex = droppedCardSiblingIndex;
                card.transform.SetSiblingIndex(droppedCardSiblingIndex);
                AddNewStateSection(droppedCardSiblingIndex, action);
            }
            ShowStatesList();
        } else
        {
            card.transform.SetParent(card.originalParent);
            card.transform.SetSiblingIndex(card.siblingIndex);
            Debug.Log("original: " + card.transform.GetSiblingIndex());
            Debug.Log("new: " + droppedCardSiblingIndex);
            
            if (card.transform.GetSiblingIndex() > droppedCardSiblingIndex)
            {
                if (ArePreConditionsMet(droppedCardSiblingIndex, action))
                {
                    card.siblingIndex = droppedCardSiblingIndex;
                    card.transform.SetSiblingIndex(droppedCardSiblingIndex);
                    UpdateStatesSection(droppedCardSiblingIndex);
                    
                    ShowStatesList();
                }
            } else if (card.transform.GetSiblingIndex() < droppedCardSiblingIndex)
            {
                if (ArePreviousCardsMatched(droppedCardSiblingIndex, action))
                {
                    card.siblingIndex = droppedCardSiblingIndex;
                    card.transform.SetSiblingIndex(droppedCardSiblingIndex);
                    UpdateStatesSection(droppedCardSiblingIndex);

                    ShowStatesList();
                }
            }
            
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
            if (!statesSection[siblingIndex].IsConditionMatch(condition.Key, condition.Value)) { return false; }
        }

        return true;
    }

    bool ArePreviousCardsMatched(int new_siblingIndex, GAction action)
    {
        for (int i = action.transform.GetSiblingIndex() + 1; i <= new_siblingIndex; i++)
        {
            WorldStates new_state = new WorldStates(statesSection[i]);
            GAction action_to_check = content.GetChild(i).GetComponent<GAction>();
            foreach (KeyValuePair<string, int>  effect in action.effects)
            {
                new_state.ModifyState(effect.Key, -(effect.Value));
            }
            foreach(KeyValuePair<string, int> condition in action_to_check.preconditions)
            {
                if (!new_state.IsConditionMatch(condition.Key, condition.Value)) { return false; }
            } 
        }

        return true;
    }

    void AddNewStateSection(int siblingIndex, GAction action)
    {
        List<WorldStates> tempStatesSection = new List<WorldStates>();

        WorldStates newState = new WorldStates();
        statesSection.Add(newState);
        int i = 0;

        for (i = 0; i <= siblingIndex; i++)
        {
            tempStatesSection.Add(new WorldStates(statesSection[i]));
        }

        for (int j = i; j < statesSection.Count; j++)
        {
            tempStatesSection.Add(new WorldStates(statesSection[j - 1]));
            foreach (KeyValuePair<string, int> effect in action.effects)
            {
                tempStatesSection[j].ModifyState(effect.Key, effect.Value);
            }
        }

        statesSection = tempStatesSection;
    }

    void UpdateStatesSection(int siblingIndex)
    {
        List<WorldStates> tempStatesSection = new List<WorldStates>();

        tempStatesSection.Add(new WorldStates(statesSection[0]));
        
        int i = 1;
        foreach(Transform card in content)
        {
            GAction action = card.GetComponent<GAction>();

            tempStatesSection.Add(new WorldStates(tempStatesSection[i - 1]));
            
            foreach (KeyValuePair<string, int> effect in action.effects)
            {
                tempStatesSection[i].ModifyState(effect.Key, effect.Value);
            }

            i++;
        }    

        statesSection = tempStatesSection;
    }

    void ShowStatesList()
    {
        int i = 0;
        foreach(WorldStates states in statesSection)
        {
            Debug.Log("Section " + i + ":");
            states.ShowStates();
            i++;
        }
    }
}
