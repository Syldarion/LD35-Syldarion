using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryDropObject : MonoBehaviour, IDropHandler
{
    public RectTransform Container;

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}

    public void OnDrop(PointerEventData eventData)
    {
        if (DraggableObject.DragObject)
        {
            DraggableObject.DragObject.transform.SetParent(Container, false);
            DraggableObject.DragObject.GetComponent<CanvasGroup>().alpha = 1;
            DraggableObject.DragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

            AttachmentReferencePanel arp = DraggableObject.DragObject.GetComponent<AttachmentReferencePanel>();

            if(arp)
            {
                AttachmentManager.Instance.DetachAttachment(arp.ReferenceAttachment);
            }
        }
    }
}