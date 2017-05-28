﻿namespace ExBuddy.OrderBotTags.Gather.Rotations
{
	using Attributes;
	using ff14bot;
	using Interfaces;
	using System.Threading.Tasks;

	[GatheringRotation("Collect515", 30, 600)]
	public sealed class Collect515GatheringRotation : CollectableGatheringRotation, IGetOverridePriority
	{
		#region IGetOverridePriority Members

		int IGetOverridePriority.GetOverridePriority(ExGatherTag tag)
		{
			// if we have a collectable && the collectable value is greater than or equal to 515: Priority 515
			if (tag.CollectableItem != null && tag.CollectableItem.Value >= 515)
			{
				return 515;
			}
			return -1;
		}

		#endregion IGetOverridePriority Members

		public override async Task<bool> ExecuteRotation(ExGatherTag tag)
		{
			if (tag.IsUnspoiled())
			{
				await DiscerningMethodical(tag);
				await DiscerningMethodical(tag);
				await DiscerningMethodical(tag);
			}
			else
			{
				if (Core.Player.CurrentGP >= 600)
				{
					await DiscerningMethodical(tag);
					await DiscerningMethodical(tag);
					await DiscerningMethodical(tag);
					await IncreaseChance(tag);
					return true;
				}

				await Impulsive(tag);
				await Impulsive(tag);
				await Methodical(tag);
			}
			return true;
		}
	}
}