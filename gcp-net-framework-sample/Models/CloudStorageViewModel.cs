using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcp_net_framework_sample.Models
{
    public class CloudStorageViewModel
    {
        public bool MissingBucketName { get; set; } = false;
        public string Content { get; set; } = "";
        public bool SavedNewContent { get; set; } = false;
        public string MediaLink { get; set; } = "";
    }
}