
using Tool.ObjectPool;
using UnityEngine;

public class ItemOnRecycle : MonoBehaviour, IOnRecycle
{
    public void OnRecycle()
    {
        transform.localScale = Vector3.one;
    }
}
