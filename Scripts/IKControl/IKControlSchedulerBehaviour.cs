using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Animations.Rigging;
using System.Collections.Generic;

public class IKControlSchedulerBehaviour : PlayableBehaviour
{

	private IEnumerable<TimelineClip> m_Clips;
	private PlayableDirector m_Director;
	internal PlayableDirector director
	{
		get { return m_Director; }
		set { m_Director = value; }
	}
	internal IEnumerable<TimelineClip> clips
	{
		get { return m_Clips; }
		set { m_Clips = value; }
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
		if (m_Clips == null)
			return;

		int inputPort = 0;
		foreach (TimelineClip clip in m_Clips)
		{
			ScriptPlayable<IKControlBehaviour> scriptPlayable =
				(ScriptPlayable<IKControlBehaviour>)playable.GetInput(inputPort);

			IKControlBehaviour IKBehaviour = scriptPlayable.GetBehaviour();

			if (IKBehaviour != null)
			{
				//double preloadTime = Math.Max(0.0, IKBehaviour.preloadTime);
				//if (m_Director.time >= clip.start + clip.duration ||
				//	m_Director.time <= clip.start - preloadTime)
				//	IKBehaviour.StopVideo();
				//else if (m_Director.time > clip.start - preloadTime)
				//	IKBehaviour.PrepareVideo();
			}

			++inputPort;
		}
	}
}
