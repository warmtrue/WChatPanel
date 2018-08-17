using UnityEngine;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using UnityEditor;

namespace WCP
{
    public class ConfigFileFactory
    {
        [MenuItem("Assets/Create/Chat Panel Config File", priority = 201)]
        private static void MenuCreateChatPanelConfigFile()
        {
            var icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<DoCreateChatPanelConfigFile>(), "ChatPanelConfig.asset", icon,
                null);
        }

        internal static ChatPanelConfigFile CreateChatPanelConfigFileAtPath(string path)
        {
            var profile = ScriptableObject.CreateInstance<ChatPanelConfigFile>();
            profile.name = Path.GetFileName(path);
            AssetDatabase.CreateAsset(profile, path);
            return profile;
        }
    }

    internal class DoCreateChatPanelConfigFile : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            ChatPanelConfigFile profile = ConfigFileFactory.CreateChatPanelConfigFileAtPath(pathName);
            ProjectWindowUtil.ShowCreatedAsset(profile);
        }
    }
}