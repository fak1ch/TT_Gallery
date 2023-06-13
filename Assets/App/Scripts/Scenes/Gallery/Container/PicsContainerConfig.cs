using System;
using App.Scripts.General.ObjectPool;

namespace App.Scripts.Scenes.Gallery
{
    [Serializable]
    public class PicsContainerConfig
    {
        public PoolData<Pic> PicPoolData;
    }
}