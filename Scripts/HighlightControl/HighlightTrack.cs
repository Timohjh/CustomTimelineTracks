using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackBindingType(typeof(GameObject))]
[TrackClipType(typeof(HighlightClip))]
public class HighlightTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		var director = go.GetComponent<PlayableDirector>();
		var boundObject = director.GetGenericBinding(this) as GameObject;

		foreach (var clip in GetClips())
		{
			var myAsset = clip.asset as HighlightClip;
			if (myAsset != null)
			{
				myAsset.template.start = clip.start;
				myAsset.template.end = clip.end;

				if (boundObject != null)
				{
					if (boundObject.transform.parent != null)
					{
						clip.displayName = boundObject.transform.parent.name;
					}
					else
					{
						clip.displayName = boundObject.name;
					}
				}
				else
				{
					clip.displayName = "Highlight Clip";
				}
			}
		}

		return base.CreateTrackMixer(graph, go, inputCount);
	}
}