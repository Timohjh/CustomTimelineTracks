using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.1f,1f,0f)]
[TrackBindingType(typeof(GameObject))]
[TrackClipType(typeof(RewindControlClip))]
public class RewindControlTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (var clip in GetClips())
		{
			var myAsset = clip.asset as RewindControlClip;
			if (myAsset)
			{
				myAsset.template.start = clip.start;
				myAsset.template.end = clip.end;
			}
		}
		return base.CreateTrackMixer(graph, go, inputCount);
	}
}