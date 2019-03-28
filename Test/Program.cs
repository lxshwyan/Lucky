using Lucky.Core.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Lucky.Core.Cache;
using Lucky.Core.Model;
using Lucky.Core.CachingQueue;
using Lucky.Core.Extention;
using Lucky.Core.Common;
using Lucky.Core.Quartz;
using Lucky.Core.RedisClient;
using System.Threading;
using StackExchange.Redis;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Lucky.Core.Plugins;
using Lucky.Core.MEF;
using Lucky.Core.MEF.PaymentPlugins;
using Lucky.Core.FileOperation;
using Lucky.Core.DapperHelper;
using Lucky.Core.CodeGenerator;
using System.Transactions;
using Dapper;
using Lucky.Core.Config;
using System.Data;
using Lucky.Core.Excel;

namespace Test
{
    public class Program
    {   
        static void Main(string[] args)
        {
            LoggerManager log = LoggerManager.Instance;

          
            #region LazyTest

           // LazyTest();
            #endregion

            #region 代码生成器
               CodeGenerator();
            #endregion

            #region 各种测试调用
            // _redis = new RedisStackExchangeHelper();
            //  JobTest.TestQuarz();
            // Subscribe();
            //  new Program().CreateDomainTest(); 

            //var cachManger = CacheManager.Instance;
            //string userName = "李小双";
            //cachManger.SetCache("name", userName);

            //var queueManager = QueueManager.Instance;
            //queueManager.Push("name", userName.SerializeToBin());


            //log.Debug(cachManger.GetCache("name").ToString());
            //log.Debug(queueManager.Pop("name").DeserializeToBin<string>());

            // TestRedisQueueManager();
            #endregion

            #region MEF测试  
            // log.LoggerTimer("MEFPlugin 测试时间", () => { new MEFTest().Test(); });
            #endregion

            #region  测试 Plinq

            // PlinqTest();
            #endregion


            #region Excel

           // ExcelTest();
            #endregion

            Console.Read();
        }

        #region 测试Redis队列
        private static RedisStackExchangeHelper _redis;
        /// <summary>
        /// 测试Redis队列
        /// </summary>
        static void TestRedisQueueManager()
        {
            // string input = "";
            //while (true)
            //{
            //    input = Console.ReadLine();
            //    RedisQueueManager.Push("lxsh", input);
            //}

            RedisQueueManager.DoQueue<string>(str => {
                Console.WriteLine(str);
            }, "lxsh");
        }

        static void Subscribe()
        {
            Console.WriteLine("请输入发布订阅类型?1:发布；2：订阅");
            var type = Console.ReadLine();
            if (type == "1")
            {
                Pub();
            }
            else if (type == "2")
            {
                Sub();
            }
            Console.ReadLine();
        }
        static async Task Pub()
        {
            Console.WriteLine("请输入要发布向哪个通道？");
            var channelKey = Console.ReadLine();
            string input = "测试Reddis发布订阅消息";
            Console.WriteLine("正在发送消息");
            while (true)
            {
                //  input = Console.ReadLine();
                _redis.Publish(channelKey, input);
                _redis.Publish("gb", "gb");
                Thread.Sleep(1000);
            }
            //await Task.Delay(10);
            //for (int i = 0; i < 10; i++)
            //{
            //    await _redis.PublishAsync(channel, i.ToString());
            //}  
        }

        static async Task Sub()
        {


            Console.WriteLine("请输入您要订阅哪个通道的信息？");
            var channelKey = Console.ReadLine();
            await _redis.SubscribeAsync(channelKey, (channel, message) =>
            {
                Console.WriteLine($"{DateTime.Now.ToString()}从 {channel} 接收到发布的内容为：" + message);
            });
            await _redis.SubscribeAsync("gb", (channel, message) =>
            {
                Console.WriteLine($"{DateTime.Now.ToString()}从 {channel} 接收到发布的内容为：" + message);
            });
            Console.WriteLine("您订阅的通道为：<< " + channelKey + " >> ! 请耐心等待消息的到来！！");
        }
        static void Tran()
        {
            var tran = _redis.CreateTransaction();

            tran.StringSetAsync("test1", "xiaopotian");
            tran.StringSetAsync("test2", "xiaopangu");
            var commit = tran.ExecuteAsync();
            Console.WriteLine(commit);
        }
        static void Lock()
        {
            Console.WriteLine("Start..........");
            var db = _redis.GetDatabase();
            RedisValue token = Environment.MachineName;
            //实际项目秒杀此处可换成商品ID
            if (db.LockTake("test", token, TimeSpan.FromSeconds(10)))
            {
                try
                {
                    Console.WriteLine("Working..........");
                    Thread.Sleep(5000);
                }
                finally
                {
                    db.LockRelease("test", token);
                }
            }

            Console.WriteLine("Over..........");
        }
        #endregion     

        #region  RuntimeTextTemplateTest
        static string RuntimeTextTemplateTest()
        {
            RuntimeTextTemplate1 t = new RuntimeTextTemplate1();
            return t.TransformText();

        }
        #endregion

        #region 测试Dapper 自动生成代码

