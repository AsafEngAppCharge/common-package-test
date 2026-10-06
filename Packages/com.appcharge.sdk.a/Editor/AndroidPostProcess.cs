using System.IO;
using Appcharge.Common.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Appcharge.SdkA.Editor
{
    public class AndroidPostProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android) return;
            Apply();
        }

        [MenuItem("Appcharge/Common Test/Apply SDK A Android Ensures")]
        public static void Apply()
        {
            var gradle = Path.Combine(Application.dataPath, "Plugins/Android/mainTemplate.gradle");
            var manifest = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

            GradleEnsure.EnsureImplementation(gradle, "implementation 'androidx.core:core-ktx:1.13.1'", "androidx.core:core-ktx");
            GradleEnsure.EnsureImplementation(gradle, "implementation 'com.appcharge:android-sdk-a:1.0.0'", "com.appcharge:android-sdk-a");

            ManifestEnsure.EnsureUsesPermission(manifest, "android.permission.INTERNET");
            ManifestEnsure.EnsureMetaData(manifest, "com.appcharge.sdka.ENGINE_SDK_VERSION", "1.0.0");
            AssetDatabase.Refresh();
        }
    }
}
