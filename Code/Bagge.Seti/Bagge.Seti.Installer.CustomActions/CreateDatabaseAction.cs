using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Bagge.Seti.Installer.CustomActions
{
	public class CreateDatabaseAction: CustomAction
	{

		public override void Commit(System.Collections.IDictionary savedState)
		{
			Microsoft.Data.ConnectionUI.DataConnectionDialog dialog = new Microsoft.Data.ConnectionUI.DataConnectionDialog();
			Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(dialog);
			dialog.ShowDialog();

			string connectionString = string.Format("Server={0};Database={1};uid={2};pwd={3}",
				savedState["DATABASESERVER"], savedState["DATABASENAME"],
				savedState["DATABASEUSERNAME"], savedState["DATABASEPASSWORD"]);

			using(var cn = new SqlConnection(connectionString))
			{
				var sc = new ServerConnection(cn);
				sc.ExecuteNonQuery(ContentHelper.GetContent("Bagge.Seti.Installer.CustomActions.Content.DatabaseCreate.sql"));
				sc.ExecuteNonQuery(ContentHelper.GetContent("Bagge.Seti.Installer.CustomActions.Content.DatabaseStartupData.sql"));
			}
		}
	}
}
