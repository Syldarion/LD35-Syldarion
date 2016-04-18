using UnityEngine;
using System.Collections;

public class JumpJetAttachment : Attachment
{
    public JumpJetAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Jump Jet";
        BuildCost = 500;
        IsAttached = false;
        OnAttachEffect = "\tAllow the player to double jump";
        OnDisassembleEffect = "\tGrants a permanent +2 to jumping power";
        Level = 1;
        OneTimeActivated = false;
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
        if (OneTimeActivated)
            return;

        AttachedTo.Owner.CanDoubleJump = true;

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 1)
            return;

        //increase extra jump count

        base.LevelUp();
    }
}