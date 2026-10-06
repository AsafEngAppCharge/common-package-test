using Appcharge.Common.Threading;
using UnityEngine;

namespace Appcharge.SdkB
{
    public static class Facade
    {
        public static void Ping()
        {
            MainThreadDispatcher.Instance.Enqueue(() =>
                Debug.Log("[SDK B] ran on main thread via shared dispatcher"));
        }
    }
}
