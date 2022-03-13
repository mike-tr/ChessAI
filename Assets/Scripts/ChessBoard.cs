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
    public BitString hash { get; private set; }
    public int movesMade { get; private set; }

    public ChessBoard () {
        // Create a plain new Board
        this.movesMade = 0;
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
        hash = ChessBoardMemory.instance.BoardHash(this);
    }

    public ChessBoard Copy () {
        return new ChessBoard (this);
    }

    private ChessBoard (ChessBoard other) {
        // Generate a copy of a board
        this.currentPlayer = other.currentPlayer;
        this.hash = other.hash;
        this.pieces.Add (white, new List<ChessPiece> ());
        this.pieces.Add (black, new List<ChessPiece> ());
        this.movesMade = other.movesMade;
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                nodes[x, y] = new ChessNode (this, x, y);
            }
        }

        foreach (var piece in other.pieces[black]) {
            nodes[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
        foreach (var piece in other.pieces[white]) {
            nodes[piece.node.x, piece.node.y].InitializePiece (piece.type, piece.color, this, piece.moved);
        }
    }

    private List<PieceMove>[] PlayerMoves = new List<PieceMove>[4];
    private int GetIndex (PlayerColor color, bool validated) {
        return validated ? 2 + (int) color : (int) color;
    }

    public List<PieceMove> GetAllPlayerMoves (PlayerColor color, bool validated) {
        int index = GetIndex (color, validated);
        if (PlayerMoves[index] != null) {
            return PlayerMoves[index];
        }
        List<PieceMove> moves = new List<PieceMove> ();
        // for each of the current player units calculate all possible moves.
        foreach (var piece in pieces[color]) {
            if (!validated) {
                // get any move.
                moves.AddRange(piece.GetMoves());
            } else {
                // get only valid moves.
                moves.AddRange(piece.GetValidMoves());
            }
        }
        // a.k.a its possible we might actually try to draw the same moves multiple times per turn,
        // so why not save them?, hopefully it wont cause memory leak.
        PlayerMoves[index] = moves;
        return moves;
    }

    public bool IsChecked (PlayerColor color) {
        var enemyColor = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
        ChessNode node = kings[color].node;
        foreach (PieceType type in System.Enum.GetValues (typeof (PieceType))) {
            if (type == PieceType.none) {
                continue;
            }
            var piece = new ChessPiece (node, type, color, true);
            foreach (var move in piece.GetMoves ()) {
                var current = GetPieceAt (move.end.x, move.end.y);
                if (current != null && current.type == piece.type && current.color == enemyColor) {
                    return true;
                }
            }
        }
        return false;
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

    public ChessBoard ApplyMove(PieceMove move){
        var finalBoard = this.Copy ();
        var end_node = move.end.GetNode (finalBoard);
        // Remove hash of prev piece position
        ChessBoardMemory memory = ChessBoardMemory.instance;
        BitString encoding = memory.GetEncoding(move.start, this);
        finalBoard.hash *= encoding;
        // Remove hash of next piece position ( if there was unit on it )
        encoding = memory.GetEncoding(end_node);
        finalBoard.hash *= encoding;

        // Apply new turn.
        finalBoard.ChangeTurn ();
        end_node.ReplaceWithAnother (move.start.GetNode (finalBoard));

        // if its a pawn and it got to the "end" convert it to a queen.
        var piece = end_node.piece;
        piece.moved = true;
        if (piece.type == PieceType.Pawn) {
            if (piece.color == PlayerColor.black) {
                if (end_node.y == 0) {
                    piece.type = PieceType.Queen;
                }
            } else if (end_node.y == 7) {
                piece.type = PieceType.Queen;
            }
        }
        // Add hash of new position.
        encoding = memory.GetEncoding(end_node);
        finalBoard.hash *= encoding;
        finalBoard.movesMade++;
        
        // ChessBoard saved = memory.GetBoard(finalBoard.movesMade, finalBoard.hash);
        // if(saved == null){
        //     saved = finalBoard;
        //     memory.push(saved);
        // }

        //Debug.Log(finalBoard.hash);
        //Debug.Log(memory.BoardHash(finalBoard));
        return finalBoard;
    }
}
