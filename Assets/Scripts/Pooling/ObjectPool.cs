using System.Collections.Generic;
using UnityEngine;

namespace CoreBreach.Pooling
{

    public class ObjectPool<T> where T : Component
    {
        private readonly T prefab;
        private readonly Transform parent;
        private readonly Stack<T> available = new Stack<T>();

        public int CountAvailable => available.Count;

        public ObjectPool(T prefab, Transform parent, int initialSize = 0)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T instance = CreateInstance();
                instance.gameObject.SetActive(false);
                available.Push(instance);
            }
        }

        public T Get()
        {
            T item = available.Count > 0 ? available.Pop() : CreateInstance();
            item.gameObject.SetActive(true);

            if (item is IPoolable poolable)
            {
                poolable.OnSpawned();
            }
            return item;
        }

        public void Release(T item)
        {
            if (item == null) return;

            if (item is IPoolable poolable)
            {
                poolable.OnDespawned();
            }
            item.gameObject.SetActive(false);
            available.Push(item);
        }

        private T CreateInstance()
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}