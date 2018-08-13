using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace WCP
{
    /// <summary>
    /// chat panel such as wechat
    /// </summary>
    /// <seealso cref="T:UnityEngine.MonoBehaviour" />
    public class WChatPanel : MonoBehaviour
    {
        public ChatPanelConfigFile configFile;
        public GameObject chatElement;

        private int m_photoSize = 0;
        private int m_width = 0;
        private int m_height = 0;

        private ScrollRect m_scrollRect = null;
        private RectTransform m_contentTransform = null;
        private RectTransform m_scrollRectTransform = null;
        private VerticalLayoutGroup m_verticalLayoutGroup = null;

        private readonly List<ChatElement> m_chatElementList = new List<ChatElement>();

        private void Awake()
        {
            Assert.IsNotNull(chatElement);

            m_scrollRect = transform.GetChild(0).gameObject.GetComponent<ScrollRect>();
            Assert.IsNotNull(m_scrollRect);

            m_contentTransform = m_scrollRect.content;
            Assert.IsNotNull(m_contentTransform);

            m_scrollRectTransform = m_scrollRect.transform as RectTransform;
            Assert.IsNotNull(m_scrollRectTransform);

            m_verticalLayoutGroup = m_contentTransform.transform.GetChild(0).GetComponent<VerticalLayoutGroup>();
            Assert.IsNotNull(m_verticalLayoutGroup);
        }

        private void UpdateLayout()
        {
            m_scrollRectTransform.sizeDelta = new Vector2(m_width, m_height);
            for (int i = 0; i < m_chatElementList.Count; i++)
                m_chatElementList[i].SetSize(m_width, m_photoSize);
            m_contentTransform.sizeDelta = new Vector2(m_verticalLayoutGroup.preferredWidth, -m_verticalLayoutGroup.preferredHeight);
        }

        public void AddChat(bool isLeft, string info)
        {
            GameObject go = Instantiate(chatElement);
            go.transform.SetParent(m_verticalLayoutGroup.transform);

            ChatElement ce = go.GetComponent<ChatElement>();
            ce.SetSize(m_width, m_photoSize);
            ce.Config(isLeft, info, this);
            m_chatElementList.Add(ce);

			Canvas.ForceUpdateCanvases();
            m_contentTransform.sizeDelta = new Vector2(m_verticalLayoutGroup.preferredWidth, -m_verticalLayoutGroup.preferredHeight);
            // scroll to bottom
            m_scrollRect.verticalNormalizedPosition = 0;
        }

        private void Update()
        {
            if (m_width != configFile.width || m_height != configFile.height || m_photoSize != configFile.photoSize)
            {
                // dirty
                m_width = configFile.width;
                m_height = configFile.height;
                m_photoSize = configFile.photoSize;
                UpdateLayout();
            }
        }
    }
}