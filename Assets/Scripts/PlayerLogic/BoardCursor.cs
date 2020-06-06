using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCursor : MonoBehaviour {
    public static BoardCursor cursor;
    TileHandler tile;
    Collider2D coll;
    public TeamColor player = TeamColor.white;
    // Start is called before the first frame update
    void Start () {
        coll = GetComponent<Collider2D> ();
        cursor = this;
    }

    public void SetPos (Vector2 pos) {
        coll.enabled = true;
        //tile.SetFocus (false);
        transform.position = pos;
    }

    public void SetTile (TileHandler tile) {
        coll.enabled = false;
        if (this.tile)
            this.tile.ResetTile ();

        this.tile = tile;
        tile.GetMoves (player);
        //tile.SetFocus (true, Color.cyan);
    }
}
