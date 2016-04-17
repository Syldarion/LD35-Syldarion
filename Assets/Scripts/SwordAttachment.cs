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

    public override void Deconstruct()
    {
        Player.Instance.DamageModifier++;

        base.Deconstruct();
    }
}