        static void CodeGenerator()
        {
            // var conection = ConnectionFactory.CreateConnection(DatabaseType.SqlServer, "");
            // var tables = conection.GetCurrentDatabaseTableList(DatabaseType.SqlServer);

            CodeGenerator codeGenerator = new CodeGenerator(new CodeGenerateOption()
            {
               ConnectionString = "Data Source='127.0.0.1';Initial Catalog='Lxsh.Project.DB';User ID='sa';Password='123456'",
               DbType = "1",//数据库类型是SqlServer,其他数据类型参照枚举DatabaseType
               Author = "lxsh",//作者名称
               OutputPath = "C:\\LuckyCoreCodeGenerator",//模板代码生成的路径
               ModelsNamespace = "Lucky.Team.Models",//实体命名空间
               IRepositoryNamespace = "Lucky.Team.IRepository",//仓储接口命名空间
               RepositoryNamespace = "Lucky.Team.Repository.SqlServer",//仓储命名空间
               IServicesNamespace = "Lucky.Team.IServices",//服务接口命名空间
               ServicesNamespace = "Lucky.Team.Services",//服务命名空间 
             //  DataBase = "Lxsh.Project.DB"//数据库名称
             });
            codeGenerator.GenerateTemplateCodesFromDatabase();

        }

        #endregion

        #region    AppDomainc 测试
        private List<AppDomain> appDomains = new List<AppDomain>();
        public void CreateDomainTest()
        {
            //if (e.ChangeType == WatcherChangeTypes.Deleted)
            //{
            //    var appDomain = appDomains.SingleOrDefault(o => o.FriendlyName == e.Name);
            //    AppDomain.Unload(appDomain);
            //    appDomains.Remove(appDomain);
            //}

            //if (e.ChangeType == WatcherChangeTypes.Created)
            //{
            //    var appDomain = AppDomain.CreateDomain(e.Name);
            //    appDomain.Load(e.FullPath);
            //    appDomains.Add(appDomain);
            //}
        }
        #endregion
                
        #region FileWatcher测试  
        /// <summary>
        ///  FileWatcher测试
        /// </summary>
        static void FileWatchTest()
        {
            FileWatcher fileWatcher = new FileWatcher();    
            fileWatcher.WatcherStrat("c:\\test", "*.txt", true, true);
            fileWatcher.fileSystemEventHandler += (object sender, FileSystemEventArgs e) =>
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Deleted:
                        Console.WriteLine("文件删除事件处理逻辑{0}  {1}   {2}", e.ChangeType, e.FullPath, e.Name); 
                        break;
                    case WatcherChangeTypes.Created:
                        Console.WriteLine("文件新建事件处理逻辑 {0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
                        break;
                    case WatcherChangeTypes.Changed:
                        Console.WriteLine("文件改变事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
                        break;
                }
            };
            fileWatcher.renamedEventHandler += (object sender, RenamedEventArgs e) =>
            {
                Console.WriteLine("文件重命名事件处理逻辑{0}  {1}  {2} {3}", e.ChangeType, e.FullPath, e.OldName, e.Name);
            };
        }
        #endregion

        #region Plinq

        static void PlinqTest()
        {

            List<string> dates = new List<string>();
            Random rd = new Random();
            string[] s1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//字符列表
            Parallel.For(1000000000,0 , i => { dates.Add(s1[rd.Next(0, s1.Length)]); });
           
            LoggerManager log = LoggerManager.Instance;
            log.LoggerTimer("link查询", () =>
            {
                var date = dates.Where(d => d.Contains("l") || d.Contains("L") || d.Contains("R") || d.Contains("M") || d.Contains("N"));
            });
            log.LoggerTimer("Plink查询", () =>
            {
                var date = dates.AsParallel().Where(d => d.Contains("l") || d.Contains("L") || d.Contains("R") || d.Contains("M") || d.Contains("N"));
            });
        }

        #endregion

        #region 自定义配置信息

        static void CustomerConfigTest()
        {
            Console.WriteLine(CustomerConfig.GetConfig().RedisCacheConfig.ConnectionString);
        }

        #endregion

        #region Lazy 延迟加载(线程安全)
        static void LazyTest()
        {

            Lazy<Student> student= new Lazy<Student>();
            Console.WriteLine(student.IsValueCreated);
            student.Value.Name = "lxsh";
            Console.WriteLine(student.IsValueCreated);
        }

        #endregion

        #region Excel

        static void ExcelTest()
        {    
            var conection = ConnectionFactory.CreateConnection(DatabaseType.SqlServer, "");
            DataTable dataBase_SysLog=   conection.Query<Base_SysLog>("select * from Base_SysLog").ToList().ToDataTable();
            LoggerManager log = LoggerManager.Instance;
            log.LoggerTimer($"Excel导出,一共导出{dataBase_SysLog.Rows.Count}", () =>
            {
                ExcelHelper.DataTableToExcel(dataBase_SysLog,@"E:\test\log.xls", "log", true); 
            });
        
            Console.WriteLine("数据导出成功");

        }

        #endregion
    }
}
public class Student
{
    public Student()
    {
        this.Name = "DefaultName";
        this.Age = 0;
        Console.WriteLine("Student is init...");
    }

    public string Name { get; set; }
    public int Age { get; set; }
}
