using System.Collections;
using UnityEngine;

namespace WCP
{
    public class Example : MonoBehaviour
    {
        public WChatPanel wcp;

        public string word;

        public bool isDynamic = false;

        [ContextMenu("I talk")]
        private void DebugITalk()
        {
            wcp.AddChat(false, word);
        }

        [ContextMenu("You talk")]
        private void DebugYouTalk()
        {
            wcp.AddChat(true, word);
        }

        IEnumerator DebugChat()
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
            if (isDynamic)
            {
                wcp.width = 600 + (int)(Mathf.Sin(Time.time) * 100);
                wcp.height = 700 + (int)(Mathf.Sin(Time.time) * 100);
            }
        }
    }
}