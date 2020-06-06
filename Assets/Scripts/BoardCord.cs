using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCord {
    public int x { get; private set; }
    public int y { get; private set; }
    public BoardCord (int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool Overlaps (BoardCord cord) {
        return this.x == cord.x && this.y == cord.y;
    }
    public bool Overlaps (ChessNode node) {
        return this.x == node.x && this.y == node.y;
    }

    public ChessNode GetNode (ChessBoard board) {
        return board.nodes[x, y];
    }

    public TileHandler GetTileHandler (BoardDrawer drawer) {
        return drawer.tiles[x, y];
    }
}
