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

	void Start()
	{
        Instance = this;
        Inventory = new List<Attachment>();
	}

	void Update()
	{
        JumpTimer -= Time.deltaTime;
        FireTimer -= Time.deltaTime;

        if (PanelUtilities.PanelOpen)
            return;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0.0f));

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetMouseButtonDown(0))
            Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Camera.main.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
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