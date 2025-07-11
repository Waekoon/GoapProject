using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class EventCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum CardType { Action, Goal }
    public CardType cardType;
    Canvas canva;
    CanvasGroup canvasGroup;
    Vector3 defaultPos;
    Transform originalParent;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = transform.position;
        canva = GameObject.FindWithTag("Canva").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canva.transform);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.position = defaultPos;
        canvasGroup.blocksRaycasts = true;
    }

    public void ChangeDefaultPos(Vector3 pos)
    {
        defaultPos = pos;
    }

    public void SetOriginalParent(Transform parent)
    {
        originalParent = parent;
        transform.SetParent(parent);
    }
}
