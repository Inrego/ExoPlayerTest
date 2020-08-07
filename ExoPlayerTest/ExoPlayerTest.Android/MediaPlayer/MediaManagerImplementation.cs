using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media.Session;
using Android.Views;
using Android.Widget;

namespace ExoPlayerTest.Droid.MediaPlayer
{
    public class MediaManagerImplementation
    {
        public MediaPlayerState State { get; set; }

        public MediaControllerCompat MediaController { get; set; }
    }
}