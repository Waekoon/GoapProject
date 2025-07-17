using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoalSlot : MonoBehaviour, IDropHandler
{
    public Transform content;
    public GoapUI goapUI;

    GameObject goalCard;

    private void Awake()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        GoalCard card = eventData.pointerDrag.GetComponent<GoalCard>();
        if (card == null) return;

        card.SetOriginalParent(content);
        card.transform.SetParent(content);
        card.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        card.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        card.ChangeDefaultPos(card.transform.position);

        goalCard = card.gameObject;
    }
}
