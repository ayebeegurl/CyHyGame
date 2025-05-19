using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public float swipeThreshold = 2f; 

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        Vector3 currentMousePosition = GetMousePositionInWorldSpace();
        transform.position = new Vector3(currentMousePosition.x, startDragPosition.y, startDragPosition.z);
    }

    private void OnMouseUp()
    {
        float movedX = transform.position.x - startDragPosition.x;

        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (Mathf.Abs(movedX) >= swipeThreshold)
        {
            if (hitCollider != null && hitCollider.TryGetComponent(out IcardDropArea cardDropArea))
            {
                cardDropArea.OnCardDrop(this);
                gameObject.SetActive(false); 
            }
            else
            {
                transform.position = startDragPosition; 
            }
        }
        else
        {
            transform.position = startDragPosition; 
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
