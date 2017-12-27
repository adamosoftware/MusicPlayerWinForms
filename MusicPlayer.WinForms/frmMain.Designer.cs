namespace MusicPlayer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslMusicFolder = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tslProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnPlayPause = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSongGroups = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroupLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroupSongCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSongs = new System.Windows.Forms.DataGridView();
            this.colSongsTrackNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSongTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSongArtists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lookForNewMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslMusicFolder,
            this.toolStripProgressBar1,
            this.tslProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(570, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslMusicFolder
            // 
            this.tslMusicFolder.IsLink = true;
            this.tslMusicFolder.Name = "tslMusicFolder";
            this.tslMusicFolder.Size = new System.Drawing.Size(81, 17);
            this.tslMusicFolder.Text = "(music folder)";
            this.tslMusicFolder.Click += new System.EventHandler(this.tslMusicFolder_Click);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // tslProgress
            // 
            this.tslProgress.Name = "tslProgress";
            this.tslProgress.Size = new System.Drawing.Size(39, 17);
            this.tslProgress.Text = "Ready";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.lookForNewMusicToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 92);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.selectToolStripMenuItem.Text = "Select...";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSearch,
            this.btnPlayPause});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(570, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbSearch
            // 
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(125, 25);
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayPause.Image")));
            this.btnPlayPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(49, 22);
            this.btnPlayPause.Text = "Play";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSongGroups);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSongs);
            this.splitContainer1.Size = new System.Drawing.Size(570, 244);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 4;
            // 
            // dgvSongGroups
            // 
            this.dgvSongGroups.AllowUserToAddRows = false;
            this.dgvSongGroups.AllowUserToDeleteRows = false;
            this.dgvSongGroups.AllowUserToResizeRows = false;
            this.dgvSongGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSongGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSongGroups.ColumnHeadersVisible = false;
            this.dgvSongGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colGroupLabel,
            this.colGroupSongCount});
            this.dgvSongGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSongGroups.Location = new System.Drawing.Point(0, 0);
            this.dgvSongGroups.MultiSelect = false;
            this.dgvSongGroups.Name = "dgvSongGroups";
            this.dgvSongGroups.ReadOnly = true;
            this.dgvSongGroups.RowHeadersVisible = false;
            this.dgvSongGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSongGroups.Size = new System.Drawing.Size(570, 109);
            this.dgvSongGroups.TabIndex = 0;
            this.dgvSongGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSongGroups_CellClick);
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 5;
            // 
            // colGroupLabel
            // 
            this.colGroupLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGroupLabel.DataPropertyName = "Label";
            this.colGroupLabel.HeaderText = "Label";
            this.colGroupLabel.Name = "colGroupLabel";
            this.colGroupLabel.ReadOnly = true;
            // 
            // colGroupSongCount
            // 
            this.colGroupSongCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colGroupSongCount.DataPropertyName = "SongCount";
            this.colGroupSongCount.HeaderText = "Song Count";
            this.colGroupSongCount.Name = "colGroupSongCount";
            this.colGroupSongCount.ReadOnly = true;
            this.colGroupSongCount.Width = 5;
            // 
            // dgvSongs
            // 
            this.dgvSongs.AllowUserToAddRows = false;
            this.dgvSongs.AllowUserToDeleteRows = false;
            this.dgvSongs.AllowUserToResizeRows = false;
            this.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSongs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSongsTrackNum,
            this.colSongTitle,
            this.colSongArtists,
            this.colAlbum,
            this.colYear});
            this.dgvSongs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSongs.Location = new System.Drawing.Point(0, 0);
            this.dgvSongs.MultiSelect = false;
            this.dgvSongs.Name = "dgvSongs";
            this.dgvSongs.ReadOnly = true;
            this.dgvSongs.RowHeadersVisible = false;
            this.dgvSongs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSongs.Size = new System.Drawing.Size(570, 131);
            this.dgvSongs.TabIndex = 0;
            // 
            // colSongsTrackNum
            // 
            this.colSongsTrackNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSongsTrackNum.DataPropertyName = "TrackNumber";
            this.colSongsTrackNum.HeaderText = "#";
            this.colSongsTrackNum.Name = "colSongsTrackNum";
            this.colSongsTrackNum.ReadOnly = true;
            this.colSongsTrackNum.Width = 41;
            // 
            // colSongTitle
            // 
            this.colSongTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colSongTitle.DataPropertyName = "Title";
            this.colSongTitle.HeaderText = "Title";
            this.colSongTitle.Name = "colSongTitle";
            this.colSongTitle.ReadOnly = true;
            this.colSongTitle.Width = 56;
            // 
            // colSongArtists
            // 
            this.colSongArtists.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSongArtists.DataPropertyName = "Artist";
            this.colSongArtists.HeaderText = "Artist";
            this.colSongArtists.Name = "colSongArtists";
            this.colSongArtists.ReadOnly = true;
            // 
            // colAlbum
            // 
            this.colAlbum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAlbum.DataPropertyName = "Album";
            this.colAlbum.HeaderText = "Album";
            this.colAlbum.Name = "colAlbum";
            this.colAlbum.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colYear.DataPropertyName = "Year";
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            this.colYear.Width = 57;
            // 
            // lookForNewMusicToolStripMenuItem
            // 
            this.lookForNewMusicToolStripMenuItem.Name = "lookForNewMusicToolStripMenuItem";
            this.lookForNewMusicToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.lookForNewMusicToolStripMenuItem.Text = "Look For New Music";
            this.lookForNewMusicToolStripMenuItem.Click += new System.EventHandler(this.lookForNewMusicToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 291);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "Music Player";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslMusicFolder;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel tslProgress;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPlayPause;
        private System.Windows.Forms.ToolStripTextBox tbSearch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSongGroups;
        private System.Windows.Forms.DataGridView dgvSongs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGroupLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGroupSongCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSongsTrackNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSongTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSongArtists;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlbum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.ToolStripMenuItem lookForNewMusicToolStripMenuItem;
    }
}

