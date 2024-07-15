using BepInEx.Configuration;

namespace HideCurrentPlanetInfo
{
	public class ConfigManager
	{
		public static ConfigFile configFile;
		public static ConfigManager Instance { get; internal set; }

		public static ConfigEntry<bool> HiddenMoon { get; private set; }
		public static ConfigEntry<bool> HiddenWeather { get; private set; }
		public static ConfigEntry<bool> HiddenMoonInfo { get; private set; }

		public static void Init(ConfigFile config)
		{
			Instance = new ConfigManager(config);

			HiddenMoon = configFile.Bind("General", "HiddenMoon", true, "Hide the moon from the ship screen.");
			HiddenWeather = configFile.Bind("General", "HiddenWeather", true, "Hide the weather from the ship screen.");
			HiddenMoonInfo = configFile.Bind("General", "HiddenMoonInfo", true, "Hide the moon info from the ship screen.");
		}

		public ConfigManager(ConfigFile config)
		{
			configFile = config;
		}
	}
}
