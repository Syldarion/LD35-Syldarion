using UnityEngine;
using System.Collections;

public class HealAttachment : Attachment
{
    public int HealAmount;
    public int HealthBoostAmount;

    public HealAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Heal";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        HealAmount = 1;
        HealthBoostAmount = 10;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = string.Format("\tYour robot will heal you for {0} every second", HealAmount);
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} to health", HealthBoostAmount);
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

        HealAmount += 1;
        HealthBoostAmount += 10;

        UpdateText();

        base.LevelUp();
    }
}