using System.Collections;
using System.Threading.Tasks;
using App.Scripts.General.LoadScene;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.View;
using UnityEngine;

namespace App.Scripts.Scenes.Gallery
{
    public class PicsContainer : MonoBehaviour
    {
        public delegate void CreatePicWithSprite(Sprite sprite);
        
        [SerializeField] private Transform _picsParent;
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;
        [SerializeField] private ViewSceneConfigScriptableObject _viewSceneConfig;

        private PicsContainerConfig _config => _galleryConfig.PicsContainerConfig;
        private ObjectPool<Pic> _picPool;
        private PicsDownloader _picsDownloader;

        public void Initialize(PicsDownloader picsDownloader)
        {
            _config.PicPoolData.container = _picsParent;
            _picPool = new ObjectPool<Pic>(_config.PicPoolData);
            _picsDownloader = picsDownloader;
        }

        public IEnumerator AddPicByIndexRoutine(int index)
        {
            yield return _picsDownloader.DownloadTextureRoutine(index, CreatePic);
        }

        private void CreatePic(Sprite sprite)
        {
            Pic pic = _picPool.GetElement();
            pic.SetSprite(sprite);
            pic.OnShowPicButtonClicked += LoadViewScene;
            pic.gameObject.SetActive(true);
        }

        private void LoadViewScene(Sprite sprite)
        {
            _viewSceneConfig.SpriteFromGallery = sprite;
            SceneLoader.Instance.LoadScene(SceneEnum.View);
        }
    }
}