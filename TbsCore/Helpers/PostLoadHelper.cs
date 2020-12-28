﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravBotSharp.Files.Helpers;
using TravBotSharp.Files.Models.AccModels;
using TravBotSharp.Files.Parsers;
using TravBotSharp.Files.Tasks.LowLevel;
using static TravBotSharp.Files.Tasks.BotTask;

namespace TbsCore.Helpers
{
    /// <summary>
    /// Helper for PostTask - tasks that execute after a navigation/click
    /// </summary>
    class PostLoadHelper
    {
        /// <summary>
        /// Gets tasks that should be executed after loading a page
        /// </summary>
        /// <param name="acc">Account</param>
        /// <returns>List of tasks</returns>
        public static List<Action> GetPostLoadTasks(Account acc)
        {
            var html = acc.Wb.Html;

            var ran = new Random();

            //Web browser not initialized
            if (!UpdateAccountObject.UpdateVillages(html, acc)) return new List<Action>();
            var activeVill = acc.Villages.FirstOrDefault(x => x.Active);

            return new List<Action>() {
                () => acc.AccInfo.ServerVersion = (acc.Wb.Html.GetElementbyId("sidebarBoxDailyquests") == null ? Classificator.ServerVersionEnum.T4_5 : Classificator.ServerVersionEnum.T4_4),
                () => {
                    if (acc.Wb.CurrentUrl.Contains("dorf1")) TaskExecutor.UpdateDorf1Info(acc);
                    else if (acc.Wb.CurrentUrl.Contains("dorf2")) TaskExecutor.UpdateDorf2Info(acc);
                },
                () => acc.AccInfo.CulturePoints = RightBarParser.GetCulurePoints(html, acc.AccInfo.ServerVersion),
                () => // Village expansion
                {
                    var villExpansionReady = acc.Villages.FirstOrDefault(x => x.Expansion.ExpansionAvailable);
                    if (acc.AccInfo.CulturePoints?.MaxVillages > acc.AccInfo.CulturePoints?.VillageCount &&
                        villExpansionReady != null)
                    {
                        villExpansionReady.Expansion.ExpansionAvailable = false;
                        TaskExecutor.AddTaskIfNotExists(acc, new SendSettlers() { ExecuteAt = DateTime.Now, Vill = villExpansionReady });
                    }
                },
                () => // Beginner Quests
                {
                    acc.Quests.Quests = RightBarParser.GetBeginnerQuests(html, acc.AccInfo.ServerVersion);
                    var claimQuest = acc.Quests?.Quests?.FirstOrDefault(x => x.finished);
                    if (claimQuest != null
                        && acc.Quests.ClaimBeginnerQuests
                        )
                    {
                        TaskExecutor.AddTaskIfNotExists(acc, new ClaimBeginnerTask()
                        {
                            ExecuteAt = DateTime.Now,
                            QuestToClaim = claimQuest,
                            Vill = VillageHelper.VillageFromId(acc, acc.Quests.VillToClaim)
                        });
                    }
                },
                () => // Daily Quest
                {
                    if (acc.AccInfo.ServerVersion == Classificator.ServerVersionEnum.T4_5 &&
                    RightBarParser.CheckDailyQuest(html) &&
                    acc.Quests.ClaimDailyQuests)
                    {
                        TaskExecutor.AddTaskIfNotExists(acc, new ClaimDailyTask()
                        {
                            ExecuteAt = DateTime.Now,
                            Vill = VillageHelper.VillageFromId(acc, acc.Quests.VillToClaim)
                        });
                    }
                },
                () =>
                {
                    var goldSilver = RightBarParser.GetGoldAndSilver(html, acc.AccInfo.ServerVersion);
                    acc.AccInfo.Gold = goldSilver[0];
                    acc.AccInfo.Silver = goldSilver[1];
                },
                () => acc.AccInfo.PlusAccount = RightBarParser.HasPlusAccount(html, acc.AccInfo.ServerVersion),
                () => // Check messages
                {
                    if (MsgParser.UnreadMessages(html, acc.AccInfo.ServerVersion) > 0
                        && !acc.Wb.CurrentUrl.Contains("messages.php")
                        && acc.Settings.AutoReadIgms)
                    {
                        
                        TaskExecutor.AddTaskIfNotExists(acc, new ReadMessage()
                        {
                            ExecuteAt = DateTime.Now.AddSeconds(ran.Next(10, 600)), // Read msg in next 10-600 seconds
                            Priority = TaskPriority.Low
                        });
                    }
                },
                () => {
                    activeVill.Res.FreeCrop = RightBarParser.GetFreeCrop(html);
                    },
                () => activeVill.Res.Capacity = ResourceParser.GetResourceCapacity(html, acc.AccInfo.ServerVersion),
                () => activeVill.Res.Stored = ResourceParser.GetResources(html),
                () => activeVill.Timings.NextVillRefresh = DateTime.Now.AddMinutes(ran.Next(30,60)),
                () => // NPC
                {
                    float ratio = (float)activeVill.Res.Stored.Resources.Crop / activeVill.Res.Capacity.GranaryCapacity;
                    if (ratio >= 0.99 &&
                        acc.AccInfo.Gold >= 3 &&
                        activeVill.Market.Npc.Enabled &&
                        (activeVill.Market.Npc.NpcIfOverflow || !MarketHelper.NpcWillOverflow(activeVill)))
                    {  //npc crop!
                        TaskExecutor.AddTaskIfNotExistInVillage(acc, activeVill, new NPC()
                        {
                            ExecuteAt = DateTime.MinValue,
                            Vill = activeVill
                        });
                    }
                },
                () => {
                    if (acc.Settings.AutoActivateProductionBoost && CheckProductionBoost(acc))
                    {
                        TaskExecutor.AddTask(acc, new TTWarsPlusAndBoost() {
                            ExecuteAt = DateTime.Now.AddSeconds(1)
                        });
                    }
                },
                () =>
                {
                    if (activeVill.Build.InstaBuild &&
                        acc.AccInfo.Gold >= 2 &&
                        activeVill.Build.CurrentlyBuilding.Count >= (acc.AccInfo.PlusAccount ? 2 : 1) &&
                        activeVill.Build.CurrentlyBuilding.LastOrDefault().Duration
                            >= DateTime.Now.AddHours(activeVill.Build.InstaBuildHours))
                    {
                        TaskExecutor.AddTaskIfNotExistInVillage(acc, activeVill, new InstaUpgrade()
                        {
                            Vill = activeVill,
                            ExecuteAt = DateTime.Now.AddHours(-1)
                        });
                    }
                },
                () => acc.Hero.AdventureNum = HeroParser.GetAdventureNum(html, acc.AccInfo.ServerVersion),
                () => acc.Hero.Status = HeroParser.HeroStatus(html, acc.AccInfo.ServerVersion),
                () => acc.Hero.HeroInfo.Health = HeroParser.GetHeroHealth(html, acc.AccInfo.ServerVersion),
                () =>
                {
                    bool heroReady = (acc.Hero.HeroInfo.Health > acc.Hero.Settings.MinHealth &&
                        acc.Hero.Settings.AutoSendToAdventure &&
                        acc.Hero.Status == Hero.StatusEnum.Home &&
                        acc.Hero.NextHeroSend < DateTime.Now);
                    // Update adventures
                    if (heroReady &&
                        (HeroHelper.GetHeroHomeVillage(acc)? // RallyPoint in village
                            .Build?
                            .Buildings?
                            .Any(x => x.Type == Classificator.BuildingEnum.RallyPoint && 0 < x.Level) ?? false) &&
                        (acc.Hero.AdventureNum != acc.Hero.Adventures.Count() || HeroHelper.AdventureInRange(acc)))
                    {
                        // Update adventures
                        TaskExecutor.AddTaskIfNotExists(acc, new StartAdventure() { ExecuteAt = DateTime.Now.AddSeconds(10) });
                    }
                    if (acc.Hero.AdventureNum == 0 && acc.Hero.Settings.BuyAdventures) //for UNL servers, buy adventures
                    {
                        TaskExecutor.AddTaskIfNotExists(acc, new TTWarsBuyAdventure() { ExecuteAt = DateTime.Now.AddSeconds(5) });
                    }
                    if (acc.Hero.Status == Hero.StatusEnum.Dead && acc.Hero.Settings.AutoReviveHero) //if hero is dead, revive him
                    {
                        TaskExecutor.AddTaskIfNotExists(acc, new ReviveHero() {
                            ExecuteAt = DateTime.Now.AddSeconds(5),
                            Vill = AccountHelper.GetHeroReviveVillage(acc)
                        });
                    }
                    if (HeroParser.LeveledUp(html, acc.AccInfo.ServerVersion) && acc.Hero.Settings.AutoSetPoints)
                    {
                        TaskExecutor.AddTaskIfNotExists(acc, new HeroSetPoints() { ExecuteAt = DateTime.Now });
                    }
                },
                () => AutoExpandStorage(acc, activeVill)
            };
        }

        public static bool CheckProductionBoost(Account acc)
        {
            var boostProduction = acc.Wb.Html.DocumentNode.Descendants("button").FirstOrDefault(x => x.HasClass("productionBoostButton"));
            return (boostProduction != null);
        }

        /// <summary>
        /// Checks whether the resources at more than 95% of the capacity and increases if it's true.
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="vill"></param>
        private static void AutoExpandStorage(Account acc, Village vill)
        {
            if (!vill.Settings.AutoExpandStorage) return;

            double warehouse_delta = vill.Res.Capacity.WarehouseCapacity * 0.95;
            double granary_delta = vill.Res.Capacity.GranaryCapacity * 0.95;

            if (warehouse_delta <= vill.Res.Stored.Resources.Wood ||
                warehouse_delta <= vill.Res.Stored.Resources.Clay ||
                warehouse_delta <= vill.Res.Stored.Resources.Iron)
            {
                BuildingHelper.UpgradeBuildingForOneLvl(acc, vill, Classificator.BuildingEnum.Warehouse, false);
                return;
            }


            if (granary_delta <= vill.Res.Stored.Resources.Crop)
                BuildingHelper.UpgradeBuildingForOneLvl(acc, vill, Classificator.BuildingEnum.Granary, false);
        }
    }
}