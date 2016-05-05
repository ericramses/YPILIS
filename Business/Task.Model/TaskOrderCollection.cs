﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskOrderCollection : ObservableCollection<TaskOrder>
	{
		public const string PREFIXID = "TSKO";

		public TaskOrderCollection()
		{

		}        

		public TaskOrderCollection TaskOrderCollectionForReport(string reportNo)
		{
			TaskOrderCollection result = new TaskOrderCollection();
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.ReportNo == reportNo) result.Add(taskOrder);
			}

			return result;
		}

		public bool Exists(string taskName)
		{
			bool result = false;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskName == taskName)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public TaskOrder GetTaskOrder(string taskOrderId)
		{
			TaskOrder result = null;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskOrderId == taskOrderId)
				{
					result = taskOrder;
					break;
				}
			}
			return result;
		}

        public TaskOrder GetTaskOrderByReportNo(string reportNo)
        {
            TaskOrder result = null;
            foreach (TaskOrder taskOrder in this)
            {
                if (taskOrder.ReportNo == reportNo)
                {
                    result = taskOrder;
                    break;
                }
            }
            return result;
        }

		public TaskOrder GetTaskOrderByName(string taskName)
		{
			TaskOrder result = null;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskName == taskName)
				{
					result = taskOrder;
					break;
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderCytologySlideDisposal(int days)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskCytologySlideDisposal task = new TaskCytologySlideDisposal();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestDailyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(days);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderCytologySlideDisposal taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrderCytologySlideDisposal(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrder);
				actionDate = actionDate.AddDays(1);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, false);			

			actionDate = actionDate.AddDays(-1);
			result.Message = "Daily Task Order Cytology Slide Disposal have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderSurgicalSpecimenDisposal(int days)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskSurgicalSpecimenDisposal task = new TaskSurgicalSpecimenDisposal();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestDailyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(days);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderSurgicalSpecimenDisposal taskOrderSurgical = new YellowstonePathology.Business.Task.Model.TaskOrderSurgicalSpecimenDisposal(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrderSurgical);
				actionDate = actionDate.AddDays(1);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, true);			
			actionDate = actionDate.AddDays(-1);
			result.Message = "Daily Task Order Surgical Specimen Disposal have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderPOCReport(int weeks)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskPOCReport task = new TaskPOCReport();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestWeeklyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();			
			YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(weeks * 7);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderPOCReport taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrderPOCReport(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrder);

				actionDate = actionDate.AddDays(7);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, true);
            actionDate = actionDate.AddDays(-7);
			result.Message = "Daily Task Order POC Report have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}
	}
}
