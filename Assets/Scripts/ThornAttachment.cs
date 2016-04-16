using UnityEngine;
using System.Collections;

public class ThornAttachment : Attachment
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
        base.Deconstruct();
    }
}