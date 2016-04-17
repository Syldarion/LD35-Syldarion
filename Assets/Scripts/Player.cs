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

    public Image PowerupBar;
    public Text PowerupText;

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

        if (AttachmentManager.Instance.is_open || WorkshopManager.Instance.is_open)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanSprint)
            Speed *= 1.5f;
        else if (Input.GetKeyUp(KeyCode.LeftShift) && CanSprint)
            Speed /= 1.5f;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StopAllCoroutines();
                StartCoroutine(AttackPowerup());
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                StopAllCoroutines();
                StartCoroutine(DefensePowerup());
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                StopAllCoroutines();
                StartCoroutine(UtilityPowerup());
            }
        }

        if (Input.GetMouseButtonDown(0))
            Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Camera.main.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
	}

    void FixedUpdate()
    {
        if (AttachmentManager.Instance.is_open || WorkshopManager.Instance.is_open)
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

        if (other.name == "KillingFloor")
            Die();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Workshop>())
            AtWorkshop = false;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
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

    public override IEnumerator AttackPowerup()
    {
        float powerup_timer = 10.0f;
        float old_cooldown = FireCooldown;

        DamageModifier += 20;
        FireCooldown = 0.1f;

        PowerupBar.color = Color.red;

        while (powerup_timer > 0.0f)
        {
            PowerupBar.transform.localScale = new Vector3(powerup_timer / 10.0f, 1.0f);
            PowerupText.text = string.Format("Attack Power: {0}s", powerup_timer.ToString("F"));

            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        DamageModifier -= 20;
        FireCooldown = old_cooldown;

        PowerupBar.color = Color.white;

        PowerupBar.transform.localScale = new Vector3(1.0f, 1.0f);
        PowerupText.text = "No Power";

        DestroyCompanion();
    }

    public override IEnumerator DefensePowerup()
    {
        float powerup_timer = 10.0f;

        Armor += 20;

        PowerupBar.color = Color.green;

        while (powerup_timer > 0.0f)
        {
            PowerupBar.transform.localScale = new Vector3(powerup_timer / 10.0f, 1.0f);
            PowerupText.text = string.Format("Defense Power: {0}s", powerup_timer.ToString("F"));

            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Armor -= 20;

        PowerupBar.color = Color.white;

        PowerupBar.transform.localScale = new Vector3(1.0f, 1.0f);
        PowerupText.text = "No Power";

        DestroyCompanion();
    }

    public override IEnumerator UtilityPowerup()
    {
        float powerup_timer = 10.0f;
        float old_j_force = JumpForce;
        float old_m_force = MoveForce;
        float old_speed = Speed;

        JumpForce *= 2;
        MoveForce *= 2;
        Speed *= 2;

        PowerupBar.color = Color.blue;

        while (powerup_timer > 0.0f)
        {
            PowerupBar.transform.localScale = new Vector3(powerup_timer / 10.0f, 1.0f);
            PowerupText.text = string.Format("Utility Power: {0}s", powerup_timer.ToString("F"));

            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        JumpForce = old_j_force;
        MoveForce = old_m_force;
        Speed = old_speed;

        PowerupBar.color = Color.white;

        PowerupBar.transform.localScale = new Vector3(1.0f, 1.0f);
        PowerupText.text = "No Power";

        DestroyCompanion();
    }
}