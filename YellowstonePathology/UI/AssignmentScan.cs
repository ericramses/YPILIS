using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    public class AssignmentScan : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ScanId;
        private int m_AssignedToId;
        private string m_AssignedTo;
        private int m_AssignedById;
        private string m_AssignedBy;
        private bool m_Assigned;
        private string m_Comment;

        public AssignmentScan()
        {
            this.m_Assigned = false;
        }                       

        public AssignmentScan(YellowstonePathology.Business.BarcodeScanning.Barcode barcode, Business.User.SystemUser assignedTo, Business.User.SystemUser assignedBy)
        {
            this.m_Assigned = false;

            this.m_ScanId = barcode.ID;
            this.m_AssignedTo = assignedTo.UserName;
            this.m_AssignedToId = assignedTo.UserId;
            this.m_AssignedBy = assignedBy.UserName;
            this.m_AssignedById = assignedBy.UserId;
        }

        public string ScanId
        {
            get { return this.m_ScanId; }
            set
            {
                if(this.m_ScanId != value)
                {
                    this.m_ScanId = value;
                    this.NotifyPropertyChanged("ScanId");
                }
            }
        }

        public int AssignedToId
        {
            get { return this.m_AssignedToId; }
            set
            {
                if(this.m_AssignedToId != value)
                {
                    this.m_AssignedToId = value;
                    this.NotifyPropertyChanged("AssignedToId");
                }
            }
        }

        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if(this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }

        public int AssignedById
        {
            get { return this.m_AssignedById; }
            set
            {
                if (this.m_AssignedById != value)
                {
                    this.m_AssignedById = value;
                    this.NotifyPropertyChanged("AssignedById");
                }
            }
        }

        public string AssignedBy
        {
            get { return this.m_AssignedBy; }
            set
            {
                if (this.m_AssignedBy != value)
                {
                    this.m_AssignedBy = value;
                    this.NotifyPropertyChanged("AssignedBy");
                }
            }
        }

        public bool Assigned
        {
            get { return this.m_Assigned; }
            set
            {
                if (this.m_Assigned != value)
                {
                    this.m_Assigned = value;
                    this.NotifyPropertyChanged("Assigned");
                }
            }
        }

        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
