using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;

public class AttachmentReferencePanel : MonoBehaviour, IPointerDownHandler
{
    enum LevelNames
    {
        One = 1,
        Two = 2,
        Three = 3
    }

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
        transform.GetChild(1).GetComponent<Text>().text = string.Format("Level {0}", Enum.GetName(typeof(LevelNames), (LevelNames)ReferenceAttachment.Level));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (AttachmentManager.Instance.is_open)
            AttachmentManager.Instance.LoadAttachment(ReferenceAttachment);
        else if (WorkshopManager.Instance.is_open)
        {
            WorkshopManager.Instance.SelectedReferencePanel = this;
            WorkshopManager.Instance.LoadAttachment(ReferenceAttachment);
        }
    }
}