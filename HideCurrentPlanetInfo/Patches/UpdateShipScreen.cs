using System.Text.RegularExpressions;
using HarmonyLib;
using UnityEngine;

namespace HideCurrentPlanetInfo.Patches
{
	class ShipScreenPatch
	{
		internal static void ScreenUpdatePatch(SelectableLevel level, WeatherRegistry.Weather weather, string screenText)
		{
			// do something with the screen text

			string newScreenText = screenText;

			Regex regex = new(@"(?<=^[A-Z]+\:\ ).+$", RegexOptions.Multiline);

			Color textColor = new(0.6f, 0.3f, 0.4f, 1);

			// log the regex matches
			MatchCollection matches = regex.Matches(screenText);

			foreach (Match match in matches)
			{
				Plugin.logger.LogInfo($"Match: {match.Value}");
				newScreenText = newScreenText.Replace(match.Value, $"<color=#{ColorUtility.ToHtmlStringRGB(textColor)}>[REDACTED]</color>");
			}

			// replace all matches with [REDACTED]
			// newScreenText = regex.Replace(screenText, "[REDACTED]");

			Plugin.logger.LogWarning($"Screen text:\n{screenText}\n->\n{newScreenText}\n");

			StartOfRound.Instance.screenLevelDescription.text = newScreenText;
		}
	}
}
