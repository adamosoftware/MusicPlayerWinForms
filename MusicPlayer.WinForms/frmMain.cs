using AdamOneilSoftware;
using MusicPlayer.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class frmMain : Form
    {
        private Options _options;
        private Mp3Database _db;
        private Mp3Player _player;
        private Mp3File _song;

        public frmMain()
        {
            InitializeComponent();
            dgvSongs.AutoGenerateColumns = false;
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
                if (tbSearch.Text.Length >= 2)
                {
                    toolStripProgressBar1.Visible = true;
                    using (var cn = _db.GetConnection())
                    {
                        var songs = await _db.FindSongsAsync(cn, tbSearch.Text);
                        BindingList<Mp3File> songList = new BindingList<Mp3File>(songs.ToList());
                        dgvSongs.DataSource = songList;
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
                var songs = dgvSongs.DataSource as BindingList<Mp3File>;
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

        private void filterArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FilterCriteria criteria = new FilterCriteria() { Artist = _song.Artist };
                string json = JsonConvert.SerializeObject(criteria);
                tbSearch.Text = json;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void dgvSongs_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {                
                _song = dgvSongs.Rows[e.RowIndex].DataBoundItem as Mp3File;
            }
        }

        private void filterAlbumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FilterCriteria criteria = new FilterCriteria() { Artist = _song.Artist, Album = _song.Album };
                string json = JsonConvert.SerializeObject(criteria);
                tbSearch.Text = json;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}