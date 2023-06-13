using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu
{
    public class DeviceRotationInstaller : MonoBehaviour
    {
        [SerializeField] private bool _portrait;
        [SerializeField] private bool _portraitUpsideDown;
        [SerializeField] private bool _landscapeLeft;
        [SerializeField] private bool _landscapeRight;

        private void Start()
        {
            Screen.autorotateToPortrait = _portrait;
            Screen.autorotateToPortraitUpsideDown = _portraitUpsideDown;
            Screen.autorotateToLandscapeLeft = _landscapeLeft;
            Screen.autorotateToLandscapeRight = _landscapeRight;

            if ((_portrait == true || _portraitUpsideDown == true) && _landscapeLeft == false && _landscapeRight == false)
            {
                if (Screen.orientation != ScreenOrientation.Portrait 
                    && Screen.orientation != ScreenOrientation.PortraitUpsideDown)
                {
                    Screen.orientation = ScreenOrientation.Portrait;
                }
            }
            else if ((_landscapeLeft == true || _landscapeRight == true) && _portrait == false && _portraitUpsideDown == false)
            {
                if (Screen.orientation != ScreenOrientation.LandscapeLeft 
                    && Screen.orientation != ScreenOrientation.LandscapeRight)
                {
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                }
            }
        }
    }
}