﻿using FluentResults;
using MainCore.Common.Enums;

namespace MainCore.Common.Errors.TrainTroop
{
    public class MissingResource : Error
    {
        private MissingResource(BuildingEnums building) : base($"Dont have enough resource to train troop in {building}")
        {
        }

        public static MissingResource Error(BuildingEnums building) => new(building);
    }
}