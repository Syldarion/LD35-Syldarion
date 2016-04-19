using UnityEngine;
using System.Collections;

public class HealAttachment : Attachment
{
    public int HealAmount;

    public HealAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Heal";
        BuildCost = 500;
        IsAttached = false;
        OnAttachEffect = "\tYour robot will periodically heal you";
        OnDisassembleEffect = "\tGrants a permanent +10 to health";
        Level = 1;
        OneTimeActivated = false;

        HealAmount = 1;
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        HealAttachment new_attachment = new HealAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.MaxHealth += 10;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        AttachedTo.Owner.Damage(-HealAmount);

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        HealAmount++;

        base.LevelUp();
    }
}