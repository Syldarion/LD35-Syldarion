using UnityEngine;
using System.Collections;

public class JumpJetAttachment : Attachment
{
    public JumpJetAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Jump Jet";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "";
        OnDisassembleEffect = "";
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        JumpJetAttachment new_attachment = new JumpJetAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.JumpForce++;

        base.Deconstruct();
    }
}