using UnityEngine;
using System.Collections;

public class HealAttachment : Attachment
{
    public int HealAmount;

    public HealAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Heal";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tYour robot will periodically heal you";
        OnDisassembleEffect = "\tGrants a permanent +10 to health";

        OneTimeActivated = false;

        HealAmount = 5;
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
        Player.Instance.Health += 10;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        AttachedTo.Owner.Health = Mathf.Clamp(AttachedTo.Owner.Health + HealAmount, 0, AttachedTo.Owner.MaxHealth);

        base.ExecuteFunction();
    }
}