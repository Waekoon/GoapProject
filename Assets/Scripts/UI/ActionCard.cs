using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ActionCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum CardType { Action, Goal }
    public CardType cardType;
    public int siblingIndex;

    Canvas canva;
    CanvasGroup canvasGroup;
    Transform originalParent;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        SetCorrectSilbingPos(transform.position);

        canvasGroup.blocksRaycasts = true;
    }

    public int SetCorrectSilbingPos(Vector3 pos)
    {
        int closestIndex = 0;
        bool isOnTheLeft = true;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform sibling = transform.parent.GetChild(i);
            if (sibling == this.transform) continue;

            float dist = pos.x - sibling.position.x;
            if (Mathf.Abs(dist) < closestDistance)
            {
                closestDistance = dist;
                closestIndex = i;

                if (dist < 0)
                {
                    isOnTheLeft = true;
                } else
                {
                    isOnTheLeft = false;
                }
            }
        }

        if (isOnTheLeft == true)
        {
            siblingIndex = closestIndex;

        } else
        {
            siblingIndex = closestIndex + 1;
        }

        transform.SetSiblingIndex(siblingIndex);
        return siblingIndex;
    }

    public void SetOriginalParent(Transform parent)
    {
        originalParent = parent;
    }
}
