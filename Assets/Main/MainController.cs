using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainController : MonoBehaviour
{

    public int playerInitLives = 3;
    private int playerLives;
    public GameObject tileMapObject;

    public BoundsInt area;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = playerInitLives;
        ExploreGrid();
    }

    void EnemyTurn(){}
    void EnemyReacts(){}
    void Display(){}

    void PlayerTurn(){
        Display();
    }

    void NextTurn(){
        EnemyTurn();
        PlayerTurn();
    }
// .orientationMatrix
    TileBase[] ExploreGrid(){
    //    Tilemap bro = tileMap.GetComponent<Tilemap>();

    //    Vector3 min = new Vector3(5, 0, 3);
    //    Vector3 size = new Vector3(10, 15, 20);
    //    BoundsInt area = new BoundsInt(min, size);
    //    TileBase[] tileArray = bro.GetTilesBlock(area);
    //    print(tileArray.Length);
    //     for (int index = 0; index < tileArray.Length; index++){
    //         if(tileArray[index] != null)
    //           print(tileArray[index].name);
    //     }
        Tilemap tileMap = tileMapObject.GetComponent<Tilemap>();
        BoundsInt bounds = tileMap.cellBounds;
        TileBase[] allTiles = tileMap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                } else {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }   
        return allTiles;
    }

    void EndState(){
        playerLives = playerInitLives;
    }
}
