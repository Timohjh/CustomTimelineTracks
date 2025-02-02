using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Animations.Rigging;
using System;

[Serializable]
[TrackColor(0.8294409f, 1f, 0.654717f)]
[TrackClipType(typeof(IKControlClip))]
public class IKControlTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();

		ScriptPlayable<IKControlSchedulerBehaviour> playable =
			ScriptPlayable<IKControlSchedulerBehaviour>.Create(graph, inputCount);

		IKControlSchedulerBehaviour ikSchedulerPlayableBehaviour =
			   playable.GetBehaviour();

		if (ikSchedulerPlayableBehaviour != null)
		{
			ikSchedulerPlayableBehaviour.director = playableDirector;
			ikSchedulerPlayableBehaviour.clips = GetClips();
		}

		return playable;
	}
}
