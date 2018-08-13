using System.Collections;
using UnityEngine;
using WCP;


public class ChatPanelExample : MonoBehaviour
{
    public WChatPanel wcp;

    public bool dynamicTest = false;

    private IEnumerator DebugChat()
    {
        wcp.AddChat(true, "i say hello world");
        yield return new WaitForSeconds(2);
        wcp.AddChat(false, "你好世界");
        yield return new WaitForSeconds(2);
        wcp.AddChat(true, "i say hello world hello world hello world hello world ");
        yield return new WaitForSeconds(2);
        wcp.AddChat(false, "你好世界,你好世界,你好世界,你好世界,你好世界,你好世界,你好世界");
    }

    private void Start()
    {
        StartCoroutine("DebugChat");
    }

    private void Update()
    {
        if (dynamicTest)
        {
            wcp.configFile.width = 600 + (int) (Mathf.Sin(Time.time) * 100);
            wcp.configFile.height = 700 + (int) (Mathf.Sin(Time.time) * 100);
        }
    }
}