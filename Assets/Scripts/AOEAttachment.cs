using UnityEngine;
using System.Collections;

public class AOEAttachment : Attachment
{
    public AOEAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "AOE";
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