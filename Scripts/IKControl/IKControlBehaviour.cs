using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Animations.Rigging;

public class IKControlBehaviour : PlayableBehaviour
{
	public TwoBoneIKConstraint TwoBone;
	public Rig Rig;
	public CopyTarget TargetComponent;
	public Transform Target;
	public float EndValue = 1f;
	public float StartValue;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		// Set the target to the same as the TargetComponent when the clip starts
		if (EndValue != 0 && Target != null)
		{
			TargetComponent.currentTarget = Target;
			if (!TargetComponent.currentTarget.gameObject.TryGetComponent(out TransformSaver saver) && Application.isPlaying)
				TargetComponent.currentTarget.gameObject.AddComponent<TransformSaver>();
		}
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (!Application.isPlaying)
			return;

		// Get the clip time of the playable
		float clipTime = (float)playable.GetTime();
		// Get the duration of the clip
		float clipDuration = (float)playable.GetDuration();
		// Normalize the clip time to be between 0 and 1
		float normalizedTime = clipTime / clipDuration;
		// Interpolate the weight value using Mathf.Lerp
		float weight = Mathf.SmoothStep(StartValue, EndValue, normalizedTime);

		if (Mathf.Abs(weight - EndValue) < 0.02f)
		{
			SetWeight(EndValue);
			Debug.Log("1 " +weight);
		}
		else if (Mathf.Abs(weight - EndValue) > 0.98f)
		{
			SetWeight(StartValue);
			Debug.Log("2 " +weight);
		}
		else
		{
			SetWeight(weight);
			Debug.Log("3 " +weight);
		}
	}

	public void SetWeight(float w) 
	{
		if (TwoBone != null)
			TwoBone.weight = w;

		if (Rig != null)
			Rig.weight = w;
	}
}
