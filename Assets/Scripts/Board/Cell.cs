using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int BoardX { get; private set; }

    public int BoardY { get; private set; }

    public Item Item { get; private set; }

    public Cell NeighbourUp { get; set; }

    public Cell NeighbourRight { get; set; }

    public Cell NeighbourBottom { get; set; }

    public Cell NeighbourLeft { get; set; }

    public CellData cellData;

    public bool IsEmpty => Item == null;

    public void Setup(int cellX, int cellY)
    {
        this.BoardX = cellX;
        this.BoardY = cellY;
        cellData.boardX = cellX;
        cellData.boardY = cellY;
    }

    public bool IsNeighbour(Cell other)
    {
        return BoardX == other.BoardX && Mathf.Abs(BoardY - other.BoardY) == 1 ||
            BoardY == other.BoardY && Mathf.Abs(BoardX - other.BoardX) == 1;
    }


    public void Free()
    {
        Item = null;
    }

    public void Assign(Item item)
    {
        Item = item;
        Item.SetCell(this);
        if (Item is NormalItem normalItem)
        {
            cellData.normalType = normalItem.ItemType;
        }
        else
        {
            cellData.isNormal = false;
        }
    }

    public void ApplyItemPosition(bool withAppearAnimation)
    {
        Item.SetViewPosition(this.transform.position);

        if (withAppearAnimation)
        {
            Item.ShowAppearAnimation();
        }
    }

    internal void Clear()
    {
        if (Item != null)
        {
            Item.Clear();
            Item = null;
        }
    }

    internal bool IsSameType(Cell other)
    {
        return Item != null && other.Item != null && Item.IsSameType(other.Item);
    }

    internal void ExplodeItem()
    {
        if (Item == null) return;

        Item.ExplodeView();
        Item = null;
    }

    internal void AnimateItemForHint()
    {
        Item.AnimateForHint();
    }

    internal void StopHintAnimation()
    {
        Item?.StopAnimateForHint();
    }

    internal void ApplyItemMoveToPosition()
    {
        Item.AnimationMoveToPosition();
    }

    internal List<NormalItem.eNormalType> GetAllNeighborNormalTypes()
    {
        var types = new List<NormalItem.eNormalType>(4);

        if (NeighbourBottom != null)
        {
            if (NeighbourBottom.Item is NormalItem nitem)
            {
                types.Add(nitem.ItemType);
            }
        }

        if (NeighbourLeft != null)
        {
            if (NeighbourLeft.Item is NormalItem nitem)
            {
                types.Add(nitem.ItemType);
            }
        }
                
        if (NeighbourUp != null)
        {
            if (NeighbourUp.Item is NormalItem nitem)
            {
                types.Add(nitem.ItemType);
            }
        }
                
        if (NeighbourRight != null)
        {
            if (NeighbourRight.Item is NormalItem nitem)
            {
                types.Add(nitem.ItemType);
            }
        }

        return types;
    }
}

[Serializable]
public class CellData
{
    public int boardX;
    public int boardY;
    public NormalItem.eNormalType normalType;
    public bool isNormal;

    public CellData(int boardX, int boardY, NormalItem.eNormalType normalType)
    {
        this.boardX = boardX;
        this.boardY = boardY;
        this.normalType = normalType;
    }

    public CellData Clone() => new CellData(boardX, boardY, normalType);
}
