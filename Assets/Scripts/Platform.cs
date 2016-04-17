using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    public bool CanMove;
    public float PlatformSpeed;

    public Vector2 StartPosition;
    public Vector2 EndPosition;

	void Start()
	{
        PlatformSpeed = Random.Range(0.2f, 0.5f);
	}
	
	void Update()
	{
		if(CanMove)
        {
            var t = Mathf.PingPong(Time.time * PlatformSpeed, 1);
            transform.position = Vector2.Lerp(StartPosition, EndPosition, t);
        }
	}

    public void Initialize()
    {
        if (Random.Range(0, 4) > 2)
        {
            CanMove = true;

            switch (Random.Range(0, 2))
            {
                case 0:
                    StartPosition = (Vector2)transform.position + new Vector2(Random.Range(1.0f, 5.0f), 0.0f);
                    EndPosition = (Vector2)transform.position + new Vector2(Random.Range(-5.0f, -1.0f), 0.0f);
                    break;
                case 1:
                    StartPosition = (Vector2)transform.position + new Vector2(0.0f, Random.Range(1.0f, 5.0f));
                    EndPosition = (Vector2)transform.position + new Vector2(0.0f, Random.Range(-5.0f, -1.0f));
                    break;
                default:
                    StartPosition = EndPosition = transform.position;
                    return;
            }
        }
        else
            StartPosition = EndPosition = transform.position;
    }
}