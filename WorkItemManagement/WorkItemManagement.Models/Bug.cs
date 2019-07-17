using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.Abstract;
using WorkItemManagement.Models.Constants;

namespace WorkItemManagement.Models.WorkItems
{
    public class Bug : Task, IBug, ITask, IWorkItem
    {
        private readonly ICollection<string> stepsToReproduce;

        public Bug(string title, string description, PriorityType priorityType,
                    BugSeverityType bugSeverityType, IMember assignee, ICollection<string> stepToReproduce)
                  : base(title, description, priorityType, assignee)
        {
            this.PriorityType = priorityType;
            this.SeverityType = bugSeverityType;
            this.StatusType = BugStatusType.Active;
            this.stepsToReproduce = new List<string>();
        }

        public BugSeverityType SeverityType { get; private set; }

        public BugStatusType StatusType { get;  set ; }

        public ICollection<string> StepsToReproduce
        {
            get
            {
                return new List<string>(this.stepsToReproduce);
            }
        }

        public void ChangeSeverity(string newSeverity)
        {
            if (newSeverity == null)
            {
                throw new ArgumentNullException(ModelConstants.InvalidNullStatus);
            }

            else
            {
                BugSeverityType severityEnum = Enum.Parse<BugSeverityType>(newSeverity);
                this.SeverityType = severityEnum;
            }
        }

        public override void ChangeStatus(string newStatus)
        {
            if (newStatus == null)
            {
                throw new ArgumentNullException(ModelConstants.InvalidNullStatus);
            }

            else
            {
                BugStatusType statusEnum = Enum.Parse<BugStatusType>(newStatus);
                this.StatusType = statusEnum;
            }
        }

        public override Enum GetStatus()
        {
            return this.StatusType;
        }

        public override Enum GetPriority()
        {
            return this.PriorityType;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Step to reproduce: {string.Join(" ", stepsToReproduce)}");
            sb.AppendLine($" Severity: \"{this.SeverityType}\"");
            sb.AppendLine($" Status: \"{this.StatusType}\"");
            sb.AppendLine($" Assignee: \"{this.Assignee.Name}\"");

            if (ListOfComments.Count > 0)
            {
                foreach (var comment in listOfComments)
                {
                    sb.AppendLine($" Comment author: \"{comment.Author.Name}\"");
                    sb.AppendLine($" Message: \"{comment.Message}\"");
                }
            }
           
            return sb.ToString().Trim();
        }

    }
}
