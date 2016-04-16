using UnityEngine;
using System.Collections;

public class GathererAttachment : Attachment
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
        base.Deconstruct();
    }
}