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
			AlwaysActive, LV7_Only
		}

		public enum ApplicationTarget
		{
			Soldier, Faction
		}

		public enum TriggerConditions
		{
			Threshold, Continuous
		}

		[ConfigField(text: "When does the effect apply?", description: "By default the EXP->SP overflow will only occur for LV7 Soldiers that will otherwise waste the EXP earned on missions. \n\nAlwaysActive makes it apply for all soldiers regardless of level.")]
		public ApplicationRequirement WhenToApply = ApplicationRequirement.LV7_Only;

		[ConfigField(text: "Overflow Beneficiary", description: "Select whether the generated SP goes to the individual soldier or the common pool.")]
		public ApplicationTarget Target = ApplicationTarget.Soldier;
		
		[ConfigField(text: "Trigger Condition", description: "Threshold: Soldier earns a fixed amount of SP for earning above the threshold. \n\nContinuous: Gain SP as a ratio of EXP earned.")]
		public TriggerConditions Trigger = TriggerConditions.Continuous;
		
		[ConfigField(text: "EXP:SP Ratio or Threshold Value", description: "Set the EXP to SP ratio or EXP Threshold for an SP payout. Value must be positive.")]
		public int Ratio = 50;

		[ConfigField(text: "Threshold SP Gain", description: "Set the SP gain while using the Threshold Trigger condition.")]
		public int SingleUseSPGain = 3;
	}
}
