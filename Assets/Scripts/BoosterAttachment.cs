using UnityEngine;
using System.Collections;

public class BoosterAttachment : Attachment
{
    public int MovementSpeedBoost;

    public BoosterAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Booster";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        MovementSpeedBoost = 1;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = "\tAllows player to sprint with the shift key";
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} movement speed boost", MovementSpeedBoost);
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        BoosterAttachment new_attachment = new BoosterAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.Speed += MovementSpeedBoost;

        AttachedTo.Owner.CanSprint = false;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        if (OneTimeActivated)
            return;

        AttachedTo.Owner.CanSprint = true;

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 1)
            return;

        MovementSpeedBoost++;

        UpdateText();

        base.LevelUp();
    }
}