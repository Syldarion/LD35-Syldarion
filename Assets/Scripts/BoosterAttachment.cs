﻿using UnityEngine;
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

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        if (OneTimeActivated)
            return;

        //activate bool to allow sprinting

        base.ExecuteFunction();
    }
}