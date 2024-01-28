using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomTile : TileBase
{
    public GameObject tileObject; 
    public Sprite sprite; 

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = tileObject;
        tileData.colliderType = Tile.ColliderType.Sprite;
        tileData.sprite = sprite; 

        if (tileData.sprite == null && tileObject != null)
        {
            SpriteRenderer spriteRenderer = tileObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                tileData.sprite = spriteRenderer.sprite;
            }else{
                TileSpriteRenderer tileSpriteRenderer = tileObject.GetComponent<TileSpriteRenderer>();
                tileData.sprite = tileSpriteRenderer.sprite;
            }
        }
    }
}
