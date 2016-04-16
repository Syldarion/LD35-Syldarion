using UnityEngine;
using System.Collections;

public class ShieldAttachment : Attachment
{
	void Start()
	{
        Class = AttachmentClass.Defense;
	}
	
	void Update()
	{
		
	}

    public override void Deconstruct()
    {
        Player.Instance.Armor++;

        base.Deconstruct();
    }
}