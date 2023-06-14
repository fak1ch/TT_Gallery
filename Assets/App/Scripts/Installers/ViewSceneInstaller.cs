using System;
using App.Scripts.Scenes.Gallery;
using App.Scripts.Scenes.View;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Installers
{
    public class ViewSceneInstaller : Installer
    {
        [SerializeField] private Image _image;
        [SerializeField] private ViewSceneConfigScriptableObject _viewSceneConfig;

        private void Awake()
        {
            _image.sprite = _viewSceneConfig.SpriteFromGallery;
        }
    }
}