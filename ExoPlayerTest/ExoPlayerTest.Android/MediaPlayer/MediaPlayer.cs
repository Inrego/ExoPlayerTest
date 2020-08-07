using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Media.Browse;
using Android.OS;
using Android.Runtime;
using Android.Service.Media;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Util;
using ExoPlayerTest.Droid.MediaPlayer.FromScratch;
using ExoPlayerTest.Services;
using Xamarin.Forms;
using MediaPlayer = ExoPlayerTest.Droid.MediaPlayer.MediaPlayer;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(MediaPlayer))]
namespace ExoPlayerTest.Droid.MediaPlayer
{
    public class MediaPlayer : IMediaPlayer
    {
        public MediaPlayer()
        {
            MediaBrowserConnectionCallback = new MediaBrowserConnectionCallback
            {
                OnConnectedImpl = () =>
                {
                    Console.WriteLine("OnConnected");
                },
                OnConnectionFailedImpl = () =>
                {
                    Console.WriteLine("ConnectionFailed");
                },
                OnConnectionSuspendedImpl = () =>
                {
                    Console.WriteLine("ConnectionSuspended");
                }
            };
        }
        protected virtual Java.Lang.Class ServiceType { get; } = Java.Lang.Class.FromType(typeof(PlaybackService));
        protected MediaBrowserConnectionCallback MediaBrowserConnectionCallback { get; set; }
        public async Task StartPlaying(string url)
        {
            await Test();
        }
        // This doesn't crash, but it doesn't player either.
        private async Task Test()
        {
            var context = Android.App.Application.Context.ApplicationContext;
            var mediaBrowser = new MediaBrowserCompat(context, new ComponentName(context, ServiceType),
                MediaBrowserConnectionCallback, null);
            MediaBrowserConnectionCallback.OnConnectedImpl = () =>
            {
                var mediaController = new MediaControllerCompat(context, mediaBrowser.SessionToken);
                MediaControllerCompat.SetMediaController(MainActivity.CurrentActivity, mediaController);
                mediaController.GetTransportControls().PlayFromUri(Uri.Parse("https://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3"), Bundle.Empty);
            };
            mediaBrowser.Connect();
        }

        private async Task Advanced()
        {
            var manager = new MediaBrowserManager();
            await manager.Init();
            var controller = MediaBrowserManager.MediaManager.MediaController;
            controller.GetTransportControls().PlayFromUri(Uri.Parse("https://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3"), Bundle.Empty);
        }

        private async Task Simple()
        {
            var context = Android.App.Application.Context.ApplicationContext;

            var player = new SimpleExoPlayer.Builder(context).Build();
            var datasourceFactory = new DefaultDataSourceFactory(context, Util.GetUserAgent(context, "test"));
            var mediaSource = new ProgressiveMediaSource.Factory(datasourceFactory).CreateMediaSource(Uri.Parse("https://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3"));
            player.Prepare(mediaSource);
            player.PlayWhenReady = true;
        }
    }
}