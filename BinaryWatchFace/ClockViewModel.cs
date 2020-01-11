using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace BinaryWatchFace
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public enum BitStyle
        {
            DIGITS = 0, DIGITS_LED = 1, CIRCLES = 2, CIRCLES_LED = 3
        }

        public enum BitColor
        {
            BLUE = 0, GREEN = 1
        }

        DateTime _time;
        bool _ambientMode;
        bool _militaryTime;
        Color _powersOfTwoColor;
        Color _backgroundColor;
        Color _backgroundColorSetting;
        string _bitOnImage;
        string _bitOffImage;
        string _bitOnAmbientImage;
        string _bitOffAmbientImage;
        string _backgroundImage;
        ICommand _tapCommand;
        bool _customizeVisible;
        BitStyle _style;
        BitColor _color;

        public ClockViewModel()
        {
            // Initialize display            
            BackgroundColorSetting = Color.FromHex("#61A9FF"); // blue  // "#B2FF33": yellow-green     
            PowersOfTwoColor = Color.FromHex("#000000");
            WatchStyle = BitStyle.DIGITS_LED;
            WatchColor = BitColor.BLUE;
            AmbientMode = false;
            MilitaryTime = false;
            _tapCommand = new Command(OnTapped);            
        }

        public ICommand TapCommand
        {
            get { return _tapCommand; }
        }

        public BitStyle WatchStyle
        {
            get => _style;
            set
            {
                if (_style == value) return;
                _style = value;
                switch (_style)
                {
                    case BitStyle.DIGITS:
                        BitOnImage = "bit_digit_on.png";
                        BitOffImage = "bit_digit_off.png";
                        BitOnAmbientImage = "bit_digit_on.png";
                        BitOffAmbientImage = "bit_digit_off.png";
                        break;
                    case BitStyle.DIGITS_LED:
                        BitOnImage = "bit_digit_on_LED.png";
                        BitOffImage = "bit_digit_off.png";
                        BitOnAmbientImage = "bit_digit_on.png";
                        BitOffAmbientImage = "bit_digit_off.png";
                        break;
                    case BitStyle.CIRCLES:
                        BitOnImage = "bit_circle_on.png";
                        BitOffImage = "bit_circle_off.png";
                        BitOnAmbientImage = "bit_circle_on.png";
                        BitOffAmbientImage = "bit_circle_off.png";
                        break;
                    case BitStyle.CIRCLES_LED:
                        BitOnImage = "bit_circle_on_LED.png";
                        BitOffImage = "bit_circle_off.png";
                        BitOnAmbientImage = "bit_circle_on.png";
                        BitOffAmbientImage = "bit_circle_off.png";
                        break;
                }
            }                 
        }

        public BitColor WatchColor
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                switch (_color)
                {
                    case BitColor.BLUE:
                        BackgroundColorSetting = Color.FromHex("#61A9FF"); // blue  
                        break;
                    case BitColor.GREEN:
                        BackgroundColorSetting = Color.FromHex("#73E500"); // yellow-green
                        break;                    
                }
            }
        }

        void OnTapped(object s)
        {

            switch (s)
            {
                case "Style":
                    WatchStyle = (BitStyle)(((int)_style + 1) % Enum.GetNames(typeof(BitStyle)).Length);
                    break;
                case "Color":
                    WatchColor = (BitColor)(((int)_color + 1) % Enum.GetNames(typeof(BitColor)).Length);
                    break;
                default:
                    IsCustomizeVisible = !IsCustomizeVisible;
                    break;
            }                                          

        }

        public bool IsCustomizeVisible
        {
            get => _customizeVisible;
            set
            {
                if (_customizeVisible == value) return;
                _customizeVisible = value;
                OnPropertyChanged(nameof(IsCustomizeVisible));
            }
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

        public bool AmbientMode
        {
            get => _ambientMode;
            set
            {
                _ambientMode = value;
                OnPropertyChanged(nameof(AmbientMode));

                if (_ambientMode)
                {
                    BackgroundColor = Color.FromHex("#888888");                    
                }
                else
                {
                    BackgroundColor = BackgroundColorSetting;
                }
            }
        }

        public string BitOnImage
        {
            get => _bitOnImage;
            set
            {
                if (_bitOnImage == value) return;
                _bitOnImage = value;
                OnPropertyChanged(nameof(BitOnImage));
            }
        }
        public string BitOffImage
        {
            get => _bitOffImage;
            set
            {
                if (_bitOffImage == value) return;
                _bitOffImage = value;
                OnPropertyChanged(nameof(BitOffImage));
            }
        }
        public string BitOnAmbientImage
        {
            get => _bitOnAmbientImage;
            set
            {
                if (_bitOnAmbientImage == value) return;
                _bitOnAmbientImage = value;
                OnPropertyChanged(nameof(BitOnAmbientImage));
            }
        }
        public string BitOffAmbientImage
        {
            get => _bitOffAmbientImage;
            set
            {
                if (_bitOffAmbientImage == value) return;
                _bitOffAmbientImage = value;
                OnPropertyChanged(nameof(BitOffAmbientImage));
            }
        }

        public bool IsSeconds1On { get; private set; }
        public bool IsSeconds2On { get; private set; }
        public bool IsSeconds4On { get; private set; }
        public bool IsSeconds8On { get; private set; }
        public bool IsSeconds16On { get; private set; }
        public bool IsSeconds32On { get; private set; }

        public bool IsMinutes1On { get; private set; }
        public bool IsMinutes2On { get; private set; }
        public bool IsMinutes4On { get; private set; }
        public bool IsMinutes8On { get; private set; }
        public bool IsMinutes16On { get; private set; }
        public bool IsMinutes32On { get; private set; }

        public bool IsHours1On { get; private set; }
        public bool IsHours2On { get; private set; }
        public bool IsHours4On { get; private set; }
        public bool IsHours8On { get; private set; }
        public bool IsHours16On { get; private set; }
        public bool IsHours32On { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void convertSecondsToBinary(int seconds)
        {
            bool isSeconds32On = (seconds / 32 % 2 == 1);

            if (isSeconds32On.CompareTo(IsSeconds32On) != 0)
            {
                IsSeconds32On = isSeconds32On;
                OnPropertyChanged(nameof(IsSeconds32On));
            }

            bool isSeconds16On = (seconds / 16 % 2 == 1);

            if (isSeconds16On.CompareTo(IsSeconds16On) != 0)
            {
                IsSeconds16On = isSeconds16On;
                OnPropertyChanged(nameof(IsSeconds16On));
            }

            bool isSeconds8On = (seconds / 8 % 2 == 1);

            if (isSeconds8On.CompareTo(IsSeconds8On) != 0)
            {
                IsSeconds8On = isSeconds8On;
                OnPropertyChanged(nameof(IsSeconds8On));
            }

            bool isSeconds4On = (seconds / 4 % 2 == 1);

            if (isSeconds4On.CompareTo(IsSeconds4On) != 0)
            {
                IsSeconds4On = isSeconds4On;
                OnPropertyChanged(nameof(IsSeconds4On));
            }

            bool isSeconds2On = (seconds / 2 % 2 == 1);

            if (isSeconds2On.CompareTo(IsSeconds2On) != 0)
            {
                IsSeconds2On = isSeconds2On;
                OnPropertyChanged(nameof(IsSeconds2On));
            }

            bool isSeconds1On = (seconds / 1 % 2 == 1);

            if (isSeconds1On.CompareTo(IsSeconds1On) != 0)
            {
                IsSeconds1On = isSeconds1On;
                OnPropertyChanged(nameof(IsSeconds1On));
            }
        }

        protected void convertMinutesToBinary(int minutes)
        {
            bool isMinutes32On = (minutes / 32 % 2 == 1);

            if (isMinutes32On.CompareTo(IsMinutes32On) != 0)
            {
                IsMinutes32On = isMinutes32On;
                OnPropertyChanged(nameof(IsMinutes32On));
            }

            bool isMinutes16On = (minutes / 16 % 2 == 1);

            if (isMinutes16On.CompareTo(IsMinutes16On) != 0)
            {
                IsMinutes16On = isMinutes16On;
                OnPropertyChanged(nameof(IsMinutes16On));
            }

            bool isMinutes8On = (minutes / 8 % 2 == 1);

            if (isMinutes8On.CompareTo(IsMinutes8On) != 0)
            {
                IsMinutes8On = isMinutes8On;
                OnPropertyChanged(nameof(IsMinutes8On));
            }

            bool isMinutes4On = (minutes / 4 % 2 == 1);

            if (isMinutes4On.CompareTo(IsMinutes4On) != 0)
            {
                IsMinutes4On = isMinutes4On;
                OnPropertyChanged(nameof(IsMinutes4On));
            }

            bool isMinutes2On = (minutes / 2 % 2 == 1);

            if (isMinutes2On.CompareTo(IsMinutes2On) != 0)
            {
                IsMinutes2On = isMinutes2On;
                OnPropertyChanged(nameof(IsMinutes2On));
            }

            bool isMinutes1On = (minutes / 1 % 2 == 1);

            if (isMinutes1On.CompareTo(IsMinutes1On) != 0)
            {
                IsMinutes1On = isMinutes1On;
                OnPropertyChanged(nameof(IsMinutes1On));
            }
        }

        protected void convertHoursToBinary(int hours)
        {

            if (!_militaryTime)
            {
                hours %= 12;
            }

            bool isHours16On = (hours / 16 % 2 == 1);

            if (isHours16On.CompareTo(IsHours16On) != 0)
            {
                IsHours16On = isHours16On;
                OnPropertyChanged(nameof(IsHours16On));
            }

            bool isHours8On = (hours / 8 % 2 == 1);

            if (isHours8On.CompareTo(IsHours8On) != 0)
            {
                IsHours8On = isHours8On;
                OnPropertyChanged(nameof(IsHours8On));
            }

            bool isHours4On = (hours / 4 % 2 == 1);

            if (isHours4On.CompareTo(IsHours4On) != 0)
            {
                IsHours4On = isHours4On;
                OnPropertyChanged(nameof(IsHours4On));
            }

            bool isHours2On = (hours / 2 % 2 == 1);

            if (isHours2On.CompareTo(IsHours2On) != 0)
            {
                IsHours2On = isHours2On;
                OnPropertyChanged(nameof(IsHours2On));
            }

            bool isHours1On = (hours / 1 % 2 == 1);

            if (isHours1On.CompareTo(IsHours1On) != 0)
            {
                IsHours1On = isHours1On;
                OnPropertyChanged(nameof(IsHours1On));
            }
        }

    }

    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }

}
