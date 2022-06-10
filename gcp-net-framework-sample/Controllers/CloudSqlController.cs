using Dapper;
using gcp_net_framework_sample.Models;
using Google.Cloud.SecretManager.V1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gcp_net_framework_sample.Controllers
{
    public class CloudSqlController : Controller
    {
        // GET: CloudSql
        public ActionResult Index()
        {
            var model = new CloudSqlViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CloudSqlViewModel model)
        {

            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            SecretVersionName secretVersionName = new SecretVersionName(model.ProjectID, model.UserName, "latest");
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);
            var userName = result.Payload.Data.ToStringUtf8();

            secretVersionName = new SecretVersionName(model.ProjectID, model.Password, "latest");
            result = client.AccessSecretVersion(secretVersionName);
            var pwd = result.Payload.Data.ToStringUtf8();
            pwd = Encoding.UTF8.GetString(Convert.FromBase64String(pwd));
            string sqlconstring = $"User ID={userName};Password={pwd};Initial Catalog={model.DataBase};Server={model.ServerName}";
            using (var conn = new SqlConnection(sqlconstring))
            {
                string sql = "select 'server name: ' + @@SERVERNAME + ', vsersion: ' + @@VERSION";
                model.Result = (string)conn.ExecuteScalar(sql);
            }

            return View(model);
        }
    }
}