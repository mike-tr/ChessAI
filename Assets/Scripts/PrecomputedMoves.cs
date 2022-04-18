using System.Collections.Generic;
using static System.Math;
using UnityEngine;

namespace Chess {
    public static class PrecomputedMoves {
        private const int Right = 0;
        private const int Left = 1;
        private const int Up = 2;
        private const int Down = 3;
        private const int UpRight = 4;
        private const int DownLeft = 5;
        private const int UpLeft = 6;
        private const int DownRight = 7;
        // Directions on the map the directions are right left up down up-right down-left up-left down-right
        public static readonly int[] Directions = { 1, -1, 8, -8, 9, -9, 7, -7 };
        // Stores the number of moves that can be done in the directions from a given position
        public static readonly int[][] NumSquaresToEdge;

        public static readonly int[][] KingMoves;

        public static readonly int[][][] PawnMoves;
        public static readonly int[][][] PawnAttackMoves;
        public static readonly int[][] KnightMoves;

        static PrecomputedMoves() {
            Debug.Log("calculated all moves...");
            // calculates how many moves can be done in each direction for any square.
            NumSquaresToEdge = new int[64][];
            for (int i = 0; i < 64; i++) {
                int right = i % 8;
                int up = i / 8;
                int down = 7 - up;
                int left = 7 - right;
                NumSquaresToEdge[i] = new int[Directions.Length];
                NumSquaresToEdge[i][Right] = left;
                NumSquaresToEdge[i][Left] = right;
                NumSquaresToEdge[i][Up] = down;
                NumSquaresToEdge[i][Down] = up;
                NumSquaresToEdge[i][UpRight] = Min(down, left);
                NumSquaresToEdge[i][UpLeft] = Min(down, right);
                NumSquaresToEdge[i][DownLeft] = Min(up, right);
                NumSquaresToEdge[i][DownRight] = Min(up, left);
            }
            // generate all possible king moves for each square
            KingMoves = new int[64][];
            for (int i = 0; i < 64; i++) {
                var legalKingMoves = new List<int>();
                for (int j = 0; j < Directions.Length; j++) {
                    if (NumSquaresToEdge[i][j] != 0) {
                        // add new location
                        legalKingMoves.Add(i + Directions[j]);
                    }
                }
                KingMoves[i] = legalKingMoves.ToArray();
            }


            int[] allKnightJumps = { 15, 17, -17, -15, 10, -6, 6, -10 };
            KnightMoves = new int[64][];
            for (int i = 0; i < 64; i++) {
                var legalKnightMoves = new List<int>();
                int y = i / 8;
                int x = i % 8;
                for (int j = 0; j < allKnightJumps.Length; j++) {
                    int move = allKnightJumps[j];
                    int ny = ((i + move) / 8);
                    int nx = ((i + move) % 8);
                    if (i + move > 63 || i + move < 0) {
                        continue;
                    }
                    //Debug.Log(y + " " + ny + " | " + x + " " + nx);

                    if ((Abs(nx - x) == 1 && Abs(ny - y) == 2) || (Abs(nx - x) == 2 && Abs(ny - y) == 1)) {
                        // add new location
                        legalKnightMoves.Add(i + allKnightJumps[j]);
                    }
                    //legalKnightMoves.Add(i + allKnightJumps[j]);
                }
                KnightMoves[i] = legalKnightMoves.ToArray();
            }
        }

        // explicit "constructor"
        public static void Initialize() {
            Initialized = true;
        }
        public static bool Initialized { get; private set; } = false;
    }
}