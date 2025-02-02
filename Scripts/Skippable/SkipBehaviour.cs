using System;
using UnityEngine.Playables;
using UnityEngine;

[Serializable]
public class SkipBehaviour : PlayableBehaviour
{
	public double start;
	public double end;
	[Tooltip("-1 to use clip's end as the skip")]
	public double skipHere = -1;
	ISkippable script;
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{

		if (!Application.isPlaying)
			return;

		// Get the current time of the director
		double currentTime = TimelinePlayer.Instance.Director.time;
		if (script == null)
		{
			var skip = playerData as GameObject;
			if (skip != null)
				script = skip.GetComponent<ISkippable>();
		}

		if (script.Skip())
		{
			if (currentTime >= start && currentTime <= end)
			{
				// Skip to the end of the clip by setting the time of the director
				TimelinePlayer.Instance.Director.time = skipHere < 0 ? end : skipHere;
			}
		}
	}
}