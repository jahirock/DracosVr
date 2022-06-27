using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject[] enemies;
    public int numberOfEnemies;

    void Start()
    {
        StartCoroutine("GenerateNewEnemy");
    }

    IEnumerator GenerateNewEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, transform);
            yield return new WaitForSeconds(Random.Range(10F, 20F));
        }
    }
}
