using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCord {
    int x, y;
    public BoardCord (int x, int y) {
        this.x = x;
        this.y = y;
    }

    public ChessNode GetNode (ChessBoard board) {
        return board.board[x, y];
    }

    public TileHandler GetTileHandler (BoardDrawer drawer) {
        return drawer.tiles[x, y];
    }
}
