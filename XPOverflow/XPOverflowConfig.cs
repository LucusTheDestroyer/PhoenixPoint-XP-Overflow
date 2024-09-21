using PhoenixPoint.Modding;

namespace XPOverflow
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class XPOverflowConfig : ModConfig
	{
		public enum ApplicationRequirement
		{
			Any, LV7
		}

		public enum ApplicationTarget
		{
			Soldier, Faction
		}

		public enum ApplicationLimit
		{
			Single, Unlimited
		}

		[ConfigField(text: "Soldier Level Requirement", description: "LV7: Overflow only applies for LV7 Soldiers that will otherwise waste the EXP earned on missions. \n\nAny: Overflow applies for all soldiers regardless of level.")]
		public ApplicationRequirement WhenToApply = ApplicationRequirement.LV7;

		[ConfigField(text: "Overflow Beneficiary", description: "Select whether the generated SP goes to the soldier or to the common pool.")]
		public ApplicationTarget Target = ApplicationTarget.Soldier;
		
		[ConfigField(text: "Application Limit", description: "Single: SP payout occurs once per soldier if they earn more EXP than the requirement. \n\nUnlimited: Earn an SP payout for each multiple of the EXP Requirement.")]
		public ApplicationLimit Limit = ApplicationLimit.Unlimited;
		
		[ConfigField(text: "XP Requirement", description: "Set the XP required per SP payout. Value must be positive.")]
		public int Ratio = 50;

		[ConfigField(text: "SP Gain per payout", description: "Set the SP gain per payout. Value must be positive.")]
		public int SPGain = 1;
	}
}
