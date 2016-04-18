using UnityEngine;
using System.Collections;

public class GathererAttachment : Attachment
{
    public int GatheringPercentBoost;

    public GathererAttachment()
    {
        Class = AttachmentClass.Utility;
        AttachmentName = "Gatherer";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        GatheringPercentBoost = 10;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = "\tEnemies have a chance to drop double parts";
        OnDisassembleEffect = string.Format("\tGrants a +{0}% boost to part gathering", GatheringPercentBoost);
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
        Player.Instance.PartDropModifier += (float)GatheringPercentBoost / 100;

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

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        GatheringPercentBoost += 10;

        UpdateText();

        base.LevelUp();
    }
}