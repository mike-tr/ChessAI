using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove {
    public BoardCord start { get; protected set; }
    public BoardCord end { get; protected set; }
    protected ChessBoard board;
    public PieceMove (ChessNode start, ChessNode end) {
        this.board = start.board;
        this.start = start.GetCord ();
        this.end = end.GetCord ();
    }

    public static bool CheckOverlap (List<PieceMove> moves, BoardCord cord) {
        foreach (var move in moves) {
            if (move.CheckOverlap (cord))
                return true;
        }
        return false;
    }

    public static bool CheckOverlap (List<PieceMove> moves, ChessNode node) {
        foreach (var move in moves) {
            if (move.CheckOverlap (node))
                return true;
        }
        return false;
    }

    public bool CheckOverlap (List<PieceMove> moves) {
        foreach (var move in moves) {
            if (move.CheckOverlap (end))
                return true;
        }
        return false;
    }
    public bool CheckOverlap (ChessNode node) {
        return end.Overlaps (node);
    }

    public bool CheckOverlap (BoardCord cord) {
        return end.Overlaps (cord);
    }

    public virtual ChessBoard ApplyMove () {
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
