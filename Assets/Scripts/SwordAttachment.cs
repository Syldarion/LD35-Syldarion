using UnityEngine;
using System.Collections;

public class SwordAttachment : Attachment
{
    public SwordAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "Sword";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        SwordAttachment new_attachment = new SwordAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.DamageModifier++;

        base.Deconstruct();
    }
}