using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform parent;
    [SerializeField] private float timeDelay;

    public int enemiesCount;

    private void Awake()
    {
        enemiesCount = enemies.Length;
    }
    public void Spawn(int wave, int count)
    {
        StartCoroutine(SpawnCoroutine(wave, count));
    }


    IEnumerator  SpawnCoroutine(int wave, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemies[wave], transform.position, Quaternion.identity, parent);
            yield return new WaitForSeconds(timeDelay);
        }   
    }
}
