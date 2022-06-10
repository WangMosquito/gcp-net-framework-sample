using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcp_net_framework_sample.Models
{
    public class CloudSqlViewModel
    {
        public string ProjectID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; }

        public string DataBase { get;set; }
        public string Result { get; set; }
    }
}