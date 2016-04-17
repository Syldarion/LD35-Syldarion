using UnityEngine;
using System.Collections;

public class ThornAttachment : Attachment
{
    public ThornAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Thorns";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        ThornAttachment new_attachment = new ThornAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        base.Deconstruct();
    }
}