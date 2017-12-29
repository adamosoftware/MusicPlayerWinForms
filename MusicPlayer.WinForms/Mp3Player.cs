using MusicPlayer.Models;
using NAudio.Wave;
using System;

namespace MusicPlayer
{
    public class Mp3Player : IDisposable
    {
        // thanks to https://stackoverflow.com/a/15025797/2023653

        private IWavePlayer _player;
        private AudioFileReader _reader;
        private int _index = 0;
        private bool _playNextOnStop = false;
        private Mp3File _current;

        private readonly Mp3File[] _songs;

        public event EventHandler SongPlaying;

        public Mp3Player(Mp3File[] songs)
        {
            _songs = songs;            
            _player = new WaveOut();
            _player.PlaybackStopped += PlaybackStopped;
            _playNextOnStop = true;            
        }

        public Mp3File Current { get { return _current; } }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (_playNextOnStop)
            {
                _index++;
                if (_index >= _songs.Length) _index = 0;
                Play(_index);
            }
        }

        public void Play(int index = 0)
        {
            _current = _songs[index];           

            _reader = new AudioFileReader(_current.Path);
            _player.Init(_reader);
            _player.Play();
            SongPlaying?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            _playNextOnStop = false;
            _player?.Stop();
        }

        public void Dispose()
        {
            _player?.Stop();
            _player?.Dispose();
            _reader?.Dispose();
        }
    }
}