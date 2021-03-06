﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [HideInInspector]
    public static LevelGenerator Instance;

    public int CurrentLevel;
    
    public GameObject PlatformPrefab;
    public GameObject SpawnPoint;
    public GameObject ExitPointPrefab;
    public GameObject EnemyPrefab;
    public GameObject WorkshopPrefab;

    public Text FloorCountText;

    //Level gen vars
    public float LevelWidth;
    public float LevelHeight;
    public int PlatformCount;
    public Vector2 MinPlatformDistance;
    public Vector2 MaxPlatformDistance;
    public float MinPlatformWidth;
    public float MaxPlatformWidth;
    public float MaxPlatformOverlap;
    public int PathCount;
    public int EnemyCount;
    public int WorkshopCount;

    public List<Enemy> Enemies;
    public List<Workshop> Workshops;

    List<GameObject> platforms;
    List<ExitPoint> exits;

    List<int> occupied_platform_indices;

	void Start()
	{
        Instance = this;

        CurrentLevel = 1;

        Enemies = new List<Enemy>();
        Workshops = new List<Workshop>();
        platforms = new List<GameObject>();
        exits = new List<ExitPoint>();
        occupied_platform_indices = new List<int>();

        GenerateLevel();
	}
	
	void Update()
	{
		
	}

    public void GenerateLevel()
    {
        FloorCountText.text = string.Format("Floor {0}", CurrentLevel.ToString());

        //Create first platform
        CreatePlatform(Vector2.zero, 10.0f);

        float path_width = LevelHeight / PathCount;                     //Total width for each path to create platforms in

        for (int p = 0; p < PathCount; p++)
        {
            float path_center = -(path_width * (p - PathCount / 2));    //Y-value for center of path
            float furthest_right = 5.0f;                                //X-value of right side of furthest out platform
            float last_y = 0.0f;                                        //Y-value of last created platform

            for (int i = 0; i < PlatformCount && furthest_right < LevelWidth; i++)
            {
                float new_plat_length = Random.Range(MinPlatformWidth, MaxPlatformWidth);

                Vector2 new_plat_position = new Vector2();
                new_plat_position.x = furthest_right + (new_plat_length / 2) + Random.Range(MinPlatformDistance.x, MaxPlatformDistance.x) - Random.Range(0.0f, MaxPlatformOverlap);
                if (i < 7)
                    new_plat_position.y = path_center / 7 * (i + 1);
                else
                    new_plat_position.y = Mathf.Clamp(last_y + Random.Range(-MaxPlatformDistance.y, MaxPlatformDistance.y), path_center - path_width / 2, path_center + path_width / 2);

                CreatePlatform(new_plat_position, new_plat_length);

                furthest_right = new_plat_position.x + (new_plat_length / 2);
                last_y = new_plat_position.y;
            }

            ExitPoint new_exit_point = Instantiate(ExitPointPrefab).GetComponent<ExitPoint>();
            new_exit_point.name = string.Format("Path{0}Exit", p + 1);
            new_exit_point.transform.position = platforms[platforms.Count - 1].transform.position + new Vector3(0.0f, 2.0f);

            exits.Add(new_exit_point);

            platforms[platforms.Count - 1].GetComponent<Platform>().CanMove = false;

            occupied_platform_indices.Add(platforms.Count - 1);
        }

        //platforms[0] should be the first spawned platform at (0,0)
        SpawnPoint.transform.position = platforms[0].transform.position + new Vector3(0.0f, 5.0f);

        int plat_index;

        for(int i = 0; i < EnemyCount; i++)
        {
            Enemy new_enemy = Instantiate(EnemyPrefab).GetComponent<Enemy>();
            do
            {
                plat_index = Random.Range(1, platforms.Count);
                new_enemy.transform.position = platforms[plat_index].transform.position + new Vector3(0.0f, 5.0f);
            }
            while (occupied_platform_indices.Contains(plat_index));

            occupied_platform_indices.Add(plat_index);

            Enemies.Add(new_enemy);
        }

        for(int i = 0; i < WorkshopCount; i++)
        {
            Workshop new_workshop = Instantiate(WorkshopPrefab).GetComponent<Workshop>();
            do
            {
                plat_index = Random.Range(1, platforms.Count);
                new_workshop.transform.position = platforms[plat_index].transform.position + new Vector3(0.0f, 1.5f);
            }
            while (occupied_platform_indices.Contains(plat_index));

            platforms[plat_index].GetComponent<Platform>().CanMove = false;
            occupied_platform_indices.Add(plat_index);

            Workshops.Add(new_workshop);
        }

        Player.Instance.Spawn(SpawnPoint.transform.position);
    }

    public void CleanupLevel()
    {
        foreach (GameObject platform in platforms)
            Destroy(platform);
        platforms.Clear();

        foreach (Enemy enemy in Enemies)
            Destroy(enemy.gameObject);
        Enemies.Clear();

        foreach (Workshop workshop in Workshops)
            Destroy(workshop.gameObject);
        Workshops.Clear();

        foreach (ExitPoint exit in exits)
            Destroy(exit.gameObject);
        exits.Clear();

        occupied_platform_indices.Clear();
    }

    void CreatePlatform(Vector2 position, float length)
    {
        GameObject new_platform = Instantiate(PlatformPrefab);
        new_platform.transform.position = position;
        new_platform.transform.localScale = new Vector3(length, 1.0f);

        new_platform.GetComponent<Platform>().Initialize();

        platforms.Add(new_platform);
    }
}