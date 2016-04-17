using UnityEngine;
using System.Collections;

public class SwordAttachment : Attachment
{
    public float AttackRadius;
    public int AttackDamage;

    public SwordAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "Sword";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tAllows your robot to attack enemies with a sword";
        OnDisassembleEffect = "\tGrants a permanent +1 damage boost";
        Level = 1;
        OneTimeActivated = false;

        AttackRadius = 2.0f;
        AttackDamage = 15;
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
        Player.Instance.DamageModifier++;

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
        if (Player.Instance.Parts < BuildCost && Level < 3)
            return;

        AttackDamage += 2;

        base.LevelUp();
    }
}