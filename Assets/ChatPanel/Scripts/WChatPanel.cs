using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace WCP
{
    public class ChatElementInfo
    {
        public bool left;
        public string text;
        public float bornTime;
        public int photoId;
        public ChatPanelConfigFile configFile;
    }

    /// <summary>
    /// chat panel such as wechat
    /// </summary>
    /// <seealso cref="T:UnityEngine.MonoBehaviour" />
    public class WChatPanel : MonoBehaviour
    {
        public ChatPanelConfigFile configFile;
        public string chatElementName;

        private int m_photoSize = 0;
        private int m_width = 0;
        private int m_height = 0;
        private int m_scrollBarWidth = 0;

        private LoopVerticalScrollRect m_scrollRect = null;
        private RectTransform m_scrollRectTransform = null;
        private RectTransform m_scrollBarRectTransform = null;
        private Image m_backgroundImage = null;

        private readonly List<ChatElementInfo> m_chatElementInfoList = new List<ChatElementInfo>();

        private void Awake()
        {
            m_scrollRect = GetComponentInChildren<LoopVerticalScrollRect>();
            Assert.IsNotNull(m_scrollRect);
            m_scrollRectTransform = m_scrollRect.gameObject.GetComponent<RectTransform>();
            Assert.IsNotNull(m_scrollRectTransform);
            m_backgroundImage = m_scrollRect.gameObject.GetComponent<Image>();
            Assert.IsNotNull(m_backgroundImage);

            m_scrollRect.prefabSource.prefabName = chatElementName;
            m_scrollBarRectTransform = m_scrollRect.verticalScrollbar.gameObject.GetComponent<RectTransform>();
            Assert.IsNotNull(m_scrollBarRectTransform);

            m_width = configFile.width;
            m_height = configFile.height;
            m_photoSize = configFile.photoSize;
            m_scrollBarWidth = configFile.scrollBarWidth;
            m_backgroundImage.color = configFile.backgroundColor;
            UpdateLayout();
        }

        public void Clear()
        {
            m_chatElementInfoList.Clear();
            Rebuild();
        }

        private void UpdateLayout()
        {
            m_scrollRectTransform.sizeDelta = new Vector2(m_width, m_height);
            m_scrollBarRectTransform.sizeDelta = new Vector2(m_scrollBarWidth, m_height);
            m_scrollBarRectTransform.anchoredPosition = new Vector2(m_width * 0.5f + m_scrollBarWidth * 0.5f, 0);

            for (int i = 0; i < m_scrollRect.content.childCount; i++)
            {
                ChatElement ce = m_scrollRect.content.GetChild(i).GetComponent<ChatElement>();
                ce.UpdateSize(m_width, m_photoSize);
            }
        }

        public void Rebuild()
        {
            m_scrollRect.objectsToFill = m_chatElementInfoList.ToArray();
            m_scrollRect.totalCount = m_chatElementInfoList.Count;
            m_scrollRect.RefillCells();

            // scroll to bottom
            m_scrollRect.RefillCellsFromEnd();
        }

        public void AddChat(bool isLeft, string info, int photoId = -1)
        {
            m_chatElementInfoList.Add(new ChatElementInfo()
            {
                left = isLeft,
                text = info,
                bornTime = Time.time,
                photoId = photoId,
                configFile = this.configFile
            });
        }

        public void AddChatAndUpdate(bool isLeft, string info, int photoId = -1)
        {
            m_chatElementInfoList.Add(new ChatElementInfo()
            {
                left = isLeft,
                text = info,
                bornTime = Time.time,
                photoId = photoId,
                configFile = this.configFile
            });

            Rebuild();
        }

        private void Update()
        {
            if (m_width != configFile.width || m_height != configFile.height
                                            || m_photoSize != configFile.photoSize ||
                                            m_scrollBarWidth != configFile.scrollBarWidth)
            {
                // dirty
                m_width = configFile.width;
                m_height = configFile.height;
                m_photoSize = configFile.photoSize;
                m_scrollBarWidth = configFile.scrollBarWidth;
                UpdateLayout();
            }
        }
    }
}