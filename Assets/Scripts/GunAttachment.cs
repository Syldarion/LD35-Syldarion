using UnityEngine;
using System.Collections;

public class GunAttachment : Attachment
{
	void Start()
	{
        Class = AttachmentClass.Attack;
	}
	
	void Update()
	{
		
	}

    public override void Deconstruct()
    {
        Player.Instance.DamageModifier++;

        base.Deconstruct();
    }
}