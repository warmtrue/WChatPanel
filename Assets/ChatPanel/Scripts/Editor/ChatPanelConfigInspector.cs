using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChatPanelExample))]
public class CardConfigInspector : Editor
{
    private string m_textI = "";
    private string m_textU = "";

    private void SendText(bool isLeft, string text)
    {
        ChatPanelExample cpe = target as ChatPanelExample;
        if (cpe == null)
            return;

        cpe.wcp.AddChat(isLeft, text);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        m_textI = EditorGUILayout.TextField("I say:", m_textI);
        if (GUILayout.Button("Send"))
        {
            SendText(false, m_textI);
            m_textI = "";
            GUIUtility.keyboardControl = 0;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        m_textU = EditorGUILayout.TextField("You say:", m_textU);
        if (GUILayout.Button("Send"))
        {
            SendText(true, m_textU);
            m_textU = "";
            GUIUtility.keyboardControl = 0;
        }

        EditorGUILayout.EndHorizontal();
    }
}