using UnityEngine;
using System.Collections;

public class SwordAttachment : Attachment
{
    public float AttackRadius;
    public int AttackDamage;
    public int DamageBoost;

    public SwordAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "Sword";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        AttackRadius = 2.0f;
        AttackDamage = 15;
        DamageBoost = 1;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = string.Format("\tDeals {0} damage per second to enemies in a 2 unit radius", AttackDamage);
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} damage boost", DamageBoost);
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        SwordAttachment new_attachment = new SwordAttachment();
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
            if (Vector2.Distance(AttachedTo.Owner.transform.position, enemy.transform.position) <= AttackRadius)
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