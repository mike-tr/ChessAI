using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessNode
{
    public ChessPiece piece;
    public int x, y;

    public ChessNode(int x, int y)
    {
        this.x = x;
        this.y = y;
        piece = null;
    }

    public ChessPiece InitializePiece(PieceType type, TeamColor player, ChessBoard board)
    {
        piece = new ChessPiece(this, type, player);
        board.pieces[player].Add(piece);
        return piece;
    }

    public bool TheSameAs(ChessNode node)
    {
        return node.x == x && node.y == y;
    }
}