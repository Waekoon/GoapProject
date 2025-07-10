using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    bool isOccupied = false;
    public GoapUI goapUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (isOccupied) return;

        EventCard card = eventData.pointerDrag.GetComponent<EventCard>();
        if (card != null)
        {
            card.SetOriginalParent(transform);
            card.transform.parent = transform;
            card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            card.ChangeDefaultPos(card.transform.position);

            if (card.cardType == CardType.Goal)
            {
                goapUI.GetAgent().SetGoal(new SubGoal("ResourceRetrieved", 3, false));
            }

            isOccupied = true;
        }
    }
}
