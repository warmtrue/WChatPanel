using UnityEngine;
using UnityEngine.UI;

namespace WCP
{
    public class ChatElement : MonoBehaviour
    {
        public Image backGround;
        public Text text;
        public Image photo;

        private HorizontalLayoutGroup m_horizontalLayoutGroup = null;
        private Image m_photoImage = null;
        private LayoutElement m_layoutElement = null;
        private int m_textWidth = 0;
        private int m_photoSize = 0;

        void ScrollCellContent(object info)
        {
            ChatElementInfo chatElementInfo = (ChatElementInfo)info;
            m_textWidth = chatElementInfo.width;
            m_photoSize = chatElementInfo.photoSize;

            text.text = chatElementInfo.text;
            if (chatElementInfo.left)
            {
                m_horizontalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
                photo.transform.SetSiblingIndex(0);
                m_photoImage.color = Color.red;
                backGround.sprite = chatElementInfo.bgSprite;
                text.rectTransform.anchoredPosition = new Vector2(-14, -10);
            }
            else
            {
                m_horizontalLayoutGroup.childAlignment = TextAnchor.UpperRight;
                backGround.transform.SetSiblingIndex(0);
                m_photoImage.color = Color.green;
                backGround.sprite = chatElementInfo.bgSprite;
                text.rectTransform.anchoredPosition = new Vector2(-25, -10);
            }

            UpdateLayout();
        }

        private void Awake()
        {
            m_horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
            m_photoImage = photo.GetComponent<Image>();
            m_layoutElement = GetComponent<LayoutElement>();
        }

        public void UpdateSize(int textWidth, int photoSize)
        {
            if (m_textWidth == textWidth && m_photoSize == photoSize)
                return;

            m_textWidth = textWidth;
            m_photoSize = photoSize;

            m_photoImage.rectTransform.sizeDelta = new Vector2(m_photoSize, m_photoSize);
            UpdateLayout();
        }

        private static float GetTextWidth(Text textIn)
        {
            float width = 0;
            UICharInfo[] charInfos = textIn.cachedTextGeneratorForLayout.GetCharactersArray();
            for (int i = 0; i < charInfos.Length; i++)
                width += charInfos[i].charWidth;

            return width;
        }

        [ContextMenu("debug update size")]
        private void UpdateLayout()
        {
            float textWidth = m_textWidth * 0.5f - 30;
            text.rectTransform.sizeDelta = new Vector2(textWidth, Screen.height);

            var settings = text.GetGenerationSettings(text.rectTransform.sizeDelta);
            text.cachedTextGeneratorForLayout.Populate(text.text, settings);

            float textHeight = text.cachedTextGeneratorForLayout.lineCount * (3 + text.fontSize * 1.2f);

            if (text.cachedTextGeneratorForLayout.lineCount == 1)
                textWidth = GetTextWidth(text);

            text.rectTransform.sizeDelta = new Vector2(textWidth, textHeight);
            backGround.rectTransform.sizeDelta = new Vector2(textWidth + 40, textHeight + 20);

            float elementHeight = textHeight + 20;
            if (m_photoImage.rectTransform.sizeDelta.y > elementHeight)
                elementHeight = m_photoImage.rectTransform.sizeDelta.y;

            m_layoutElement.preferredWidth = m_textWidth;
            m_layoutElement.preferredHeight = elementHeight;
        }        
    }
}