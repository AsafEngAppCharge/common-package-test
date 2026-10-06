using System.IO;
using System.Xml;
using UnityEngine;

namespace Appcharge.Common.Editor
{
    public static class ManifestEnsure
    {
        private const string AndroidNs = "http://schemas.android.com/apk/res/android";

        public static void EnsureUsesPermission(string manifestPath, string permission)
        {
            if (!File.Exists(manifestPath))
            {
                Debug.LogWarning($"[Appcharge.Common] Manifest missing: {manifestPath}");
                return;
            }

            var doc = new XmlDocument();
            doc.Load(manifestPath);
            var root = doc.DocumentElement;
            if (root == null) return;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node is XmlElement el && el.LocalName == "uses-permission" &&
                    el.GetAttribute("name", AndroidNs) == permission)
                {
                    Debug.Log($"[Appcharge.Common] Manifest already has {permission}");
                    return;
                }
            }

            if (root.GetAttribute("xmlns:android") != AndroidNs)
                root.SetAttribute("xmlns:android", AndroidNs);

            var perm = doc.CreateElement("uses-permission");
            perm.SetAttribute("name", AndroidNs, permission);
            root.AppendChild(perm);
            doc.Save(manifestPath);
            Debug.Log($"[Appcharge.Common] Added {permission}");
        }

        public static void EnsureMetaData(string manifestPath, string name, string value)
        {
            if (!File.Exists(manifestPath)) return;

            var doc = new XmlDocument();
            doc.Load(manifestPath);
            var app = doc.SelectSingleNode("//application") as XmlElement;
            if (app == null) return;

            foreach (XmlNode node in app.ChildNodes)
            {
                if (node is XmlElement el && el.LocalName == "meta-data" &&
                    el.GetAttribute("name", AndroidNs) == name)
                {
                    el.SetAttribute("value", AndroidNs, value);
                    doc.Save(manifestPath);
                    return;
                }
            }

            var meta = doc.CreateElement("meta-data");
            meta.SetAttribute("name", AndroidNs, name);
            meta.SetAttribute("value", AndroidNs, value);
            app.AppendChild(meta);
            doc.Save(manifestPath);
        }
    }
}
