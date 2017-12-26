using AdamOneilSoftware;
using MusicPlayer.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class frmMain : Form
    {
        private Options _options;
        private Mp3Database _db;

        public frmMain()
        {
            InitializeComponent();
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
                }
                else
                {
                    tslProgress.Text = "Please select music folder...";
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
                    IProgress<string> progress = new Progress<string>(ShowProgress);
                    toolStripProgressBar1.Visible = true;
                    await _db.FillAsync(progress);
                    toolStripProgressBar1.Visible = false;

                    ShowDbMetrics();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
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
    }
}