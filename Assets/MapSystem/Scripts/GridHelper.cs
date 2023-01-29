using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHelper : MonoBehaviour {

    public Tilemap enemiesTilemap;
    public GameObject enemy;

    public Vector3 spawn;
    public Vector3 ending;

    // Start is called before the first frame update
    void Start() {
        ExploreGrid();
        SpawnEnemy();
    }

    public Vector3 GetTileWorldPosition(Vector2Int position) {
        Vector3 worldPostion = Vector3.zero;
        Vector3Int tilePosition = new Vector3Int(position.x, position.y, 0);
        worldPostion = enemiesTilemap.CellToWorld(tilePosition);
        return worldPostion;
    }

    TileBase[] ExploreGrid() {
        BoundsInt bounds = enemiesTilemap.cellBounds;
        TileBase[] allTiles = enemiesTilemap.GetTilesBlock(bounds);

        for (int y = 0; y < bounds.size.y; y++) {
            for (int x = 0; x < bounds.size.x; x++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    Vector3 worldPosition = enemiesTilemap.CellToWorld(tilePosition);
                    spawn = worldPosition;
                }
            }
        }
        return allTiles;
    }

    void SpawnEnemy() {
        Instantiate(enemy, spawn, Quaternion.identity);
    }
}
