using UnityEngine;
using System.Collections;

public class Attachment
{
    public enum AttachmentClass
    {
        Attack = 1,
        Defense = 2,
        Utility = 4,
        None = 8
    }
    public AttachmentClass Class;

    public string AttachmentName;
    public int BuildCost;

    public bool IsAttached;

    public string OnAttachEffect;
    public string OnDisassembleEffect;

    public int Level;

    public Attachment()
    {
        Class = AttachmentClass.None;
        AttachmentName = "None";
        BuildCost = 0;
        IsAttached = false;
    }

    public void BuildAttachment()
    {
        if (Player.Instance.Parts < BuildCost)
            return;

        Attachment new_attachment = new Attachment();
        Player.Instance.AddToInventory(new_attachment);
        Player.Instance.Parts -= BuildCost;
    }

    public virtual void Deconstruct()
    {

    }
}