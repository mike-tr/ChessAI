using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexMove : PieceMove {

    BoardCord secondaryStart;
    BoardCord secondaryEnd;
    public ComplexMove (ChessNode start, ChessNode end,
        ChessNode secondaryStart, ChessNode secondaryEnd, bool valid) : base (start, end) {
        this.secondaryEnd = secondaryEnd.GetCord ();
        this.secondaryStart = secondaryStart.GetCord ();
        this.valid = true;
        this.validated = true;
    }

    public override ChessBoard ApplyMove () {
        if (finalBoard != null) {
            return finalBoard;
        }
        var board = base.ApplyMove ();
        var enode = secondaryEnd.GetNode (board);
        enode.ReplaceWithAnother (secondaryStart.GetNode (board));
        enode.piece.moved = true;
        finalBoard = board;
        return finalBoard;
    }
}
