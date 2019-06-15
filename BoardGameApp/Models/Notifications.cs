using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class Notifications
    {
        public const string MAX_LEN = "Maksymalna długość {0} to {1}";
        public const string REQ = "Należy wypełnić pole {0}";
        public const string NUMERIC = "{0} musi być cyfrą";
        public const string MESSAGE_SUCCESS = "messageSuccess";
        public const string MESSAGE_ALERT = "messageAlert";
        public const string MESSAGE_INFO = "messageInfo";
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_USER = "User";
        public const string ROLE_EMPLOYEE = "Employee";
    }
}