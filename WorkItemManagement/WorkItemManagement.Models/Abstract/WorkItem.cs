using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Models.Abstract
{
    public abstract class WorkItem : IWorkItem
    {
        private string title;
        private string description;
        protected ICollection<string> listOfChanges;
        protected ICollection<IComment> listOfComments;

        public WorkItem(string title, string description)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Description = description;
            this.listOfChanges = new List<string>();
            this.listOfComments = new List<IComment>();
        }

        public Guid Id { get; protected set; }

        public string Title
        {
            get
            {
                return this.title;
            }
            protected set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Title cannot be null or empty string.");
                }
                if (value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentException("Title must be a string between 10 and 50 symbols.");
                }

                this.title = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            protected set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Description cannot be null or empty string");
                }
                if (value.Length < 10 || value.Length > 500)
                {
                    throw new ArgumentException("Description should be a string between 10 and 500 symbols.");
                }

                this.description = value;
            }
        }

        public ICollection<string> ListOfChanges
        {
            get
            {
                return this.listOfChanges;
            }

        }

        public ICollection<IComment> ListOfComments
        {
            get
            {
                return new List<IComment>(this.listOfComments);
            }
        }

        public void AddComment(IComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("Comment cannot be null");
            }
            this.listOfComments.Add(comment); 
        }

        public abstract void ChangeStatus(string newStatus);

        public abstract Enum GetStatus();

        //public abstract Enum ChangeStatus();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("-------------------------------------");
            sb.AppendLine($"Work item type : \"{this.GetType().Name}\"");
            sb.AppendLine($" Id: \"{this.Id}\"");
            sb.AppendLine($" Title: \"{this.Title}\"");
            sb.AppendLine($" Description:\" {this.Description}\"");

            return sb.ToString().TrimEnd();
        }
    }
}
