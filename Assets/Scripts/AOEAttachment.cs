using UnityEngine;
using System.Collections;

public class AOEAttachment : Attachment
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
        base.Deconstruct();
    }
}