using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Bagge.Seti.WebSite
{
	public partial class UpgradeSql : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void _upload_Click(object sender, EventArgs e)
		{
			if (_file.HasFile)
			{
				using (var reader = new StreamReader(_file.FileContent))
				{
					string sql = reader.ReadToEnd();
					ExecuteSql(sql);
					_message.Text = "Comando ejecutado correctamente";
				}
			}
		}

		private static void ExecuteSql(string sql)
		{
			using (var cn = System.Data.Common.DbProviderFactories.GetFactory("Default").CreateConnection())
			{
				cn.Open();
				using (var cmd = cn.CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.Text;
					cmd.CommandText = sql;
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
