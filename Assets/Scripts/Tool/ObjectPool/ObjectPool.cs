﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tool.ObjectPool
{
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        public int CreatPool(Object prefab, uint capacity = 1)
		{
			var poolID = prefab.GetInstanceID();

			if (_prefabPools.ContainsKey(poolID))
				return poolID;

			var stack = new Stack<GameObject>();

			for (int i = 0; i < capacity; i++)
			{
				if (false == CreateObject(prefab, out var go))
				{
					break;
				}

				go.SetActive(false);
				stack.Push(go);
			}

			_prefabPools.Add(poolID, prefab);
			_objectPools.Add(poolID, stack);

			return poolID;
		}

		/// <summary>
		/// create game object from prefab
		/// </summary>
		/// <param name="prefab"></param>
		/// <param name="go"></param>
		/// <returns></returns>
		private bool CreateObject(Object prefab, out GameObject go)
		{
			go = Instantiate(prefab) as GameObject;

			if (go == null)
			{
				Debug.LogError($"Cant create object prefab: {(prefab == null ? "NULL" : prefab.name)}");
				return false;
			}

			// init obj in pool
			var objInPool = go.GetOrAddComponent<ObjectInPool>();
			objInPool.InitPool(prefab.GetInstanceID(), this);

			return true;
		}

		public void Recycle(int poolID, GameObject obj)
		{
			if (_objectPools.ContainsKey(poolID) == false)
				throw new Exception($"Cant Found Pool ID: {poolID}");

			obj.SetActive(false);
			_objectPools[poolID].Push(obj);
		}

		/// <summary>
		/// Require a game object from pool
		/// </summary>
		/// <param name="poolID"></param>
		/// <param name="go"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public bool Use(int poolID, out GameObject go)
		{
			if (_prefabPools.ContainsKey(poolID) == false)
			{
				throw new ArgumentException($"Cant Found Pool ID: {poolID}");
			}

			var stacks = _objectPools[poolID];

			if (stacks != null && stacks.Count > 0)
			{
				go = stacks.Pop();
				if (go)
				{
					go.SetActive(true);
					return true;
				}
			}

			var prefab = _prefabPools[poolID];

			if (CreateObject(prefab, out go))
				return true;

			return false;
		}
		
		public bool Use(Object prefab, out GameObject go) => Use(prefab.GetInstanceID(), out go);
		

		private static readonly Dictionary<int, Stack<GameObject>> _objectPools = new Dictionary<int, Stack<GameObject>>(512);
		private static readonly Dictionary<int, Object> _prefabPools = new Dictionary<int, Object>(512);

		private readonly List<IRecycleHandle> _recycleHandles = new List<IRecycleHandle>(32);

		public void ClearPool() => _objectPools.Clear();
    }
}