using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class AttachmentReferencePanel : MonoBehaviour, IPointerDownHandler
{
    public Attachment ReferenceAttachment;

	void Start()
	{

	}
	
	void Update()
	{
	}

    public void Initialize(Attachment reference)
    {
        ReferenceAttachment = reference;

        transform.GetChild(0).GetComponent<Text>().text = ReferenceAttachment.AttachmentName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (AttachmentManager.Instance.is_open)
            AttachmentManager.Instance.LoadAttachment(ReferenceAttachment);
        else if (WorkshopManager.Instance.is_open)
            WorkshopManager.Instance.LoadAttachment(ReferenceAttachment);
    }
}