using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessBrain {
    protected BoardDrawer board;
    protected ChessGameHandler handler;
    protected TeamColor color;

    public ChessBrain (BoardDrawer board, ChessGameHandler handler, TeamColor color) {
        this.handler = handler;
        this.board = board;
        this.color = color;
    }

    public abstract void Play ();
}
