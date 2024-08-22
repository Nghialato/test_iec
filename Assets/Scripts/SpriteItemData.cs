using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SpriteItemData", fileName = "SpriteItemData", order = 0)]
public class SpriteItemData : ScriptableObject
{
    public List<SpriteByEnum> spriteItemList = new List<SpriteByEnum>(8);
}

[Serializable]
public struct SpriteByEnum
{
    public NormalItem.eNormalType normalType;
    public Sprite sprite;
}