using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        var list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        var rnd = URandom.Range(0, list.Count);
        var result = list[rnd];

        return result;
    }
    
    public static NormalItem.eNormalType GetSmallestAmountNormalTypeExcept(NormalItem.eNormalType[] types, in Dictionary<NormalItem.eNormalType, int> amountNormalType)
    {
        var list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        var smallestAmount = Int32.MaxValue;
        var result = NormalItem.eNormalType.TYPE_ONE;

        foreach (var type in list)
        {
            if (amountNormalType[type] < smallestAmount)
            {
                result = type;
                smallestAmount = amountNormalType[type];
            }
        }        

        return result;
    }
}
