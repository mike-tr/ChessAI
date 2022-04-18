using System.Collections.Generic;
using static System.Math;
using UnityEngine;
using Chess;

namespace ChessGraphics {
    public static class ChessAssetsLoader {
        public static readonly Dictionary<int, Sprite> PieceSprite;
        public static readonly string fileName = "pieces";

        static ChessAssetsLoader() {
            PieceSprite = new Dictionary<int, Sprite>();
            Sprite[] load = Resources.LoadAll<Sprite>(fileName);
            foreach (var sprite in load) {
                int id = Piece.White;
                if (sprite.name.Contains("B_")) {
                    id = Piece.Black;
                }
                if (sprite.name.Contains("Pawn")) {
                    id = id | Piece.Pawn;
                } else if (sprite.name.Contains("Rook")) {
                    id = id | Piece.Rook;
                } else if (sprite.name.Contains("King")) {
                    id = id | Piece.King;
                } else if (sprite.name.Contains("Queen")) {
                    id = id | Piece.Queen;
                } else if (sprite.name.Contains("Bishop")) {
                    id = id | Piece.Bishop;
                } else if (sprite.name.Contains("Knight")) {
                    id = id | Piece.Knight;
                }
                PieceSprite.Add(id, sprite);
            }
            Debug.Log("Done loading images...");
        }

        public static void Initialize() {
            Initialized = true;
        }
        public static bool Initialized { get; private set; } = false;
    }
}