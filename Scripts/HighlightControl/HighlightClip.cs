using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class HighlightClip : PlayableAsset, ITimelineClipAsset
{
	public HighlightBehaviour template = new HighlightBehaviour();
	public ClipCaps clipCaps => ClipCaps.None;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<HighlightBehaviour>.Create(graph, template);
	}
}