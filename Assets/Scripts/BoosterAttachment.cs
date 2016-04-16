using UnityEngine;
using System.Collections;

public class BoosterAttachment : Attachment
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
        Player.Instance.Speed++;

        base.Deconstruct();
    }
}