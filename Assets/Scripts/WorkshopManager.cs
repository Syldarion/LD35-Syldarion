using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WorkshopManager : MonoBehaviour
{
    [HideInInspector]
    public static WorkshopManager Instance;

    public GameObject AttachmentReferencePanelPrefab;

    public AttachmentReferencePanel SelectedReferencePanel;
    public Attachment CurrentlyLoadedAttachment;

    public Text AttachmentNameText;
    public Text AttachmentCostText;
    public Image AttachmentClassImage;
    public Text AttachmentOnAttachText;
    public Text AttachmentOnDisassembleText;

    public RectTransform AttachmentContainer;
    public RectTransform InventoryContainer;

    public Sprite[] ClassSprites; //0 - attack, 1 - defense, 2 - utility, 3 - none

    public Attachment[] BaseAttachments; //Attachment order: Gun, Sword, AOE, Heal, Shield, Thorns, Jump Jet, Booster, Gatherer

    public bool is_open;

	void Start()
	{
        Instance = this;

        is_open = false;

        BaseAttachments = new Attachment[9]
        {
            new GunAttachment(),
            new SwordAttachment(),
            new AOEAttachment(),
            new HealAttachment(),
            new ShieldAttachment(),
            new ThornAttachment(),
            new JumpJetAttachment(),
            new BoosterAttachment(),
            new GathererAttachment()
        };

        foreach (Attachment attachment in BaseAttachments)
        {
            AttachmentReferencePanel new_panel = Instantiate(AttachmentReferencePanelPrefab).GetComponent<AttachmentReferencePanel>();
            new_panel.transform.SetParent(AttachmentContainer, false);
            new_panel.Initialize(attachment);
        }
    }
	
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.E) && Player.Instance.AtWorkshop && !is_open && !PanelUtilities.PanelOpen)
            OpenWorkshop();
        if (Input.GetKeyDown(KeyCode.Escape) && is_open)
            CloseWorkshop();
    }

    public void OpenWorkshop()
    {
        is_open = true;

        foreach (Attachment attachment in Player.Instance.Inventory)
        {
            AttachmentReferencePanel new_panel = Instantiate(AttachmentReferencePanelPrefab).GetComponent<AttachmentReferencePanel>();
            new_panel.transform.SetParent(InventoryContainer, false);
            new_panel.Initialize(attachment);
        }

        LoadAttachment(new Attachment());

        PanelUtilities.ActivatePanel(GetComponent<CanvasGroup>());
    }

    public void CloseWorkshop()
    {
        is_open = false;

        foreach (Transform child in InventoryContainer.transform)
            Destroy(child.gameObject);

        PanelUtilities.DeactivatePanel(GetComponent<CanvasGroup>());
    }

    public void LoadAttachment(Attachment attachment)
    {
        CurrentlyLoadedAttachment = attachment;

        AttachmentNameText.text = CurrentlyLoadedAttachment.AttachmentName;
        AttachmentCostText.text = CurrentlyLoadedAttachment.BuildCost.ToString();

        switch (CurrentlyLoadedAttachment.Class)
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

    public void BuildAttachment()
    {
        if (Player.Instance.Inventory.Contains(CurrentlyLoadedAttachment))
            return;

        CurrentlyLoadedAttachment.BuildAttachment();

        Player.Instance.PartCountText.text = Player.Instance.Parts.ToString();

        AttachmentReferencePanel new_panel = Instantiate(AttachmentReferencePanelPrefab).GetComponent<AttachmentReferencePanel>();
        new_panel.transform.SetParent(InventoryContainer, false);
        new_panel.Initialize(Player.Instance.Inventory[Player.Instance.Inventory.Count - 1]);
    }

    public void DisassembleAttachment()
    {
        if (!Player.Instance.Inventory.Contains(CurrentlyLoadedAttachment))
            return;

        Destroy(SelectedReferencePanel.gameObject);

        CurrentlyLoadedAttachment.Deconstruct();
    }

    public void LevelUpAttachment()
    {
        if (!Player.Instance.Inventory.Contains(CurrentlyLoadedAttachment))
            return;

        CurrentlyLoadedAttachment.LevelUp();

        SelectedReferencePanel.Initialize(CurrentlyLoadedAttachment);

        Player.Instance.PartCountText.text = Player.Instance.Parts.ToString();
    }

    public void OnIconHover()
    {
        Tooltip.Instance.EnableTooltip(true);
        Tooltip.Instance.UpdateTooltip(Enum.GetName(typeof(Attachment.AttachmentClass), CurrentlyLoadedAttachment.Class));
    }
}