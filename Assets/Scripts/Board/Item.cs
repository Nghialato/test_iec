using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tool.ObjectPool;

[Serializable]
public class Item
{
    private static Dictionary<string, GameObject> m_itemsListPrefab = new Dictionary<string, GameObject>(16);
    
    public Cell Cell { get; private set; }

    public Transform View { get; private set; }

    protected SpriteRenderer viewSpriteRenderer;


    public virtual Item SetView()
    {
        var prefabName = GetPrefabName();

        if (string.IsNullOrEmpty(prefabName)) return this;
        
        if (m_itemsListPrefab.TryGetValue(prefabName, out var prefab) == false)
        {
            prefab = Resources.Load<GameObject>(prefabName);
            m_itemsListPrefab.Add(prefabName, prefab);
            prefab.CreatePool(10);
        }

        if (!prefab) return this;

        prefab.Use(out var view);
        View = view.transform;
        viewSpriteRenderer = View.GetComponent<SpriteRenderer>();

        return this;
    }

    protected virtual string GetPrefabName() { return string.Empty; }

    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (!View) return;

        View.DOMove(Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.position = pos;
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (!View) return;
        
        if (viewSpriteRenderer)
        {
            viewSpriteRenderer.sortingOrder = 1;
        }
    }


    public void SetSortingLayerLower()
    {
        if (!View) return;

        if (viewSpriteRenderer)
        {
            viewSpriteRenderer.sortingOrder = 0;
        }

    }

    internal void ShowAppearAnimation()
    {
        if (!View) return;

        var scale = View.localScale;
        View.localScale = Vector3.one * 0.1f;
        View.DOScale(scale, 0.1f);
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {
            View.DOScale(0.1f, 0.1f).OnComplete(
                () =>
                {
                    View.gameObject.Recycle();
                    View = null;
                }
                );
        }
    }



    internal void AnimateForHint()
    {
        if (View)
        {
            View.DOPunchScale(View.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            View.DOKill();
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            View.gameObject.Recycle();
            View = null;
        }
    }
}
