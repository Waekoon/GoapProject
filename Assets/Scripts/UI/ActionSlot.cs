using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    public Transform content;
    public GoapUI goapUI;

    public void OnDrop(PointerEventData eventData)
    {
        ActionCard card = eventData.pointerDrag.GetComponent<ActionCard>();
        if (card != null)
        {
            card.SetOriginalParent(content);
        }
    }
}
