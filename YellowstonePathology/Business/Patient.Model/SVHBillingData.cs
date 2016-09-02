using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Patient.Model
{
	[PersistentClass("tblSVHBillingData", "YPIDATA")]
	public class SVHBillingData
	{
		private const int FieldCount = 85;

		private string m_Line;

		private string m_SVHBillingDataId;
		private DateTime m_DateProcessed;
		private DateTime m_FileDate;

		private string m_ObjectId;
		private string m_AccountId;
		private string m_MRN;
		private string m_AdmitDate;
		private string m_DischDate;
		private string m_AcctBaseClass;
		private string m_PatName;
		private string m_PatDOB;
		private string m_PatSex;
		private string m_PatAdd1;
		private string m_PatAdd2;
		private string m_PatCity;
		private string m_PatState;
		private string m_PatZIP;
		private string m_PatHomePhone;
		private string m_PatSSN;
		private string m_Guarantor;
		private string m_GuarAdd1;
		private string m_GuarAdd2;
		private string m_GuarCity;
		private string m_GuarState;
		private string m_GuarZIP;
		private string m_GuarHmPhone;
		private string m_GuarSSN;
		private string m_GuarDOB;
		private string m_GuarEmp;
		private string m_GuarEmpAdd1;
		private string m_GuarEmpAdd2;
		private string m_GuarEmpCity;
		private string m_GuarEmpState;
		private string m_GuarEmpZip;
		private string m_GuarEmpPhone;
		private string m_AttendingProv;
		private string m_NPI;
		private string m_DX1;
		private string m_DX2;
		private string m_DX3;
		private string m_DX4;
		private string m_DX5;
		private string m_DX6;
		private string m_DX7;
		private string m_DX8;
		private string m_DX9;
		private string m_DX10;
		private string m_InsPlan1;
		private string m_BenPlan1;
		private string m_BenPlan1Add1;
		private string m_BenPlan1Add2;
		private string m_BenPlan1City;
		private string m_BenPlan1State;
		private string m_BenPlan1Zip;
		private string m_BenPlan1Phone;
		private string m_Subs1Name;
		private string m_Subs1DOB;
		private string m_Subs1Sex;
		private string m_PatRelToSubs1;
		private string m_InsPlan2;
		private string m_BenPlan2;
		private string m_BenPlan2Add1;
		private string m_BenPlan2Add2;
		private string m_BenPlan2City;
		private string m_BenPlan2State;
		private string m_BenPlan2Zip;
		private string m_BenPlan2Phone;
		private string m_Subs2Name;
		private string m_Subs2DOB;
		private string m_Subs2Sex;
		private string m_PatRelToSubs2;
		private string m_InsPlan3;
		private string m_BenPlan3;
		private string m_BenPlan3Add1;
		private string m_BenPlan3Add2;
		private string m_BenPlan3City;
		private string m_BenPlan3State;
		private string m_BenPlan3Zip;
		private string m_BenPlan3Phone;
		private string m_Subs3Name;
		private string m_Subs3DOB;
		private string m_Subs3Sex;
		private string m_PatRelToSubs3;
		private string m_InsPolicyID1;
		private string m_InsPolicyID2;
		private string m_InsPolicyID3;
		private string m_InsGroupID1;
		private string m_InsGroupID2;
		private string m_InsGroupID3;

		public SVHBillingData()
		{

		}

		public SVHBillingData(string objectId, string line, DateTime dateProcessed, DateTime fileDate)
		{
			this.m_ObjectId = objectId;
			this.m_Line = line;
			string[] fields = line.Split('|');

			this.m_SVHBillingDataId = Guid.NewGuid().ToString();
			this.m_DateProcessed = dateProcessed;
			this.m_FileDate = fileDate;

			System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();

			for (int i = 0; i < fields.Length; i++)
			{
				foreach (System.Reflection.PropertyInfo property in properties)
				{
					object[] attributes = property.GetCustomAttributes(typeof(ImportIndex), true);
					if (attributes.Length == 1)
					{
						ImportIndex importIndex = (ImportIndex)attributes[0];
						if (importIndex.Index - 1 == i)
						{
							property.SetValue(this, fields[i], null);
							break;
						}
					}
				}
			}
		}

		[PersistentDocumentIdProperty(50)]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
				}
			}
		}

		[PersistentPrimaryKeyProperty(false, 500)]
		public string SVHBillingDataId
		{
			get { return this.m_SVHBillingDataId; }
			set { this.m_SVHBillingDataId = value; }
		}

		[PersistentProperty()]
		public DateTime DateProcessed
		{
			get { return this.m_DateProcessed; }
			set { this.m_DateProcessed = value; }
		}

		[PersistentProperty()]
		public DateTime FileDate
		{
			get { return this.m_FileDate; }
			set { this.m_FileDate = value; }
		}

		[ImportIndex(1)]
		[PersistentStringProperty(500)]
		public string AccountId
		{
			get { return this.m_AccountId; }
			set { this.m_AccountId = value; }
		}

		[ImportIndex(2)]
		[PersistentStringProperty(500)]
		public string MRN
		{
			get { return this.m_MRN; }
			set { this.m_MRN = value; }
		}

		[ImportIndex(3)]
		[PersistentStringProperty(500)]
		public string AdmitDate
		{
			get { return this.m_AdmitDate; }
			set { this.m_AdmitDate = value; }
		}

		[ImportIndex(4)]
		[PersistentStringProperty(500)]
		public string DischDate
		{
			get { return this.m_DischDate; }
			set { this.m_DischDate = value; }
		}

		[ImportIndex(5)]
		[PersistentStringProperty(500)]
		public string AcctBaseClass
		{
			get { return this.m_AcctBaseClass; }
			set { this.m_AcctBaseClass = value; }
		}

		[ImportIndex(6)]
		[PersistentStringProperty(500)]
		public string PatName
		{
			get { return this.m_PatName; }
			set { this.m_PatName = value; }
		}

		[ImportIndex(7)]
		[PersistentStringProperty(500)]
		public string PatDOB
		{
			get { return this.m_PatDOB; }
			set { this.m_PatDOB = value; }
		}

		[ImportIndex(8)]
		[PersistentStringProperty(500)]
		public string PatSex
		{
			get { return this.m_PatSex; }
			set { this.m_PatSex = value; }
		}

		[ImportIndex(9)]
		[PersistentStringProperty(500)]
		public string PatAdd1
		{
			get { return this.m_PatAdd1; }
			set { this.m_PatAdd1 = value; }
		}

		[ImportIndex(10)]
		[PersistentStringProperty(500)]
		public string PatAdd2
		{
			get { return this.m_PatAdd2; }
			set { this.m_PatAdd2 = value; }
		}

		[ImportIndex(11)]
		[PersistentStringProperty(500)]
		public string PatCity
		{
			get { return this.m_PatCity; }
			set { this.m_PatCity = value; }
		}

		[ImportIndex(12)]
		[PersistentStringProperty(500)]
		public string PatState
		{
			get { return this.m_PatState; }
			set { this.m_PatState = value; }
		}

		[ImportIndex(13)]
		[PersistentStringProperty(500)]
		public string PatZIP
		{
			get { return this.m_PatZIP; }
			set { this.m_PatZIP = value; }
		}

		[ImportIndex(14)]
		[PersistentStringProperty(500)]
		public string PatHomePhone
		{
			get { return this.m_PatHomePhone; }
			set { this.m_PatHomePhone = value; }
		}

		[ImportIndex(15)]
		[PersistentStringProperty(500)]
		public string PatSSN
		{
			get { return this.m_PatSSN; }
			set { this.m_PatSSN = value; }
		}

		[ImportIndex(16)]
		[PersistentStringProperty(500)]
		public string Guarantor
		{
			get { return this.m_Guarantor; }
			set { this.m_Guarantor = value; }
		}

		[ImportIndex(17)]
		[PersistentStringProperty(500)]
		public string GuarAdd1
		{
			get { return this.m_GuarAdd1; }
			set { this.m_GuarAdd1 = value; }
		}

		[ImportIndex(18)]
		[PersistentStringProperty(500)]
		public string GuarAdd2
		{
			get { return this.m_GuarAdd2; }
			set { this.m_GuarAdd2 = value; }
		}

		[ImportIndex(19)]
		[PersistentStringProperty(500)]
		public string GuarCity
		{
			get { return this.m_GuarCity; }
			set { this.m_GuarCity = value; }
		}

		[ImportIndex(20)]
		[PersistentStringProperty(500)]
		public string GuarState
		{
			get { return this.m_GuarState; }
			set { this.m_GuarState = value; }
		}

		[ImportIndex(21)]
		[PersistentStringProperty(500)]
		public string GuarZIP
		{
			get { return this.m_GuarZIP; }
			set { this.m_GuarZIP = value; }
		}

		[ImportIndex(22)]
		[PersistentStringProperty(500)]
		public string GuarHmPhone
		{
			get { return this.m_GuarHmPhone; }
			set { this.m_GuarHmPhone = value; }
		}

		[ImportIndex(23)]
		[PersistentStringProperty(500)]
		public string GuarSSN
		{
			get { return this.m_GuarSSN; }
			set { this.m_GuarSSN = value; }
		}

		[ImportIndex(24)]
		[PersistentStringProperty(500)]
		public string GuarDOB
		{
			get { return this.m_GuarDOB; }
			set { this.m_GuarDOB = value; }
		}

		[ImportIndex(25)]
		[PersistentStringProperty(500)]
		public string GuarEmp
		{
			get { return this.m_GuarEmp; }
			set { this.m_GuarEmp = value; }
		}

		[ImportIndex(26)]
		[PersistentStringProperty(500)]
		public string GuarEmpAdd1
		{
			get { return this.m_GuarEmpAdd1; }
			set { this.m_GuarEmpAdd1 = value; }
		}

		[ImportIndex(27)]
		[PersistentStringProperty(500)]
		public string GuarEmpAdd2
		{
			get { return this.m_GuarEmpAdd2; }
			set { this.m_GuarEmpAdd2 = value; }
		}

		[ImportIndex(28)]
		[PersistentStringProperty(500)]
		public string GuarEmpCity
		{
			get { return this.m_GuarEmpCity; }
			set { this.m_GuarEmpCity = value; }
		}

		[ImportIndex(29)]
		[PersistentStringProperty(500)]
		public string GuarEmpState
		{
			get { return this.m_GuarEmpState; }
			set { this.m_GuarEmpState = value; }
		}

		[ImportIndex(30)]
		[PersistentStringProperty(500)]
		public string GuarEmpZip
		{
			get { return this.m_GuarEmpZip; }
			set { this.m_GuarEmpZip = value; }
		}

		[ImportIndex(31)]
		[PersistentStringProperty(500)]
		public string GuarEmpPhone
		{
			get { return this.m_GuarEmpPhone; }
			set { this.m_GuarEmpPhone = value; }
		}

		[ImportIndex(32)]
		[PersistentStringProperty(500)]
		public string AttendingProv
		{
			get { return this.m_AttendingProv; }
			set { this.m_AttendingProv = value; }
		}

		[ImportIndex(33)]
		[PersistentStringProperty(500)]
		public string NPI
		{
			get { return this.m_NPI; }
			set { this.m_NPI = value; }
		}

		[ImportIndex(34)]
		[PersistentStringProperty(500)]
		public string DX1
		{
			get { return this.m_DX1; }
			set { this.m_DX1 = value; }
		}

		[ImportIndex(35)]
		[PersistentStringProperty(500)]
		public string DX2
		{
			get { return this.m_DX2; }
			set { this.m_DX2 = value; }
		}

		[ImportIndex(36)]
		[PersistentStringProperty(500)]
		public string DX3
		{
			get { return this.m_DX3; }
			set { this.m_DX3 = value; }
		}

		[ImportIndex(37)]
		[PersistentStringProperty(500)]
		public string DX4
		{
			get { return this.m_DX4; }
			set { this.m_DX4 = value; }
		}

		[ImportIndex(38)]
		[PersistentStringProperty(500)]
		public string DX5
		{
			get { return this.m_DX5; }
			set { this.m_DX5 = value; }
		}

		[ImportIndex(39)]
		[PersistentStringProperty(500)]
		public string DX6
		{
			get { return this.m_DX6; }
			set { this.m_DX6 = value; }
		}

		[ImportIndex(40)]
		[PersistentStringProperty(500)]
		public string DX7
		{
			get { return this.m_DX7; }
			set { this.m_DX7 = value; }
		}

		[ImportIndex(41)]
		[PersistentStringProperty(500)]
		public string DX8
		{
			get { return this.m_DX8; }
			set { this.m_DX8 = value; }
		}

		[ImportIndex(42)]
		[PersistentStringProperty(500)]
		public string DX9
		{
			get { return this.m_DX9; }
			set { this.m_DX9 = value; }
		}

		[ImportIndex(43)]
		[PersistentStringProperty(500)]
		public string DX10
		{
			get { return this.m_DX10; }
			set { this.m_DX10 = value; }
		}

		[ImportIndex(44)]
		[PersistentStringProperty(500)]
		public string InsPlan1
		{
			get { return this.m_InsPlan1; }
			set { this.m_InsPlan1 = value; }
		}

		[ImportIndex(45)]
		[PersistentStringProperty(500)]
		public string BenPlan1
		{
			get { return this.m_BenPlan1; }
			set { this.m_BenPlan1 = value; }
		}

		[ImportIndex(46)]
		[PersistentStringProperty(500)]
		public string BenPlan1Add1
		{
			get { return this.m_BenPlan1Add1; }
			set { this.m_BenPlan1Add1 = value; }
		}

		[ImportIndex(47)]
		[PersistentStringProperty(500)]
		public string BenPlan1Add2
		{
			get { return this.m_BenPlan1Add2; }
			set { this.m_BenPlan1Add2 = value; }
		}

		[ImportIndex(48)]
		[PersistentStringProperty(500)]
		public string BenPlan1City
		{
			get { return this.m_BenPlan1City; }
			set { this.m_BenPlan1City = value; }
		}

		[ImportIndex(49)]
		[PersistentStringProperty(500)]
		public string BenPlan1State
		{
			get { return this.m_BenPlan1State; }
			set { this.m_BenPlan1State = value; }
		}

		[ImportIndex(50)]
		[PersistentStringProperty(500)]
		public string BenPlan1Zip
		{
			get { return this.m_BenPlan1Zip; }
			set { this.m_BenPlan1Zip = value; }
		}

		[ImportIndex(51)]
		[PersistentStringProperty(500)]
		public string BenPlan1Phone1
		{
			get { return this.m_BenPlan1Phone; }
			set { this.m_BenPlan1Phone = value; }
		}

		[ImportIndex(52)]
		[PersistentStringProperty(500)]
		public string Subs1Name1
		{
			get { return this.m_Subs1Name; }
			set { this.m_Subs1Name = value; }
		}

		[ImportIndex(53)]
		[PersistentStringProperty(500)]
		public string Subs1DOB
		{
			get { return this.m_Subs1DOB; }
			set { this.m_Subs1DOB = value; }
		}

		[ImportIndex(54)]
		[PersistentStringProperty(500)]
		public string Subs1Sex
		{
			get { return this.m_Subs1Sex; }
			set { this.m_Subs1Sex = value; }
		}

		[ImportIndex(55)]
		[PersistentStringProperty(500)]
		public string PatRelToSubs1
		{
			get { return this.m_PatRelToSubs1; }
			set { this.m_PatRelToSubs1 = value; }
		}

		[ImportIndex(56)]
		[PersistentStringProperty(500)]
		public string InsPlan2
		{
			get { return this.m_InsPlan2; }
			set { this.m_InsPlan2 = value; }
		}

		[ImportIndex(57)]
		[PersistentStringProperty(500)]
		public string BenPlan2
		{
			get { return this.m_BenPlan2; }
			set { this.m_BenPlan2 = value; }
		}

		[ImportIndex(58)]
		[PersistentStringProperty(500)]
		public string BenPlan2Add1
		{
			get { return this.m_BenPlan2Add1; }
			set { this.m_BenPlan2Add1 = value; }
		}

		[ImportIndex(59)]
		[PersistentStringProperty(500)]
		public string BenPlan2Add2
		{
			get { return this.m_BenPlan2Add2; }
			set { this.m_BenPlan2Add2 = value; }
		}

		[ImportIndex(60)]
		[PersistentStringProperty(500)]
		public string BenPlan2City
		{
			get { return this.m_BenPlan2City; }
			set { this.m_BenPlan2City = value; }
		}

		[ImportIndex(61)]
		[PersistentStringProperty(500)]
		public string BenPlan2State
		{
			get { return this.m_BenPlan2State; }
			set { this.m_BenPlan2State = value; }
		}

		[ImportIndex(62)]
		[PersistentStringProperty(500)]
		public string BenPlan2Zip
		{
			get { return this.m_BenPlan2Zip; }
			set { this.m_BenPlan2Zip = value; }
		}

		[ImportIndex(63)]
		[PersistentStringProperty(500)]
		public string BenPlan2Phone
		{
			get { return this.m_BenPlan2Phone; }
			set { this.m_BenPlan2Phone = value; }
		}

		[ImportIndex(64)]
		[PersistentStringProperty(500)]
		public string Subs2Name
		{
			get { return this.m_Subs2Name; }
			set { this.m_Subs2Name = value; }
		}

		[ImportIndex(65)]
		[PersistentStringProperty(500)]
		public string Subs2DOB
		{
			get { return this.m_Subs2DOB; }
			set { this.m_Subs2DOB = value; }
		}

		[ImportIndex(66)]
		[PersistentStringProperty(500)]
		public string Subs2Sex
		{
			get { return this.m_Subs2Sex; }
			set { this.m_Subs2Sex = value; }
		}

		[ImportIndex(67)]
		[PersistentStringProperty(500)]
		public string PatRelToSubs2
		{
			get { return this.m_PatRelToSubs2; }
			set { this.m_PatRelToSubs2 = value; }
		}

		[ImportIndex(68)]
		[PersistentStringProperty(500)]
		public string InsPlan3
		{
			get { return this.m_InsPlan3; }
			set { this.m_InsPlan3 = value; }
		}

		[ImportIndex(69)]
		[PersistentStringProperty(500)]
		public string BenPlan3
		{
			get { return this.m_BenPlan3; }
			set { this.m_BenPlan3 = value; }
		}

		[ImportIndex(70)]
		[PersistentStringProperty(500)]
		public string BenPlan3Add1
		{
			get { return this.m_BenPlan3Add1; }
			set { this.m_BenPlan3Add1 = value; }
		}

		[ImportIndex(71)]
		[PersistentStringProperty(500)]
		public string BenPlan3Add2
		{
			get { return this.m_BenPlan3Add2; }
			set { this.m_BenPlan3Add2 = value; }
		}

		[ImportIndex(72)]
		[PersistentStringProperty(500)]
		public string BenPlan3City
		{
			get { return this.m_BenPlan3City; }
			set { this.m_BenPlan3City = value; }
		}

		[ImportIndex(73)]
		[PersistentStringProperty(500)]
		public string BenPlan3State
		{
			get { return this.m_BenPlan3State; }
			set { this.m_BenPlan3State = value; }
		}

		[ImportIndex(74)]
		[PersistentStringProperty(500)]
		public string BenPlan3Zip
		{
			get { return this.m_BenPlan3Zip; }
			set { this.m_BenPlan3Zip = value; }
		}

		[ImportIndex(75)]
		[PersistentStringProperty(500)]
		public string BenPlan3Phone
		{
			get { return this.m_BenPlan3Phone; }
			set { this.m_BenPlan3Phone = value; }
		}

		[ImportIndex(76)]
		[PersistentStringProperty(500)]
		public string Subs3Name
		{
			get { return this.m_Subs3Name; }
			set { this.m_Subs3Name = value; }
		}

		[ImportIndex(77)]
		[PersistentStringProperty(500)]
		public string Subs3DOB
		{
			get { return this.m_Subs3DOB; }
			set { this.m_Subs3DOB = value; }
		}

		[ImportIndex(78)]
		[PersistentStringProperty(500)]
		public string Subs3Sex
		{
			get { return this.m_Subs3Sex; }
			set { this.m_Subs3Sex = value; }
		}

		[ImportIndex(79)]
		[PersistentStringProperty(500)]
		public string PatRelToSubs3
		{
			get { return this.m_PatRelToSubs3; }
			set { this.m_PatRelToSubs3 = value; }
		}

		[ImportIndex(80)]
		[PersistentStringProperty(500)]
		public string InsPolicyID1
		{
			get { return this.m_InsPolicyID1; }
			set { this.m_InsPolicyID1 = value; }
		}

		[ImportIndex(81)]
		[PersistentStringProperty(500)]
		public string InsPolicyID2
		{
			get { return this.m_InsPolicyID2; }
			set { this.m_InsPolicyID2 = value; }
		}

		[ImportIndex(82)]
		[PersistentStringProperty(500)]
		public string InsPolicyID3
		{
			get { return this.m_InsPolicyID3; }
			set { this.m_InsPolicyID3 = value; }
		}

		[ImportIndex(83)]
		[PersistentStringProperty(500)]
		public string InsGroupID1
		{
			get { return this.m_InsGroupID1; }
			set { this.m_InsGroupID1 = value; }
		}

		[ImportIndex(84)]
		[PersistentStringProperty(500)]
		public string InsGroupID2
		{
			get { return this.m_InsGroupID2; }
			set { this.m_InsGroupID2 = value; }
		}

		[ImportIndex(85)]
		[PersistentStringProperty(500)]
		public string InsGroupID3
		{
			get { return this.m_InsGroupID3; }
			set { this.m_InsGroupID3 = value; }
		}
	}
}
