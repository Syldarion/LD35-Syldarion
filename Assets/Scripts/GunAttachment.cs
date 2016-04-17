using UnityEngine;
using System.Collections;

public class GunAttachment : Attachment
{
    public GunAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "Gun";
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