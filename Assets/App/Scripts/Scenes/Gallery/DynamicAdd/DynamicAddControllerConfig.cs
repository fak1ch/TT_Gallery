using System;

namespace App.Scripts.Scenes.Gallery
{
    [Serializable]
    public class DynamicAddControllerConfig
    {
        public float MinScrollRectPositionForAdd;
        public int StartIndex = 1;
        public int StartPicCount = 6;
    }
}