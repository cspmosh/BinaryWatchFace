using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BinaryWatchFace
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        DateTime _time;
        bool _ambientMode;
        bool _militaryTime;
        bool _showPowersOfTwo;
        Color _powersOfTwoColorSetting;
        Color _powersOfTwoColor;
        Color _backgroundColor;
        Color _backgroundColorSetting;        
        string _bitOnImage;
        string _bitOffImage;        
        string _bitOnImageSetting;
        string _bitOffImageSetting;
        string _backgroundImage;
        int _secondsRowHeight;        

        public ClockViewModel()
        {            
            // Initialize display            
            BackgroundColorSetting = Color.FromHex("#61A9FF"); // blue  // "#B2FF33": yellow-green     
            PowersOfTwoColorSetting = Color.FromHex("#000000");
            BitOnImageSetting = "bit_circle_on_LED.png";            
            BitOffImageSetting = "bit_circle_off.png";
            AmbientMode = false;
            MilitaryTime = false; 
            SecondsRowHeight = 40;
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();

                if (!AmbientMode) convertSecondsToBinary(_time.Second);
                convertMinutesToBinary(_time.Minute);
                convertHoursToBinary(_time.Hour);
            }
        }

        public bool MilitaryTime
        {
            get => _militaryTime;
            set
            {
                if (_militaryTime == value) return;
                _militaryTime = value;
                OnPropertyChanged(nameof(MilitaryTime));
                convertHoursToBinary(_time.Hour);
            }
        }

        public bool ShowPowersOfTwo
        {
            get => _showPowersOfTwo;
            set
            {
                if (_showPowersOfTwo == value) return;
                _showPowersOfTwo = value;
                OnPropertyChanged(nameof(ShowPowersOfTwo));                
            }
        }

        public Color PowersOfTwoColorSetting
        {
            get => _powersOfTwoColorSetting;
            set
            {
                if (_powersOfTwoColorSetting == value) return;
                _powersOfTwoColorSetting = value;
                OnPropertyChanged(nameof(PowersOfTwoColorSetting));
                PowersOfTwoColor = _powersOfTwoColorSetting;
            }
        }

        public Color PowersOfTwoColor
        {
            get => _powersOfTwoColor;
            set
            {
                if (_powersOfTwoColor == value) return;
                _powersOfTwoColor = value;
                OnPropertyChanged(nameof(PowersOfTwoColor));
            }
        }

        public Color BackgroundColor
        { 
            get => _backgroundColor;
            set 
            {
                if (_backgroundColor == value) return;
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            } 
        }

        public int SecondsRowHeight
        {
            get => _secondsRowHeight;
            set
            {
                if (_secondsRowHeight == value) return;
                _secondsRowHeight = value;
                OnPropertyChanged(nameof(SecondsRowHeight));
            }
        }

        public Color BackgroundColorSetting
        {
            get => _backgroundColorSetting;
            set
            {
                if (_backgroundColorSetting == value) return;
                _backgroundColorSetting = value;
                OnPropertyChanged(nameof(BackgroundColorSetting));
                BackgroundColor = _backgroundColorSetting;
            }
        }

        public string BitOnImageSetting
        {
            get => _bitOnImageSetting;
            set
            {
                if (_bitOnImageSetting == value) return;
                _bitOnImageSetting = value;                
                BitOnImage = _bitOnImageSetting;
                OnPropertyChanged(nameof(BitOnImageSetting));
            }
        }

        public string BitOffImageSetting
        {
            get => _bitOffImageSetting;
            set
            {
                if (_bitOffImageSetting == value) return;
                _bitOffImageSetting = value;
                BitOffImage = _bitOffImageSetting;
                OnPropertyChanged(nameof(BitOffImageSetting));
            }
        }

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                if (_backgroundImage == value) return;
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        public bool AmbientMode 
        {
            get => _ambientMode;
            set
            {
                _ambientMode = value;
                OnPropertyChanged(nameof(AmbientMode));                

                if (_ambientMode)
                {
                    BackgroundColor = Color.FromHex("#000000");
                    PowersOfTwoColor = Color.FromHex("#000000");
                    if (BitOnImage == "bit_digit_on_LED.png")
                    {
                        BitOnImage = "bit_digit_on.png";
                    } else if (BitOnImage == "bit_circle_on_LED.png")
                    {
                        BitOnImage = "bit_circle_on.png";
                    }
                    SecondsRowHeight = 0;
                } else
                {                        
                    SecondsRowHeight = 40;
                    if (BitOnImage != BitOnImageSetting)
                    {
                        BitOnImage = BitOnImageSetting;
                    }                                    
                    PowersOfTwoColor = PowersOfTwoColorSetting;
                    BackgroundColor = BackgroundColorSetting;
                }                
            }
        }

        public bool ActiveMode { get { return !AmbientMode; } }

        public string BitOnImage
        {
            get => _bitOnImage;
            set
            {
                if (_bitOnImage == value) return;
                _bitOnImage = value;
                OnPropertyChanged();
            }
        }
        public string BitOffImage
        {
            get => _bitOffImage;
            set
            {
                if (_bitOffImage == value) return;
                _bitOffImage = value;
                OnPropertyChanged();
            }
        }

        public bool IsSeconds1Visible { get; private set; }
        public bool IsSeconds2Visible { get; private set; }
        public bool IsSeconds4Visible { get; private set; }
        public bool IsSeconds8Visible { get; private set; }
        public bool IsSeconds16Visible { get; private set; }
        public bool IsSeconds32Visible { get; private set; }

        public bool IsNotSeconds1Visible { get { return !IsSeconds1Visible; } }
        public bool IsNotSeconds2Visible { get { return !IsSeconds2Visible; } }
        public bool IsNotSeconds4Visible { get { return !IsSeconds4Visible; } }
        public bool IsNotSeconds8Visible { get { return !IsSeconds8Visible; } }
        public bool IsNotSeconds16Visible { get { return !IsSeconds16Visible; } }
        public bool IsNotSeconds32Visible { get { return !IsSeconds32Visible; } }

        public bool IsMinutes1Visible { get; private set; }
        public bool IsMinutes2Visible { get; private set; }
        public bool IsMinutes4Visible { get; private set; }
        public bool IsMinutes8Visible { get; private set; }
        public bool IsMinutes16Visible { get; private set; }
        public bool IsMinutes32Visible { get; private set; }

        public bool IsNotMinutes1Visible { get { return !IsMinutes1Visible; } }
        public bool IsNotMinutes2Visible { get { return !IsMinutes2Visible; } }
        public bool IsNotMinutes4Visible { get { return !IsMinutes4Visible; } }
        public bool IsNotMinutes8Visible { get { return !IsMinutes8Visible; } }
        public bool IsNotMinutes16Visible { get { return !IsMinutes16Visible; } }
        public bool IsNotMinutes32Visible { get { return !IsMinutes32Visible; } }

        public bool IsHours1Visible { get; private set; }
        public bool IsHours2Visible { get; private set; }
        public bool IsHours4Visible { get; private set; }
        public bool IsHours8Visible { get; private set; }
        public bool IsHours16Visible { get; private set; }
        public bool IsHours32Visible { get; private set; }

        public bool IsNotHours1Visible { get { return !IsHours1Visible; } }
        public bool IsNotHours2Visible { get { return !IsHours2Visible; } }
        public bool IsNotHours4Visible { get { return !IsHours4Visible; } }
        public bool IsNotHours8Visible { get { return !IsHours8Visible; } }
        public bool IsNotHours16Visible { get { return !IsHours16Visible; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void convertSecondsToBinary(int seconds)
        {
            bool isSeconds32Visible = (seconds / 32 % 2 == 1);

            if (isSeconds32Visible.CompareTo(IsSeconds32Visible) != 0)
            {
                IsSeconds32Visible = isSeconds32Visible;
                OnPropertyChanged(nameof(IsSeconds32Visible));
                OnPropertyChanged(nameof(IsNotSeconds32Visible));
            }

            bool isSeconds16Visible = (seconds / 16 % 2 == 1);

            if (isSeconds16Visible.CompareTo(IsSeconds16Visible) != 0)
            {
                IsSeconds16Visible = isSeconds16Visible;
                OnPropertyChanged(nameof(IsSeconds16Visible));
                OnPropertyChanged(nameof(IsNotSeconds16Visible));
            }

            bool isSeconds8Visible = (seconds / 8 % 2 == 1);

            if (isSeconds8Visible.CompareTo(IsSeconds8Visible) != 0)
            {
                IsSeconds8Visible = isSeconds8Visible;
                OnPropertyChanged(nameof(IsSeconds8Visible));
                OnPropertyChanged(nameof(IsNotSeconds8Visible));
            }

            bool isSeconds4Visible = (seconds / 4 % 2 == 1);

            if (isSeconds4Visible.CompareTo(IsSeconds4Visible) != 0)
            {
                IsSeconds4Visible = isSeconds4Visible;
                OnPropertyChanged(nameof(IsSeconds4Visible));
                OnPropertyChanged(nameof(IsNotSeconds4Visible));
            }

            bool isSeconds2Visible = (seconds / 2 % 2 == 1);

            if (isSeconds2Visible.CompareTo(IsSeconds2Visible) != 0)
            {
                IsSeconds2Visible = isSeconds2Visible;
                OnPropertyChanged(nameof(IsSeconds2Visible));
                OnPropertyChanged(nameof(IsNotSeconds2Visible));
            }

            bool isSeconds1Visible = (seconds / 1 % 2 == 1);

            if (isSeconds1Visible.CompareTo(IsSeconds1Visible) !=0)
            {
                IsSeconds1Visible = isSeconds1Visible;
                OnPropertyChanged(nameof(IsSeconds1Visible));
                OnPropertyChanged(nameof(IsNotSeconds1Visible));
            }
        }

        protected void convertMinutesToBinary(int minutes)
        {
            bool isMinutes32Visible = (minutes / 32 % 2 == 1);

            if (isMinutes32Visible.CompareTo(IsMinutes32Visible) != 0)
            {
                IsMinutes32Visible = isMinutes32Visible;
                OnPropertyChanged(nameof(IsMinutes32Visible));
                OnPropertyChanged(nameof(IsNotMinutes32Visible));
            }

            bool isMinutes16Visible = (minutes / 16 % 2 == 1);

            if (isMinutes16Visible.CompareTo(IsMinutes16Visible) != 0)
            {
                IsMinutes16Visible = isMinutes16Visible;
                OnPropertyChanged(nameof(IsMinutes16Visible));
                OnPropertyChanged(nameof(IsNotMinutes16Visible));
            }

            bool isMinutes8Visible = (minutes / 8 % 2 == 1);

            if (isMinutes8Visible.CompareTo(IsMinutes8Visible) != 0)
            {
                IsMinutes8Visible = isMinutes8Visible;
                OnPropertyChanged(nameof(IsMinutes8Visible));
                OnPropertyChanged(nameof(IsNotMinutes8Visible));
            }

            bool isMinutes4Visible = (minutes / 4 % 2 == 1);

            if (isMinutes4Visible.CompareTo(IsMinutes4Visible) != 0)
            {
                IsMinutes4Visible = isMinutes4Visible;
                OnPropertyChanged(nameof(IsMinutes4Visible));
                OnPropertyChanged(nameof(IsNotMinutes4Visible));
            }

            bool isMinutes2Visible = (minutes / 2 % 2 == 1);

            if (isMinutes2Visible.CompareTo(IsMinutes2Visible) != 0)
            {
                IsMinutes2Visible = isMinutes2Visible;
                OnPropertyChanged(nameof(IsMinutes2Visible));
                OnPropertyChanged(nameof(IsNotMinutes2Visible));
            }

            bool isMinutes1Visible = (minutes / 1 % 2 == 1);

            if (isMinutes1Visible.CompareTo(IsMinutes1Visible) != 0)
            {
                IsMinutes1Visible = isMinutes1Visible;
                OnPropertyChanged(nameof(IsMinutes1Visible));
                OnPropertyChanged(nameof(IsNotMinutes1Visible));
            }
        }

        protected void convertHoursToBinary(int hours)
        {
                       
            if (!_militaryTime)
            {
                hours %= 12;    
            }
           
            bool isHours16Visible = (hours / 16 % 2 == 1);

            if (isHours16Visible.CompareTo(IsHours16Visible) != 0)
            {
                IsHours16Visible = isHours16Visible;
                OnPropertyChanged(nameof(IsHours16Visible));
                OnPropertyChanged(nameof(IsNotHours16Visible));
            }

            bool isHours8Visible = (hours / 8 % 2 == 1);

            if (isHours8Visible.CompareTo(IsHours8Visible) != 0)
            {
                IsHours8Visible = isHours8Visible;
                OnPropertyChanged(nameof(IsHours8Visible));
                OnPropertyChanged(nameof(IsNotHours8Visible));
            }

            bool isHours4Visible = (hours / 4 % 2 == 1);

            if (isHours4Visible.CompareTo(IsHours4Visible) != 0)
            {
                IsHours4Visible = isHours4Visible;
                OnPropertyChanged(nameof(IsHours4Visible));
                OnPropertyChanged(nameof(IsNotHours4Visible));
            }

            bool isHours2Visible = (hours / 2 % 2 == 1);

            if (isHours2Visible.CompareTo(IsHours2Visible) != 0)
            {
                IsHours2Visible = isHours2Visible;
                OnPropertyChanged(nameof(IsHours2Visible));
                OnPropertyChanged(nameof(IsNotHours2Visible));
            }

            bool isHours1Visible = (hours / 1 % 2 == 1);

            if (isHours1Visible.CompareTo(IsHours1Visible) != 0)
            {
                IsHours1Visible = isHours1Visible;
                OnPropertyChanged(nameof(IsHours1Visible));
                OnPropertyChanged(nameof(IsNotHours1Visible));
            }
        }


    }
}
