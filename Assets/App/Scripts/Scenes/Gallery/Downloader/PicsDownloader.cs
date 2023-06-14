using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Scripts.Scenes.Gallery
{
    public class PicsDownloader : MonoBehaviour
    {
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;

        private PicsDownloaderConfig _config => _galleryConfig.PicsDownloaderConfig;
        
        private string _linkToFolder;
        private string _fileType;
        private Dictionary<int, Sprite> _spriteByIndexDictionary;

        public void Initialize()
        {
            _spriteByIndexDictionary = new Dictionary<int, Sprite>();
            _linkToFolder = _config.LinkToFolderWithPics;
            _fileType = _config.FileType;
        }

        public IEnumerator DownloadTextureRoutine(int index, PicsContainer.CreatePicWithSprite createPicWithSpriteMethod)
        {
            Uri uri = new Uri($"{_linkToFolder}{index}.{_fileType}");
            
            using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(uri))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(unityWebRequest.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                        new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect);
                    _spriteByIndexDictionary.Add(index, sprite);
                    createPicWithSpriteMethod(_spriteByIndexDictionary[index]);
                }
            }
        }
    }
}