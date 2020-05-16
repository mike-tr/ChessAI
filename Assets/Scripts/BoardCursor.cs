using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCursor : MonoBehaviour
{
    TileHandler tile;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void SetPos(Vector2 pos)
    {
        coll.enabled = true;
        tile.SetFocus(false);
        transform.position = pos;
    }

    public void SetTile(TileHandler tile)
    {
        coll.enabled = false;
        if (this.tile)
            this.tile.SetFocus(false);

        this.tile = tile;
        tile.SetFocus(true, Color.cyan);
    }
}
