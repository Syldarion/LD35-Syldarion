using UnityEngine;
using System.Collections;

public class BoosterAttachment : Attachment
{
    public BoosterAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Booster";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void Deconstruct()
    {
        Player.Instance.Speed++;

        base.Deconstruct();
    }
}