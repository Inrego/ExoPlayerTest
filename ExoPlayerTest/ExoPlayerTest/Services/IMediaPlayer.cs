using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExoPlayerTest.Services
{
    public interface IMediaPlayer
    {
        Task StartPlaying(string url);
    }
}
