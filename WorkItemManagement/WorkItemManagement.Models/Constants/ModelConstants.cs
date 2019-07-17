using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Models.Constants
{
    public static class ModelConstants
    {
        public const string InvalidNullStatus = "Status cannot be null or empty!";

        public const string InvalidNullSize = "Size cannot be null or empty!";

        public const string InvalidActivityHistory = "Activity hystory can not be null.";

        public const string InvalidWorkItem = "Work item cannot be null.";

        public const string InvalidNullName = "Name cannot be null or empty string.";

        public const string InvalidLengthOfBoardName = "The board's name should be between 5 and 10 symbols.";

        public const string ChangesToBoard = "To board with name \"{0}\" were made the following changes:";

        public const string InvalidNullMessage = "Message cannot be null or empty string.";

        public const string InvalidLengthOfMessage = "Message should be at least 3 symbols.";

        public const string InvalidLenghtOfMemberName = "Name should be between 5 and 15 symbols.";

        public const string InvalidNullWorkItem = "Work item cannot be null";

        public const string InvalidNullMember = "Member cannot be null";

        public const string InvalidNullBoard = "Board cannot be null";

    }
}
