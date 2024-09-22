using System.Linq;
using System.Reflection;
using Base.Serialization.General;
using HarmonyLib;
using PhoenixPoint.Common.Levels.Missions;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Levels;
using UnityEngine;

namespace XPOverflow
{
	/// <summary>
	/// Mod's custom save data for tactical.
	/// </summary>
	[SerializeType(SerializeMembersByDefault = SerializeMembersType.SerializeAll)]
	public class XPOverflowTacInstanceData
	{
		public int ExampleData;
	}

	/// <summary>
	/// Represents a mod instance specific for Tactical game.
	/// Each time Tactical level is loaded, new mod's ModTactical is created. 
	/// </summary>
	public class XPOverflowTactical : ModTactical
	{
		/// <summary>
		/// Called when Tactical ends.
		/// </summary>
		public override void OnTacticalEnd() {
			XPOverflowConfig Config = ((XPOverflowMain)Main).Config;
			base.OnTacticalEnd();
			TacticalFaction playerFaction = Controller.Factions.FirstOrDefault((TacticalFaction f) => f.ParticipantKind == TacMissionParticipant.Player);
			PropertyInfo Skillpoints = AccessTools.Property(typeof(TacticalFaction), "Skillpoints");
			foreach(TacticalActor actor in playerFaction.GetOwnedActors<TacticalActor>())
			{
				if(actor == null || actor.LevelProgression == null || actor.IsDead)
                {
                    continue;
                }
				int SPtoAdd;
				int Overflow;
				if(Config.WhenToApply == XPOverflowConfig.ApplicationRequirement.LV7)
				{
					Overflow = actor.LevelProgression.ExperienceReference + actor.LevelProgression.ExperienceEarned - actor.LevelProgression.Def.XPCap;
				}
				else //AlwaysActive
				{
					Overflow = actor.LevelProgression.ExperienceEarned; //Value should always be positive here.
				}
				if(Overflow <= 0) //Early exit condition for the LV7 only case.
				{
					continue;
				}
				if(Config.Limit == XPOverflowConfig.ApplicationLimit.Single)
				{
					SPtoAdd = Overflow > Config.Ratio ? Config.SPGain : 0;
				}
				else //Continuous or repeated trigger.
				{
					SPtoAdd = (Overflow / Config.Ratio) * Config.SPGain;
				}
				if(Config.Target == XPOverflowConfig.ApplicationTarget.Faction)
				{
					int val = (int)Skillpoints.GetValue(playerFaction);
					Skillpoints.SetValue(playerFaction, val + SPtoAdd);
					continue;
				}
				actor.CharacterProgression.AddSkillPoints(SPtoAdd);
			}
		}
	}
}
