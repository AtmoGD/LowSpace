using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private bool generateOnStart = true;

    private void Start()
    {
        if (generateOnStart)
            GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
            spawnPosition += spawnPoint.position;
            int randomIndex = Random.Range(0, objects.Length);
            Instantiate(objects[randomIndex], spawnPosition, Quaternion.identity, transform);
        }
    }
}
