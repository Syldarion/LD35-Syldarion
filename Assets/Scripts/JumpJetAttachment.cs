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
        OnAttachEffect = "\tAllow the player to double jump";
        OnDisassembleEffect = "\tGrants a permanent +2 to jumping power";
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
        Player.Instance.JumpForce += 2;

        AttachedTo.Owner.CanDoubleJump = false;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        AttachedTo.Owner.CanDoubleJump = true;
    }
}