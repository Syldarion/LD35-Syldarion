using UnityEngine;
using System.Collections;

public class GunAttachment : Attachment
{
    public float AttackRadius;
    public int AttackDamage;

    public GunAttachment()
    {
        Class = AttachmentClass.Attack;
        AttachmentName = "Gun";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tAllows your robot to shoot enemies";
        OnDisassembleEffect = "\tGrants a permanent +1 damage boost";
        Level = 1;
        OneTimeActivated = false;

        AttackRadius = 20.0f;
        AttackDamage = 5;
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        GunAttachment new_attachment = new GunAttachment();
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
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        AttackDamage += 2;

        base.LevelUp();
    }
}