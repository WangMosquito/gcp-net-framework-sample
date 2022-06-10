using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcp_net_framework_sample.Models
{
    public class SecretManagerViewModel
    {
        public string ProjectID { get; set; }
        public string SecretID { get; set; }
        public string SecretVersionID { get; set; }
        public string Result { get; set; }
    }
}