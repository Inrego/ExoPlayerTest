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
using AndroidX.Media;

namespace ExoPlayerTest.Droid.MediaPlayer.FromScratch
{
    public class PlaybackService : MediaBrowserServiceCompat
    {
        private const string MY_MEDIA_ROOT_ID = "my_root_id";
        private const string MY_EMPTY_MEDIA_ROOT_ID = "empty_root_id";

        private MediaSessionCompat mediaSession;
        private PlaybackStateCompat.Builder stateBuilder;

        public override void OnCreate()
        {
            var context = Android.App.Application.Context.ApplicationContext;

            var mediaSession = new MediaSessionCompat(context, "ExoTest");

            mediaSession.SetFlags(MediaSessionCompat.FlagHandlesMediaButtons | MediaSessionCompat.FlagHandlesTransportControls);

            var stateBuilder = new PlaybackStateCompat.Builder()
                .SetActions(PlaybackStateCompat.ActionPlay | PlaybackStateCompat.ActionPlayPause);
            mediaSession.SetPlaybackState(stateBuilder.Build());

            mediaSession.SetCallback(new SessionCallback());
        }

        public override BrowserRoot OnGetRoot(string clientPackageName, int clientUid, Bundle rootHints)
        {
            return new BrowserRoot(MY_MEDIA_ROOT_ID, null);
        }

        public override void OnLoadChildren(string parentId, Result result)
        {
            
        }
    }
}