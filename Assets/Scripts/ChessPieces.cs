using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamColor {
    black,
    white,
}

public enum PieceType {
    Rook,
    Pawn,
    Knight,
    Queen,
    King,
    Bishop,
    none,
}

public class ChessPiece {
    public ChessNode node;
    public PieceType type;
    public TeamColor color;

    public bool moved;

    public ChessPiece (ChessNode node, PieceType type, TeamColor player, bool moved = false) {
        this.node = node;
        this.type = type;
        this.color = player;
        this.moved = moved;
    }

    public ChessPiece (ChessPiece piece) {
        this.node = piece.node;
        this.type = piece.type;
        this.color = piece.color;
        this.moved = piece.moved;
    }

    public List<PieceMove> GetValidMoves () {
        List<PieceMove> nonValidatedMoves = GetMoves ();
        var validated = new List<PieceMove> ();
        if (type != PieceType.King) {
            BoardCord kingCord = node.board.kings[color].node.GetCord ();
            Debug.Log (kingCord.x + " ," + kingCord.y);
            //List<PieceMove> moves = node.board.GetAllPlayerMoves (OppositeTeam (), false);
            foreach (var move in nonValidatedMoves) {
                List<PieceMove> moves = move.ApplyMove ().GetAllPlayerMoves (OppositeTeam (), false);
                if (!PieceMove.CheckOverlap (moves, kingCord)) {
                    validated.Add (move);
                }
            }
        } else {
            List<PieceMove> moves = node.board.GetAllPlayerMoves (OppositeTeam (), false);
            foreach (var move in nonValidatedMoves) {
                if (!move.CheckOverlap (moves)) {
                    validated.Add (move);
                }
            }
        }
        return validated;
    }

    public List<PieceMove> GetMoves () {
        var list = new List<PieceMove> ();
        switch (type) {
            case PieceType.Pawn:
                list = PawnMove ();
                break;
            case PieceType.Knight:
                AddIfValid (list, 1, 2);
                AddIfValid (list, -1, 2);
                AddIfValid (list, 1, -2);
                AddIfValid (list, -1, -2);

                AddIfValid (list, 2, 1);
                AddIfValid (list, 2, -1);
                AddIfValid (list, -2, 1);
                AddIfValid (list, -2, -1);
                break;
            case PieceType.Bishop:
                AddLine (list, 1, 1);
                AddLine (list, -1, 1);
                AddLine (list, 1, -1);
                AddLine (list, -1, -1);
                break;
            case PieceType.Rook:
                AddLine (list, 1, 0);
                AddLine (list, 0, 1);
                AddLine (list, -1, 0);
                AddLine (list, 0, -1);
                break;
            case PieceType.Queen:
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        if (x != 0 || y != 0) {
                            AddLine (list, x, y);
                        }
                    }
                }
                break;
            case PieceType.King:
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        if (x != 0 || y != 0) {
                            AddIfValid (list, x, y);
                        }
                    }
                }
                break;
        }
        return list;
    }

    public void AddLine (List<PieceMove> list, int dirX, int dirY) {
        int x = dirX;
        int y = dirY;
        while (AddIfValid (list, x, y) > 0) {
            x += dirX;
            y += dirY;
        }
    }

    public PieceMove KingMove (int offset_x, int offset_y) {
        PieceMove move = null;
        ChessNode current = node.GetNodeFrom (offset_x, offset_y);
        if (current == null)
            return null;
        if (current != node && !IsAlly (current.piece)) {
            // check if cant be killed, and return if cant
            List<PieceMove> moves = node.board.GetAllPlayerMoves (OppositeTeam (), false);
            if (PieceMove.CheckOverlap (moves, node.GetCord ())) {
                return null;
            }
        }
        return move;
    }

    public int AddIfValid (List<PieceMove> list, int offset_x, int offset_y) {
        ChessNode current = node.GetNodeFrom (offset_x, offset_y);
        if (current == null)
            return 0;
        Debug.Log (current.x + " , " + current.y + " ,,," + offset_x + " , " + offset_y);
        if (current != node && !IsAlly (current.piece)) {
            list.Add (new PieceMove (node, current));
            return current.piece == null ? 1 : -1;
        }
        return 0;
    }

    public bool IsAlly (ChessPiece piece) {
        if (piece == null)
            return false;
        return piece.color == color;
    }

    public TeamColor OppositeTeam () {
        return TeamColor.white == color ? TeamColor.black : TeamColor.white;
    }

    public List<PieceMove> PawnMove () {
        Debug.Log ("----------------");
        Debug.Log (node.x + " ," + node.y);
        var list = new List<PieceMove> ();
        var dir = color == TeamColor.white ? 1 : -1;
        var current = node.GetNodeFrom (0, dir);
        if (current.piece == null) {
            list.Add (new PieceMove (node, current));
            var start = dir > 0 ? 1 : 6;
            if (node.y == start) {
                current = node.GetNodeFrom (0, dir * 2);
                if (current.piece == null) {
                    list.Add (new PieceMove (node, current));
                }
            }
        }

        current = node.GetNodeFrom (1, dir);
        if (current != null && current.piece != null && current.piece.color != color) {
            list.Add (new PieceMove (node, current));
        }
        current = node.GetNodeFrom (-1, dir);
        if (current != null && current.piece != null && current.piece.color != color) {
            list.Add (new PieceMove (node, current));
        }
        return list;
    }
}
