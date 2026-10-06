using UnityEditor;

public static class RunBothAndroidEnsures
{
    [MenuItem("Appcharge/Common Test/Apply SDK A then SDK B")]
    public static void ApplyBoth()
    {
        Appcharge.SdkA.Editor.AndroidPostProcess.Apply();
        Appcharge.SdkB.Editor.AndroidPostProcess.Apply();
    }
}
