using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamColor
{
    black,
    white,
}

public enum PieceType
{
    Rook,
    Pawn,
    Knight,
    Queen,
    King,
    Bishop,
    none,
}

public class ChessPiece
{
    public ChessNode node;
    public PieceType type;
    public TeamColor color;

    public ChessPiece(ChessNode node, PieceType type, TeamColor player)
    {
        this.node = node;
        this.type = type;
        this.color = player;
    }
}