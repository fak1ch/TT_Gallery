using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gallery
{
    [Serializable]
    public class DynamicAddControllerConfig
    {
        public float MinScrollRectPositionForAdd;
        public int _startIndex = 1;
    }
    
    public class DynamicAddController : MonoBehaviour
    {
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GridLayoutGroup _gridContainer;

        private DynamicAddControllerConfig _config => _galleryConfig.DynamicAddControllerConfig;
        private PicsContainer _picsContainer;
        private Task _dynamicAddPicsTask;
        private int _addCount;
        private int _index;

        public void Initialize(PicsContainer picsContainer)
        {
            _picsContainer = picsContainer;
            _addCount = _gridContainer.constraintCount;

            _scrollRect.onValueChanged.AddListener(ScrollRectValueChangedCallback);
        }

        private void ScrollRectValueChangedCallback(Vector2 position)
        {
            if(_dynamicAddPicsTask?.IsCompleted == false) return;
            
            _dynamicAddPicsTask = DynamicAddPics();
        }

        private async Task DynamicAddPics()
        {
            float verticalPosition = _scrollRect.verticalNormalizedPosition;
            if (verticalPosition <= _config.MinScrollRectPositionForAdd)
            {
                for (int i = 0; i < _addCount; i++)
                {
                    await _picsContainer.AddPicByIndex(_index);
                    _index++;
                }
            }
        }
    }
}