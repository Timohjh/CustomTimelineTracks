using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class RewindControlClip : PlayableAsset, ITimelineClipAsset
{
	public RewindControlBehaviour template = new RewindControlBehaviour();
	public ClipCaps clipCaps => ClipCaps.None;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<RewindControlBehaviour>.Create(graph, template);
	}
}
