using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class RewindControlBehaviour : PlayableBehaviour
{
	public double start;
	public double end;
	IRewindable rewindable;
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (!Application.isPlaying || !IsInside())
			return; 

		if(rewindable == null)
		{
			if (playerData == null)
				return;
			
			var rewind = playerData as GameObject;
			if (!rewind.TryGetComponent<IRewindable>(out rewindable))
				return; 
			
		}
		
		if (rewindable.OnRewind())
		{
			TimelinePlayer.Instance.Director.time = end;
			Debug.Log("Rewind false");
		}
		if (!rewindable.OnRewind() && end - TimelinePlayer.Instance.Director.time < 0.05)
		{
			TimelinePlayer.Instance.Director.time = start;
			TimelinePlayer.Instance.Director.Resume();
			Debug.Log("Rewind true");
		}
	}

	bool IsInside()
	{
		if (TimelinePlayer.Instance.Director.time > start && TimelinePlayer.Instance.Director.time < end + 0.05)
			return true;

		return false;
	}

}
