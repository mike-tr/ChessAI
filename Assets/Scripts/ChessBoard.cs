using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard {
    public const TeamColor white = TeamColor.white;
    public const TeamColor black = TeamColor.black;
    public Dictionary<TeamColor, List<ChessPiece>> pieces = new Dictionary<TeamColor, List<ChessPiece>> ();
    public Dictionary<TeamColor, ChessPiece> kings = new Dictionary<TeamColor, ChessPiece> ();
    public ChessNode[, ] board = new ChessNode[8, 8];
    public TeamColor currentPlayer = TeamColor.white;

    public ChessBoard () {
        // Create a plain new Board
        pieces.Add (white, new List<ChessPiece> ());
        pieces.Add (black, new List<ChessPiece> ());
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                board[x, y] = new ChessNode (this, x, y);
            }
        }
        for (int i = 0; i < 8; i++) {
            board[i, 6].InitializePiece (PieceType.Pawn, black, this);
            board[i, 1].InitializePiece (PieceType.Pawn, white, this);
        }
        initSide (7, black);
        initSide (0, white);
    }

    public ChessBoard Copy () {
        return new ChessBoard (pieces, currentPlayer);
    }

    public ChessBoard (Dictionary<TeamColor, List<ChessPiece>> copy, TeamColor nextTurn) {
        // Generate a copy of a board
        this.currentPlayer = nextTurn;
        this.pieces.Add (white, new List<ChessPiece> ());
        this.pieces.Add (black, new List<ChessPiece> ());
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                board[x, y] = new ChessNode (this, x, y);
            }
        }

        foreach (var piece in copy[black]) {
            board[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
        foreach (var piece in copy[white]) {
            board[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
    }

    public List<PieceMove> GetAllPlayerMoves (TeamColor color, bool validated) {
        List<PieceMove> moves = new List<PieceMove> ();
        foreach (var piece in pieces[color]) {
            if (!validated) {
                foreach (var move in piece.GetMoves ()) {
                    moves.Add (move);
                }
            } else {
                foreach (var move in piece.GetValidMoves ()) {
                    moves.Add (move);
                }
            }
        }
        return moves;
    }

    public void ChangeTurn () {
        if (currentPlayer == TeamColor.black)
            currentPlayer = TeamColor.white;
        else
            currentPlayer = TeamColor.black;
    }

    public ChessPiece GetPieceAt (int x, int y) {
        return board[x, y].piece;
    }

    public void initSide (int y, TeamColor player) {
        board[0, y].InitializePiece (PieceType.Rook, player, this);
        board[7, y].InitializePiece (PieceType.Rook, player, this);
        board[1, y].InitializePiece (PieceType.Knight, player, this);
        board[6, y].InitializePiece (PieceType.Knight, player, this);
        board[2, y].InitializePiece (PieceType.Bishop, player, this);
        board[5, y].InitializePiece (PieceType.Bishop, player, this);
        board[4, y].InitializePiece (PieceType.Queen, player, this);
        board[3, y].InitializePiece (PieceType.King, player, this);
    }
}
