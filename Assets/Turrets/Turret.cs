using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Turret : MonoBehaviour
{
    public Vector3[] area;
    public List<Vector3> areaList;
    //public TileBase tileBase;
    public TileBase tileExplotion;
    private Tilemap mainMapTiles;

    void Start(){
        GameObject mainMapObject = GameObject.Find("MainMap");
        mainMapTiles = mainMapObject.GetComponent<Tilemap>();
        Vector3Int bro = Vector3Int.FloorToInt(transform.position);
        //mainMapTiles.SetTile(bro, tileBase);
    }
    
    public void Shoot(ArrayList enemyList){

        areaList = new List<Vector3>();
        areaList.AddRange(area);

        // ordenarlo segun la distancia a mi torre enemyList
        enemyList.Sort((IComparer)new sortByDistance());

        foreach (GameObject enemy in enemyList){
            // 
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            // Debug.Log("Turret::EnemyController.lives: " + enemyController.lives);
            if (areaList.Contains(enemy.transform.position)){
                // enemigo encontrado, fucking shot at him
                // Debug.Log("Turret::fucking shooting");
                Vector3Int intVector = Vector3Int.FloorToInt(enemy.transform.position);
                intVector += new Vector3Int(0,0,1);
                mainMapTiles.SetTile(intVector, tileExplotion);
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
