using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackBindingType(typeof(Renderer))]
[TrackClipType(typeof(FadeClip))]
public class FadeTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (var clip in GetClips())
		{
			var myAsset = clip.asset as FadeClip;
			if (myAsset)
			{
				myAsset.template.start = clip.start;
				myAsset.template.end = clip.end;
				//myAsset.template.materialIndex = clip.mat;

			}
		}
		return base.CreateTrackMixer(graph, go, inputCount);
	}
}