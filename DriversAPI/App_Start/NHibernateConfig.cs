using DriversAPI.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NHibernate.AdoNet;
using NHibernate.AspNet.Identity.Helpers;
using NLog;
using QS.Banks.Domain;
using QS.HistoryLog;
using QS.Project.DB;
using QSOrmProject;
using QSProjectsLib;
using System;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Organizations;
using Vodovoz.NhibernateExtensions;
using Vodovoz.Tools;

namespace DriversAPI
{
    /// <summary>
    /// Настройка Nhibernate
    /// </summary>
    public class NhibernateConfig
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string configFile = "/etc/vodovoz-driver-api.conf";

        public static void CreateBaseConfig()
        {
            string mysqlServerHostName;
            uint mysqlServerPort;
            string mysqlUser;
            string mysqlPassword;
            string mysqlDatabase;

            try
            {
                var builder = new ConfigurationBuilder()
                    .AddIniFile(configFile, optional: false);

                var configuration = builder.Build();

                var mysqlSection = configuration.GetSection("Mysql");
                mysqlServerHostName = mysqlSection["host_name"];
                mysqlServerPort = uint.Parse(mysqlSection["port"]);
                mysqlUser = mysqlSection["user"];
                mysqlPassword = mysqlSection["password"];
                mysqlDatabase = mysqlSection["database"];
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Ошибка чтения конфигурационного файла.");
                return;
            }

            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();

            mySqlConnectionStringBuilder.Server = mysqlServerHostName;
            mySqlConnectionStringBuilder.Port = mysqlServerPort;
            mySqlConnectionStringBuilder.Database = mysqlDatabase;
            mySqlConnectionStringBuilder.UserID = mysqlUser;
            mySqlConnectionStringBuilder.Password = mysqlPassword;
            mySqlConnectionStringBuilder.SslMode = MySqlSslMode.None;

            QSMain.ConnectionString = mySqlConnectionStringBuilder.GetConnectionString(true);

            //Увеличиваем таймаут
            QSMain.ConnectionString += ";ConnectionTimeout=120";

            var db_config = FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
                .Dialect<MySQL57SpatialExtendedDialect>()
                .ConnectionString(QSMain.ConnectionString)
                .AdoNetBatchSize(100)
                .Driver<LoggedMySqlClientDriver>();

            var myEntities = new[] {
                typeof(ApplicationUser)
            };

            // Настройка ORM
            OrmConfig.ConfigureOrm(
                db_config,
                new System.Reflection.Assembly[] {
                    System.Reflection.Assembly.GetAssembly (typeof(QS.Project.HibernateMapping.UserBaseMap)),
                    System.Reflection.Assembly.GetAssembly (typeof(Vodovoz.HibernateMapping.OrganizationMap)),
                    System.Reflection.Assembly.GetAssembly (typeof(Bank)),
                    System.Reflection.Assembly.GetAssembly (typeof(HistoryMain)),
                },
                (cnf) =>
                {
                    cnf.DataBaseIntegration(
                        dbi =>
                        {
                            dbi.BatchSize = 100;
                            dbi.Batcher<MySqlClientBatchingBatcherFactory>();
                        }
                    );
                    cnf.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);
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