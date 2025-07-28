using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTile : MonoBehaviour
{
    public Tilemap techo;
    public Tilemap pared;
    private bool isTransparent = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("->> " + collision.name);
        if (collision.name == "FeetPlayer")
        {
            if (!isTransparent)
            {
                ChangeColor(pared, 0.3f);
                ChangeColor(techo, 0.3f);

                ChangeSortTile(pared, "Transparent");
                ChangeSortTile(techo, "TransparentRooft");

                isTransparent = true;
            }
            else if (isTransparent)
            {
                ChangeColor(pared, 1f);
                ChangeColor(techo, 1f);

                ChangeSortTile(pared, "Walls");
                ChangeSortTile(techo, "TechoPared");

                isTransparent = false;
            }
        }
    }

    private void ChangeColor(Tilemap tilemap, float alfa)
    {
        Color color = tilemap.color;
        color.a = alfa;
        tilemap.color = color;
    }

    private void ChangeSortTile(Tilemap tilemap, string sort)
    {
        TilemapRenderer renderer = tilemap.GetComponent<TilemapRenderer>();
        renderer.sortingLayerName = sort;
    }
}

