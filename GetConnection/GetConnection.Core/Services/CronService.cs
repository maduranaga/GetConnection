using GetConnection.Core.Repositories.Users;
using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
    
    public class CronService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IUsersWriteOnlyRepository _usersWriteOnlyRepository;

        private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds

        public CronService(IUsersWriteOnlyRepository usersWriteOnlyRepository)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            _usersWriteOnlyRepository.RemoveOlderToken();
            Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
        }
    }
}
