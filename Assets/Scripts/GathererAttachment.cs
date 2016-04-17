using UnityEngine;
using System.Collections;

public class GathererAttachment : Attachment
{
    public GathererAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Gatherer";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        GathererAttachment new_attachment = new GathererAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        base.Deconstruct();
    }
}