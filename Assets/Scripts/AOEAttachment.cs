using UnityEngine;
using System.Collections;

public class AOEAttachment : Attachment
{
    public int AttackDamage;
    public int DamageBoost;

    public AOEAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "AOE";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        AttackDamage = 5;
        DamageBoost = 1;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = string.Format("\tDeals {0} damage to all enemies in a 10 unit radius", AttackDamage);
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} damage boost", DamageBoost);
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        AOEAttachment new_attachment = new AOEAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.DamageModifier += DamageBoost;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        foreach (Enemy enemy in LevelGenerator.Instance.Enemies)
            if (Vector2.Distance(AttachedTo.Owner.transform.position, enemy.transform.position) <= 10.0f)
                enemy.Damage(AttackDamage);

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        AttackDamage += 2;
        DamageBoost++;

        UpdateText();

        base.LevelUp();
    }
}