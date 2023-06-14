using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gallery
{
    public class PicsDownloader : MonoBehaviour
    {
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;
        [SerializeField] private Image _testImage;

        private PicsDownloaderConfig _config => _galleryConfig.PicsDownloaderConfig;
        
        private WebClient _webClient;
        private string _linkToFolder;
        private string _fileType;
        private Dictionary<int, Sprite> _spriteByIndexDictionary;

        public void Initialize()
        {
            _webClient = new WebClient();
            _spriteByIndexDictionary = new Dictionary<int, Sprite>();
            _linkToFolder = _config.LinkToFolderWithPics;
            _fileType = _config.FileType;
        }

        public async Task GetSpriteByIndex(int index, PicsContainer.CreatePicWithSprite createPicWithSpriteMethod)
        {
            if (_spriteByIndexDictionary.ContainsKey(index))
            {
                createPicWithSpriteMethod(_spriteByIndexDictionary[index]);
            }
            
            Uri uri = new Uri($"{_linkToFolder}{index}.{_fileType}");
            
            try
            {
                byte[] bytes = await _webClient.DownloadDataTaskAsync(uri);
                
                Sprite sprite = LoadSprite(bytes);

                _spriteByIndexDictionary.Add(index, sprite);
                createPicWithSpriteMethod(_spriteByIndexDictionary[index]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private Sprite LoadSprite(byte[] bytes)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect);

            return sprite;
        }

        private void OnDestroy()
        {
            _webClient.Dispose();
        }
    }
}