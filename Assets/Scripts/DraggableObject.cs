using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject DragObject;

    public static Vector3 StartPosition;
    public static Transform StartParent;

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragObject = gameObject;
        StartPosition = transform.position;
        StartParent = transform.parent;

        transform.SetParent(GameObject.Find("Canvas").transform);

        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.rotation = Quaternion.identity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == StartParent || transform.parent == GameObject.Find("Canvas").transform)
        {
            transform.SetParent(StartParent, false);
            transform.localPosition = Vector2.zero;
        }
    }
}