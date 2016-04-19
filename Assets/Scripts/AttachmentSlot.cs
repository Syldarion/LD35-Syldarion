using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AttachmentSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public AttachmentReferencePanel CurrentAttachmentPanel;

    public int SlotNumber;

	void Start()
	{
        CurrentAttachmentPanel = null;
	}
	
	void Update()
	{
		
	}

    public void OnDrop(PointerEventData eventData)
    {
        if(DraggableObject.DragObject && transform.childCount == 0)
        {
            DraggableObject.DragObject.transform.SetParent(transform, false);
            DraggableObject.DragObject.transform.localPosition = Vector2.zero;
            DraggableObject.DragObject.transform.rotation = Quaternion.identity;
            DraggableObject.DragObject.GetComponent<RectTransform>().sizeDelta = new Vector2(180, 100);
            DraggableObject.DragObject.GetComponent<CanvasGroup>().alpha = 0;
            DraggableObject.DragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            AttachmentReferencePanel arp = DraggableObject.DragObject.GetComponent<AttachmentReferencePanel>();

            if (arp)
            {
                AttachmentManager.Instance.DetachAttachment(arp.ReferenceAttachment);
                AttachmentManager.Instance.AttachAttachment(arp.ReferenceAttachment, SlotNumber);
            }
        }
        else if(DraggableObject.DragObject)
        {
            DraggableObject.DragObject.GetComponent<CanvasGroup>().alpha = 0;
            DraggableObject.DragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.MyCompanion.Attachments[SlotNumber] != null)
            AttachmentManager.Instance.LoadAttachment(Player.Instance.MyCompanion.Attachments[SlotNumber]);
    }
}