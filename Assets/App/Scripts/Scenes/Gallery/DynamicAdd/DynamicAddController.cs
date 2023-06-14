using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gallery
{
    public class DynamicAddController : MonoBehaviour
    {
        [SerializeField] private GalleryConfigScriptableObject _galleryConfig;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GridLayoutGroup _gridContainer;

        private DynamicAddControllerConfig _config => _galleryConfig.DynamicAddControllerConfig;
        private PicsContainer _picsContainer;
        private Task _dynamicAddPicsTask;
        private Coroutine _dynamicAddPicsCoroutine;
        private int _addCount;
        private bool _dynamicAddPicsCoroutineStarted = false;
        private int _index;

        public void Initialize(PicsContainer picsContainer)
        {
            _picsContainer = picsContainer;
            _addCount = _gridContainer.constraintCount;
            _index = _config.StartIndex;
            
            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            for (int i = 0; i < _config.StartPicCount; i++)
            {
                yield return _picsContainer.AddPicByIndexRoutine(_index);
                _index++;
            }
            
            _scrollRect.onValueChanged.AddListener(ScrollRectValueChangedCallbackRoutine);
        }
        
        private void ScrollRectValueChangedCallbackRoutine(Vector2 position)
        {
            if (_dynamicAddPicsCoroutineStarted == true) return;
            
            StartCoroutine(DynamicAddPicsRoutine());
        }

        private IEnumerator DynamicAddPicsRoutine()
        {
            _dynamicAddPicsCoroutineStarted = true;
            
            float verticalPosition = _scrollRect.verticalNormalizedPosition;
            if (verticalPosition <= _config.MinScrollRectPositionForAdd)
            {
                for (int i = 0; i < _addCount; i++)
                {
                    yield return _picsContainer.AddPicByIndexRoutine(_index);
                    _index++;
                }
            }

            _dynamicAddPicsCoroutineStarted = false;
        }
    }
}