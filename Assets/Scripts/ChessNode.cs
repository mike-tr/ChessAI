using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessNode {
    public ChessPiece piece;
    public ChessBoard board;
    public int x, y;

    public ChessNode (ChessBoard board, int x, int y) {
        this.board = board;
        this.x = x;
        this.y = y;
        piece = null;
    }

    public ChessNode GetNodeFrom (int offset_x, int offset_y) {
        var nx = x + offset_x;
        if (nx > 7 || nx < 0)
            return null;
        var ny = y + offset_y;
        if (ny > 7 || ny < 0)
            return null;
        return board.board[nx, ny];
    }

    public ChessPiece InitializePiece (PieceType type, TeamColor player, ChessBoard board, bool moved = false) {
        piece = new ChessPiece (this, type, player, moved);
        board.pieces[player].Add (piece);
        return piece;
    }

    public bool ReplaceWithAnother (ChessNode node) {
        if (piece != null)
            board.pieces[piece.color].Remove (piece);
        piece = node.piece;
        piece.node = this;

        node.piece = null;
        return true;
    }

    public bool Overlaps (ChessNode node) {
        return node.x == x && node.y == y;
    }

    public BoardCord GetCord () {
        return new BoardCord (x, y);
    }
}
