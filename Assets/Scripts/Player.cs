using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : Entity
{
    [HideInInspector]
    public static Player Instance;

    public Text PartCountText;
    public List<Attachment> Inventory;

    public float PartDropModifier;

    public bool AtWorkshop;

    Rigidbody2D my_rb;

	void Start()
	{
        Instance = this;
        Inventory = new List<Attachment>();
        AtWorkshop = false;

        PartDropModifier = 1.0f;

        MovingRight = true;

        my_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
        JumpTimer -= Time.deltaTime;
        FireTimer -= Time.deltaTime;

        if (PanelUtilities.PanelOpen)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0))
            Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Camera.main.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
	}

    void FixedUpdate()
    {
        if (PanelUtilities.PanelOpen)
            return;

        float horiz_move = Input.GetAxis("Horizontal");

        if (horiz_move * my_rb.velocity.x < Speed)
            my_rb.AddForce(Vector2.right * horiz_move * MoveForce);
        if (Mathf.Abs(my_rb.velocity.x) > Speed)
            my_rb.velocity = new Vector2(Mathf.Sign(my_rb.velocity.x) * Speed, my_rb.velocity.y);

        if ((horiz_move > 0 && !MovingRight) || (horiz_move < 0 && MovingRight))
            Flip();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Platform(Clone)")
            IsGrounded = true;
        else if(other.collider.GetComponent<PartDrop>())
        {
            AddParts(other.collider.GetComponent<PartDrop>().Parts);
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>())
        {
            Damage(other.GetComponent<Projectile>().Damage);
            Destroy(other.gameObject);
        }
        else if (other.GetComponent<Workshop>())
            AtWorkshop = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Workshop>())
            AtWorkshop = false;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;

        for (int i = 0; i < 5; i++)
            AddToInventory(WorkshopManager.Instance.BaseAttachments[Random.Range(0, 9)]);
    }

    public void AddToInventory(Attachment attachment)
    {
        attachment.AttachedTo = null;

        Inventory.Add(attachment);
    }

    public void AddParts(int parts)
    {
        Parts += parts;
        PartCountText.text = Parts.ToString();
    }
}