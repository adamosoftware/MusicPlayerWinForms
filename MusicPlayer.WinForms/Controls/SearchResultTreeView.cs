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
                        TreeNode ndChild = FindOrCreateNode(ndParent, nodes[index]);
                        if (index == nodes.Length - 1)
                        {
                            ndChild.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                            ndParent.Expand();
                        }
                        ndParent = ndChild;
                    }
                }
            }

            treeView1.ExpandAll();
        }

        private TreeNode FindOrCreateNode(TreeNode ndParent, string text)
        {
            foreach (TreeNode node in ndParent.Nodes)
            {
                if (node.Text.Equals(text)) return node;
            }

            TreeNode result = new TreeNode(text);
            ndParent.Nodes.Add(result);

            return result;
        }
    }
}