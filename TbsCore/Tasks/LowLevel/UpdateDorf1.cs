﻿using System.Threading.Tasks;
using TravBotSharp.Files.Helpers;
using TravBotSharp.Files.Models.AccModels;

namespace TravBotSharp.Files.Tasks.LowLevel
{
    public class UpdateDorf1 : BotTask
    {
        public override async Task<TaskRes> Execute(Account acc)
        {
            TaskExecutor.RemoveSameTasksForVillage(acc, Vill, this.GetType(), this);

            await acc.Wb.Navigate($"{acc.AccInfo.ServerUrl}/dorf1.php");
            return TaskRes.Executed;
        }
    }
}
