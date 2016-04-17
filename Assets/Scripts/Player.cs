using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static Player Instance;

    public GameObject ProjectilePrefab;

    public Text PartCountText;

    public float Speed;
    public float BulletSpeed;
    public float FireCooldown;
    public float JumpForce;
    public float JumpCooldown;
    public bool IsGrounded;

    public Companion MyCompanion;
    public List<Attachment> Inventory;

    public int Parts;

    public int Health;
    public int Armor;
    public int DamageModifier;

    float fire_timer;
    float jump_timer;

	void Start()
	{
        Instance = this;

        Inventory = new List<Attachment>();

        fire_timer = FireCooldown;
        jump_timer = JumpCooldown;
	}

	void Update()
	{
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0.0f));

        jump_timer -= Time.deltaTime;
        fire_timer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && jump_timer <= 0.0f && IsGrounded)
        {
            jump_timer = JumpCooldown;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);

            IsGrounded = false;
        }

        if (Input.GetMouseButtonDown(0) && fire_timer <= 0.0f)
            Fire();

        Camera.main.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Platform(Clone)")
            IsGrounded = true;
    }

    public void Fire()
    {
        Vector2 world_mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff_vec = (world_mouse - (Vector2)transform.position).normalized;

        GameObject new_projectile = Instantiate(ProjectilePrefab);
        new_projectile.transform.position = transform.position + (Vector3)diff_vec;
        new_projectile.GetComponent<Rigidbody2D>().AddForce(diff_vec * BulletSpeed, ForceMode2D.Impulse);

        fire_timer = FireCooldown;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;

        for (int i = 0; i < 4; i++)
            Inventory.Add(LevelGenerator.Instance.BaseAttachments[Random.Range(0, 9)]);
    }

    public void AddToInventory(Attachment attachment)
    {
        Inventory.Add(attachment);
    }

    public void AddParts(int parts)
    {
        Parts += parts;
        PartCountText.text = Parts.ToString();
    }
}