using System.Collections.Generic;
using UnityEngine;

public static class SpriteItemDataExtension
{
    public static Dictionary<NormalItem.eNormalType, Sprite> ToDict(this SpriteItemData spriteItemData)
    {
        var dict = new Dictionary<NormalItem.eNormalType, Sprite>(8);
        foreach (var spriteByEnum in spriteItemData.spriteItemList)
        {
            dict.Add(spriteByEnum.normalType, spriteByEnum.sprite);
        }

        return dict;
    }
}