using UnityEngine;

public class CommonPackageSmokeTest : MonoBehaviour
{
    private void Start()
    {
        Appcharge.SdkA.Facade.Ping();
        Appcharge.SdkB.Facade.Ping();
    }
}
