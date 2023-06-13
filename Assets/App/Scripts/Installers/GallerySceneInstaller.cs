using System;
using App.Scripts.Scenes.Gallery;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Installers
{
    public class GallerySceneInstaller : Installer
    {
        [SerializeField] private PicsDownloader _picsDownloader;
        [SerializeField] private PicsContainer _picsContainer;
        [SerializeField] private DynamicAddController _dynamicAddController;

        private void Awake()
        {
            _picsDownloader.Initialize();
            _picsContainer.Initialize(_picsDownloader);
            _dynamicAddController.Initialize(_picsContainer);
        }
    }
}