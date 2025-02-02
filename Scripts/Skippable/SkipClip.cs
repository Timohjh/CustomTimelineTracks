using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

[System.Serializable]
public class SkipClip : PlayableAsset, ITimelineClipAsset
{
	// The clip behaviour data
	public SkipBehaviour template = new SkipBehaviour();

	// The clip caps (no blending or extrapolation supported)
	public ClipCaps clipCaps => ClipCaps.None;

	// Create a playable from this clip
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<SkipBehaviour>.Create(graph, template);
	}
}