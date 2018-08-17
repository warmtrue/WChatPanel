using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WCP
{
    [CustomEditor(typeof(ChatPanelExample))]
    public class CardConfigInspector : Editor
    {
        private bool m_isI = false;
        private int m_photoId = -1;
        private string m_text = "";
        private readonly List<int> m_photoIdList = new List<int>();
        private readonly List<string> m_photoIdStringList = new List<string>();

        private void UpdatePhotoIdList(ChatPanelConfigFile configFile)
        {
            if (m_photoIdList.Count != configFile.photoSpriteList.Count + 1)
            {
                m_photoIdList.Clear();
                m_photoIdStringList.Clear();
                m_photoIdList.Add(-1);
                m_photoIdStringList.Add("Null");
                for (int i = 0; i < configFile.photoSpriteList.Count; i++)
                {
                    m_photoIdList.Add(i);
                    m_photoIdStringList.Add(i.ToString());
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ChatPanelExample cpe = target as ChatPanelExample;
            if (cpe == null)
                return;
            WChatPanel wcp = cpe.wcp;
            UpdatePhotoIdList(wcp.configFile);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            m_isI = EditorGUILayout.Toggle("Is I:", m_isI);
            m_photoId = EditorGUILayout.IntPopup("PhotoId:", m_photoId, m_photoIdStringList.ToArray(), m_photoIdList.ToArray());
            EditorGUILayout.BeginHorizontal();
            m_text = EditorGUILayout.TextField("Say:", m_text);
            if (GUILayout.Button("Send") && Application.isPlaying)
            {
                wcp.AddChatAndUpdate(!m_isI, m_text, m_photoId);
                m_text = "";
                GUIUtility.keyboardControl = 0;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear") && Application.isPlaying)
                wcp.Clear();

            if (GUILayout.Button("Test Many") && Application.isPlaying)
                cpe.PerformanceTest(2000);

            EditorGUILayout.EndHorizontal();
        }
    }
}