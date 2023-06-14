using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gallery
{
    public class Pic : MonoBehaviour
    {
        public event Action<Sprite> OnShowPicButtonClicked;
        
        [SerializeField] private Image _image;
        [SerializeField] private Button _showPicButton;

        private void OnEnable()
        {
            _showPicButton.onClick.AddListener(ShowPicButtonClickedCallback);
        }

        private void OnDisable()
        {
            _showPicButton.onClick.RemoveListener(ShowPicButtonClickedCallback);
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        
        private void ShowPicButtonClickedCallback()
        {
            OnShowPicButtonClicked?.Invoke(_image.sprite);
        }
    }
}