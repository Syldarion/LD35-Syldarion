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
        OnAttachEffect = "\tAllows the player to have a large temporary speed boost";
        OnDisassembleEffect = "\tGrants a permanent +1 movement speed boost";

        OneTimeActivated = false;
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
        Player.Instance.Speed++;

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



        base.LevelUp();
    }
}