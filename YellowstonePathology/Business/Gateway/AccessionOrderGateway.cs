﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class AccessionOrderGateway
	{
        public static YellowstonePathology.Business.HL7View.ADTMessages GetADTMessages(string mrn)
        {
            YellowstonePathology.Business.HL7View.ADTMessages result = new HL7View.ADTMessages();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tblADT where MedicalRecordNo = @MRN;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MRN", mrn);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.HL7View.ADTMessage item = new HL7View.ADTMessage();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        item.ParseHL7();
                        result.Messages.Add(item);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.UI.EmbeddingBreastCaseList GetEmbeddingBreastCasesCollection()
        {
            YellowstonePathology.UI.EmbeddingBreastCaseList result = new UI.EmbeddingBreastCaseList();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select ao.MasterAccessionNo, ao.PFirstName, ao.PLastName, so.Collectiontime, so.ProcessorRun, " +
                "so.FixationStartTime, so.FixationEndTime, hour(timediff(fixationendtime, fixationstarttime)) FixationDurationCalc, " +
                "FixationDuration, so.Description " + 
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " +
                "where instr(so.Description, 'Breast') > 0 " +
                "and ao.AccessionDate >= date_add(curdate(), interval -30 day) " +
                "order by ao.AccessionTime desc;";
            cmd.CommandType = CommandType.Text;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.UI.EmbeddingBreastCaseListItem item = new UI.EmbeddingBreastCaseListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.UI.EmbeddingNotScannedList GetEmbeddingNotScannedCollection(DateTime accessionDate)
        {
            YellowstonePathology.UI.EmbeddingNotScannedList result = new UI.EmbeddingNotScannedList();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select a.AliquotOrderId, pso.PanelSetName, so.Description " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblSpecimenOrder so on ao.MasterAccessionno = so.MasterAccessionNo " +
                "join tblAliquotOrder a on so.SpecimenOrderId = a.SpecimenOrderId " +
                "where ao.AccessionDate = @AccessionDate and a.aliquotType = 'Block' and a.embeddingVerified = 0 and a.ClientAccessioned = 0;";                

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AccessionDate", accessionDate); 

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.UI.EmbeddingNotScannedListItem item = new UI.EmbeddingNotScannedListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Test.AliquotOrderCollection GetEmbeddingCollection(DateTime embeddingVerifiedDate)
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Test.AliquotOrderCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select a.*, null as TestOrderId " +
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder s on ao.MasterAccessionNo = s.masterAccessionNo " +
                "join tblAliquotOrder a on s.SpecimenOrderId = a.SpecimenOrderId " +
                "where a.EmbeddingVerifiedDate between @EmbeddingVerifiedDate and @EmbeddingVerifiedDatePlus1 " +
                "order by a.EmbeddingVerifiedDate desc;";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmbeddingVerifiedDate", embeddingVerifiedDate);
            cmd.Parameters.AddWithValue("@EmbeddingVerifiedDatePlus1", embeddingVerifiedDate.AddDays(1));

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(aliquotOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotOrderHoldCollection()
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Test.AliquotOrderCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblAliquotOrder where Status = 'Hold';";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(aliquotOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotOrderCollection(string specimenOrderId)
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Test.AliquotOrderCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblAliquotOrder where SpecimenOrderId = '" + specimenOrderId + "';";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(aliquotOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSpecimenOrderCollection(DateTime embeddingVerifiedDate)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Specimen.Model.SpecimenOrderCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select s.* from tblSpecimenOrder s " +
                "where exists(select * from tblAliquotOrder where specimenOrderId = s.SpecimenOrderId and " +
                "tblAliquotOrder.EmbeddingVerifiedDate between @EmbeddingVerifiedDate and @EmbeddingVerifiedDatePlus1);";
            cmd.Parameters.AddWithValue("@EmbeddingVerifiedDate", embeddingVerifiedDate);
            cmd.Parameters.AddWithValue("@EmbeddingVerifiedDatePlus1", embeddingVerifiedDate.AddDays(1));
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(specimenOrder);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSpecimenOrderCollection(string containId)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Specimen.Model.SpecimenOrderCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblSpecimenOrder where tblSpecimenOrder.ContainId = @ContainerId;";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ContainerId", containId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(specimenOrder);
                    }
                }
            }
            return result;
        }
        
        public static YellowstonePathology.Business.Typing.TypingShortcutCollection GetTypingShortcutCollectionByUser(int userId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where tblTypingShortcut.UserId = @UserId or Type = 'Global' order by Shortcut;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", userId);

            YellowstonePathology.Business.Typing.TypingShortcutCollection typingShorcutCollection = new Typing.TypingShortcutCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = new Typing.TypingShortcut();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(typingShortcut, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                        typingShorcutCollection.Add(typingShortcut);
                    }
                }
            }
            return typingShorcutCollection;
        }

        public static string GetMasterAccessionNoFromReportNo(string reportNo)
        {
            string result = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select MasterAccessionNo from tblPanelSetOrder where tblPanelSetOrder.ReportNo = @ReportNo;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public static string GetMasterAccessionNoFromContainerId(string containerId)
        {
            string result = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select MasterAccessionNo from tblSpecimenOrder where tblSpecimenOrder.ContainerId = @ContainerId;";
            cmd.Parameters.AddWithValue("@ContainerId", containerId);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public static string GetMasterAccessionNoFromAliquotOrderId(string aliquotOrderId)
        {
            string result = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select MasterAccessionNo " +
                "from tblSpecimenOrder so " +
                "join tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +
                "where ao.AliquotOrderId = @AliquotOrderId;";

            cmd.Parameters.AddWithValue("@AliquotOrderId", aliquotOrderId);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = (string)cmd.ExecuteScalar();
            }

            return result;
        }

        public static void SetPanelSetOrderAsCancelledTest(string reportNo)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Update tblPanelSetOrder set PanelSetid = 66, panelSetName = 'Test Cancelled', CaseType = 'Test Cancelled' " +
                "where tblPanelSetOrder.Reportno = @ReportNo;";
			cmd.Parameters.AddWithValue("@ReportNo", reportNo);
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

		public static void InsertTestCancelledTestOrder(string reportNo, int cancelledTestId, string cancelledTestName)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Insert tblTestCancelledTestOrder (ReportNo, CancelledTestId, CancelledTestname) " +
                "values (@ReportNo, @CancelledTestId, @CancelledTestName);";
			cmd.Parameters.AddWithValue("@ReportNo", reportNo);
			cmd.Parameters.AddWithValue("@CancelledTestId", cancelledTestId);
			cmd.Parameters.AddWithValue("@CancelledTestName", cancelledTestName);
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

        public static YellowstonePathology.Business.Test.PanelSetOrderView GetCaseToSchedule(string reportNo)
        {
			YellowstonePathology.Business.Test.PanelSetOrderView result = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder where tblPanelSetOrder.ReportNo = @ReportNo;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new Test.PanelSetOrderView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetCasesToSchedule()
        {
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where pso.Final = 1 and pso.ScheduledPublishTime is null and pso.Published = 0;";
            cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetOrderView);
					}
				}
			}
			return result;
		}

		public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetNextCasesToPublish()
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
			MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where pso.Final = 1 and pso.finalTime < date_Add(Now(), Interval -15 Minute) " +
                "and pso.ScheduledPublishTime <= Now() union " +
                "Select pso.* from tblPanelSetOrder pso join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Final = 1 and pso.finalTime < date_Add(Now(), Interval -15, Minute) and trd.ScheduledDistributionTime <= Now() " +
                "and pso.Distribute = 1;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetOrderView);
					}
				}
			}
			return result;
		}

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetNextTORD()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution limit 10;";            
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetScheduledDistribution(string reportNo)
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution where tblTestOrderReportDistribution.ScheduledDistributionTime is not null " +
                "and tblTestOrderReportDistribution.ReportNo = @ReportNo;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledDistribution()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution tor	join tblPanelSetOrder pso on tor.ReportNo = pso.ReportNo " +
                "where tor.Distributed = 0 and tor.ScheduledDistributionTime is null and pso.Final = 1 and pso.Distribute = 1 " +
                "and pso.HoldDistribution = 0;"; 
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledAmendments()
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trd.* " +
                "from tblAmendment a " +
		        "join tblTestOrderReportDistribution trd on a.ReportNo = trd.ReportNo " +
		        "where trd.TimeOfLastDistribution < a.finalTime and trd.ScheduledDistributionTime is null;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }      

        public static List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> GetUnscheduledDistribution(string reportNo)
        {
            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> result = new List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblTestOrderReportDistribution where Distributed = 0 and ScheduledDistributionTime is null and " +
                "tblTestOrderReportDistribution.ReportNo = @ReportNo;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }              

		public static YellowstonePathology.Business.Test.AccessionOrderView GetAccessionOrderView(string masterAccessionNo)
		{
			YellowstonePathology.Business.Test.AccessionOrderView result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblAccessionOrder where tblAccessionOrder.MasterAccessionNo = @MasterAccessionNo;";
			cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new Test.AccessionOrderView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

        public static List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder> GetSurgicalTestOrder()
        {
			List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder> result = new List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder>();

            MySqlCommand cmd = new MySqlCommand();            
            cmd.CommandText = "Select * " +
	            "from tblPanelSetOrder pso " +
		        "join tblSurgicalTestOrder sto on pso.ReportNo = sto.ReportNo " +
		        "where pso.OrderDate >= '1/1/2015' and cancersummary is not null;";            
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder sto = new YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(sto, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(sto);
                    }
                }
            }
            return result;
        }

        public static List<YellowstonePathology.Business.Test.PanelSetOrderView> GetUnsetDistributions()
        {
            List<YellowstonePathology.Business.Test.PanelSetOrderView> result = new List<Test.PanelSetOrderView>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblPanelSetOrder pso where final = 1 and distribute = 1 and not exists " +
                "(Select * from tblTestOrderReportDistribution where ReportNo = pso.ReportNo);";            
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = new Test.PanelSetOrderView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderView, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(panelSetOrderView);
                    }
                }
            }
            return result;
        }        

        public static YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection GetSVHBillingDataCollection(string mrn)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select * from tblSVHBillingData where MRN = @MRN;";
			cmd.Parameters.AddWithValue("@MRN", mrn);
			cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection result = new YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.Patient.Model.SVHBillingData svhBillingData = new YellowstonePathology.Business.Patient.Model.SVHBillingData();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(svhBillingData, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(svhBillingData);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Facility.Model.HostCollection GetHostCollection()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select * from tblHost;";
			cmd.CommandType = CommandType.Text;

			YellowstonePathology.Business.Facility.Model.HostCollection hostCollection = new YellowstonePathology.Business.Facility.Model.HostCollection();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Facility.Model.Host host = new YellowstonePathology.Business.Facility.Model.Host();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(host, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						hostCollection.Add(host);
					}
				}
			}
			return hostCollection;
		}		

		public static string GetNextMasterAccessionNo()
		{
			string result = null;

			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "gwGetNextMasterAccessionNo";
			cmd.CommandType = CommandType.StoredProcedure;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = dr["MasterAccessionNo"].ToString();
					}
				}
			}

			return result;
		}

		public static string NextReportNo(int panelSetId, string masterAccessionNo)
		{
			string newReportNo = string.Empty;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "pGetNextReportNo";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
			cmd.Parameters.AddWithValue("PanelSetId", panelSetId);
			MySqlParameter newReportParameter = new MySqlParameter("NewReportNo", MySqlDbType.String, 12, ParameterDirection.Output, false, 12, 12, "NewReportNo", DataRowVersion.Current, newReportNo);
			cmd.Parameters.Add(newReportParameter);
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				MySqlDataReader dr = cmd.ExecuteReader();
				string result = cmd.Parameters["NewReportNo"].Value.ToString();
				return result;
			}
		}

		public static SpecialStain.StainResultOptionList GetStainResultOptionListByStainResultId(string stainResultId)
		{
			SpecialStain.StainResultOptionList result = new SpecialStain.StainResultOptionList();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = " select sro.StainResult, sro.StainResultOptionId from tblStainResult sr join tblTestOrder tt on " +
                "sr.TestOrderId = tt.TestOrderId " +
				"join tblTest t on tt.TestId = t.TestId " +
				"join tblStainResultOptionGroup srog on t.StainResultGroupId = srog.StainResultGroupId " +
				"join tblStainResultOption sro on sro.StainResultOptionId = srog.StainResultOptionId " +
				"where sr.StainResultId = @StainResultId;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@StainResultId", stainResultId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						SpecialStain.StainResultOption stainResultOption = new SpecialStain.StainResultOption();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(stainResultOption, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(stainResultOption);
					}
				}
			}

			return result;
		}

        public static Surgical.PathologistHistoryList GetPathologistPatientHistory(string patientId)
        {
            Surgical.PathologistHistoryList result = new Surgical.PathologistHistoryList();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "prcGetPatientHistory_5";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PatientId", patientId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Surgical.PathologistHistoryItem pathologistHistoryItem = new Surgical.PathologistHistoryItem();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pathologistHistoryItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pathologistHistoryItem);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Surgical.PathologistHistoryItemListItem pathologistHistoryItemListItem = new Surgical.PathologistHistoryItemListItem();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pathologistHistoryItemListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(Surgical.PathologistHistoryItem pathologistHistoryItem in result)
                        {
                            if(pathologistHistoryItemListItem.ReportNo == pathologistHistoryItem.ReportNo)
                            {
                                pathologistHistoryItem.PathologistHistoryItemList.Add(pathologistHistoryItemListItem);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByAccessionDate(DateTime accessionDate)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate and pso.PanelSetId = 13 " +
				"ORDER BY a.AccessionTime;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@AccessionDate", accessionDate);

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByAccessionDatePQRI(DateTime finalDate)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSurgicalTestOrder sr ON pso.ReportNo = sr.ReportNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE pso.FinalDate = @FinalDate " +
				"and a.MasterAccessionNo in (Select MasterAccessionNo from tblSpecimenOrder where MasterAccessionNo = a.MasterAccessionNo " +
				"and " +
				"Locate('Prostate', Description) > 0 or " +
                "Locate('Breast', Description) > 0 or " +
                "(Locate('Colon', Description) > 0 and Locate('Rectum', Description) > 0) or " +
                "Locate('Esophagus', Description) > 0) " +
				"ORDER BY pso.OrderTime;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@FinalDate", finalDate);

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByFinalDate(DateTime finalDate)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE pso.FinalDate = @FinalDate and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@FinalDate", finalDate);

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByNotAudited()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.Final = 1 AND pso.Audited = 0 and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime;";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		public static Surgical.SurgicalOrderList GetSurgicalOrderListByIntraoperative()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT DISTINCT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSurgicalSpecimenResult ssr ON pso.ReportNo = ssr.ReportNo" +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId WHERE ssr.ImmediatePerformedBy is not null AND ssr.ImmediatePerformedBy <> ' ' " +
				"AND ssr.SurgicalSpecimenId not in (SELECT SurgicalSpecimenId FROM tblIntraoperativeConsultationResult) " +
				"ORDER BY a.AccessionTime;";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		/*public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoSignature()
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
				"WHERE pso.Final = 1 AND pso.SignatureAudit = 1 and pso.PanelSetId = 13 " +
				"ORDER BY pso.OrderTime;";
			cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}*/

        public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoGross()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from " +
                "(SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
                "pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited, a.AccessionTime " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "JOIN tblSurgicalTestOrder sto ON pso.ReportNo = sto.ReportNo " +
                "JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE sto.Grossx like '%???%' and a.AccessionDate >= date_add(curdate(), Interval -10 Day)) T1 " +
                "ORDER BY AccessionTime;";
            cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
        }

        public static Surgical.SurgicalOrderList GetSurgicalOrderListByNoClinicalInfo()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from " +
                "(SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
                "pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited, a.AccessionTime " +
                "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "JOIN tblSurgicalTestOrder sto ON pso.ReportNo = sto.ReportNo " +
                "JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE a.ClinicalHistory = '???' and a.AccessionDate >= date_add(curdate(), Interval -10 Day)) T1 " +
                "ORDER BY AccessionTime;";
            cmd.CommandType = CommandType.Text;

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
        }

		public static Surgical.SurgicalOrderList GetSurgicalOrderListForSvhClientOrder(DateTime date)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, a.AccessionDate, concat(a.PFirstName, ' ', a.PLastName) AS PatientName, pso.AcceptedDate, " +
				"pso.FinalDate, su.DisplayName AS Pathologist, su.UserId AS PathologistId, pso.Audited " +
				"FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
				"Left outer JOIN tblSystemUser su on pso.AssignedToId = su.UserId " +
                "WHERE a.AccessionDate = @AccessionDate and a.ClientId in (558,33,57,123,126,230,242,250,253,313,505,558,622,744,758,759,760,820,845,873,969,979,1025,1058,1124,1151,1306,1313,1321) " +
				"and pso.PanelSetId = 13 ORDER BY pso.OrderTime;";

			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@AccessionDate", date);

			Surgical.SurgicalOrderList result = AccessionOrderGateway.BuildSurgicalOrderList(cmd);
			return result;
		}

		private static Surgical.SurgicalOrderList BuildSurgicalOrderList(MySqlCommand cmd)
		{
			Surgical.SurgicalOrderList result = new Surgical.SurgicalOrderList();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Surgical.SurgicalOrderListItem surgicalOrderListItem = new Surgical.SurgicalOrderListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalOrderListItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(surgicalOrderListItem);
					}
				}
			}

			return result;
		}

		// This view needs work
		public static Surgical.SurgicalRescreenItemCollection GetSurgicalRescreenItemCollectionByDate(DateTime date)
		{
			Surgical.SurgicalRescreenItemCollection result = new Surgical.SurgicalRescreenItemCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select Distinct SpecimenOrderId, AccessionDate, AccessionNo, Description, PLastName, ifnull(RescreenStatus, '') " +
                "RescreenStatus from ViewAccessionOrderSurgicalResult_1 where ViewAccessionOrderSurgicalResult_1.AccessionDate = @Date " +
				"and (Locate('Breast', Description) > 0 or Locate('Prostate', Description) > 0 or Locate('Pituitary', Description) > 0 " +
                "or Locate('Brain', Description) > 0 or Locate('Thyroid', Description) > 0 " +
				"or Locate('Lung', Description) > 0);";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@Date", date);
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Surgical.SurgicalRescreenItem surgicalRescreenItem = new Surgical.SurgicalRescreenItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalRescreenItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(surgicalRescreenItem);
					}
				}
			}

			return result;
		}

		public static Surgical.SurgicalMasterLogList GetSurgicalMasterLogList(DateTime reportDate)
		{
			Surgical.SurgicalMasterLogList result = new Surgical.SurgicalMasterLogList();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "pGetSurgicalMasterLogListByDate";
            /*"declare rpts table " +
            "( " +
            "AccessionTime datetime, " +
            "ReportNo varchar(20), " +
            "AccessioningFacilityId varchar(100), " +
            "PFirstName varchar(100), " +
            "PLastName varchar(100), " +
            "PBirthdate datetime, " +
            "PhysicianName varchar(100), " +
            "ClientName varchar(100), " +
            "AliquotCount int " +
            ") " +
            "insert rpts " +
            "SELECT Distinct a.AccessionTime, pso.ReportNo, a.AccessioningFacilityId, a.PFirstName, a.PLastName, " +
            "a.PBirthdate, a.PhysicianName, a.ClientName, Count(*) AliquotCount " +
            "FROM tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
            "JOIN tblSpecimenORder so on a.MasterAccessionNo = so.MasterAccessionNo " +
            "LEFT OUTER JOIN tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +
            "WHERE AccessionDate = @ReportDate and pso.PanelSetId in (13, 50)  " +
            " group by a.AccessionTime, pso.ReportNo, a.AccessioningFacilityId, a.PFirstName, a.PLastName, " +
            "a.PBirthdate, a.PhysicianName, a.ClientName " +
            "Order By AccessionTime " +
            "SELECT rpts.* From rpts rpts Order By AccessionTime; " +
            "Select ssr.DiagnosisId, so.Description, ssr.ReportNo " +
            "FROM tblSurgicalSpecimen ssr " +
            "JOIN tblSpecimenOrder so ON ssr.SpecimenOrderId = so.SpecimenOrderId " +
            "join rpts rpts on ssr.ReportNo = rpts.ReportNo order by 1;";
            cmd.CommandType = CommandType.Text;*/
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ReportDate", reportDate);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Surgical.SurgicalMasterLogItem surgicalMasterLogItem = new Surgical.SurgicalMasterLogItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(surgicalMasterLogItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(surgicalMasterLogItem);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Surgical.MasterLogItem masterLogItem = new Surgical.MasterLogItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(masterLogItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();

                        foreach(Surgical.SurgicalMasterLogItem surgicalMasterLogItem in result)
                        {
                            if(masterLogItem.ReportNo == surgicalMasterLogItem.ReportNo)
                            {
                                surgicalMasterLogItem.MasterLogList.Add(masterLogItem);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
		}

		public static YellowstonePathology.Business.Domain.OrderLogCollection GetOrderLogCollectionByReportDate(DateTime reportDate)
		{
			YellowstonePathology.Business.Domain.OrderLogCollection results = new Domain.OrderLogCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT pso.ReportNo, su.Initials, TestName, CASE ao.LabelPrefix " +
               "WHEN 'FS' then concat('FS', ao.Label, ' - ', ifnull(so.Description, '')) " +
			  "WHEN 'CB' then concat('CB', ao.Label, ' - ', ifnull(so.Description, '')) " +
              "ELSE concat(ifnull(ao.Label, ''), ' - ', ifnull(so.Description, '')) END as Description, po.OrderTime, " +
			  "ifnull(sr.ProcedureComment, '') as ProcedureComment FROM tblPanelOrder po Left Outer JOIN tblSystemUser su ON po.OrderedById = su.userID " +
			  "JOIN tblPanelSetOrder pso ON po.ReportNo = pso.ReportNo JOIN tblTestOrder ot on ot.PanelOrderId = po.PanelOrderId " +
			  "JOIN tblAliquotOrder ao ON ot.AliquotOrderId = ao.AliquotOrderId " +
			  "JOIN tblSpecimenOrder so ON ao.SpecimenOrderId = so.SpecimenOrderId LEFT OUTER JOIN tblStainResult sr ON  sr.TestOrderId = ot.TestOrderId " +
              "WHERE po.OrderDate = @OrderDate AND po.PanelId in (19, 21) and ot.TestId not in (49) ORDER BY 5 Asc, 1;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@OrderDate", reportDate);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderLogItem orderLogItem = new YellowstonePathology.Business.Domain.OrderLogItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(orderLogItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						results.Add(orderLogItem);
					}
				}
			}
			return results;
		}

		public static View.RecentAccessionViewCollection GetRecentAccessionOrders(string pLastName, string pFirstName)
		{
			YellowstonePathology.Business.View.RecentAccessionViewCollection result = new View.RecentAccessionViewCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select ao.MasterAccessionNo, pso.ReportNo, ao.PFirstName, ao.PLastName, ao.PBirthdate, ao.AccessionTime, " +
                "ao.ClientName, ao.PhysicianName, ao.CollectionTime from tblAccessionOrder ao " +
				"left outer join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "where ao.PFirstName = @PFirstName and ao.PLastName = @PLastName and datediff(curdate(), ao.AccessionDate) <= 7 ;";
			cmd.Parameters.AddWithValue("@PLastName", pLastName);
			cmd.Parameters.AddWithValue("@PFirstName", pFirstName);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.View.RecentAccessionView recentAccessionView = new YellowstonePathology.Business.View.RecentAccessionView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(recentAccessionView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(recentAccessionView);
					}
				}
			}
			return result;
		}

		public static ReportNoCollection GetReportNumbers()
		{
			MySqlCommand cmd = new MySqlCommand("gwGetReportNumbers");
			cmd.CommandType = CommandType.StoredProcedure;

			YellowstonePathology.Business.ReportNoCollection reportNoCollection = new ReportNoCollection();

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ReportNo reportNo = new ReportNo(dr.GetString(0));
						reportNoCollection.Add(reportNo);
					}
				}
			}

			return reportNoCollection;
		}

		public static ReportNoCollection GetReportNumbersByPostDate(DateTime postDate)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select Distinct ReportNo from tblPanelSetOrderCPTCodeBill where tblPanelSetOrderCPTCodeBill.PostDate = @PostDate;";
			cmd.Parameters.AddWithValue("@PostDate", postDate);

			YellowstonePathology.Business.ReportNoCollection reportNoCollection = new ReportNoCollection();

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.ReportNo reportNo = new ReportNo(dr.GetString(0));
						reportNoCollection.Add(reportNo);
					}
				}
			}

			return reportNoCollection;
		}


        public static List<YellowstonePathology.Business.Patient.Model.SVHBillingData> GetPatientImportDataList(string reportNo)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "select svb.* " +
				"from tblAccessionOrder ao " +
				"join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"join tblSVHBillingData svb on ao.SVHMedicalRecord = svb.MRN " +
                "where pso.ReportNo = @ReportNo " +
				"order by FileDate desc;";
			cmd.Parameters.AddWithValue("@ReportNo", reportNo);

            List<YellowstonePathology.Business.Patient.Model.SVHBillingData> result = new List<YellowstonePathology.Business.Patient.Model.SVHBillingData>();

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
                        YellowstonePathology.Business.Patient.Model.SVHBillingData svhBillingData = new YellowstonePathology.Business.Patient.Model.SVHBillingData();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(svhBillingData, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(svhBillingData);
					}
				}
			}

			return result;
		}

		public static bool CanDeleteBatch(int panelOrderBatchId)
		{
			MySqlCommand cmd = new MySqlCommand("Select count(*) from tblPanelOrder where tblPanelOrder.PanelOrderBatchId = @PanelOrderBatchId;");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@PanelOrderBatchId", panelOrderBatchId);
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				MySqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				int cnt = Convert.ToInt32(dr.GetValue(0));
				return cnt == 0 ? true : false;
			}
		}

		public static int GetCountOpenCytologyCasesByCollectionDateRange(DateTime startDate, DateTime endDate)
		{
			MySqlCommand cmd = new MySqlCommand("select count(*) from tblAccessionOrder ao join tblPanelSetOrder pso on " +
                "ao.MasterAccessionNo = pso.MasterAccessionNo where " +
                "ao.CollectionDate between @StartDate and @EndDate and pso.PanelSetId = 15 and pso.Final = 0;");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@StartDate", startDate);
			cmd.Parameters.AddWithValue("@EndDate", endDate);
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				MySqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				int result = Convert.ToInt32(dr.GetValue(0));
				return result;
			}
		}

		public static void SetPatientId(string masterAccessionNo, string patientId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;

			cmd.CommandText = "Update tblAccessionOrder set tblAccessionOrder.PatientId = @PatientId " +
                "where tblAccessionOrder.MasterAccessionNo = @MasterAccessionNo;";
			cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
			cmd.Parameters.AddWithValue("@PatientId", patientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

		public static YellowstonePathology.Business.Domain.PatientHistory GetPatientHistory(string patientId)
		{
			YellowstonePathology.Business.Domain.PatientHistory result = new Domain.PatientHistory();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "pGetPatientHistoryResults";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("PatientId", patientId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult = new Domain.PatientHistoryResult();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						patientHistoryResult.WriteProperties(propertyWriter);
						result.Add(patientHistoryResult);
					}
				}
			}

			return result;
		}

        public static YellowstonePathology.Business.Test.PantherAliquotList GetPantherOrdersNotAliquoted()
        {
            YellowstonePathology.Business.Test.PantherAliquotList result = new Test.PantherAliquotList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, a.AccessionTime, a.PLastName, a.PFirstName, ao.AliquotType, " +
                "ao.ValidationDate, ao.ValidatedBy " +
                "from tblAccessionOrder a " +
                "join tblSpecimenOrder so on a.MasterAccessionNo = so.MasterAccessionNo " +
                "join tblAliquotOrder ao on so.SpecimenOrderId = ao.SpecimenOrderId " +                
                "where ao.AliquotType = 'Panther Aliquot' " +
                "and ao.Validated = 0 order by a.AccessionTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem = new Test.PantherAliquotListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherAliquotListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherAliquotListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.masterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +                 
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime;";          

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.masterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, " +
                "ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, " +
                "ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalNGCT()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, ngct.NeisseriaGonorrhoeaeResult, " +
                "ngct.ChlamydiaTrachomatisResult, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblNGCTTestOrder ngct on pso.ReportNo = ngct.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemNGCT pantherOrderListItem = new Test.PantherOrderListItemNGCT();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.OrderTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalHPV()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, psoh.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblHPVTestOrder psoh on pso.ReportNo = psoh.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 and " +
                "pso.FinalDate >= date_Add(pso.FinalDate, Intervfal -3 Month) order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotAcceptedTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionno, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, t.Result, null, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 0 order by pso.OrderTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, t.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +                
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Accepted = 1 and pso.Final = 0 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalHPV1618()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, null as Result, hpv.HPV16Result, hpv.HPV18Result, pso.HoldDistribution " +            
                "from tblPanelSetOrder pso " +
                "join tblPanelSetOrderHPV1618 hpv on pso.ReportNo = hpv.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItemHPV1618 pantherOrderListItem = new Test.PantherOrderListItemHPV1618();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalTrichomonas()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;            
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.AcceptedTime, pso.FinalTime, t.Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblTrichomonasTestOrder t on pso.ReportNo = t.ReportNo " +                
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where TechnicalComponentInstrumentId = 'PNTHR' and pso.Final = 1 order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersFinalWHP()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.ReportNo, pso.OrderTime, pso.PanelSetName, a.PLastName, " +
                "a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, null as Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblWomensHealthProfileTestOrder psowhp on pso.ReportNo = psowhp.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where pso.Final = 1 and pso.FinalDate >= date_Add(curdate(), interval -1, Month) order by pso.FinalTime desc;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

        public static YellowstonePathology.Business.Test.PantherOrderList GetPantherOrdersNotFinalWHP()
        {
            YellowstonePathology.Business.Test.PantherOrderList result = new Test.PantherOrderList();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.MasterAccessionNo, pso.MasterAccessionNo, pso.ReportNo, pso.OrderTime, " +
                "pso.PanelSetName, a.PLastName, a.PFirstName, pso.ResultCode, pso.AcceptedTime, pso.FinalTime, null as Result, pso.HoldDistribution " +
                "from tblPanelSetOrder pso " +
                "join tblWomensHealthProfileTestOrder psowhp on pso.ReportNo = psowhp.ReportNo " +
                "join tblAccessionOrder a on pso.MasterAccessionNo = a.MasterAccessionNo " +
                "where pso.Final = 0 order by pso.OrderTime;";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = new Test.PantherOrderListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pantherOrderListItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(pantherOrderListItem);
                    }
                }
            }

            return result;
        }

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetTaskOrderCollection(string acknowledgementType)
		{
            MySqlCommand cmd = new MySqlCommand("select * from tblTaskOrder where tblTaskOrder.AcknowledgementType = @AcknowledgementType and " +
                "OrderDate between date_add(curdate(), Interval-15 Day) and curdate() order by OrderDate desc; " +
                "select * from tblTaskOrderDetail tod " +
                "join tblTaskOrderDetailFedexShipment todf on tod.TaskOrderDetailId = todf.TaskOrderDetailId " +
                "where TaskOrderId in(select TaskOrderId from tblTaskOrder where " +
                "AcknowledgementType = @AcknowledgementType and OrderDate between date_add(curdate(), Interval-15 Day) and curdate()) " +
                "order by tod.TaskOrderDetailId;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AcknowledgementType", acknowledgementType);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = BuildTaskOrderCollection(cmd);
            return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetDailyTaskOrderCollection()
		{
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            string sql = "Select * from tblTaskOrder where AcknowledgementType = 'Daily' " +
	            "and Acknowledged = 0 " +
	            "and TaskDate <= curdate() order by TaskDate desc;";
            MySqlCommand cmd = new MySqlCommand(sql);	
			cmd.CommandType = CommandType.Text;
			//cmd.Parameters.AddWithValue("@AcknowledgementType", Task.Model.TaskAcknowledgementType.Daily);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(taskOrder);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetDailyTaskOrderHistoryCollection(int daysBack)
		{
			YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
			string sql = "Select * from tblTaskOrder where AcknowledgementType = 'Daily' " +
                "and TaskDate between @StartDate and @EndDate order by TaskDate desc;";
			MySqlCommand cmd = new MySqlCommand(sql);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@StartDate", DateTime.Today.AddDays(-daysBack));
			cmd.Parameters.AddWithValue("@EndDate", DateTime.Today);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(taskOrder);
					}
				}
			}

			return result;
		}

		public static DateTime GetNewestDailyTaskOrderTaskDate(string taskId)
		{
			DateTime result = DateTime.Today;
			MySqlCommand cmd = new MySqlCommand("Select max(TaskDate) from tblTaskOrder where " +
                "tblTaskOrder.AcknowledgementType = @AcknowledgementType and tblTaskOrder.TaskId = @TaskId;");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@AcknowledgementType", Task.Model.TaskAcknowledgementType.Daily);
			cmd.Parameters.AddWithValue("@TaskId", taskId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						DateTime? date = dr[0] as DateTime?;
						if (date.HasValue)
						{
							result = date.Value.AddDays(1);
						}
					}
				}
			}

			return result;
		}

		public static DateTime GetNewestWeeklyTaskOrderTaskDate(string taskId)
		{
			DateTime result = DateTime.Today;
			int currentDay = (int)result.DayOfWeek;
			int daysToAdd = 8 - currentDay;
			if (daysToAdd > 7)
			{
				daysToAdd -= 7;
			}
			result = result.AddDays(daysToAdd);
			MySqlCommand cmd = new MySqlCommand("Select max(TaskDate) from tblTaskOrder where " +
                "tblTaskOrder.AcknowledgementType = @AcknowledgementType and tblTaskOrder.TaskId = @TaskId;");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@AcknowledgementType", Task.Model.TaskAcknowledgementType.Daily);
			cmd.Parameters.AddWithValue("@TaskId", taskId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read() == true)
					{
						DateTime? date = dr[0] as DateTime?;
						if (date.HasValue)
						{
							result = date.Value.AddDays(7);
						}
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Task.Model.TaskOrderCollection GetTasksNotAcknowledged(string assignedTo, string acknowledgementType)
		{
            MySqlCommand cmd = new MySqlCommand("select * from tblTaskOrder where tblTaskOrder.AcknowledgementType = AcknowledgementType " +
                "and TaskOrderId in (Select TaskOrderId from tblTaskOrderDetail where Acknowledged = 0 and " +
                "tblTaskOrderDetail.AssignedTo = @AssignedTo) order by OrderDate desc; " +
                "select * from tblTaskOrderDetail tod join tblTaskOrderDetailFedexShipment todf on tod.TaskOrderDetailId = todf.TaskOrderDetailId " +
                "where Acknowledged = 0 and tod.AssignedTo = @AssignedTo order by tod.TaskOrderDetailId;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AssignedTo", assignedTo);
            cmd.Parameters.AddWithValue("@AcknowledgementType", acknowledgementType);
            YellowstonePathology.Business.Task.Model.TaskOrderCollection result = BuildTaskOrderCollection(cmd);
            return result;
		}

        private static Task.Model.TaskOrderCollection BuildTaskOrderCollection(MySqlCommand cmd)
        {
            YellowstonePathology.Business.Task.Model.TaskOrderCollection result = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(taskOrder);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Task.Model.TaskOrderDetail taskOrderDetail = new Task.Model.TaskOrderDetail();
                        string taskId = dr["TaskId"].ToString();
                        if (dr["TaskId"].ToString() == "FDXSHPMNT")
                        {
                            taskOrderDetail = new Task.Model.TaskOrderDetailFedexShipment();
                        }                        

                        Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrderDetail, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();

                        foreach (Task.Model.TaskOrder taskOrder in result)
                        {
                            if (taskOrderDetail.TaskOrderId == taskOrder.TaskOrderId)
                            {
                                taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

    public static Test.PanelOrder BuildPanelOrder(XElement panelOrderElement)
		{
            YellowstonePathology.Business.Panel.Model.PanelCollection panelCollection = YellowstonePathology.Business.Panel.Model.PanelCollection.GetAll();
			int panelId = Convert.ToInt32(panelOrderElement.Element("PanelId").Value);
            YellowstonePathology.Business.Panel.Model.Panel panel = panelCollection.GetPanel(panelId);
			Test.PanelOrder panelOrder = Test.PanelOrderFactory.GetPanelOrder(panel);

			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelOrderElement, panelOrder);
			xmlPropertyWriter.Write();
			List<XElement> testOrderElements = (from item in panelOrderElement.Elements("TestOrderCollection") select item).ToList<XElement>();
			foreach (XElement testOrderElement in testOrderElements.Elements("TestOrder"))
			{
				YellowstonePathology.Business.Test.Model.TestOrder testOrder = BuildTestOrder(testOrderElement);
				panelOrder.TestOrderCollection.Add(testOrder);
			}
			return panelOrder;
		}

		public static YellowstonePathology.Business.Test.Model.TestOrder BuildTestOrder(XElement testOrderElement)
		{
			YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
			xmlPropertyWriter.Write();
			return testOrder;
		}

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewByAliquotOrderId(string aliquotOrderId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select ao.AliquotOrderId MaterialId, ao.AliquotType MaterialType, a.MasterAccessionNo, a.PLastName, " +
                "a.PFirstName, ao.Label MaterialLabel" +
				"from tblAliquotOrder ao " +
				"join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
				"join tblAccessionOrder a on so.MasterAccessionNo = a.MasterAccessionNo " +
                "where ao.AliquotOrderid = @AliquotOrderId ;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@AliquotOrderId", aliquotOrderId);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewByContainerId(string containerId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select so.ContainerId MaterialId, 'Container' MaterialType, ao.MasterAccessionNo, ao.PLastName, " +
                "ao.PFirstName, so.Description MaterialLabel" +
                "from tblAccessionOrder ao " +
                "join tblSpecimenOrder so on ao.MasterAccessionNo = so.MasterAccessionNo " +
                "where so.ContainerId = @ContainerId ;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ContainerId", "CTNR" + containerId);

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return result;
        }

		public static YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView GetMaterialTrackingScannedItemViewBySlideOrderId(string slideOrderId)
		{

			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select s.SlideOrderId MaterialId, 'Slide' MaterialType, a.MasterAccessionNo, a.PLastName, " +
                "a.PFirstName, s.Label MaterialLabel" +
				"from tblSlideOrder s " +
				"join tblAliquotOrder ao on s.AliquotOrderId = ao.AliquotOrderId " +
				"join tblSpecimenOrder so on ao.SpecimenOrderId = so.SpecimenOrderId " +
				"join tblAccessionOrder a on so.MasterAccessionNo = a.MasterAccessionNo " +
                "where s.SlideOrderid = @SlideOrderId ;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@SlideOrderId", slideOrderId);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView result = null;
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Monitor.Model.DistributionCollection GetPendingDistributions()
		{
			YellowstonePathology.Business.Monitor.Model.DistributionCollection result = new YellowstonePathology.Business.Monitor.Model.DistributionCollection();
			MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ao.AccessionTime, pso.ReportNo, pso.PanelSetName, pso.FinalTime, ao.PhysicianName, " +
                "ao.ClientName, Minute(timediff(Now(), trd.ScheduledDistributionTime))	MinutesSinceScheduled, trd.Distributed " +
	            "from tblAccessionOrder ao " +
		        "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
		        "join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Distribute = 1 and pso.Final = 1 and pso.finalTime < date_Add(Now(), Interval -15 Minute) and " +
                "trd.Distributed = 0 and trd.ScheduledDistributionTime is not null " +
				"Order By trd.ScheduledDistributionTime;";

			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.Distribution distribution = new YellowstonePathology.Business.Monitor.Model.Distribution();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(distribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(distribution);
					}
				}
			}

			return result;
		}		

		public static YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection GetPendingCytologyScreening()
		{
			YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection result = new YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select pso.ReportNo, ao.AccessionTime, cpo.ScreeningType, cpo.ScreenedByName, " +
                "su1.DisplayName AssignedToName, po.AcceptedTime ScreeningFinalTime, pso.FinalTime CaseFinalTime, " +
                "ao.ClientName, ao.PhysicianName ProviderName, pso.Final,  " +
				"(Select count(*) from tblPanelOrder where Reportno = pso.ReportNo) as ScreeningCount " +
				"from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
				"join tblPanelOrderCytology cpo on po.PanelOrderId = cpo.PanelORderId " +
				"left outer join tblSystemUser su on po.OrderedById = su.UserId " +
				"left outer join tblSystemUser su1 on po.AssignedToId = su1.UserId " +
				"Where pso.PanelSetId = 15 And po.Accepted  = 0 and pso.Final = 0 " +
				"Order By ao.AccessionTime;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.CytologyScreening cytologyScreening = new YellowstonePathology.Business.Monitor.Model.CytologyScreening();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyScreening, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(cytologyScreening);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Monitor.Model.PendingTestCollection GetPendingTestCollection()
		{
			YellowstonePathology.Business.Monitor.Model.PendingTestCollection result = new YellowstonePathology.Business.Monitor.Model.PendingTestCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select pso.ReportNo, pso.PanelSetName TestName, pso.ExpectedFinalTime, pso.OrderTime, " +
                "ao.ClientName, ao.PhysicianName ProviderName, su.DisplayName AssignedTo, pso.IsDelayed " +
				"from tblPanelSetOrder pso " +
				"join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo	" +
				"join tblSystemUser su on pso.AssignedToId = su.UserId " +
				"where final = 0 and panelSetId <> 212 " +  //Will not show Missing Information Tests!
				"order by ExpectedFinalTime;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Monitor.Model.PendingTest pendingTest = new YellowstonePathology.Business.Monitor.Model.PendingTest();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pendingTest, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(pendingTest);
					}
				}
			}

			return result;
		}

        public static YellowstonePathology.Business.Monitor.Model.MissingInformationCollection GetMissingInformationCollection()
        {
            YellowstonePathology.Business.Monitor.Model.MissingInformationCollection result = new YellowstonePathology.Business.Monitor.Model.MissingInformationCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select pso.MasterAccessionNo, pso.ReportNo, pso.PanelSetName TestName, pso.ExpectedFinalTime, " +
                "pso.OrderTime, mit.FirstCallComment, mit.SecondCallComment, ao.PhysicianName ProviderName " +
                "from tblPanelSetOrder pso " +
                "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo	" +
                "join tblMissingInformationTestOrder mit on pso.ReportNo = mit.ReportNo " +
                "join tblSystemUser su on pso.AssignedToId = su.UserId " +
                "where pso.PanelSetId = 212 and pso.Final = 0 " +
                "order by ExpectedFinalTime;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Monitor.Model.MissingInformation missingInformation = new YellowstonePathology.Business.Monitor.Model.MissingInformation();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(missingInformation, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(missingInformation);
                    }
                }
            }

            return result;
        }

        /*public static YellowstonePathology.Business.NeogenomicsResultCollection GetNeogenomicsResultCollection()
		{            
			YellowstonePathology.Business.NeogenomicsResultCollection result = new YellowstonePathology.Business.NeogenomicsResultCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblNeogenomicsResult order by DateResultReceived desc;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.NeogenomicsResult neogenomicsResult = new YellowstonePathology.Business.NeogenomicsResult();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(neogenomicsResult, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(neogenomicsResult);
					}
				}
			}
			return result;
		}*/

		public static YellowstonePathology.Business.Test.Model.StainTest GetStainTestByTestId(int testId)
		{
			YellowstonePathology.Business.Test.Model.StainTest result = null;
			MySqlCommand cmd = new MySqlCommand("SELECT * from tblStainTest where tblStainTest.TestId = @TestId;");
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@TestId", testId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Test.Model.StainTest();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.ImmunoComment GetImmunoCommentByImmunocommentId(int immunoCommentId)
		{
			YellowstonePathology.Business.Domain.ImmunoComment result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblImmunoComment where tblImmunoComment.immunocommentid = @ImmunoCommentId;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@ImmunoCommentId", immunoCommentId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.ImmunoComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.OrderCommentCollection GetAllLabEvents()
		{
			YellowstonePathology.Business.Domain.OrderCommentCollection result = new Domain.OrderCommentCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblOrderComment order by OrderCommentId;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderComment orderComment = new Domain.OrderComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(orderComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(orderComment);
					}
				}
			}
			return result;
		}

		public static Domain.OrderComment GetLabEventByEventId(int orderCommentId)
		{
			Domain.OrderComment result = new Domain.OrderComment();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblOrderComment where tblOrderComment.OrderCommentId = @OrderCommentId;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@OrderCommentId", orderCommentId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.OrderComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection GetPanelSetCaseTypeCollection()
		{
			YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection result = new YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "select distinct CaseType from tblPanelSet where CaseType is not null;";
			cmd.CommandType = CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType panelSetCaseType = new YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetCaseType, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(panelSetCaseType);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection GetOtherConditions()
		{
			YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection result = new Domain.Cytology.OtherConditionCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT LineID, OtherCondition OtherConditionText from tblCytologyOtherConditions order by OtherCondition;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Cytology.OtherCondition otherCondition = new Domain.Cytology.OtherCondition();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(otherCondition, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(otherCondition);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.OtherCondition GetOtherConditionById(int otherConditionId)
		{
			YellowstonePathology.Business.Domain.Cytology.OtherCondition result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT LineID, OtherCondition OtherConditionText from tblCytologyOtherConditions " +
                "where tblCytologyOtherConditions.LineId = @LineId;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@LineId", otherConditionId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.Cytology.OtherCondition();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection GetCytologyReportComments()
		{
			YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection result = new Domain.Cytology.CytologyReportCommentCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment, AbbreviatedComment from tblCytologyReportComment order by AbbreviatedComment;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Cytology.CytologyReportComment cytologyReportComment = new Domain.Cytology.CytologyReportComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyReportComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(cytologyReportComment);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Domain.Cytology.CytologyReportComment GetCytologyReportCommentById(int commentId)
		{
			YellowstonePathology.Business.Domain.Cytology.CytologyReportComment result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment, AbbreviatedComment from tblCytologyReportComment " +
                "where tblCytologyReportComment.CommentId = @CommentId;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@CommentId", commentId);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Domain.Cytology.CytologyReportComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection GetScreeningImpressions()
		{
			YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection result = new Cytology.Model.ScreeningImpressionCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblCytologyScreeningImpression order by ResultCode;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression = new YellowstonePathology.Business.Cytology.Model.ScreeningImpression();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(screeningImpression, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(screeningImpression);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.ScreeningImpression GetScreeningImpressionByResultCode(string resultCode)
		{
			YellowstonePathology.Business.Cytology.Model.ScreeningImpression result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblCytologyScreeningImpression where tblCytologyScreeningImpression.ResultCode = @ResultCode;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@ResultCode", resultCode);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Cytology.Model.ScreeningImpression();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection GetSpecimenAdequacy()
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection result = new Cytology.Model.SpecimenAdequacyCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblSpecimenAdequacy;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenAdequacy, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(specimenAdequacy);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy GetSpecimenAdequacyByResultCode(string resultCode)
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy result = null;
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblSpecimenAdequacy where tblSpecimenAdequacy.ResultCode = @ResultCode;";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.AddWithValue("@ResultCode", resultCode);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection GetSpecimenAdequacyComments()
		{
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection result = new Cytology.Model.SpecimenAdequacyCommentCollection();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT CommentID as CommentId, Comment from tblCytologySAComments order by Comment;";
			cmd.CommandType = System.Data.CommandType.Text;

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment specimenAdequacyComment = new YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenAdequacyComment, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(specimenAdequacyComment);
					}
				}
			}

			return result;
		}

        public static List<Business.MasterAccessionNo> GetMasterAccessionNoList(DateTime accessionDate)
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select MasterAccessionNo from tblAccessionOrder where AccessionDate = '" + accessionDate.ToString() + "';";
            cmd.CommandType = CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledAmendments()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select distinct pso.MasterAccessionNo " +
                "from tblAmendment a " +
                "join tblTestOrderReportDistribution trd on a.ReportNo = trd.ReportNo " +
                "join tblPanelSetOrder pso on trd.ReportNo = pso.ReportNo " +
                "where trd.TimeOfLastDistribution < a.finalTime and trd.ScheduledDistributionTime is null and " +
                "a.finalTime < date_Add(now(), Interval -15 Minute) and a.DistributeOnFinal = 1 ;";

            cmd.CommandType = CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnsetDistributions()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "Select distinct MasterAccessionNo from tblPanelSetOrder pso where final = 1 and distribute = 1 and not exists (Select * from tblTestOrderReportDistribution where reportNo = pso.ReportNo)";
            cmd.CommandText = "Select distinct MasterAccessionNo from tblPanelSetOrder pso where final = 1 and " +
                "pso.finalTime < date_Add(Now(), Interval -15 Minute) and distribute = 1 and not exists " +
                "(Select * from tblTestOrderReportDistribution where ReportNo = pso.ReportNo);";
            cmd.CommandType = CommandType.Text;
            MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString);       
            cn.Open();
            cmd.Connection = cn;
            MySqlDataReader dr = null;

            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                    result.Add(man);
                }
            }
            catch(System.Data.SqlClient.SqlException e)
            {
                if(e.Number == 1205) //1205 is an sql deadlock
                {
                    System.Threading.Thread.Sleep(5000);
                    GetCasesWithUnsetDistributions();
                }
                else
                {
                    throw;
                }
            }   
            finally
            {
                dr.Close();
                cn.Close();
            }         
            
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledDistributions()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select distinct pso.MasterAccessionNo from tblTestOrderReportDistribution tor " +
                "join tblPanelSetOrder pso on tor.ReportNo = pso.ReportNo " +
                "where tor.Distributed = 0 and tor.ScheduledDistributionTime is null and pso.Final = 1 and " +
                "pso.finalTime < date_Add(now(), Interval -15 Minute) and pso.Distribute = 1 and pso.HoldDistribution = 0;";

            cmd.CommandType = CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.MasterAccessionNo> GetCasesWithUnscheduledPublish()
        {
            List<Business.MasterAccessionNo> result = new List<Business.MasterAccessionNo>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select Distinct MasterAccessionNo from tblPanelSetOrder pso where pso.Final = 1 and " +
                "pso.finalTime < date_Add(now(), Interval -15 Minute) and pso.ScheduledPublishTime is null and pso.Published = 0;";

            cmd.CommandType = CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.MasterAccessionNo man = Business.MasterAccessionNo.Parse(dr[0].ToString(), true);
                        result.Add(man);
                    }
                }
            }
            return result;
        }

        public static List<Business.ReportNo> GetNextReportNumbersToPublish()
        {
            List<Business.ReportNo> result = new List<Business.ReportNo>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select Distinct pso.ReportNo from tblPanelSetOrder pso where pso.Final = 1 and " +
                "pso.ScheduledPublishTime <= now() union " +
                "Select pso.* from tblPanelSetOrder pso join tblTestOrderReportDistribution trd on pso.ReportNo = trd.ReportNo " +
                "where pso.Final = 1 and trd.ScheduledDistributionTime <= now() and pso.Distribute = 1;";

            cmd.CommandType = CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.ReportNo rno = new Business.ReportNo(dr[0].ToString());
                        result.Add(rno);
                    }
                }
            }
            return result;
        }
    }
}
