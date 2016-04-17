using UnityEngine;
using System.Collections;

public class Companion : MonoBehaviour
{
    public Entity Owner;

    public enum CompanionType
    {
        Attack = 3,
        Defense = 6,
        Utility = 12,
        Other = 0
    }
    public CompanionType Type;

    public Attachment[] Attachments;

    public bool PowerActivated;

    public bool Idle;

	void Start()
	{
        Type = CompanionType.Other;
        Attachments = new Attachment[3];
        PowerActivated = false;
        Idle = true;

        StartCoroutine(RunFunctions());
	}
	
	void Update()
	{
        if (Idle)
            transform.localPosition = new Vector2(-1.0f + Mathf.Cos(Time.time) * 0.5f, 1.0f + Mathf.Sin(Time.time) * 0.5f);
	}

    public void SetOwner(Entity new_owner)
    {
        Owner = new_owner;
        if (Owner.MyCompanion)
            Destroy(Owner.MyCompanion);
        Owner.MyCompanion = this;
        transform.SetParent(Owner.transform);
    }

    public void AddAttachment(Attachment attachment, int slot)
    {
        if (Attachments[slot] != null && Owner == Player.Instance)
            Player.Instance.AddToInventory(Attachments[slot]);

        Attachments[slot] = attachment;
        Attachments[slot].AttachedTo = this;

        UpdateType();
    }

    public void ActivatePowerup()
    {
        switch (Type)
        {
            case CompanionType.Attack:
                Owner.StopAllCoroutines();
                Owner.StartCoroutine(Owner.AttackPowerup());
                break;
            case CompanionType.Defense:
                Owner.StopAllCoroutines();
                Owner.StartCoroutine(Owner.DefensePowerup());
                break;
            case CompanionType.Utility:
                Owner.StopAllCoroutines();
                Owner.StartCoroutine(Owner.UtilityPowerup());
                break;
            case CompanionType.Other:
            default:
                return;
        }

        PowerActivated = true;
    }

    public void UpdateType()
    {
        int attachment_type_sum = 0;

        for (int i = 0; i < 3; i++)
        {
            if (Attachments[i] != null)
                attachment_type_sum += (int)Attachments[i].Class;
            else
            {
                Type = CompanionType.Other;
                return;
            }
        }

        if (attachment_type_sum == 3 || attachment_type_sum == 6 || attachment_type_sum == 12)
        {
            Type = (CompanionType)attachment_type_sum;
            if (!PowerActivated)
                ActivatePowerup();
        }
        else
            Type = CompanionType.Other;
    }

    public void EnableSlotTooltip(int slot)
    {
        if(Attachments[slot] != null)
        {
            Tooltip.Instance.EnableTooltip(true);
            Tooltip.Instance.UpdateTooltip(Attachments[slot].AttachmentName);
        }
        else
        {
            Tooltip.Instance.EnableTooltip(true);
            Tooltip.Instance.UpdateTooltip("No attachment");
        }
    }

    IEnumerator RunFunctions()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
                if (Attachments[i] != null)
                    Attachments[i].ExecuteFunction();

            yield return new WaitForSeconds(1.0f);
        }
    }
}