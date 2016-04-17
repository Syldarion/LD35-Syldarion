using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : Entity
{
    void Start()
	{
        Parts = Random.Range(50, 100);

        StartCoroutine(LookForPlayer());
    }
	
	void Update()
	{
        JumpTimer -= Time.deltaTime;
        FireTimer -= Time.deltaTime;
    }

    IEnumerator LookForPlayer()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 20.0f)
                Fire(Player.Instance.transform.position);

            yield return new WaitForSeconds(FireCooldown + Random.Range(-0.5f, 0.5f));
        }
    }
}