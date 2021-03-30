using DriversAPI.App_Start;
using QS.Banks.Domain;
using QS.HistoryLog;
using QS.Project.DB;
using QSProjectsLib;
using System.Web.Http;
using Vodovoz.NhibernateExtensions;
using Vodovoz.Tools;
using NHibernate.AdoNet;
using QSOrmProject;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Organizations;
using Vodovoz.Domain.Employees;
using MySql.Data.MySqlClient;

namespace AutoFacWithWebAPI.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

			CreateBaseConfig();
		}

		static void CreateBaseConfig()
		{
			MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();

			mySqlConnectionStringBuilder.Server = "localhost";
			mySqlConnectionStringBuilder.Port = 3306;
			mySqlConnectionStringBuilder.Database = "vodovoz_dev_local";
			mySqlConnectionStringBuilder.UserID = "root";
			mySqlConnectionStringBuilder.Password = "P@ssw0rd";
			mySqlConnectionStringBuilder.SslMode = MySqlSslMode.None;

			QSMain.ConnectionString = mySqlConnectionStringBuilder.GetConnectionString(true);

			//Увеличиваем таймоут
			QSMain.ConnectionString += ";ConnectionTimeout=120";

			var db_config = FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
				.Dialect<MySQL57SpatialExtendedDialect>()
				.ConnectionString(QSMain.ConnectionString)
				.AdoNetBatchSize(100)
				.Driver<LoggedMySqlClientDriver>();

			// Настройка ORM
			OrmConfig.ConfigureOrm(
				db_config,
				new System.Reflection.Assembly[] {
					System.Reflection.Assembly.GetAssembly (typeof(QS.Project.HibernateMapping.UserBaseMap)),
					System.Reflection.Assembly.GetAssembly (typeof(Vodovoz.HibernateMapping.OrganizationMap)),
					System.Reflection.Assembly.GetAssembly (typeof(Bank)),
					System.Reflection.Assembly.GetAssembly (typeof(HistoryMain)),
				},
				(cnf) => {
					cnf.DataBaseIntegration(
					dbi => {
						dbi.BatchSize = 100;
						dbi.Batcher<MySqlClientBatchingBatcherFactory>();
					}
				);
				}
			);

			HistoryMain.Enable();

			//Настройка ParentReference
			ParentReferenceConfig.AddActions(new ParentReferenceActions<Organization, Account>
			{
				AddNewChild = (o, a) => o.AddAccount(a)
			});
			ParentReferenceConfig.AddActions(new ParentReferenceActions<Counterparty, Account>
			{
				AddNewChild = (c, a) => c.AddAccount(a)
			});
			ParentReferenceConfig.AddActions(new ParentReferenceActions<Employee, Account>
			{
				AddNewChild = (c, a) => c.AddAccount(a)
			});
			ParentReferenceConfig.AddActions(new ParentReferenceActions<Trainee, Account>
			{
				AddNewChild = (c, a) => c.AddAccount(a)
			});
		}
	}
}