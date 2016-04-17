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

    public override void Deconstruct()
    {
        Player.Instance.JumpForce++;

        base.Deconstruct();
    }
}