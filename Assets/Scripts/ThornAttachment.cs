using UnityEngine;
using System.Collections;

public class ThornAttachment : Attachment
{
    public float PushbackRadius;
    public float PushbackForce;
    public int DamageReflectionBoost;

    public ThornAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Thorns";
        BuildCost = 500;
        IsAttached = false;
        Level = 1;
        OneTimeActivated = false;

        PushbackRadius = 5.0f;
        PushbackForce = 5.0f;
        DamageReflectionBoost = 1;

        UpdateText();
    }

    void UpdateText()
    {
        OnAttachEffect = string.Format("\tPeriodically knocks enemies in a {0} unity radius away from the player", PushbackRadius);
        OnDisassembleEffect = string.Format("\tGrants a permanent +{0} damage reflection boost", DamageReflectionBoost);
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
        Player.Instance.DamageReflection += DamageReflectionBoost;

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        foreach(Enemy enemy in LevelGenerator.Instance.Enemies)
            if (Vector2.Distance(AttachedTo.Owner.transform.position, enemy.transform.position) <= PushbackRadius)
                enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - AttachedTo.Owner.transform.position).normalized * PushbackForce, ForceMode2D.Impulse);

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 3)
            return;

        PushbackRadius += 2.0f;
        DamageReflectionBoost++;

        UpdateText();

        base.LevelUp();
    }
}