using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : IComparable<PieceMove> {
    public BoardCord start { get; protected set; }
    public BoardCord end { get; protected set; }
    protected ChessBoard board;
    protected ChessBoard finalBoard = null;
    protected bool valid = false;
    protected bool validated = false;
    protected PlayerColor playerColor;
    public float score = 0;
    public PieceMove (ChessNode start, ChessNode end) {
        this.board = start.board;
        this.start = start.GetCord ();
        this.end = end.GetCord ();
        playerColor = start.piece.color;
    }

    public int CompareTo (PieceMove other) {
        if (other == null) {
            return 0;
        }
        return other.score > score ? 1 : other.score == score ? 0 : -1;
    }

    public bool IsPartOf (ChessBoard board) {
        return board == this.board;
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

    public bool IsValid () {
        if (valid) {
            return true;
        } else if (validated) {
            return false;
        }
        // so if we didnt validate the move, we validate it.
        validated = true;
        var enemyColor = playerColor == PlayerColor.white ? PlayerColor.black : PlayerColor.white;

        // we get the state of the game after the move
        var board = GetNextBoard ();

        // we get the king position
        BoardCord kingCord = board.kings[playerColor].node.GetCord ();

        valid = !board.IsChecked (playerColor);
        return valid;
    }

    public virtual ChessBoard GetNextBoard () {
        // if (finalBoard != null) {
        //     return this.finalBoard;
        // }

        finalBoard = board.Copy ();
        var enode = end.GetNode (finalBoard);
        finalBoard.ChangeTurn ();

        enode.ReplaceWithAnother (start.GetNode (finalBoard));
        var piece = enode.piece;
        piece.moved = true;
        if (piece.type == PieceType.Pawn) {
            if (piece.color == PlayerColor.black) {
                if (enode.y == 0) {
                    piece.type = PieceType.Queen;
                }
            } else if (enode.y == 7) {
                piece.type = PieceType.Queen;
            }
        }
        return finalBoard;
    }
}
