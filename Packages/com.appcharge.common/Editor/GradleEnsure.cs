using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Appcharge.Common.Editor
{
    /// <summary>
    /// Idempotent mainTemplate.gradle mutations. Safe to call from multiple SDK post-processors.
    /// </summary>
    public static class GradleEnsure
    {
        public static void EnsureImplementation(string gradlePath, string dependencyLine, string artifactId)
        {
            if (!File.Exists(gradlePath))
            {
                Debug.LogWarning($"[Appcharge.Common] Gradle file missing: {gradlePath}");
                return;
            }

            var text = File.ReadAllText(gradlePath);
            if (text.Contains(artifactId))
            {
                Debug.Log($"[Appcharge.Common] Gradle already has {artifactId}");
                return;
            }

            const string marker = "**DEPS**";
            if (text.Contains(marker))
            {
                text = text.Replace(marker, marker + Environment.NewLine + "    " + dependencyLine);
            }
            else
            {
                var match = Regex.Match(text, @"dependencies\s*\{");
                if (!match.Success)
                    throw new InvalidOperationException("No dependencies { block and no **DEPS** marker.");
                var insertAt = match.Index + match.Length;
                text = text.Insert(insertAt, Environment.NewLine + "    " + dependencyLine);
            }

            File.WriteAllText(gradlePath, text);
            Debug.Log($"[Appcharge.Common] Injected {artifactId}");
        }
    }
}
