using UnityEngine;
using System.Collections;

public class HealAttachment : Attachment
{
    public HealAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Heal";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        HealAttachment new_attachment = new HealAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.Health += 10;

        base.Deconstruct();
    }
}