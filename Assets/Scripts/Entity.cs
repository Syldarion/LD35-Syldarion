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
    public bool CanDoubleJump;
    public bool IsGrounded;
    public bool MovingRight;
    public bool DoubleDrops;

    public Companion MyCompanion;

    public int Parts;

    public int MaxHealth;
    public int Health;
    public int Armor;
    public int DamageModifier;
    public int DamageReflection;

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

        if (other.name == "KillingFloor")
            Die();
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
        drop_parts.GetComponent<PartDrop>().Parts = (int)(Parts * Player.Instance.PartDropModifier);

        if(Player.Instance.DoubleDrops && Random.Range(0, 100) > 75)
        {
            drop_parts = Instantiate(PartDropPrefab);
            drop_parts.transform.position = transform.position;
            drop_parts.GetComponent<PartDrop>().Parts = (int)(Parts * Player.Instance.PartDropModifier);
        }

        if (GetComponent<Enemy>())
            LevelGenerator.Instance.Enemies.Remove(GetComponent<Enemy>());

        else if (GetComponent<Player>())
        { //Switch to main menu
        }

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

        Vector3 diff_vec = Vector3.Normalize((target - transform.position));

        GameObject new_projectile = Instantiate(ProjectilePrefab);
        new_projectile.transform.position = transform.position + diff_vec * 1.5f;
        new_projectile.GetComponent<Rigidbody2D>().AddForce(diff_vec * BulletSpeed, ForceMode2D.Impulse);
        new_projectile.GetComponent<Projectile>().Damage = 10 + DamageModifier;

        GetComponent<AudioSource>().Play();
    }

    public void DestroyCompanion()
    {
        for (int i = 0; i < 3; i++)
            MyCompanion.Attachments[i] = null;
    }

    public virtual IEnumerator AttackPowerup()
    {
        float powerup_timer = 10.0f;
        float old_cooldown = FireCooldown;

        DamageModifier += 20;
        FireCooldown = 0.1f;

        while(powerup_timer > 0.0f)
        {
            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        DamageModifier -= 20;
        FireCooldown = old_cooldown;

        DestroyCompanion();
    }

    public virtual IEnumerator DefensePowerup()
    {
        float powerup_timer = 10.0f;

        Armor += 20;

        while (powerup_timer > 0.0f)
        {
            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Armor -= 20;

        DestroyCompanion();
    }

    public virtual IEnumerator UtilityPowerup()
    {
        float powerup_timer = 10.0f;
        float old_j_force = JumpForce;
        float old_m_force = MoveForce;

        JumpForce *= 3;
        MoveForce *= 2;

        while (powerup_timer > 0.0f)
        {
            powerup_timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        JumpForce = old_j_force;
        MoveForce = old_m_force;

        DestroyCompanion();
    }
}