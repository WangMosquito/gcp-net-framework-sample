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
        private string _bucketName;
        private string _bucketObjectName;

        public class Form
        {
            public string Content { get; set; } = "";
        }

        public CloudStorageController()
        {
            _storage = StorageClient.Create();
            _bucketName = Environment.GetEnvironmentVariable("bucketName");
            _bucketObjectName = Environment.GetEnvironmentVariable("bucketobjectName");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = new CloudStorageViewModel();

            try
            {

                //await _storage.DeleteObjectAsync(
                //     _configuration["bucketName"], _configuration["objectName"]);

                // Get the storage object.
                var storageObject =
                    await _storage.GetObjectAsync(_bucketName, _bucketObjectName);

                // Download the storage object.
                MemoryStream m = new MemoryStream();
                await _storage.DownloadObjectAsync(
                     _bucketName, _bucketObjectName, m);
                m.Seek(0, SeekOrigin.Begin);
                byte[] content = new byte[m.Length];
                m.Read(content, 0, content.Length);
                model.Content = Encoding.UTF8.GetString(content);
            }
            catch (GoogleApiException e)
            when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Does not exist yet.  No problem.
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(Form sendForm)
        {
            var model = new CloudStorageViewModel();
            // Take the content uploaded in the form and upload it to
            // Google Cloud Storage.

            await _storage.UploadObjectAsync(
                 _bucketName, _bucketObjectName, "text/plain",
                new MemoryStream(Encoding.UTF8.GetBytes(sendForm.Content)));


            model.Content = sendForm.Content;
            model.SavedNewContent = true;

            return RedirectToAction("index");
        }
    }
}