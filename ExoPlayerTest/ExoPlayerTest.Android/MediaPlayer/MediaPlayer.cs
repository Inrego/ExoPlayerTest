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
using ExoPlayerTest.Services;
using Xamarin.Forms;
using MediaPlayer = ExoPlayerTest.Droid.MediaPlayer.MediaPlayer;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(MediaPlayer))]
namespace ExoPlayerTest.Droid.MediaPlayer
{
    public class MediaPlayer : IMediaPlayer
    {
        protected virtual Java.Lang.Class ServiceType { get; } = Java.Lang.Class.FromType(typeof(MediaBrowserService));
        protected MediaBrowserConnectionCallback MediaBrowserConnectionCallback { get; set; }
        public async Task StartPlaying(string url)
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