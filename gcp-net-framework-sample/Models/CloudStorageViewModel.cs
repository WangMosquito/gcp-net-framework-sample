using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcp_net_framework_sample.Models
{
    public class CloudStorageViewModel
    {
        public string BucketName { get; set; }
        public string BucketobjectName { get; set; }
        public bool MissingBucketName { get; set; } = false;
        public string Content { get; set; }
        public bool SavedNewContent { get; set; } = false;
    }
}