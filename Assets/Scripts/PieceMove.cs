using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove {
    public BoardCord start { get; private set; }
    public BoardCord end { get; private set; }
    private ChessBoard board;
    public PieceMove (ChessNode start, ChessNode end) {
        this.board = start.board;
        this.start = start.GetCord ();
        this.end = end.GetCord ();
    }

    public bool CheckOverlap (ChessNode node) {
        return end.Overlaps (node);
    }

    public bool CheckOverlap (BoardCord cord) {
        return end.Overlaps (cord);
    }

    public ChessBoard ApplyMove () {
        var newboard = board.Copy ();
        var enode = end.GetNode (newboard);
        newboard.ChangeTurn ();

        enode.ReplaceWithAnother (start.GetNode (newboard));
        var piece = enode.piece;
        piece.moved = true;
        if (piece.type == PieceType.Pawn) {
            if (piece.color == TeamColor.black) {
                if (enode.y == 0) {
                    piece.type = PieceType.Queen;
                }
            } else if (enode.y == 7) {
                piece.type = PieceType.Queen;
            }
        }
        return newboard;
    }
}
