using System.Collections.Generic;
using UnityEngine;

namespace WCP
{
    public class ChatPanelConfigFile : ScriptableObject
    {
        public int photoSize;
        public int width;
        public int height;
        public int scrollBarWidth;
        public Sprite iBallon;
        public Sprite youBallon;
        public float animateTime;
        public Color backgroundColor;
        public List<Sprite> photoSpriteList;
    }
}