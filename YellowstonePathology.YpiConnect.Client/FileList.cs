using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.YpiConnect.Client
{
    public class FileList : ObservableCollection<FileListItem>
    {
        public FileList()
        {
                        
        }

        public void FromFolderName(string folderName, string filter)
        {
            this.Clear();
            string[] files = Directory.GetFiles(folderName);
            foreach (string file in files)
            {
                string[] slashSplit = file.Split('\\');
                string[] dotSplit = slashSplit[slashSplit.Length - 1].Split('.');

                if (dotSplit[dotSplit.Length - 1] == filter)
                {
                    FileListItem item = new FileListItem();
                    item.Fill(file);
                    this.Add(item);
                }
            }            
        }

        public void FromFolderName(string folderName)
        {            
            string[] files = Directory.GetFiles(folderName);            
            foreach (string file in files)
            {
                FileListItem item = new FileListItem();
                item.Fill(file);
                this.Add(item);
            }            
        }                       
    }    
}
