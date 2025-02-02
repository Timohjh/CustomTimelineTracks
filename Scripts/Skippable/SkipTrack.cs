using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0.5f, 0f)]
[TrackBindingType(typeof(GameObject))]
[TrackClipType(typeof(SkipClip))]
public class SkipTrackAsset : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (var clip in GetClips())
		{
			var myAsset = clip.asset as SkipClip;
			if (myAsset)
			{
				myAsset.template.start = clip.start;
				myAsset.template.end = clip.end;
			}
		}
		return base.CreateTrackMixer(graph, go, inputCount);
	}
}
