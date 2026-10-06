using System;
using System.Collections.Generic;
using UnityEngine;

namespace Appcharge.Common.Threading
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        private static MainThreadDispatcher _instance;
        private readonly Queue<Action> _queue = new Queue<Action>();
        private readonly object _lock = new object();

        public static MainThreadDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("AppchargeMainThreadDispatcher");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<MainThreadDispatcher>();
                }

                return _instance;
            }
        }

        public void Enqueue(Action action)
        {
            if (action == null) return;
            lock (_lock) { _queue.Enqueue(action); }
        }

        private void Update()
        {
            lock (_lock)
            {
                while (_queue.Count > 0)
                    _queue.Dequeue()?.Invoke();
            }
        }
    }
}
