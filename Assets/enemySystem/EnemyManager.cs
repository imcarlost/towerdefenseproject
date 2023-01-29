using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // routePoints is a list of points that the enemy will follow
    public ArrayList routePointList = new ArrayList();
    // enemy waves
    public EnemyWave[] enemyWaves;
    // time between waves
    private float enemySpawnRateSeconds = 0.5f;
    public float timeBetweenWaves = 5f;
    private ArrayList enemyPrefabList = new ArrayList();
    private ArrayList enemyList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        // debug route points
        routePointList.Add(new Vector3(-3, 1, 0));
        routePointList.Add(new Vector3(-2, 1, 0));
        routePointList.Add(new Vector3(-2, 2, 0));
        routePointList.Add(new Vector3(-2, 3, 0));
        routePointList.Add(new Vector3(-1, 3, 0));
        routePointList.Add(new Vector3(0, 3, 0));
        routePointList.Add(new Vector3(1, 3, 0));
        routePointList.Add(new Vector3(2, 3, 0));
        routePointList.Add(new Vector3(2, 2, 0));
        routePointList.Add(new Vector3(2, 1, 0));
        routePointList.Add(new Vector3(1, 1, 0));
        routePointList.Add(new Vector3(1, 0, 0));
        routePointList.Add(new Vector3(0, 0, 0));

        processWaves();

    }

    // JUST FOR DEBUGGING
    void OnGUI()
    {
        // on spacebar press down
        Event e = Event.current;
        if(e.isKey && e.keyCode == KeyCode.Space && e.type == EventType.KeyDown)
        {
            processTurn();
        }
        
    }

    // processTurn() spawns the next enemy in the wave
    void processTurn()
    {
        instantiateNextEnemy();
        moveEnemies();
        
    }

    void processWaves()
    {
        // get each enemy wave
        foreach(EnemyWave enemyWave in enemyWaves)
        {
            loadWave(enemyWave);
        }
    }

    void loadWave(EnemyWave enemyWave){

        // clear the enemy prefab list
        enemyPrefabList.Clear();

        // get each enemy group
        foreach(EnemyGroup enemyGroup in enemyWave.enemyGroups)
        {
            // get each enemy
            for(int i = 0; i < enemyGroup.quantity; i++)
            {
                // add enemy to the list
                enemyPrefabList.Add(enemyGroup.enemyPrefab);
            }
        }
    }

    void instantiateNextEnemy()
    {
        if(enemyPrefabList.Count == 0)
        {
            return;
        }

        // get the first enemy prefab and remove it from the list
        GameObject enemyPrefab = enemyPrefabList[0] as GameObject;
        enemyPrefabList.RemoveAt(0);

        // spawn enemy
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // set the route points for the enemy
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.routePointList = routePointList;

        enemy.transform.position = (Vector3)routePointList[0];

        // add enemy to the enemy list
        enemyList.Add(enemy);
    }

    void moveEnemies()
    {
        // list of elements to delete
        ArrayList deleteList = new ArrayList();

        // get each enemy
        foreach(GameObject enemy in enemyList)
        {
            // get the enemy controller
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            // move the enemy
            if(!enemyController.move()){
                // if the enemy is destroyed, remove it from the list
                deleteList.Add(enemy);
            }
        }
        // remove the enemies from the list
        foreach(GameObject enemy in deleteList)
        {
            enemyList.Remove(enemy);
        }
    }



}
