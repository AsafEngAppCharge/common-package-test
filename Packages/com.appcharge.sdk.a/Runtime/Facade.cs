using Appcharge.Common.Threading;
using UnityEngine;

namespace Appcharge.SdkA
{
    public static class Facade
    {
        public static void Ping()
        {
            MainThreadDispatcher.Instance.Enqueue(() =>
                Debug.Log("[SDK A] ran on main thread via shared dispatcher"));
        }
    }
}
