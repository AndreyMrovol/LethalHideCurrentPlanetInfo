using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HideCurrentPlanetInfo.Patches;
using WeatherRegistry;

namespace HideCurrentPlanetInfo
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInDependency(WeatherRegistry.Plugin.GUID, BepInDependency.DependencyFlags.HardDependency)]
	public class Plugin : BaseUnityPlugin
	{
		internal static MrovLib.Logger logger = new(PluginInfo.PLUGIN_GUID);
		internal static Harmony harmony = new(PluginInfo.PLUGIN_GUID);

		private void Awake()
		{
			harmony.PatchAll();

			WeatherRegistry.EventManager.MapScreenUpdated.AddListener(
				(data) => ShipScreenPatch.ScreenUpdatePatch(data.level, data.weather, data.screenText)
			);

			// Plugin startup logic
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}
	}
}
