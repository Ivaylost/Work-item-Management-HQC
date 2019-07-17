using System;
using WorkItemManagement.Contracts;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Models
{
    public class Activity : IActivity
    {
        private string description;
        private IMember maker;
        private  DateTime dateOfChange;

        public Activity(string description)
        {
            this.Description = description;
            this.DateOfChange = DateTime.Now;
        }

        public Activity(string description, IMember maker)
            :this(description)
        {
            this.Maker = maker;
            this.DateOfChange = DateTime.Now;
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            private set
            {
                this.description = value;
            }
        }
        public IMember Maker
        {
            get
            {
                return this.maker;
            }
            set
            {
                this.maker = value;
            }
        }
        public DateTime DateOfChange
        {
            get
            {
                return this.dateOfChange;
            }

            private set
            {
                this.dateOfChange = value;
            }
        }

    }
}
