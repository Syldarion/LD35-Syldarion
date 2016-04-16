using UnityEngine;
using System.Collections;

public class JumpJetAttachment : Attachment
{
	void Start()
	{
        Class = AttachmentClass.Utility;
	}
	
	void Update()
	{
		
	}

    public override void Deconstruct()
    {
        Player.Instance.JumpForce++;

        base.Deconstruct();
    }
}