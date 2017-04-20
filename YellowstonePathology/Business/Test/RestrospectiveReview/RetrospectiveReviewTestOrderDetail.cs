﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.RetrospectiveReview
{
    [PersistentClass("tblRetrospectiveReviewTestOrderDetail", "YPIDATA")]
    public class RetrospectiveReviewTestOrderDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_RetrospectiveReviewTestOrderDetailId;
        private string m_Result;
        private string m_Comment;
        private string m_Type;
        private string m_Level;
        private string m_Impact;
        private string m_SpecimenDescription;
        private int m_SpecimenNumber;

        public RetrospectiveReviewTestOrderDetail()
        {

        }

        public RetrospectiveReviewTestOrderDetail(string retrospectiveReviewTestOrderDetailId)
        {
            this.m_RetrospectiveReviewTestOrderDetailId = retrospectiveReviewTestOrderDetailId;            
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string RetrospectiveReviewTestOrderDetailId
        {
            get { return this.m_RetrospectiveReviewTestOrderDetailId; }
            set
            {
                if (this.m_RetrospectiveReviewTestOrderDetailId != value)
                {
                    this.m_RetrospectiveReviewTestOrderDetailId = value;
                    this.NotifyPropertyChanged("RetrospectiveReviewTestOrderDetailId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Type
        {
            get { return this.m_Type; }
            set
            {
                if (this.m_Type != value)
                {
                    this.m_Type = value;
                    this.NotifyPropertyChanged("Type");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Level
        {
            get { return this.m_Level; }
            set
            {
                if (this.m_Level != value)
                {
                    this.m_Level = value;
                    this.NotifyPropertyChanged("Level");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Impact
        {
            get { return this.m_Impact; }
            set
            {
                if (this.m_Impact != value)
                {
                    this.m_Impact = value;
                    this.NotifyPropertyChanged("Impact");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string SpecimenDescription
        {
            get { return this.m_SpecimenDescription; }
            set
            {
                if (this.m_SpecimenDescription != value)
                {
                    this.m_SpecimenDescription = value;
                    this.NotifyPropertyChanged("SpecimenDescription");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, null, "null", "int")]
        public int SpecimenNumber
        {
            get { return this.m_SpecimenNumber; }
            set
            {
                if (this.m_SpecimenNumber != value)
                {
                    this.m_SpecimenNumber = value;
                    this.NotifyPropertyChanged("SpecimenNumber");
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
