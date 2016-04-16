using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static Player Instance;

    public Text PartCountText;

    public float Speed;
    public float JumpForce;
    public float JumpCooldownTimer;
    public bool IsGrounded;

    public Companion MyCompanion;
    public List<Attachment> Inventory;

    public int Parts;

    public int Health;
    public int Armor;
    public int DamageModifier;

	void Start()
	{
        Instance = this;
	}

	void Update()
	{
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0.0f));

        JumpCooldownTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && JumpCooldownTimer <= 0.0f && IsGrounded)
        {
            JumpCooldownTimer = 1.0f;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);

            IsGrounded = false;
        }

        Camera.main.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Platform(Clone)")
            IsGrounded = true;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
    }

    public void AddToInventory(Attachment attachment)
    {
        Inventory.Add(attachment);
    }

    public void AddAttachmentToCompanion(Attachment attachment, int slot)
    {
        if (!Inventory.Contains(attachment))
            return;

        Inventory.Remove(attachment);
        MyCompanion.AddAttachment(attachment, slot);
    }

    public void AddParts(int parts)
    {
        Parts += parts;
        PartCountText.text = Parts.ToString();
    }
}