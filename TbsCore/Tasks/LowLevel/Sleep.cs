﻿using System;
using System.Linq;
using System.Threading.Tasks;
using TravBotSharp.Files.Helpers;
using TravBotSharp.Files.Models.AccModels;

namespace TravBotSharp.Files.Tasks.LowLevel
{
    public class Sleep : BotTask
    {
        public bool AutoSleep { get; set; }
        public int MinSleepSec { get; set; }
        public int MaxSleepSec { get; set; }
        public override async Task<TaskRes> Execute(Account acc)
        {
            if (AutoSleep)
            {
                MinSleepSec = acc.Settings.Time.MinSleep * 60;
                MaxSleepSec = acc.Settings.Time.MaxSleep * 60;
            }
            var rand = new Random();
            int sleepSec = rand.Next(MinSleepSec, MaxSleepSec);
            var sleepEnd = DateTime.Now.AddSeconds(sleepSec);

            var reopenDriver = new ReopenDriver()
            {
                LowestPrio = TaskPriority.High,
                ReopenAt = sleepEnd
            };

            await reopenDriver.Execute(acc);


            if (AutoSleep)
            {
                this.NextExecute = DateTime.Now + TimeHelper.GetWorkTime(acc);
            }

            return TaskRes.Executed;
        }

        /// <summary>
        /// Check if there is a high priority task that has to be executed.
        /// If there is, sleep should stop.
        /// </summary>
        /// <param name="acc">Account</param>
        /// <returns>Whether there are high priority tasks to be executed</returns>
        private bool NoHighPriorityTask(Account acc)
        {
            return !acc.Tasks.Any(x =>
                x.ExecuteAt <= DateTime.Now &&
                x.Priority == TaskPriority.High
            );
        }
    }
}
