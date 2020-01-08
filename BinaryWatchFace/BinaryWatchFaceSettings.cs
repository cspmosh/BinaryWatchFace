using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BinaryWatchFace
{
    class BinaryWatchFaceSettings
    {
        private static BinaryWatchFaceSettings _settings;        
            
        private string _backgroundImage;
        private Color _backgroundColor;
        private string _bitOnImage;
        private string _bitOffImage;
        private bool _militaryTime;
        private bool _hidePowersOfTwo;

        private BinaryWatchFaceSettings()
        {
            _backgroundColor = Color.FromHex("#61A9FF"); // blue
            //_backgroundImage = "background_active.png";
            _bitOnImage = "light_on_LED.png";
            _bitOffImage = "light_off.png";
        }

        public static BinaryWatchFaceSettings getInstance()
        {
            if (_settings == null)
            {
                _settings = new BinaryWatchFaceSettings();
            }
            return _settings;
        }

    }
}
