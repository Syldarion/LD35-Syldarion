using UnityEngine;
using System.Collections;

public class ThornAttachment : Attachment
{
    public float PushbackRadius;
    public float PushbackForce;

    public ThornAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Thorns";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tPeriodically knocks enemies away from the player";
        OnDisassembleEffect = "\tGrants a permanent +1 damage reflection boost";

        PushbackRadius = 5.0f;
        PushbackForce = 5.0f;
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        ThornAttachment new_attachment = new ThornAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.DamageReflection++;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        foreach(Enemy enemy in LevelGenerator.Instance.Enemies)
            if (Vector2.Distance(AttachedTo.Owner.transform.position, enemy.transform.position) <= PushbackRadius)
                enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - AttachedTo.Owner.transform.position).normalized * PushbackForce, ForceMode2D.Impulse);
    }
}