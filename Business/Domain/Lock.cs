using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public delegate void LockStatusChangedEventHandler(object sender, EventArgs e);

	public class Lock : DomainBase
	{
		public event LockStatusChangedEventHandler LockStatusChanged;

		YellowstonePathology.Business.Interface.ILockable m_Lockable;
		YellowstonePathology.Business.Domain.KeyLock m_KeyLock;

		Nullable<DateTime> m_LockDate;
		string m_LockedBy = string.Empty;        

		LockModeEnum m_LockingMode = LockModeEnum.NeverAttemptLock;
		string m_LockImage;

		string m_LockedImagePath = @"\Graphics\Locked.gif";
		string m_UnlockedImagePath = @"\Graphics\UnLocked.gif";

		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public Lock(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_SystemIdentity = systemIdentity;
			this.m_LockImage = this.m_LockedImagePath;
			this.m_KeyLock = new KeyLock();
		}
       
		public void ToggleLockingMode()
		{
			switch (this.m_LockingMode)
			{
				case LockModeEnum.AlwaysAttemptLock:
					this.m_LockingMode = LockModeEnum.NeverAttemptLock;
					this.ReleaseLock();
                    this.m_LockImage = this.m_LockedImagePath;
					break;
				case LockModeEnum.NeverAttemptLock:
					this.m_LockingMode = LockModeEnum.AlwaysAttemptLock;
                    this.GetLock();
                    this.m_LockImage = this.m_UnlockedImagePath;
					break;
			}

			this.NotifyPropertyChanged("LockAquired");
			this.NotifyPropertyChanged("LockImage");
			OnLockStatusChanged(this);            
		}

		public YellowstonePathology.Business.Interface.ILockable Lockable
		{
			get { return this.m_Lockable; }
		}

		public void SetLockable(YellowstonePathology.Business.Interface.ILockable lockable)
		{
			if (this.LockAquired)
			{
				this.ReleaseLock();
			}
			this.m_Lockable = lockable;

			this.Lockable.GetKeyLock(this.m_KeyLock);
			this.GetLock();
		}

		public void SetLockingMode(LockModeEnum lockingMode)
		{
            this.m_LockingMode = lockingMode;
            switch(this.m_LockingMode) 
            {
                case LockModeEnum.AlwaysAttemptLock:                    
                    this.m_LockImage = this.m_UnlockedImagePath;
                    break;
                case LockModeEnum.NeverAttemptLock:
                    this.m_LockImage = this.m_LockedImagePath;
                    break;
            }
            OnLockStatusChanged(this);                        
		}

        public LockModeEnum LockingMode
        {
            get { return this.m_LockingMode; }
        }

		public string LockImage
		{
			get { return this.m_LockImage; }
			set { this.m_LockImage = value; }
		}

		public Nullable<DateTime> LockDate
		{
			get { return this.m_LockDate; }
			set
			{
				if (value != this.m_LockDate)
				{
					this.m_LockDate = value;
					this.NotifyPropertyChanged("LockDate");
					this.NotifyPropertyChanged("LockMessage");
				}
			}
		}

		public string LockedBy
		{
			get { return this.m_LockedBy; }
			set
			{
				if (value != this.m_LockedBy)
				{
					this.m_LockedBy = value;
					this.NotifyPropertyChanged("LockedBy");
					this.NotifyPropertyChanged("LockMessage");
				}
			}
		}        

		public string LockMessage
		{
			get
			{
				string message = string.Empty;
				if (this.m_LockDate.HasValue == true)
				{
					message = "Lock aquired by " + this.LockedBy + " at " + this.m_LockDate.Value.ToShortTimeString();
				}
				else
				{
					message = "Read Only";
				}
				return message;
			}
		}

		public void ReleaseUserLocks()
		{
			YellowstonePathology.Business.Gateway.LockGateway.ReleaseUserLocks(this.m_SystemIdentity.User);
		}

		public void ReleaseLock()
		{
            if (this.LockAquired == true)
            {
                YellowstonePathology.Business.Gateway.LockGateway.ReleaseLock(this.m_KeyLock, this.m_SystemIdentity.User);
                this.LockDate = null;
                this.LockedBy = string.Empty;
                OnLockStatusChanged(this);
            }
		}

		public bool GetLock()
		{
            bool result = false;
			if (this.m_LockingMode == LockModeEnum.AlwaysAttemptLock)
			{
				if (this.m_KeyLock != null)
				{
                    if (this.m_SystemIdentity.IsKnown == true)
                    {
                        YellowstonePathology.Business.Gateway.LockGateway.GetLock(this.m_KeyLock, this.m_SystemIdentity.User, this);
                        OnLockStatusChanged(this);                        
                        result = true;
                    }                    
				}
			}
			else
			{
				LockedBy = string.Empty;
				LockDate = null;
			}
            return result;
		}

		public bool LockAquired
		{
			get { return this.LockedBy == this.m_SystemIdentity.User.UserName; }
		}

		private void OnLockStatusChanged(object sender)
		{
			if (this.LockStatusChanged != null)
			{
				EventArgs eventArgs = new EventArgs();
				LockStatusChanged(sender, eventArgs);
			}
		}
	}
}
