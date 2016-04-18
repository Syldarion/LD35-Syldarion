using UnityEngine;
using System.Collections;

public class JumpJetAttachment : Attachment
{
    public float JumpingPowerBoost;

    public JumpJetAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Jump Jet";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        JumpingPowerBoost = 2.0f;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = "\tAllow the player to double jump";
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} to jumping power", JumpingPowerBoost);
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
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        JumpingPowerBoost += 2.0f;

        UpdateText();

        base.LevelUp();
    }
}