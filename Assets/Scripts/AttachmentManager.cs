using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class AttachmentManager : MonoBehaviour
{
    [HideInInspector]
    public static AttachmentManager Instance;

    public GameObject AttachmentReferencePanelPrefab;

    public Attachment CurrentlyLoadedAttachment;

    public Text AttachmentNameText;
    public Text AttachmentCostText;
    public Image AttachmentClassImage;
    public Text AttachmentOnAttachText;
    public Text AttachmentOnDisassembleText;
    
    public AttachmentSlot[] Slots;
    public RectTransform InventoryContainer;

    public Sprite[] ClassSprites; //0 - attack, 1 - defense, 2 - utility, 3 - none

    public bool is_open;

    void Start()
	{
        Instance = this;

        is_open = false;
	}
	
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.O) && !is_open && !PanelUtilities.PanelOpen)
            OpenAttachmentManager();
        else if (Input.GetKeyDown(KeyCode.Escape) && is_open)
            CloseAttachmentManager();
	}

    public void OpenAttachmentManager()
    {
        is_open = true;

        foreach(Attachment attachment in Player.Instance.Inventory)
        {
            AttachmentReferencePanel new_panel = Instantiate(AttachmentReferencePanelPrefab).GetComponent<AttachmentReferencePanel>();
            new_panel.transform.SetParent(InventoryContainer, false);
            new_panel.Initialize(attachment);
        }

        //for (int i = 0; i < 3; i++)
        //    if (Player.Instance.MyCompanion.Attachments[i] != null)
        //    {
        //        AttachmentReferencePanel new_panel = Instantiate(AttachmentReferencePanelPrefab).GetComponent<AttachmentReferencePanel>();
        //        new_panel.transform.SetParent(Slots[i].transform, false);
        //        new_panel.Initialize(Player.Instance.MyCompanion.Attachments[i]);
        //        new_panel.GetComponent<CanvasGroup>().alpha = 0;
        //        new_panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //    }

        LoadAttachment(new Attachment());

        PanelUtilities.ActivatePanel(GetComponent<CanvasGroup>());
    }

    public void CloseAttachmentManager()
    {
        is_open = false;

        foreach (Transform child in InventoryContainer.transform)
            Destroy(child.gameObject);

        //for (int i = 0; i < 3; i++)
        //    if (Slots[i].transform.childCount > 0)
        //        Destroy(Slots[i].transform.GetChild(0).gameObject);

        PanelUtilities.DeactivatePanel(GetComponent<CanvasGroup>());
    }

    public void LoadAttachment(Attachment attachment)
    {
        CurrentlyLoadedAttachment = attachment;

        AttachmentNameText.text = CurrentlyLoadedAttachment.AttachmentName;
        AttachmentCostText.text = CurrentlyLoadedAttachment.BuildCost.ToString();
        
        switch(CurrentlyLoadedAttachment.Class)
        {
            case Attachment.AttachmentClass.Attack:
                AttachmentClassImage.sprite = ClassSprites[0];
                break;
            case Attachment.AttachmentClass.Defense:
                AttachmentClassImage.sprite = ClassSprites[1];
                break;
            case Attachment.AttachmentClass.Utility:
                AttachmentClassImage.sprite = ClassSprites[2];
                break;
            case Attachment.AttachmentClass.None:
            default:
                AttachmentClassImage.sprite = ClassSprites[3];
                break;
        }

        AttachmentOnAttachText.text = string.Format("On Attach:\n{0}", CurrentlyLoadedAttachment.OnAttachEffect);
        AttachmentOnDisassembleText.text = string.Format("On Disassemble:\n{0}", CurrentlyLoadedAttachment.OnDisassembleEffect);
    }

    public void AttachAttachment(Attachment attachment, int slot)
    {
        Player.Instance.Inventory.Remove(attachment);
        Player.Instance.MyCompanion.AddAttachment(attachment, slot);

        switch(attachment.Class)
        {
            case Attachment.AttachmentClass.Attack:
                Slots[slot].GetComponent<Image>().color = Color.red;
                break;
            case Attachment.AttachmentClass.Defense:
                Slots[slot].GetComponent<Image>().color = Color.green;
                break;
            case Attachment.AttachmentClass.Utility:
                Slots[slot].GetComponent<Image>().color = Color.blue;
                break;
            case Attachment.AttachmentClass.None:
            default:
                Slots[slot].GetComponent<Image>().color = Color.white;
                break;
        }
    }

    public void DetachAttachment(Attachment attachment)
    {
        for (int i = 0; i < 3; i++)
        {
            if(Player.Instance.MyCompanion.Attachments[i] == attachment)
            {
                Player.Instance.AddToInventory(attachment);
                Player.Instance.MyCompanion.Attachments[i] = null;
                attachment.OneTimeActivated = false;

                Slots[i].GetComponent<Image>().color = Color.white;

                return;
            }
        }
    }

    public void OnIconHover()
    {
        Tooltip.Instance.EnableTooltip(true);
        Tooltip.Instance.UpdateTooltip(Enum.GetName(typeof(Attachment.AttachmentClass), CurrentlyLoadedAttachment.Class));
    }
}