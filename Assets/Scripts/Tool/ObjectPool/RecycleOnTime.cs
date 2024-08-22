using UnityEngine;

namespace Tool.ObjectPool
{
    class RecycleOnTime : MonoBehaviour, IRecycleHandle
    {
        public void SetRecycle(float time = 2)
        {
            Invoke(nameof(Recycle), time);
        }

        private void Recycle()
        {
            gameObject.Recycle();
        }
    }
}