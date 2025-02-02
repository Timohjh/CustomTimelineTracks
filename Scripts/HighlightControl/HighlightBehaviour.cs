using System;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

[Serializable]
public class HighlightBehaviour : PlayableBehaviour
{
    public double start;
    public double end;
    private Renderer renderer;
    private Material cloneMaterial;
    private bool initialized = false;
    private BookInteraction bookInteraction;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying)
            return;
        
        if (!initialized)
        {
            Initialize(playerData as GameObject);
        }

        if (cloneMaterial == null)
        {
            Debug.LogWarning("Clone material is null.");
            return;
        }

        // Get global time within the timeline
        double currentTime = playable.GetGraph().GetRootPlayable(0).GetTime();

        float intensity = 0f; // Default to 0

        // Check if the book has been collected
        if (bookInteraction != null && bookInteraction.HasBeenCollected)
        {
            SetIntensity(0f);
            return;
        }

        // Check if we're within the clip's boundaries
        if (currentTime >= start && currentTime <= end)
        {
            // Ramping up in the first second after the start
            if (currentTime <= start + 1)
            {
                intensity = Mathf.Lerp(0f, 1f, (float)(currentTime - start));
            }
            // Ramping down in the last second before the end
            else if (currentTime >= end - 1)
            {
                intensity = Mathf.Lerp(0f, 1, (float)(end - currentTime));
            }
            // Middle of the clip: stay at 1
            else
            {
                intensity = 1f;
            }
        }

        // If the intensity is very low at the end, clamp it to 0
        if (intensity < 0.05f)
        {
            intensity = 0f;
        }

        SetIntensity(intensity);
    }

    private void Initialize(GameObject gameObject)
    {
        if (gameObject != null)
        {
            renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                cloneMaterial = Object.Instantiate(renderer.material);
                renderer.material = cloneMaterial;
            }
            else
            {
                Debug.LogWarning("SkinnedMeshRenderer or its material is null.");
            }

            // Get the BookInteraction component from the parent
            bookInteraction = gameObject.GetComponentInParent<BookInteraction>();
            if (bookInteraction == null)
            {
                Debug.LogWarning("BookInteraction component not found in parent hierarchy.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject is null in Initialize.");
        }

        initialized = true;
    }

    private void SetIntensity(float intensity)
    {
        if (cloneMaterial != null)
        {
            cloneMaterial.SetFloat("_Intensity", intensity);
        }
        else
        {
            Debug.LogWarning("Attempted to set intensity on a null material.");
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        if (cloneMaterial != null)
        {
            Object.Destroy(cloneMaterial);
            cloneMaterial = null;
        }
    }
}