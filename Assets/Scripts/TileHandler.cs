using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileHandler : MonoBehaviour
{
    private ChessNode node;
    public BoardDrawer drawer;
    public SpriteRenderer pieceRenderer;
    public SpriteRenderer focusRenderer;
    public Vector2 offset;
    public void Initialize(BoardDrawer drawer, Transform holder, int x, int y, Vector2 tileOffset)
    {
        this.drawer = drawer;
        transform.parent = holder;
        transform.localPosition = new Vector2(offset.x + x * tileOffset.x, offset.y + y * tileOffset.y);
    }
    public void SetNode(ChessNode node)
    {
        this.node = node;
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (node == null)
            return;

        focusRenderer.enabled = false;
        if (node.piece != null)
        {
            pieceRenderer.enabled = true;
            var piece = node.piece;
            pieceRenderer.sprite = drawer.sprites[piece.color][piece.type];
        }
        else
        {
            pieceRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("WOW WOW");
    }
}
