using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessBrain {
    protected BoardDrawer drawer;
    protected ChessBoard board;
    protected ChessGameHandler handler;
    protected TeamColor color;

    public ChessBrain (BoardDrawer drawer, ChessGameHandler handler, TeamColor color) {
        this.handler = handler;
        this.drawer = drawer;
        this.color = color;
        board = drawer.board;
    }

    public abstract void Play ();

    public void MakeAMove (PieceMove move) {
        if (move.IsPartOf (board)) {
            drawer.SwitchBoard (move.ApplyMove ());
        }
    }

    public List<PieceMove> GetValidMoves () {
        return board.GetAllPlayerMoves (color, true);
    }
}
