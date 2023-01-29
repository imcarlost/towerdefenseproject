using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHelper : MonoBehaviour {

    private Tilemap tilemap;

    // Start is called before the first frame update
    void Awake() {
        tilemap = GetComponent<Tilemap>();
    }

    public void OnMouseDown(Vector3 position) {
        GetTileAt(GetGameTilePosition(position));
    }

    public Vector3Int GetGameTilePosition(Vector3 position) {
        position -= new Vector3(0, -1, 0);
        Debug.LogWarning(tilemap.WorldToCell(position));
        return tilemap.WorldToCell(position);
    }

    public TileBase GetTileAt(Vector3Int position) {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        TileBase tile = allTiles[position.x + position.y * bounds.size.x];
        if (tile != null) {
            Debug.LogWarning(tile.name);
        }
        return tile;
    }

    public Vector3 GetTileWorldPosition(Vector2Int position) {
        Vector3Int tilePosition = new Vector3Int(position.x, position.y, 0);
        Vector3 worldPostion = tilemap.CellToWorld(tilePosition);
        return worldPostion;
    }

    /*
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
    */
}
