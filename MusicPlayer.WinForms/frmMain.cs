using AdamOneilSoftware;
using MusicPlayer.Models;
using System;
using System.ComponentModel;
using System.Data;
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

        public frmMain()
        {
            InitializeComponent();
            dgvSongGroups.AutoGenerateColumns = false;
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
                bool hideSearch = true;
                if (tbSearch.Text.Length >= 2)
                {
                    hideSearch = false;
                    toolStripProgressBar1.Visible = true;                    
                    var songGroups = await _db.FindSongGroupsAsync(tbSearch.Text);

                    BindingList<SongGroup> data = new BindingList<SongGroup>(songGroups.ToList());
                    BindingSource bs = new BindingSource();
                    bs.DataSource = data;
                    dgvSongGroups.DataSource = bs;
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

        private async void dgvSongGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SongGroup sg = (dgvSongGroups.DataSource as BindingSource).Current as SongGroup;
                if (sg != null)
                {
                    toolStripProgressBar1.Visible = true;
                    using (var cn = _db.GetConnection())
                    {
                        var songs = await sg.GetSongsAsync(cn);
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
    }
}