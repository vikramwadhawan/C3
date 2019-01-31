using Colligo.O365Product.LoggerService;
using Colligo.O365Product.RM.SPContentWebJob.BLL;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Colligo.O365Product.RM.SPContentWebJob
{
    class Program
    {
        private static readonly ILogManager _logger = new ApplicationLog();
        private static SpContentJobManager jobManager = new SpContentJobManager(SpContentJobConstant.AdminApiUrl, _logger);
        private static SPContentManager spManager = new SPContentManager(SpContentJobConstant.SpApiUrl, _logger);
        static Timer eventSettingProcesser = new Timer();

        static void Main(string[] args)
        {

            try
            {
                log4net.Config.XmlConfigurator.Configure();
                SetupSpContentJob();
#if !DEBUG

          //  host.RunAndBlock();
#else
                while (true)
                {
                    //  GetJobsFromQueue();
                }
#endif
            }
            catch (Exception ex)
            {
                _logger.Error("SpContentJob startup Error : ", ex);
            }
        }

        private static void SetupSpContentJob()
        {
#if DEBUG
            eventSettingProcesser.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
#else
             eventSettingProcesser.Interval = TimeSpan.FromMinutes(Convert.ToDouble(SpContentJobConstant.SpContentIntervalInMinute)).TotalMilliseconds;
#endif                        
            eventSettingProcesser.Elapsed += OnEventSettingProcesserElapsed;
            eventSettingProcesser.Start();
            OnEventSettingProcesserElapsed(null, null);
        }
        private static async void OnEventSettingProcesserElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Info("SetupSpContentJob Activity start at " + DateTime.Now.ToString());
            jobManager.SetAuthorization();
            SpContentJobHandler.SetProperties(_logger, jobManager, spManager);
            await SpContentJobHandler.HandleSpContentJob();
        }
    }
}
