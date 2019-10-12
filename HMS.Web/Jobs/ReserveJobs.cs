using HMS.Service.Core;
using HMS.Service.Persistance;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HMS.Model.Core.DomainModels;

namespace HMS.Web.Jobs
{
    public class ReserveJobs : IJob
    {
        private IUnitOfWork uow;

        public ReserveJobs()
        {
            uow = new UnitOfWork(new Models.ApplicationDbContext());
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            Guid reserveId = Guid.Parse(dataMap.GetString("reserveId"));

            Reserve reserve = uow.Reserve.Find(reserveId);

            if (reserve.Status == Status.Temporary)
            {
                uow.Reserve.Remove(reserve);
                uow.Complete();
            }
        }

        public static async void Start(Guid reserveId, DateTime date)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ReserveJobs>()
                .UsingJobData("reserveId", reserveId.ToString())
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerOrderMinus", $"Job_{reserveId.ToString()}")
                .StartAt(date)
                .Build();

            bool flag = await scheduler.CheckExists(trigger.Key);
            if (flag)
            {
                await scheduler.RescheduleJob(trigger.Key, trigger);
            }
            else
            {
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}