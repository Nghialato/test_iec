using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : Item
{
    public enum eBonusType
    {
        NONE,
        HORIZONTAL,
        VERTICAL,
        ALL
    }

    public eBonusType ItemType;

    public void SetType(eBonusType type)
    {
        ItemType = type;
    }

    protected override string GetPrefabName()
    {
        string prefabname = string.Empty;
        switch (ItemType)
        {
            case eBonusType.NONE:
                break;
            case eBonusType.HORIZONTAL:
                prefabname = Constants.PREFAB_BONUS_HORIZONTAL;
                break;
            case eBonusType.VERTICAL:
                prefabname = Constants.PREFAB_BONUS_VERTICAL;
                break;
            case eBonusType.ALL:
                prefabname = Constants.PREFAB_BONUS_BOMB;
                break;
        }

        return prefabname;
    }

    internal override bool IsSameType(Item other)
    {
        BonusItem it = other as BonusItem;

        return it != null && it.ItemType == this.ItemType;
    }

    // Explode View Shouldn't have Activate Bonus
    public List<Cell> GetListExplode()
    {
        switch (ItemType)
        {
            case eBonusType.HORIZONTAL:
                return ExplodeHorizontalLine();
            case eBonusType.VERTICAL:
                return ExplodeVerticalLine();
            case eBonusType.ALL:
                return ExplodeBomb();
            default:
                Debug.LogError("Not Found Bonus Type");
                return default;
        }
    }

    private List<Cell> ExplodeBomb()
    {
        var list = new List<Cell>();
        if (Cell.NeighbourBottom) list.Add(Cell.NeighbourBottom);
        if (Cell.NeighbourUp) list.Add(Cell.NeighbourUp);
        if (Cell.NeighbourLeft)
        {
            list.Add(Cell.NeighbourLeft);
            if (Cell.NeighbourLeft.NeighbourUp)
            {
                list.Add(Cell.NeighbourLeft.NeighbourUp);
            }
            if (Cell.NeighbourLeft.NeighbourBottom)
            {
                list.Add(Cell.NeighbourLeft.NeighbourBottom);
            }
        }
        if (Cell.NeighbourRight)
        {
            list.Add(Cell.NeighbourRight);
            if (Cell.NeighbourRight.NeighbourUp)
            {
                list.Add(Cell.NeighbourRight.NeighbourUp);
            }
            if (Cell.NeighbourRight.NeighbourBottom)
            {
                list.Add(Cell.NeighbourRight.NeighbourBottom);
            }
        }

        return list;
    }

    private List<Cell> ExplodeVerticalLine()
    {
        var list = new List<Cell>();

        Cell newcell = Cell;
        while (true)
        {
            Cell next = newcell.NeighbourUp;
            if (next == null) break;

            list.Add(next);
            newcell = next;
        }

        newcell = Cell;
        while (true)
        {
            Cell next = newcell.NeighbourBottom;
            if (next == null) break;

            list.Add(next);
            newcell = next;
        }

        return list;
    }

    private List<Cell> ExplodeHorizontalLine()
    {
        var list = new List<Cell>();

        Cell newcell = Cell;
        while (true)
        {
            Cell next = newcell.NeighbourRight;
            if (next == null) break;

            list.Add(next);
            newcell = next;
        }

        newcell = Cell;
        while (true)
        {
            Cell next = newcell.NeighbourLeft;
            if (next == null) break;

            list.Add(next);
            newcell = next;
        }

        return list;
    }
}
