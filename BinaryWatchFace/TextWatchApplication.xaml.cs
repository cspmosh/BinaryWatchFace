﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BinaryWatchFace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextWatchApplication : Application
    {
        public TextWatchApplication()
        {
            InitializeComponent();
        }
    }

}