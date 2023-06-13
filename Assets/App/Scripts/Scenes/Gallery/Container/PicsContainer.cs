using System.Threading.Tasks;
using App.Scripts.General.ObjectPool;
using UnityEngine;

namespace App.Scripts.Scenes.Gallery
{
    public class PicsContainer : MonoBehaviour
    {
        public delegate void CreatePicWithSprite(Sprite sprite);
        
        [SerializeField] private Transform _picsParent;
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;

        private PicsContainerConfig _config => _galleryConfig.PicsContainerConfig;
        private ObjectPool<Pic> _picPool;
        private PicsDownloader _picsDownloader;

        public void Initialize(PicsDownloader picsDownloader)
        {
            _config.PicPoolData.container = _picsParent;
            _picPool = new ObjectPool<Pic>(_config.PicPoolData);
            _picsDownloader = picsDownloader;
        }
        
        public async Task AddPicByIndex(int index)
        {
            await _picsDownloader.GetSpriteByIndex(index, CreatePic);
        }

        private void CreatePic(Sprite sprite)
        {
            Pic pic = _picPool.GetElement();
            pic.SetSprite(sprite);
            pic.gameObject.SetActive(true);
        }
    }
}