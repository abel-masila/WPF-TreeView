using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace wpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region onLoaded
        /// <summary>
        /// When The application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //get all logical drives
            foreach(var drive in Directory.GetLogicalDrives())
            {
                //create a new item
                var newItem = new TreeViewItem();
                //set header 
                newItem.Header = drive;
                //add fullpath
                newItem.Tag = drive;
                //add dummy item
                newItem.Items.Add(null);
                //Listen out for items being expanded
                newItem.Expanded += Folder_Expanded;
                //add to main Treeview
                FolderView.Items.Add(newItem);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            //if item contains dummy data
            if (item.Items.Count != 1 || item.Items[0)!= null)
                return;
            //Clear dummy data
            item.Items.Clear();
            //get fullpath
            var fullpath = (string)item.Tag;
            //Create a blank list of directories
            var directories = new List<string>();
            //try get directories from folder
            //ignoring errors
            try
            {
                var dirs = Directory.GetDirectories(fullpath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }
            directories.ForEach(directoryPath =>
            {
                var subItem=new TreeViewItem()
                {
                    Header=Path.GetDirectoryName(directoryPath),
                    Tag=directoryPath
                };
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;
            });
        }
        #endregion
    }
}
