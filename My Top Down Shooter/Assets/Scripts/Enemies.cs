using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemies : MonoBehaviour
{
    public bool enemiesDied;
    public Teleport teleport;
    public int enemiesKilled;
    public EnemyHandler[] prefabs;
    public GameObject[] spawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        enemiesDied = false;
    for(int j=0; j<3; j++)
      { 
        for(int i = 0; i < 2; i++)
        {
            GameObject spawnPoint = GetRandomSpawnPoint();
            EnemyHandler enemy = Instantiate(this.prefabs[j], this.transform);
            enemy.killed += EnemyKilled;
            enemy.transform.localPosition = spawnPoint.transform.position;
        }
      }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        if(enemiesKilled == 6)
        {
            enemiesDied = true;
            teleport.IsActive(enemiesDied);
        }
    }

    GameObject GetRandomSpawnPoint()
    {
        return spawnEnemies[Random.Range(0, spawnEnemies.Length)]; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
