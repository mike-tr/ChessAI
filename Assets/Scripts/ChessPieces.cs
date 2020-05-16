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

    public List<BoardCord> GetMoves()
    {
        var list = new List<BoardCord>();
        switch (type)
        {
            case PieceType.Pawn:
                list = PawnMove();
                break;
            case PieceType.Knight:
                AddIfValid(list, 1, 2);
                AddIfValid(list, -1, 2);
                AddIfValid(list, 1, -2);
                AddIfValid(list, 1, -2);

                AddIfValid(list, 2, 1);
                AddIfValid(list, 2, -1);
                AddIfValid(list, -2, 1);
                AddIfValid(list, -2, -1);
                break;
        }
        return list;
    }

    public ChessNode AddIfValid(List<BoardCord> list, int offset_x, int offset_y)
    {
        var current = node.GetNodeFrom(offset_x, offset_y);
        if (current != node && !current.piece.IsAlly(this))
        {
            list.Add(current.GetCord());
            return current;
        }
        return null;
    }

    public bool IsAlly(ChessPiece piece)
    {
        if (piece == null)
            return false;
        return piece.color == color;
    }

    public List<BoardCord> PawnMove()
    {
        var list = new List<BoardCord>();
        var dir = color == TeamColor.white ? 1 : -1;
        var current = node.GetNodeFrom(0, dir);
        if (current.piece == null)
        {
            list.Add(current.GetCord());
            var start = dir > 0 ? 1 : 6;
            if (node.y == start)
            {
                current = node.GetNodeFrom(0, dir * 2);
                if (current.piece == null)
                {
                    list.Add(current.GetCord());
                }
            }

            current = node.GetNodeFrom(1, dir);
            if (current != null && current.piece != null)
            {
                list.Add(current.GetCord());
            }
            current = node.GetNodeFrom(-1, dir);
            if (current != null && current.piece != null)
            {
                list.Add(current.GetCord());
            }
        }
        return list;
    }
}