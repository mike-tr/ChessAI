using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard {
    public const PlayerColor white = PlayerColor.white;
    public const PlayerColor black = PlayerColor.black;
    public Dictionary<PlayerColor, List<ChessPiece>> pieces { get; private set; } = new Dictionary<PlayerColor, List<ChessPiece>> ();
    public Dictionary<PlayerColor, ChessPiece> kings { get; private set; } = new Dictionary<PlayerColor, ChessPiece> ();
    public ChessNode[, ] nodes { get; private set; } = new ChessNode[8, 8];
    public PlayerColor currentPlayer { get; private set; } = PlayerColor.white;
    public PlayerColor enemyColor { get { return currentPlayer == PlayerColor.white ? PlayerColor.black : PlayerColor.white; } }

    public ChessBoard () {
        // Create a plain new Board
        pieces.Add (white, new List<ChessPiece> ());
        pieces.Add (black, new List<ChessPiece> ());
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                nodes[x, y] = new ChessNode (this, x, y);
            }
        }
        for (int i = 0; i < 8; i++) {
            nodes[i, 6].InitializePiece (PieceType.Pawn, black, this);
            nodes[i, 1].InitializePiece (PieceType.Pawn, white, this);
        }
        initSide (7, black);
        initSide (0, white);
    }

    public ChessBoard Copy () {
        return new ChessBoard (pieces, currentPlayer);
    }

    public ChessBoard (Dictionary<PlayerColor, List<ChessPiece>> copy, PlayerColor nextTurn) {
        // Generate a copy of a board
        this.currentPlayer = nextTurn;
        this.pieces.Add (white, new List<ChessPiece> ());
        this.pieces.Add (black, new List<ChessPiece> ());
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                nodes[x, y] = new ChessNode (this, x, y);
            }
        }

        foreach (var piece in copy[black]) {
            nodes[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
        foreach (var piece in copy[white]) {
            nodes[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
    }

    private List<PieceMove>[] PlayerMoves = new List<PieceMove>[4];
    private int GetIndex (PlayerColor color, bool validated) {
        return validated ? 2 + (int) color : (int) color;
    }

    public List<PieceMove> GetAllPlayerMoves (PlayerColor color, bool validated) {
        int index = GetIndex (color, validated);
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
        // a.k.a its possible we might actually try to draw the same moves multiple times per turn,
        // so why not save them?, hopefully it wont cause memory leak.
        PlayerMoves[index] = moves;
        return moves;
    }

    public void ChangeTurn () {
        if (currentPlayer == PlayerColor.black)
            currentPlayer = PlayerColor.white;
        else
            currentPlayer = PlayerColor.black;
    }

    public ChessPiece GetPieceAt (int x, int y) {
        return nodes[x, y].piece;
    }

    public void initSide (int y, PlayerColor player) {
        nodes[0, y].InitializePiece (PieceType.Rook, player, this);
        nodes[7, y].InitializePiece (PieceType.Rook, player, this);
        nodes[1, y].InitializePiece (PieceType.Knight, player, this);
        nodes[6, y].InitializePiece (PieceType.Knight, player, this);
        nodes[2, y].InitializePiece (PieceType.Bishop, player, this);
        nodes[5, y].InitializePiece (PieceType.Bishop, player, this);
        nodes[4, y].InitializePiece (PieceType.Queen, player, this);
        nodes[3, y].InitializePiece (PieceType.King, player, this);
    }
}
