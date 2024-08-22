using UnityEngine;

namespace Tool.ObjectPool
{
    public interface IObjectPool
    {
        /// <summary>
        /// Create Object Pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="capacity"></param>
        /// <returns>Pool ID</returns>
        int CreatPool(Object prefab, uint capacity = 1);

        /// <summary>
        /// cycle a game object with pool id
        /// </summary>
        /// <param name="poolID"></param>
        /// <param name="obj"></param>
        void Recycle(int poolID, GameObject obj);

        /// <summary>
        /// Use a game Object In pool
        /// </summary>
        /// <param name="poolID"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        bool Use(int poolID, out GameObject go);
        
        /// <summary>
        ///  Use a game Object In pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        bool Use(Object prefab, out GameObject go);

        void ClearPool();
    }

    interface IObjectInPool
    {
        int PoolID { get; }
        void Recycle();
    }
}