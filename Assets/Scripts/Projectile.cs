using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int Damage;

	void Start()
	{
        Destroy(this.gameObject, 5.0f);
	}
	
	void Update()
	{
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Companion>())
            Destroy(this.gameObject);
    }
}