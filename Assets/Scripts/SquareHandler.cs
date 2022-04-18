using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessGraphics {
    [ExecuteInEditMode]
    public class SquareHandler : MonoBehaviour {
        private int SquareId;
        public BoardDrawer drawer;
        public SpriteRenderer pieceRenderer;
        public SpriteRenderer focusRenderer;
        public Vector2 offset;
        private PieceMove move;
        public void Initialize(BoardDrawer drawer, Transform holder, int x, int y, Vector2 tileOffset) {
            this.drawer = drawer;
            this.SquareId = y * 8 + x;
            transform.parent = holder;
            transform.localPosition = new Vector2(offset.x + x * tileOffset.x, offset.y + y * tileOffset.y);
        }
        public void UpdateBoard() {
            //this.node = node;
            UpdateImage();
            move = null;
        }

        public void UpdateImage() {
            //Debug.Log("updating...");
            // if (node == null)
            //     return;

            focusRenderer.enabled = false;
            int piece = drawer.board.Squares[SquareId];
            //Debug.Log(piece + " " + SquareId);
            if (Piece.GetPieceType(piece) == Piece.None) {
                pieceRenderer.enabled = false;
            } else {
                pieceRenderer.enabled = true;
                pieceRenderer.sprite = ChessAssetsLoader.PieceSprite[piece];
            }
        }

        public void ResetTile() {
            ColorTile(Color.red, false);
            move = null;
        }

        public void EnableMove(PieceMove move) {
            this.move = move;
            ColorTile(Color.red, true);
        }

        public void DrawAllMoves(int player) {
            // if (node.piece != null && node.piece.color == player) {
            //     var color = Color.black;
            //     switch (node.GetPieceType()) {
            //         case PieceType.Knight:
            //             color = Color.blue;
            //             break;
            //         case PieceType.Bishop:
            //             color = Color.yellow;
            //             break;
            //         case PieceType.King:
            //             color = Color.cyan;
            //             break;
            //         case PieceType.Queen:
            //             color = Color.magenta;
            //             break;
            //         case PieceType.Pawn:
            //             color = new Color(1f, 0.5f, 0.3f);
            //             break;
            //         case PieceType.Rook:
            //             color = Color.green;
            //             break;
            //     }
            //     //ColorTile (color * 2f, true);
            //     AddTileColor(color);
            //     List<PieceMove> moves = node.piece.GetValidMoves();
            //     foreach (var move in moves) {
            //         //cord.GetTileHandler (drawer).SetFocus (true);

            //         move.end.GetTileHandler(drawer).AddTileColor(color);
            //     }
            // }
        }

        public void EnableMoves(PlayerIndexColor player) {
            if (move != null) {
                drawer.SwitchBoard(drawer.board.ApplyMove(move));
                drawer.RefreshBoard();
                return;
            }
            drawer.RefreshBoard();
            //Debug.Log("Highligthed cell " + ChessBoardReoresentation.IndexToRepresentation(SquareId));
            ColorTile(Color.cyan, true);
            if (player == drawer.CurrentTurn()) {
                int piece = drawer.board.Squares[SquareId];
                if (Piece.GetPieceType(piece) != Piece.None && Piece.IsColour(piece, player)) {

                    List<PieceMove> moves = drawer.board.GetPieceMoves(SquareId);
                    //List<PieceMove> moves = node.piece.GetMoves ();
                    foreach (var move in moves) {
                        //cord.GetTileHandler (drawer).SetFocus (true);
                        drawer.squareHandlers[move.TargetSquare].EnableMove(move);

                        //move.end.GetTileHandler(drawer).EnableMove(move);
                    }
                }
            }
        }

        public void AddTileColor(Color color) {
            focusRenderer.enabled = true;
            color.a = 0.9f;
            focusRenderer.color = (color + focusRenderer.color) * 0.5f;
        }

        public void ColorTile(Color color, bool state) {
            //Debug.Log (state);
            if (state) {
                focusRenderer.enabled = true;
                color.a = 0.25f;
                focusRenderer.color = color;
            } else {
                focusRenderer.color = Color.black * 0;
                focusRenderer.enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var logic = other.GetComponent<BoardCursor>();
            if (logic) {
                logic.SetTile(this);
            }
        }
    }
}