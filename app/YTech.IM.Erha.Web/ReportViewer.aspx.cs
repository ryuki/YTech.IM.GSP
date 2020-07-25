using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Data.Repository;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace YTech.IM.GSP.Web
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                string rpt = Request.QueryString["rpt"];

                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = Server.MapPath(string.Format("~/Views/Transaction/Report/{0}.rdlc", rpt));

                rv.LocalReport.DataSources.Clear();
                ////get datasorce
                //var ds = Request.Form["dataSource"];
                //string dsName =string.Format("{0}", Request.Form["dataSourceName"]);
                //var datasource = JsonConvert.DeserializeObject<ReportDataSource[]>(ds);
                //Type t = Type.GetType(dsName);
                //var r = Activator.CreateInstance(t);
          
                ////ReportDataSource reportDataSource = new ReportDataSource(dsName, datasource);
                ////rv.LocalReport.DataSources.Add(reportDataSource);
                //ReportDataSource[] repCol = datasource as ReportDataSource[];
                //if (repCol != null)
                //{
                //    foreach (ReportDataSource d in repCol)
                //    {
                //        ReportDataSource newds = new ReportDataSource();
                //        newds.Name = d.Name;
                //        newds.Value = GetList(d.Value.ToString(),dsName);

                //        //new JavaScriptSerializer().Deserialize<IList<MBrand>>(d.Value.ToString());
                //        // JsonConvert.DeserializeAnonymousType(d.Value.ToString(), t);
                //        rv.LocalReport.DataSources.Add(newds);
                //    }
                //}

                ReportDataSource[] repCol = Session["ReportData"] as ReportDataSource[];
                if (repCol != null)
                {
                    foreach (ReportDataSource d in repCol)
                    {
                        rv.LocalReport.DataSources.Add(d);
                    }
                }

                //ReportDataSource d = Session["ReportData"] as ReportDataSource;// new ReportDataSource();
                //d.Name = Session["ReportDataName"].ToString();
                //d.Value = Session["ReportData"] as IEnumerable;

                rv.LocalReport.Refresh();
            }
        }

        private object GetList(string datasource, string dsName)
        {
            Type t = Type.GetType(dsName);
            //if (dsName.Equals(typeof(MBrand).AssemblyQualifiedName))
            {
                return JsonConvert.DeserializeObject(datasource, t);
            }
            return null;
        }
    }
}