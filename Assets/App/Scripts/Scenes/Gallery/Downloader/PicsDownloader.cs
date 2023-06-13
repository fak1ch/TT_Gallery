using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gallery
{
    public class PicsDownloader : MonoBehaviour
    {
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;

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
                string savePath = Path.Combine(Application.persistentDataPath, $"{index}.{_fileType}");
                await _webClient.DownloadFileTaskAsync(uri, savePath);

                Sprite sprite = LoadSprite(savePath);
                _spriteByIndexDictionary.Add(index, sprite);
                createPicWithSpriteMethod(_spriteByIndexDictionary[index]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private Sprite LoadSprite(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            
            if (File.Exists(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                    new Vector2(0.5f, 0.5f));
                
                return sprite;
            }
            
            return null;
        }

        private void OnDestroy()
        {
            _webClient.Dispose();
        }
    }
}