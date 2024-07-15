using System.Text;
using System.Text.RegularExpressions;
using HarmonyLib;
using TMPro;
using UnityEngine;
using WeatherRegistry;

namespace HideCurrentPlanetInfo.Patches
{
	class ShipScreenPatch
	{
		internal static void ScreenUpdatePatch(SelectableLevel level, WeatherRegistry.Weather weather, string screenText)
		{
			// do something with the screen text

			string newScreenText = screenText;

			Regex regex = new(@"(?<=^(?!ORBITING|WEATHER)[A-Z]+\:\ ).+$", RegexOptions.Multiline);

			Color textColor = new(0.6f, 0.3f, 0.4f, 1);
			string redactedText = $"<color=#{ColorUtility.ToHtmlStringRGB(textColor)}>[REDACTED]</color>";

			// conditions based on config
			MatchCollection matches = regex.Matches(screenText);
			Plugin.logger.LogWarning($"Match count: {matches.Count}");

			foreach (Match match in matches)
			{
				Plugin.logger.LogInfo($"Match: {match.Value}");
				Plugin.logger.LogDebug($"{ConfigManager.HiddenMoonInfo.Value}");

				if (ConfigManager.HiddenMoonInfo.Value)
				{
					newScreenText = regex.Replace(newScreenText, redactedText);
				}
			}

			if (ConfigManager.HiddenMoon.Value)
			{
				Regex moonRegex = new(@"(?<=ORBITING\:\ ).+$", RegexOptions.Multiline);
				// replace all matches with [REDACTED]

				newScreenText = moonRegex.Replace(newScreenText, redactedText);
			}

			if (ConfigManager.HiddenWeather.Value)
			{
				Regex weatherRegex = new(@"(?<=WEATHER\:\ ).+$", RegexOptions.Multiline);
				// replace all matches with [REDACTED]

				newScreenText = weatherRegex.Replace(weatherRegex.Match(newScreenText).Value, redactedText);
			}

			Plugin.logger.LogWarning($"Screen text:\n{screenText}\n->\n{newScreenText}\n");

			StartOfRound.Instance.screenLevelDescription.text = newScreenText;
		}
	}
}
