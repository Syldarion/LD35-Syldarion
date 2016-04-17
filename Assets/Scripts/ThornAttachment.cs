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

    public override void Deconstruct()
    {
        base.Deconstruct();
    }
}