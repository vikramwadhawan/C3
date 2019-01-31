using Colligo.O365Product.ADGUpdateWebJob.BLL;
using Colligo.O365Product.LoggerService;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Colligo.O365Product.ADGUpdateWebJob
{
    class Program
    {
        private static readonly ILogManager _logger = new ApplicationLog();
        private static ADGJobManager jobManager = new ADGJobManager(ADGUpdateConstant.AdminApiUrl, _logger);
        static Timer adgProcesser = new Timer();

        static void Main(string[] args)
        {

            try
            {
                log4net.Config.XmlConfigurator.Configure();
                SetupADGProcesser();
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
                _logger.Error("ADG startup Error : ", ex);
            }
        }

        private static void SetupADGProcesser()
        {
#if DEBUG
            adgProcesser.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
#else
             adgProcesser.Interval = TimeSpan.FromMinutes(Convert.ToDouble(ADGUpdateConstant.ADGUpdateIntervalInMinute)).TotalMilliseconds;
#endif

            adgProcesser.Elapsed += OnADGProcesserElapsed;
            adgProcesser.Start();
            OnADGProcesserElapsed(null, null);
        }
        private static async void OnADGProcesserElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Info("HandleADGAgent Activity start at " + DateTime.Now.ToString());
            jobManager.SetAuthorization();
            ADGAgentHandler.SetProperties(_logger, jobManager);
            await ADGAgentHandler.HandleADGAgentActivity();
        }
    }
}
