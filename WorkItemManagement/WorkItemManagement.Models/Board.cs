using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.Constants;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Models
{
    public class Board : IBoard
    {
        private string name;
        private ICollection<IActivity> listOfActivity;
        private ICollection<IWorkItem> listOfWorkItems;

        public Board(string name)
        {
            this.Name = name;
            this.listOfWorkItems = new List<IWorkItem>();
            this.listOfActivity = new List<IActivity>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            protected set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ModelConstants.InvalidNullName);
                }
                if (value.Length < 5 || value.Length > 10)
                {
                    throw new ArgumentException(ModelConstants.InvalidLengthOfBoardName);
                }

                this.name = value;
            }
        }

        public ICollection<IActivity> ListOfActivity
        {
            get
            {
                return new List<IActivity>(listOfActivity);
            }
        }

        public ICollection<IWorkItem> ListOfWorkItems
        {
            get
            {
                return new List<IWorkItem>(listOfWorkItems);
            }
        }

        public SerializationInfo ModelsConstants { get; private set; }

        public void AddBoardActivity(IActivity boardActivity)
        {
            if (boardActivity == null)
            {
                throw new ArgumentException(ModelConstants.InvalidActivityHistory);
            }

            this.listOfActivity.Add(boardActivity);
        }

        public void AddBoardWorkItem(IWorkItem workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentException(ModelConstants.InvalidWorkItem);
            }

            this.listOfWorkItems.Add(workItem);
        }

        public string ReturnBoardActivityToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(ModelConstants.ChangesToBoard, this.Name));

            foreach (var activity in ListOfActivity)
            {
                sb.AppendLine($"On {activity.DateOfChange} : {activity.Description}");
            }

            return sb.ToString().Trim();
        }

        public string ReturnBoardWorkItemsToString(string workItemType)
        {
            var sb = new StringBuilder();

            foreach (var workItem in listOfWorkItems)
            {
                if (workItemType.ToLower() == workItem.GetType().Name.ToLower())
                {
                    sb.AppendLine(workItem.ToString());
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string ReturnBoardWorkItemsToString()
        {
            var sb = new StringBuilder();

            foreach (var workItem in listOfWorkItems)
            {
                sb.AppendLine(workItem.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string ReturnBoardWorkItemsByStatusToString(string workItemStatus)
        {
            var sb = new StringBuilder();

            foreach (var workItem in this.listOfWorkItems)

                if (workItem.GetStatus().ToString() == workItemStatus)
                {

                    sb.AppendLine(workItem.ToString());
                }

            return sb.ToString().TrimEnd();
        }

    }

}

