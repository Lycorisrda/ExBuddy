﻿namespace ExBuddy.OrderBotTags.Gather.Rotations
{
    using System.Threading.Tasks;

    using ff14bot;
    using ff14bot.Managers;

    //Name, RequiredGp, RequiredTime
    [GatheringRotation("YieldAndQuality", 600, 0)]
    public class YieldAndQualityGatheringRotation : SmartGatheringRotation, IGetOverridePriority
    {
        public override async Task<bool> ExecuteRotation(GatherCollectableTag tag)
        {
            var level = Core.Player.ClassLevel;

            if (GatheringManager.SwingsRemaining > 4 ||
                ShouldForceUseRotation(tag, level))
            {
                if (Core.Player.CurrentGP >= 500 && level >= 40)
                {
                    await tag.Cast(Ability.IncreaseGatherYield2);

                    if (Core.Player.CurrentGP >= 100)
                    {
                        await tag.Cast(Ability.IncreaseGatherQuality10);
                    }

                    return await base.ExecuteRotation(tag);
                }
            }

            return true;
        }

        int IGetOverridePriority.GetOverridePriority(GatherCollectableTag tag)
        {
            if (tag.CollectableItem != null)
            {
                return -1;
            }

            if (tag.GatherItem.HqChance < 1)
            {
                return -1;
            }

            if (tag.GatherIncrease == GatherIncrease.YieldAndQuality
                || (tag.GatherIncrease == GatherIncrease.Auto && Core.Player.ClassLevel >= 40 && Core.Player.CurrentGP >= 650))
            {
                return 9001;
            }

            return -1;
        }
    }
}