using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour
{
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>())
        {
            LevelGenerator.Instance.CurrentLevel++;
            LevelGenerator.Instance.CleanupLevel();
            LevelGenerator.Instance.GenerateLevel();
        }
    }
}