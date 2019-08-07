using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderViewList : ObservableCollection<TaskOrderView>
    {
        public TaskOrderViewList(TaskOrderCollection taskOrderCollection)
        {
            foreach(TaskOrder taskOrder in taskOrderCollection)
            {
                this.Add(new Model.TaskOrderView(taskOrder));
            }
        }
    }
}
