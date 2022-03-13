using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor {
    black = 0,
    white = 1,
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
    public PieceType type = PieceType.none;
    public PlayerColor color;

    public bool moved;

    public ChessPiece (ChessNode node, PieceType type, PlayerColor player, bool moved = false) {
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
        // this method returns, all the valid moves for a given piece, 
        // a.k.a every move that wont result in you'r king being eaten.
        List<PieceMove> nonValidatedMoves = GetMoves ();
        var validMoves = new List<PieceMove> ();
        foreach (var move in nonValidatedMoves) {
            if (move.IsValid ()) {
                validMoves.Add (move);
            }
        }
        AddKingMoves (validMoves);
        return validMoves;
    }

    public void AddKingMoves (List<PieceMove> validMoves) {
        if (type == PieceType.King) {
            List<PieceMove> moves = node.board.GetAllPlayerMoves (OppositeTeam (), false);
            // if the king didnt move a.k.a he is at the start pos, and he isn't attacked.
            // we can do the rook king thing, so this is the logic to check if its valid move.
            if (!moved && !PieceMove.CheckOverlap (moves, node)) {
                var board = node.board;
                var rook = board.nodes[node.x - 3, node.y];
                if (rook.GetPieceType() == PieceType.Rook && !rook.piece.moved) {
                    var rookEndCord = board.nodes[node.x - 1, node.y];
                    var kingEndCord = board.nodes[node.x - 2, node.y];
                    if (rookEndCord.piece == null && kingEndCord.piece == null) {
                        if (!PieceMove.CheckOverlap (moves, rookEndCord) &&
                            !PieceMove.CheckOverlap (moves, kingEndCord)) {
                            validMoves.Add (new ComplexMove (node, kingEndCord, rook, rookEndCord, true));
                        }
                    }
                }
                rook = board.nodes[node.x + 4, node.y];
                if (rook.GetPieceType() == PieceType.Rook && !rook.piece.moved) {
                    var rookEndCord = board.nodes[node.x + 1, node.y];
                    var kingEndCord = board.nodes[node.x + 2, node.y];
                    var lastPos = board.nodes[node.x + 3, node.y];
                    if (rookEndCord.piece == null && kingEndCord.piece == null && lastPos.piece == null) {
                        if (!PieceMove.CheckOverlap (moves, rookEndCord) &&
                            !PieceMove.CheckOverlap (moves, kingEndCord) && !PieceMove.CheckOverlap (moves, lastPos)) {
                            validMoves.Add (new ComplexMove (node, kingEndCord, rook, rookEndCord, true));
                        }
                    }
                }
            }
        }
    }

    public List<PieceMove> GetMoves () {
        // return a list of all available moves, those are any more that are not resulting in u killing you'r own unit.
        var list = new List<PieceMove> ();
        switch (type) {
            case PieceType.Pawn:
                list = PawnMove ();
                break;
            case PieceType.Knight:
                AddMove (list, 1, 2);
                AddMove (list, -1, 2);
                AddMove (list, 1, -2);
                AddMove (list, -1, -2);

                AddMove (list, 2, 1);
                AddMove (list, 2, -1);
                AddMove (list, -2, 1);
                AddMove (list, -2, -1);
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
                            AddMove (list, x, y);
                        }
                    }
                }
                break;
        }
        return list;
    }

    public void AddLine (List<PieceMove> list, int dirX, int dirY) {
        // Add moves on a line untill, we hit obstacle.
        int x = dirX;
        int y = dirY;
        while (AddMove (list, x, y) > 0) {
            x += dirX;
            y += dirY;
        }
    }

    public int AddMove (List<PieceMove> list, int offset_x, int offset_y) {
        // Add the given move into the list,
        // if the node exist, and it doesnt contain an ally on it. ( we dont wanna eat an ally )
        ChessNode current = node.GetNodeFrom (offset_x, offset_y);
        if (current == null)
            return 0;
        //Debug.Log (current.x + " , " + current.y + " ,,," + offset_x + " , " + offset_y);
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

    public PlayerColor OppositeTeam () {
        // return the color of the enemy
        return PlayerColor.white == color ? PlayerColor.black : PlayerColor.white;
    }

    public List<PieceMove> PawnMove () {
        // pawns have wierd logic, so they deserve a method of their own.
        var list = new List<PieceMove> ();
        var dir = color == PlayerColor.white ? 1 : -1;
        var current = node.GetNodeFrom (0, dir);
        //Debug.Log (node.x + " , " + node.y + "  : " + type + " " + color);
        if (current != null && current.piece == null) {
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
