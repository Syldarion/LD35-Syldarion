using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : Entity
{
    void Start()
	{
        Parts = Random.Range(10, 100);
    }
	
	void Update()
	{
        JumpTimer -= Time.deltaTime;
        FireTimer -= Time.deltaTime;
    }
}