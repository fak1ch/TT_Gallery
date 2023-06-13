using App.Scripts.General.ObjectPool;
using UnityEngine;

namespace App.Scripts.Scenes.Gallery
{
    [CreateAssetMenu(fileName = "GalleryConfig", menuName = "App/GalleryConfig")]
    public class GalleryConfigScriptableObject : ScriptableObject
    {
        public PicsContainerConfig PicsContainerConfig;
        public PicsDownloaderConfig PicsDownloaderConfig;
        public DynamicAddControllerConfig DynamicAddControllerConfig;
    }
}