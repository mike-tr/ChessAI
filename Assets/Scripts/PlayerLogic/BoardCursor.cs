using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessGraphics {
    public class BoardCursor : MonoBehaviour {
        public static BoardCursor cursor;
        SquareHandler tile;
        Collider2D coll;
        public PlayerIndexColor player = PlayerIndexColor.white;
        // Start is called before the first frame update
        void Start() {
            coll = GetComponent<Collider2D>();
            cursor = this;
        }

        public void SetPos(Vector2 pos) {
            coll.enabled = true;
            //tile.SetFocus (false);
            transform.position = pos;
        }

        public void SetTile(SquareHandler tile) {
            coll.enabled = false;
            if (this.tile)
                this.tile.ResetTile();

            this.tile = tile;
            tile.EnableMoves(player);
            //tile.SetFocus (true, Color.cyan);
        }
    }
}
