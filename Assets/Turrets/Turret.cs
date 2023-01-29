using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Vector3 position;
    public Vector3[] area;
    public GameObject turretOrigin;
    public void Shoot(ArrayList enemyList){
        if (area == null)
            area = new [] { new Vector3(0, 3, 0) };

        List<Vector3> areaList = new List<Vector3>();
        areaList.AddRange(area);


        // ordenarlo segun la distancia a mi torre enemyList
        enemyList.Sort((IComparer)new sortByDistance());

        foreach (GameObject enemy in enemyList){
            // 
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            Debug.Log("enemyController.lives: " + enemyController.lives);

            if (areaList.Contains(enemy.transform.position)){
                // enemigo encontrado, fucking shot at him
                
                enemyController.lives -= 1;
                return;
            }
        }
    }


    // List<int> data1 = new List<int> {1,2,3,4,5};
    // List<string> data2 = new List<string>{"6","3"};
    // var newData = data1.Select(i => i.ToString()).Intersect(data2);
    private class sortByDistance: IComparer{
      int IComparer.Compare(object a, object b){
         Vector3 p1=((GameObject)a).transform.position;
         Vector3 p2=((GameObject)b).transform.position;

         int bro = (int) Vector3.Distance(p1, p2);
         return bro;
      }
   }
}
