using UnityEngine;
using System.Collections;

public class HealAttachment : Attachment
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
        Player.Instance.Health += 10;

        base.Deconstruct();
    }
}