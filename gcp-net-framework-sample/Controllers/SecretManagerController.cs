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
            try
            {
                string projectid = Environment.GetEnvironmentVariable("projectid");
                string secretId = Environment.GetEnvironmentVariable("secretId");
                string secretVersionId = Environment.GetEnvironmentVariable("secretVersionId");

                Console.WriteLine($"projectid {projectid}");
                Console.WriteLine($"secretId {secretId}");
                Console.WriteLine($"secretVersionId {secretVersionId}");

                SecretManagerServiceClient client = SecretManagerServiceClient.Create();
                SecretVersionName secretVersionName = new SecretVersionName(projectid, secretId, secretVersionId);
                AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);
                return Content(result.Payload.Data.ToStringUtf8());
            }
            catch (Exception ex)
            {
                return Content(ex.StackTrace);
            }
        }
    }
}