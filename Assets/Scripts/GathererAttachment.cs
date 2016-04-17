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

    public override void Deconstruct()
    {
        base.Deconstruct();
    }
}