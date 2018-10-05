﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class PreviousResultGateway
    {
        private Dictionary<string, string> m_TableDictionary;        

        public PreviousResultGateway()
        {
            this.m_TableDictionary = new Dictionary<string, string>();

            string jak2Exon1214SQL = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblJAK2Exon1214TestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblJAK2Exon1214TestOrder", jak2Exon1214SQL);

            string bcrablByFishTestOrder = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblBCRABLByFishTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblBCRABLByFishTestOrder", bcrablByFishTestOrder);

            string bcrablByPCRTestOrder = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblBCRABLByPCRTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblBCRABLByPCRTestOrder", bcrablByPCRTestOrder);

            string calreticulinMutation = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblCalreticulinMutationAnalysisTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblCalreticulinMutationAnalysisTestOrder", calreticulinMutation);

            string mpl = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderMPL b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblPanelSetOrderMPL", mpl);

            string jak2V617FTestOrder = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblJAK2V617FTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblJAK2V617FTestOrder", jak2V617FTestOrder);

            string brafMutationAnalysisTestOrder = "Select concat(b.Result, ' - ', b.indication) `Result`, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblBRAFMutationAnalysisTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblBRAFMutationAnalysisTestOrder", brafMutationAnalysisTestOrder);

            string psoMPNStandardReflex = "Select concat('V617: ', b.JAK2V617FResult, ', Exon 1214: ', case when b.JAK2Exon1214Result is null then '' else b.JAK2Exon1214Result end) `Result`, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderMPNStandardReflex b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblPanelSetOrderMPNStandardReflex", psoMPNStandardReflex);

            string psoMPNExtendedReflex = "Select concat('V617: ', b.JAK2V617FResult, ', CalR: ', b.CalreticulinMutationAnalysisResult, ', MPL: ', b.MPLResult) `Result`, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderMPNExtendedReflex b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblPanelSetOrderMPNExtendedReflex", psoMPNExtendedReflex);

            string chromosomeAnalysisTestOrder = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblChromosomeAnalysisTestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblChromosomeAnalysisTestOrder", chromosomeAnalysisTestOrder);

            string pdl122C3TestOrder = "Select b.Result, pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate, pso.FinalDate,  pso.PanelSetId " +
                "FROM tblAccessionOrder a " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPDL122C3TestOrder b on pso.ReportNo = b.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.PanelSetId  =  @PanelSetId " +
                "and pso.OrderDate between @StartDate and @EndDate " +
                "and pso.final = 1 order by pso.FinalDate desc;";
            this.m_TableDictionary.Add("tblPDL122C3TestOrder", pdl122C3TestOrder);
        }

        public YellowstonePathology.Business.PreviousResultCollection GetPreviousResultsByTestFinal(int panelSetId, DateTime startDate, DateTime endDate, string tableName)
        {
            PreviousResultCollection result = new PreviousResultCollection();
                        
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = this.m_TableDictionary[tableName];
            cmd.CommandText = cmd.CommandText.Replace("table_name", tableName);
            cmd.Parameters.AddWithValue("@PanelSetId", panelSetId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PreviousResult previousResult = new PreviousResult();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(previousResult, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(previousResult);
                    }
                }
            }
            return result;
        }
    }
}
