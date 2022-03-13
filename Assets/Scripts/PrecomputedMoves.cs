using System.Collections.Generic;
using static System.Math;
using UnityEngine;

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
            NumSquaresToEdge[i][Right] = right;
            NumSquaresToEdge[i][Left] = left;
            NumSquaresToEdge[i][Up] = up;
            NumSquaresToEdge[i][Down] = down;
            NumSquaresToEdge[i][UpRight] = Min(up, right);
            NumSquaresToEdge[i][UpLeft] = Min(up, left);
            NumSquaresToEdge[i][DownLeft] = Min(down, left);
            NumSquaresToEdge[i][DownRight] = Min(down, right);
        }
        // generate all possible king moves for each square
        KingMoves = new int[64][];
        var legalKingMoves = new List<int>();
        for (int i = 0; i < 64; i++) {
            for (int j = 0; j < Directions.Length; j++) {
                if (NumSquaresToEdge[i][j] != 0) {
                    legalKingMoves.Add(Directions[j]);
                }
            }
            KingMoves[i] = legalKingMoves.ToArray();
        }


        int[] allKnightJumps = { 15, 17, -17, -15, 10, -6, 6, -10 };
    }

    // explicit "constructor"
    public static void Initialize() {
        Initialized = true;
    }
    public static bool Initialized { get; private set; } = false;


}
