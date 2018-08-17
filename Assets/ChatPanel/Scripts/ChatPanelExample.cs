using System.Collections;
using UnityEngine;
using WCP;


public class ChatPanelExample : MonoBehaviour
{
    public WChatPanel wcp;

    public bool dynamicTest = false;

    private IEnumerator DebugChat()
    {
        wcp.AddChatAndUpdate(true, "i say hello world", 1);
        yield return new WaitForSeconds(2);
        wcp.AddChatAndUpdate(false, "你好世界", 0);
        yield return new WaitForSeconds(2);
        wcp.AddChatAndUpdate(true, "i say hello world hello world hello world hello world ", 2);
        yield return new WaitForSeconds(2);
        wcp.AddChatAndUpdate(false, "你好世界,你好世界,你好世界,你好世界,你好世界,你好世界,你好世界", 0);
    }

    private void Start()
    {
        StartCoroutine("DebugChat");
    }

    public void PerformanceTest(int count)
    {
        for (int i = 0; i < count; i++)
            wcp.AddChat(Random.Range(0.0f, 1.0f) > 0.5f, i.ToString(), Random.Range(0, wcp.configFile.photoSpriteList.Count));

        wcp.Rebuild();
    }

    private void Update()
    {
        if (dynamicTest)
        {
            wcp.configFile.width = 600 + (int) (Mathf.Sin(Time.time) * 100);
            wcp.configFile.height = 700 + (int) (Mathf.Sin(Time.time) * 100);
            wcp.configFile.scrollBarWidth = 25 + (int)(Mathf.Sin(Time.time) * 10);
            wcp.configFile.photoSize = 100 + (int)(Mathf.Sin(Time.time) * 40);
            RectTransform rectTransform = wcp.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(Mathf.Sin(Time.time), Mathf.Sin(Time.time)) * 100;
        }
    }
}