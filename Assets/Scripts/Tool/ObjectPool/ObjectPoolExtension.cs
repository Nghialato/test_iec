using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Tool.ObjectPool
{
    public static class ObjectPoolExtension
    {
        private static List<IRecycleHandle> _recycleHandles = new List<IRecycleHandle>(32);

        private static IObjectPool _objectPool;

        public static void Init(IObjectPool objectPool) => _objectPool = objectPool;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Recycle(this GameObject gameObject) => gameObject.GetComponent<IObjectInPool>()?.Recycle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitRecycleHandle(this GameObject gameObject, float lifeTime)
        {
            _recycleHandles.Clear();
            gameObject.GetComponentsInChildren(false, _recycleHandles);
            foreach (var handle in _recycleHandles)
            {
                handle.SetRecycle(lifeTime);
            }

            if (_recycleHandles.Count == 0)
            {
                var recycleOnTime = gameObject.AddComponent<RecycleOnTime>();
                recycleOnTime.SetRecycle(lifeTime);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePool(this Object prefab, uint cap = 1)=> _objectPool.CreatPool(prefab, cap);
		
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Use(this Object prefab, out GameObject go) => _objectPool.Use(prefab, out go);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var t = gameObject.GetComponent<T>();
            if (t == default)
                t = gameObject.AddComponent<T>();

            return t;
        }
    }
}