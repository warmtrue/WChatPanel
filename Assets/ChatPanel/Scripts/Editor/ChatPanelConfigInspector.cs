using UnityEngine;
using UnityEditor;
using WCP;

[CustomEditor(typeof(ChatPanelExample))]
public class CardConfigInspector : Editor
{
    private string m_textI = "";
    private string m_textU = "";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ChatPanelExample cpe = target as ChatPanelExample;
        if (cpe == null)
            return;
        WChatPanel wcp = cpe.wcp;

        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        m_textI = EditorGUILayout.TextField("I say:", m_textI);
        if (GUILayout.Button("Send") && Application.isPlaying)
        {
            wcp.AddChatAndUpdate(false, m_textI);
            m_textI = "";
            GUIUtility.keyboardControl = 0;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        m_textU = EditorGUILayout.TextField("You say:", m_textU);
        if (GUILayout.Button("Send") && Application.isPlaying)
        {
            wcp.AddChatAndUpdate(true, m_textU);
            m_textU = "";
            GUIUtility.keyboardControl = 0;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear") && Application.isPlaying)
            wcp.Clear();

        if (GUILayout.Button("Test Many") && Application.isPlaying)
            cpe.PerformanceTest(2000);

        EditorGUILayout.EndHorizontal();
    }
}