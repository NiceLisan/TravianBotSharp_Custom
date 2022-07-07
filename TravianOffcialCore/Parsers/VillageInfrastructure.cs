﻿using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TravianOffcialCore.Parsers
{
    public class VillageInfrastructure
    {
        public static List<HtmlNode> GetBuildingNodes(HtmlDocument doc)
        {
            var villageContentNode = doc.GetElementbyId("villageContent");
            if (villageContentNode is null) return new();
            var list = villageContentNode.Descendants("div").Where(x => x.HasClass("buildingSlot")).ToList();
            list.RemoveAt(list.Count - 1);
            return list;
        }

        public static int GetId(HtmlNode node)
        {
            return node.GetAttributeValue<int>("data-aid", -1);
        }

        public static int GetType(HtmlNode node)
        {
            return node.GetAttributeValue<int>("data-gid", -1);
        }

        public static int GetLevel(HtmlNode node)
        {
            var aNode = node.Descendants("a").FirstOrDefault();
            if (aNode is null) return -1;
            return aNode.GetAttributeValue<int>("data-level", -1);
        }

        public static bool IsUnderConstruction(HtmlNode node)
        {
            return node.Descendants("a").Any(x => x.HasClass("underConstruction"));
        }
    }
}