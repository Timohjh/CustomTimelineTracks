using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Animations.Rigging;
using System.Threading;

[Serializable]
public class IKControlClip : PlayableAsset
{
	public ExposedReference<TwoBoneIKConstraint> twoBone;
	public ExposedReference<Rig> rig;
	public ExposedReference<CopyTarget> copyTarget;
	public ExposedReference<Transform> target;

	[SerializeField, NotKeyable]
	public float EndValue;
	[SerializeField, NotKeyable]
	public float StartValue;


	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		ScriptPlayable<IKControlBehaviour> playable =
			ScriptPlayable<IKControlBehaviour>.Create(graph);

		IKControlBehaviour playableBehaviour = playable.GetBehaviour();

		playableBehaviour.TwoBone = twoBone.Resolve(graph.GetResolver());
		playableBehaviour.Rig = rig.Resolve(graph.GetResolver());
		playableBehaviour.TargetComponent = copyTarget.Resolve(graph.GetResolver());
		playableBehaviour.Target = target.Resolve(graph.GetResolver());
		playableBehaviour.EndValue = EndValue;
		playableBehaviour.StartValue = StartValue;


		return playable;
	}
}
