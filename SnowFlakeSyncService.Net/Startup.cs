using Geckoboard;
using geckoboardcsharp;
using geckoboardcsharp.Models;
using Geckonet.Core.Serialization;
using Hangfire;
using Hangfire.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;
using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exception = System.Exception;

namespace SnowFlakeSyncService.Net
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app);
            app.Use<GlobalExceptionMiddleware>();
            // Hangfire configuration
            var options = new SQLiteStorageOptions();

            GlobalConfiguration.Configuration.UseSQLiteStorage("SQLiteHangfire", options);
            var option = new BackgroundJobServerOptions { WorkerCount = 1 };
            app.UseHangfireServer(option);
            app.UseHangfireDashboard();

            var boardData = File.ReadAllText("pie.json");
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new DatasetFieldConverter());

            using (JsonReader reader = new JsonTextReader(new StringReader(boardData)))
            {
                var array = JArray.Load(reader);
                foreach (var job in array)
                {
                    string value = job.ToString(Formatting.None);
                    var typedBoard = JsonConvert.DeserializeObject<ConfigurationDataset>(value);
                    RecurringJob.AddOrUpdate(typedBoard.DataSetName, () => Run(typedBoard), Cron.MinuteInterval(typedBoard.PollInterval), TimeZoneInfo.Utc);
                    
                }
            }
        }

        public void Run(ConfigurationDataset configBoard)
        {
            try
            {
                var geckoboardClient = new GeckoboardClient(configBoard.GeckoBoadApiKey);
                var canConnect = geckoboardClient.Ping();

                var client = geckoboardClient.Datasets();
                try
                {
                    client.Delete(configBoard.DataSetName);
                    Console.WriteLine($"Deleted {configBoard.DataSetName} successfully : {DateTime.Now}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var original = Console.BackgroundColor;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(e);
                    Console.BackgroundColor = original;
                }

                var cleanBoard = new GeckoDataset()
                {
                    Fields = configBoard.Fields,
                    UniqueBy = configBoard.UniqueBy,

                };
                try
                {
                    string jsonFields1 = cleanBoard.JSon();
                    var dataset = client.FindOrCreateByJson(configBoard.DataSetName, jsonFields1);
                    Console.WriteLine($"created {configBoard.DataSetName} successfully : {DateTime.Now}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var original = Console.BackgroundColor;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(e);
                    Console.BackgroundColor = original;
                }

                var objupd = new GeckoDataset()
                {
                    Data = new List<Dictionary<string, object>>()
                };

                using (IDbConnection conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = configBoard.SnowflakeConnection;

                    conn.Open();

                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = configBoard.DataStatement;
                    IDataReader reader = cmd.ExecuteReader();
                    var dbObject = new ExpandoObject();
                    var cols = reader.GetSchemaTable().Rows;
                    for (int i = 0; i < cols.Count; i++)
                    {
                        object v = cols[i][0];
                        ((IDictionary<string, object>)dbObject).Add(v.ToString(), v);
                    }
                    List<dynamic> personList = new List<dynamic>();
                    var allSales = reader.DataReaderMapToList(dbObject);
                    conn.Close();

                    foreach (ExpandoObject saleOrder in allSales)
                    {

                        Dictionary<string, object> item = new Dictionary<string, object>() { };

                        foreach (var cleanBoardField in cleanBoard.Fields)
                        {

                            var entry = saleOrder.SingleOrDefault(p => p.Key.ToUpper().Equals(cleanBoardField.Value.Name,
                                  StringComparison.InvariantCultureIgnoreCase));
                            var colValue = entry.Value;
                            var hasNoValue = ReferenceEquals(colValue, null) || colValue == DBNull.Value;
                            if (hasNoValue)
                            {

                                if (cleanBoardField.Value.Type == DatasetFieldType.datetime || cleanBoardField.Value.Type == DatasetFieldType.date)
                                {
                                    if (cleanBoardField.Value.DefaultValue == "now")
                                    {
                                        item.Add(cleanBoardField.Value.Name.ToLower(), DateTime.Now);
                                    }
                                    else
                                    {
                                        item.Add(cleanBoardField.Value.Name.ToLower(), DateTime.Parse(cleanBoardField.Value.DefaultValue));
                                    }
                                }
                                else
                                {
                                    item.Add(cleanBoardField.Value.Name.ToLower(), "");
                                }
                            }
                            else
                            {
                                item.Add(cleanBoardField.Value.Name.ToLower(), colValue);
                            }
                        }
                        objupd.Data.Add(item);
                    }
                }
                // Act
                var jsonFields = objupd.JSon();
                var result = client.UpdateDataset(jsonFields, configBoard.DataSetName);
                Console.WriteLine($"uploaded successfully: {configBoard.DataSetName} on: {DateTime.Now}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var original = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.BackgroundColor = original;
            }
            
        }
    }
}

