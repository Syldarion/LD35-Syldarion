using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Entity : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject PartDropPrefab;

    public Image HealthBar;

    public float MoveForce;
    public float Speed;
    public float BulletSpeed;
    public float FireCooldown;
    public float JumpForce;
    public float JumpCooldown;
    public bool IsGrounded;
    public bool MovingRight;

    public Companion MyCompanion;

    public int Parts;

    public int MaxHealth;
    public int Health;
    public int Armor;
    public int DamageModifier;

    public float FireTimer;
    public float JumpTimer;

    void Start()
    {
        FireTimer = FireCooldown;
        JumpTimer = JumpCooldown;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Platform(Clone)")
            IsGrounded = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>())
        {
            Damage(other.GetComponent<Projectile>().Damage);
            Destroy(other.gameObject);
        }
    }

    protected void Flip()
    {
        MovingRight = !MovingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Damage(int damage)
    {
        Health -= damage - Armor;

        HealthBar.rectTransform.localScale = new Vector3((float)Health / (float)MaxHealth, 1.0f, 1.0f);

        if (Health <= 0)
            Die();
    }

    protected void Die()
    {
        GameObject drop_parts = Instantiate(PartDropPrefab);
        drop_parts.transform.position = transform.position;
        drop_parts.GetComponent<PartDrop>().Parts = Parts;

        Destroy(gameObject);
    }

    protected void Jump()
    {
        if (!IsGrounded || JumpTimer > 0.0f)
            return;

        JumpTimer = JumpCooldown;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);

        IsGrounded = false;
    }

    public void Fire(Vector3 target)
    {
        if (FireTimer > 0.0f)
            return;

        FireTimer = FireCooldown;

        Vector3 diff_vec = (target - transform.position).normalized;

        GameObject new_projectile = Instantiate(ProjectilePrefab);
        new_projectile.transform.position = transform.position + diff_vec * 1.5f;
        new_projectile.GetComponent<Rigidbody2D>().AddForce(diff_vec * BulletSpeed, ForceMode2D.Impulse);
        new_projectile.GetComponent<Projectile>().Damage = 10 + DamageModifier;

        GetComponent<AudioSource>().Play();
    }
}