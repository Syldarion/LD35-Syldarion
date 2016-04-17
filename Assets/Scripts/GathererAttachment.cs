using UnityEngine;
using System.Collections;

public class GathererAttachment : Attachment
{
    public GathererAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Gatherer";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tEnemies have a chance to drop double parts";
        OnDisassembleEffect = "\tGrants a +10% boost to part gathering";

        OneTimeActivated = false;
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        GathererAttachment new_attachment = new GathererAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.PartDropModifier += 0.1f;

        AttachedTo.Owner.DoubleDrops = false;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        if (OneTimeActivated)
            return;

        AttachedTo.Owner.DoubleDrops = true;

        base.ExecuteFunction();
    }
}