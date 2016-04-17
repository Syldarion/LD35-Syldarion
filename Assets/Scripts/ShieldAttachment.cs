using UnityEngine;
using System.Collections;

public class ShieldAttachment : Attachment
{
    public ShieldAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Shield";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        ShieldAttachment new_attachment = new ShieldAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.Armor++;

        base.Deconstruct();
    }
}