using System;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.Abstract;
using WorkItemManagement.Models.Constants;

namespace WorkItemManagement.Models.WorkItems
{
    public class FeedBack : WorkItem, IFeedback
    {
        public FeedBack(string title, string description, FeedbackStatusType feedbackStatusType, int rating)
                       : base(title, description)
        {
            this.Rating = rating;
            this.StatusType = feedbackStatusType;
        }

        public int Rating { get; set; }
        
        public FeedbackStatusType StatusType { get; private set; }

        public void ChangeRating(int rating)
        {
            this.Rating = rating;
        }

        public override Enum GetStatus()
        {
            return this.StatusType;
        }

        public override void ChangeStatus(string newStatus)
        {
            if (newStatus == null)
            {
                throw new ArgumentNullException(ModelConstants.InvalidNullStatus);
            }

            else
            {
                FeedbackStatusType statusEnum = Enum.Parse<FeedbackStatusType>(newStatus);
                this.StatusType = statusEnum;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Status: \"{this.StatusType}\"");
            sb.AppendLine($" Rating: \"{this.Rating}\"");

            return sb.ToString().TrimEnd();
        }
    }
}
