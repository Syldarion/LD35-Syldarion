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

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        AOEAttachment new_attachment = new AOEAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        base.Deconstruct();
    }
}