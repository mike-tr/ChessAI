using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public class PieceMove : IComparable<PieceMove> {
        public readonly int StartSquare;
        public readonly int TargetSquare;
        public readonly int PromotionType;
        public float Score = 0;
        public PieceMove(int startSquare, int targetSqaure) {
            this.StartSquare = startSquare;
            this.TargetSquare = targetSqaure;
            this.PromotionType = Piece.Queen;
        }

        public PieceMove(int startSquare, int targetSqaure, int promotionType) {
            this.StartSquare = startSquare;
            this.TargetSquare = targetSqaure;
            this.PromotionType = promotionType;
        }

        public int CompareTo(PieceMove other) {
            if (other == null) {
                return 0;
            }
            return other.Score > Score ? 1 : other.Score == Score ? 0 : -1;
        }


    }
}