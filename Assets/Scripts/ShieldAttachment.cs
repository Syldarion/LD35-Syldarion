using UnityEngine;
using System.Collections;

public class ShieldAttachment : Attachment
{
    public ShieldAttachment()
    {
        Class = AttachmentClass.Defense;
        AttachmentName = "Shield";
        BuildCost = 1000;
        IsAttached = false;
        OnAttachEffect = "\tAllows your robot to deflect shots from behind";
        OnDisassembleEffect = "\tGrants a permanent +1 armor boost";
        Level = 1;
        OneTimeActivated = false;
    }

    public override void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        ShieldAttachment new_attachment = new ShieldAttachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public override void Deconstruct()
    {
        Player.Instance.Armor++;

        if (AttachedTo.GetComponent<BoxCollider2D>())
            Player.Destroy(AttachedTo.GetComponent<BoxCollider2D>());

        base.Deconstruct();
    }

    public override void ExecuteFunction()
    {
        if (OneTimeActivated)
            return;

        BoxCollider2D back_shield = AttachedTo.gameObject.AddComponent<BoxCollider2D>();
        back_shield.offset = new Vector2(0.0f, -2.0f);
        back_shield.size = new Vector2(1.0f, 5.0f);
        back_shield.isTrigger = true;

        base.ExecuteFunction();
    }

    public override void LevelUp()
    {
        if (Player.Instance.Parts < BuildCost || Level >= 1)
            return;



        base.LevelUp();
    }
}