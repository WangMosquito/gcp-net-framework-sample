using gcp_net_framework_sample.Models;
using Google.Cloud.SecretManager.V1;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gcp_net_framework_sample.Controllers
{
    public class SecretManagerController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View(new SecretManagerViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(SecretManagerViewModel model)
        {
            model.SecretVersionID = string.IsNullOrWhiteSpace(model.SecretVersionID) ? "latest" : model.SecretVersionID;
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            SecretVersionName secretVersionName = new SecretVersionName(model.ProjectID, model.SecretID, model.SecretVersionID);
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);
            model.Result = result.Payload.Data.ToStringUtf8();
            return View(model);
        }
    }
}