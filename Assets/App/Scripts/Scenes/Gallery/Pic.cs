using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gallery
{
    public class Pic : MonoBehaviour
    {
        public Sprite Sprite => _image.sprite;
        
        [SerializeField] private Image _image;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}