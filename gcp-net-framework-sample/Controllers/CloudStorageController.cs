using gcp_net_framework_sample.Models;
using Google;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gcp_net_framework_sample.Controllers
{
    public class CloudStorageController : Controller
    { 
        private readonly StorageClient _storage;

        public CloudStorageController()
        {
            _storage = StorageClient.Create();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = new CloudStorageViewModel();

            try
            {
                model.BucketName = Request["BucketName"];
                model.BucketobjectName = Request["BucketobjectName"];

                //await _storage.DeleteObjectAsync(
                //     _configuration["bucketName"], _configuration["objectName"]);
                if (string.IsNullOrWhiteSpace(model.BucketName) == false &&
                    string.IsNullOrWhiteSpace(model.BucketobjectName) == false)
                {
                    // Get the storage object.
                    var storageObject =
                        await _storage.GetObjectAsync(model.BucketName, model.BucketobjectName);

                    // Download the storage object.
                    MemoryStream m = new MemoryStream();
                    await _storage.DownloadObjectAsync(
                         model.BucketName, model.BucketobjectName, m);
                    m.Seek(0, SeekOrigin.Begin);
                    byte[] content = new byte[m.Length];
                    m.Read(content, 0, content.Length);
                    model.Content = Encoding.UTF8.GetString(content);
                }
            }
            catch (GoogleApiException e)
            when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Does not exist yet.  No problem.
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CloudStorageViewModel model)
        {
            // Take the content uploaded in the form and upload it to
            // Google Cloud Storage.

            await _storage.UploadObjectAsync(
                 model.BucketName, model.BucketobjectName, "text/plain",
                new MemoryStream(Encoding.UTF8.GetBytes(model.Content)));


            model.Content = model.Content;
            model.SavedNewContent = true;

            return RedirectToAction("index");
        }
    }
}