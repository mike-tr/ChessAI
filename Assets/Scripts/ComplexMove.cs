using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexMove : PieceMove {

    BoardCord secondaryStart;
    BoardCord secondaryEnd;
    public ComplexMove (ChessNode start, ChessNode end,
        ChessNode secondaryStart, ChessNode secondaryEnd) : base (start, end) {
        this.secondaryEnd = secondaryEnd.GetCord ();
        this.secondaryStart = secondaryStart.GetCord ();
    }

    public override ChessBoard ApplyMove () {
        var board = base.ApplyMove ();
        var enode = secondaryEnd.GetNode (board);
        enode.ReplaceWithAnother (secondaryStart.GetNode (board));
        enode.piece.moved = true;
        return board;
    }
}
