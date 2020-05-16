using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDrawer : MonoBehaviour
{
    public string fileName;
    public Dictionary<TeamColor, Dictionary<PieceType, Sprite>> sprites = new Dictionary<TeamColor, Dictionary<PieceType, Sprite>>();
    public TileHandler[,] tiles = new TileHandler[8, 8];
    public ChessBoard board;
    public TileHandler handlerPrefab;

    public Vector2 boardOffset;
    public Vector2 tileOffset;
    private Transform holder;

    private void Start()
    {
        var sp = GetComponent<SpriteRenderer>();
        Debug.Log(sp.bounds.size);
        tileOffset.x = sp.bounds.size.x / 8;
        tileOffset.y = sp.bounds.size.y / 8;

        board = new ChessBoard();
        sprites.Add(TeamColor.black, new Dictionary<PieceType, Sprite>());
        sprites.Add(TeamColor.white, new Dictionary<PieceType, Sprite>());
        holder = new GameObject("holder").transform;
        holder.parent = transform;
        holder.localPosition = boardOffset;
        Sprite[] load = Resources.LoadAll<Sprite>(fileName);
        foreach (var current in load)
        {
            TeamColor color = TeamColor.white;
            if (current.name.Contains("B_"))
            {
                color = TeamColor.black;
            }
            PieceType type = PieceType.Pawn;
            foreach (PieceType t in System.Enum.GetValues(typeof(PieceType)))
            {
                if (current.name.Contains(t.ToString()))
                {
                    type = t;
                }
            }
            sprites[color].Add(type, current);
        }
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tiles[x, y] = Instantiate(handlerPrefab);
                tiles[x, y].Initialize(this, holder, x, y, tileOffset);
            }
        }
        SwitchBoard(board);
    }

    public void SwitchBoard(ChessBoard newBoard)
    {
        board = newBoard;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tiles[x, y].SetNode(newBoard.board[x, y]);
            }
        }
    }
}
