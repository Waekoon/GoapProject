using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoalCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]public int siblingIndex;
    [HideInInspector]public Transform originalParent;
    
    Vector3 defaultPos;
    Canvas canva;
    CanvasGroup canvasGroup;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canva = GameObject.FindWithTag("Canva").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        siblingIndex = transform.GetSiblingIndex();
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
        transform.SetSiblingIndex(siblingIndex);

        canvasGroup.blocksRaycasts = true;
    }

    public void SetOriginalParent(Transform parent)
    {
        originalParent = parent;
    }

    public void ChangeDefaultPos(Vector3 pos)
    {
        defaultPos = pos;
    }
}
