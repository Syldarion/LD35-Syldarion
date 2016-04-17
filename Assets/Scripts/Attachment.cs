using UnityEngine;
using System.Collections;

public class Attachment
{
    public Companion AttachedTo;

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

    public virtual void BuildAttachment()
    {
        
    }

    public virtual void Deconstruct()
    {
        Player.Instance.Inventory.Remove(this);
    }

    public virtual void ExecuteFunction()
    {

    }
}