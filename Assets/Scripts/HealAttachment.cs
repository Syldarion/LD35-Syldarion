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

    public override void Deconstruct()
    {
        Player.Instance.Health += 10;

        base.Deconstruct();
    }
}