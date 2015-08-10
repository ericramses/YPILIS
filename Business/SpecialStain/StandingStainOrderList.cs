using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.SpecialStain
{
	public class StandingStainOrderList : ObservableCollection<StandingStainOrderListItem>
	{
		public StandingStainOrderList()
		{ 

        }

		public void Fill()
		{
			this.Clear();
			LoadData();
		}

		private void LoadData()
		{
			this.Add(new StandingStainOrderListItem(2401, 107, "Helicobacter pylori", "STOMACH", 0));
			this.Add(new StandingStainOrderListItem(2401, 107, "Helicobacter pylori", "GASTRIC", 0));
            this.Add(new StandingStainOrderListItem(2401, 107, "Helicobacter pylori", "ANTRUM", 0));

            this.Add(new StandingStainOrderListItem(2879, 107, "Helicobacter pylori", "STOMACH", 0));
            this.Add(new StandingStainOrderListItem(2879, 107, "Helicobacter pylori", "GASTRIC", 0));
            this.Add(new StandingStainOrderListItem(2879, 107, "Helicobacter pylori", "ANTRUM", 0));

			this.Add(new StandingStainOrderListItem(604, 115, "Iron", "BONE MARROW", 0));

			this.Add(new StandingStainOrderListItem(274, 107, "Helicobacter pylori", "ANTRAL", 0));
			this.Add(new StandingStainOrderListItem(274, 107, "Helicobacter pylori", "LESSER CURVATURE", 0));
		}
	}

	public class StandingStainOrderListItem : ListItem
	{
		private int m_PhysicianId;
		private int m_TestId;
		private string m_TestName;
		private string m_Keyword;
		private string m_SpecimenId;

		public StandingStainOrderListItem(int physicianId, int testId, string testName, string keyword, int specimenId)
		{
			this.m_PhysicianId = physicianId;
			this.m_TestId = testId;
			this.m_TestName = testName;
			this.m_Keyword = keyword;
		}

		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
		}

		public int TestId
		{
			get { return this.m_TestId; }
		}

		public string TestName
		{
			get { return this.m_TestName; }
		}

		public string Keyword
		{
			get { return this.m_Keyword; }
		}

		public string SpecimenId
		{
			get { return this.m_SpecimenId; }
			set { this.m_SpecimenId = value; }
		}
	}
}
