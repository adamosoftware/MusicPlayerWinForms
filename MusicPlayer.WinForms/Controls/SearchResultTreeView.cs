using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static MusicPlayer.Models.Mp3Database;

namespace MusicPlayer.Controls
{
    public partial class SearchResultTreeView : UserControl
    {
        public SearchResultTreeView()
        {
            InitializeComponent();            
        }

        public event EventHandler SongSearchClicked;

        public void Fill(IEnumerable<Search> results)
        {
            treeView1.Nodes.Clear();

            foreach (var typeGrp in results.GroupBy(item => item.Type))
            {
                TreeNode ndRoot = new TreeNode(typeGrp.Key);
                treeView1.Nodes.Add(ndRoot);
                
                foreach (var item in typeGrp)
                {
                    TreeNode ndParent = ndRoot;

                    var nodes = item.Label.Split('|');
                    for (int index = 0; index < nodes.Length; index++)
                    {
                        // the last node in the path is considered the search node that loads songs when clicked
                        TreeNode ndChild = FindOrCreateNode(ndParent, nodes[index], (index == nodes.Length - 1));

                        var songNode = ndChild as SongSearchNode;
                        if (songNode != null) songNode.Search = item;

                        ndParent = ndChild;
                    }
                }
            }

            treeView1.ExpandAll();
        }

        private TreeNode FindOrCreateNode(TreeNode ndParent, string text, bool executeSearch)
        {
            foreach (TreeNode node in ndParent.Nodes)
            {
                if (node.Text.Equals(text)) return node;
            }

            TreeNode result = (executeSearch) ? new SongSearchNode(text) : new TreeNode(text);

            if (executeSearch)
            {
                result.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
            }

            ndParent.Nodes.Add(result);

            return result;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SongSearchNode node = e.Node as SongSearchNode;
            if (node != null) SongSearchClicked?.Invoke(node, new EventArgs());
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SongSearchNode node = e.Node as SongSearchNode;
            if (node != null) SongSearchClicked?.Invoke(node, new EventArgs());
        }
    }

    public class SongSearchNode : TreeNode
    {
        public SongSearchNode(string text) : base(text)
        {
        }

        public Search Search { get; set; }
    }
}