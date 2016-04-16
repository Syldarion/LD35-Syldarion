using UnityEngine;
using System.Collections;

public class Attachment
{
    public enum AttachmentClass
    {
        Attack = 1,
        Defense = 2,
        Utility = 4
    }
    public AttachmentClass Class;

    public string AttachmentName;
    public int BuildCost;
    
    public Attachment()
    {

    }

    public Attachment(AttachmentClass a_class, string name, int cost)
    {
        Class = a_class;
        AttachmentName = name;
        BuildCost = cost;
    }

    public Attachment(Attachment copy)
    {
        Class = copy.Class;
        AttachmentName = copy.AttachmentName;
        BuildCost = copy.BuildCost;
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