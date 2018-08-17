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
        private float m_bornTime = 0;
        private ChatPanelConfigFile m_configFile = null;

        private void Awake()
        {
            m_horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
            m_photoImage = photo.GetComponent<Image>();
            m_layoutElement = GetComponent<LayoutElement>();
        }

        private void ScrollCellContent(object info)
        {
            ChatElementInfo chatElementInfo = (ChatElementInfo) info;
            m_configFile = chatElementInfo.configFile;
            if (m_configFile == null)
            {
                Debug.LogError("Null Config File");
                return;
            }

            int photoId = chatElementInfo.photoId;
            if (photoId >= 0 && photoId < m_configFile.photoSpriteList.Count)
            {
                m_photoImage.sprite = m_configFile.photoSpriteList[photoId];
                m_photoImage.color = Color.white;
            }
            else
            {
                m_photoImage.sprite = null;
                m_photoImage.color = chatElementInfo.left ? Color.red : Color.green;
            }

            m_textWidth = m_configFile.width;
            m_photoSize = m_configFile.photoSize;
            m_bornTime = chatElementInfo.bornTime;

            text.text = chatElementInfo.text;
            if (chatElementInfo.left)
            {
                m_horizontalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
                photo.transform.SetSiblingIndex(0);
                backGround.sprite = m_configFile.youBallon;
                text.rectTransform.anchoredPosition = new Vector2(-14, -10);
            }
            else
            {
                m_horizontalLayoutGroup.childAlignment = TextAnchor.UpperRight;
                backGround.transform.SetSiblingIndex(0);
                backGround.sprite = m_configFile.iBallon;
                text.rectTransform.anchoredPosition = new Vector2(-25, -10);
            }

            UpdateLayout();
        }

        private static Color m_sTempColor = Color.white;

        private static Color ColorWithAlpha(Color color, float alpha)
        {
            m_sTempColor = color;
            m_sTempColor.a = alpha;
            return m_sTempColor;
        }

        private void Update()
        {
            if (m_configFile == null || m_configFile.animateTime == 0)
                return;
            float alpha = (Time.time - m_bornTime) / m_configFile.animateTime;
            alpha = Mathf.Clamp01(alpha);
            text.color = ColorWithAlpha(text.color, alpha);
            backGround.color = ColorWithAlpha(backGround.color, alpha);
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

        [ContextMenu("update size")]
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