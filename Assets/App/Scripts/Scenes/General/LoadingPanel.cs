using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.General
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private Tween _loadingTween;
        
        public void StartLoading(float duration)
        {
            _image.fillAmount = 0;
            gameObject.SetActive(true);
            
            _loadingTween?.Kill();
            _loadingTween = _image.DOFillAmount(1, duration);
            _loadingTween.OnComplete(() => gameObject.SetActive(false));
        }
    }
}