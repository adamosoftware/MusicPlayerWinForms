﻿using AdamOneilSoftware;
using MusicPlayer.Controls;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static MusicPlayer.Models.Mp3Database;

namespace MusicPlayer
{
    public partial class frmMain : Form
    {
        private Options _options;
        private Mp3Database _db;
        private Mp3Player _player;

        public frmMain()
        {
            InitializeComponent();            
            dgvSongs.AutoGenerateColumns = false;

            artistResults.SongSearchClicked += LoadSongsAsync;
            albumResults.SongSearchClicked += LoadSongsAsync;
            songResults.SongSearchClicked += LoadSongsAsync;
        }

        private async void LoadSongsAsync(object sender, EventArgs e)
        {
            try
            {
                SongSearchNode node = sender as SongSearchNode;
                if (node != null)
                {
                    toolStripProgressBar1.Visible = true;
                    using (var cn = _db.GetConnection())
                    {
                        var songs = await node.Search.GetSongsAsync(cn);
                        BindingList<Mp3File> songList = new BindingList<Mp3File>(songs.ToList());
                        dgvSongs.DataSource = songs;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                toolStripProgressBar1.Visible = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _options = UserOptionsBase.Load<Options>("Options.xml", this);
                _options.RestoreFormPosition(_options.FormPosition, this);
                _options.TrackFormPosition(this, (fp) => _options.FormPosition = fp);

                if (Directory.Exists(_options.MusicFolder))
                {
                    _db = new Mp3Database(_options.MusicFolder);
                    tslMusicFolder.Text = _options.MusicFolder;
                    ShowDbMetrics();
                    tbSearch.Enabled = true;
                }
                else
                {
                    tslProgress.Text = "Please select music folder...";
                    tbSearch.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void tslMusicFolder_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(statusStrip1, new Point(0, statusStrip1.Size.Height));
        }

        private async void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _options.MusicFolder = dlg.SelectedPath;
                    tslMusicFolder.Text = dlg.SelectedPath;
                    _db = new Mp3Database(_options.MusicFolder);
                    await AddNewMusic();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async System.Threading.Tasks.Task AddNewMusic()
        {
            IProgress<string> progress = new Progress<string>(ShowProgress);
            toolStripProgressBar1.Visible = true;
            await _db.FillAsync(progress);
            toolStripProgressBar1.Visible = false;

            ShowDbMetrics();
            tbSearch.Enabled = true;
        }

        private void ShowDbMetrics()
        {
            tslProgress.Text = $"{_db.QuerySingle<int>("SELECT COUNT(1) FROM [Mp3File]"):n0} songs";
        }

        private void ShowProgress(string obj)
        {
            tslProgress.Text = obj;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Shell.ViewFolder(_options.MusicFolder);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
                tbSearch.Focus();
            }
        }

        private async void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool hideSearch = true;
                if (tbSearch.Text.Length >= 2)
                {
                    hideSearch = false;
                    toolStripProgressBar1.Visible = true;

                    using (var cn = _db.GetConnection())
                    {
                        var artists = await _db.FindArtistsAsync(cn, tbSearch.Text);
                        artistResults.Fill(artists);

                        var albums = await _db.FindAlbumsAsync(cn, tbSearch.Text);
                        albumResults.Fill(albums);

                        var songs = await _db.FindSongsInAlbumAsync(cn, tbSearch.Text);
                        songResults.Fill(songs);
                    }                        
                }

                splitContainer1.Panel1Collapsed = hideSearch;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                toolStripProgressBar1.Visible = false;
            }
        }

        private async void lookForNewMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await AddNewMusic();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void dgvSongs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var songs = dgvSongs.DataSource as List<Mp3File>;
                var songArray = songs.ToArray();

                _player?.Stop();
                _player = new Mp3Player(_db.Path, songArray);
                _player.SongPlaying += ShowCurrentSong;
                _player.Play(e.RowIndex);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ShowCurrentSong(object sender, EventArgs e)
        {
            Text = $"{_player.Current.Title} | {_player.Current.Artist} | {_player.Current.Album}";
        }
    }
}