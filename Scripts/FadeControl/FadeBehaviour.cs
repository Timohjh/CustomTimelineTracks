using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public class FadeBehaviour : PlayableBehaviour
{
    public double start;
    public double end;
    public float startAlpha = 0f;
    public float endAlpha = 1f;
    public string customAlphaPropertyName = "";
    public bool DisableRendererAfterFadeOut = true;
    private Renderer renderer;
    private Material[] originalMaterials;
    private Material[] instancedMaterials;
    private string[] alphaPropertyNames;
    private bool initialized = false;
    public override void OnGraphStart(Playable playable)
    {
    }
    public override void OnPlayableCreate(Playable playable)
    {
        // Debug.Log("FadeBehaviour: OnPlayableCreate called");
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying)
            return;
        if (playerData == null)
            return;
        if (!initialized)
            Initialize((playerData as Renderer).gameObject);
        if (instancedMaterials == null || instancedMaterials.Length == 0)
            return;
        float progress = (float)(playable.GetTime() / playable.GetDuration());
        float alpha = Mathf.Lerp(startAlpha, endAlpha, progress) * info.weight;
        // Enable renderer when we hit the clip
        if (renderer != null && !renderer.enabled && progress > 0)
        {
            renderer.enabled = true;
        }
        SetAlpha(alpha);
        // Check if we're at the end of the clip
        if (end - 0.01 < TimelinePlayer.Instance.Director.time)
        {
            if (renderer != null && DisableRendererAfterFadeOut)
            {
                // Disable renderer if endAlpha is less than 0.1
                renderer.enabled = endAlpha > 0.1f;
            }
        }
    }
    private void Initialize(GameObject gameObject)
    {
        if (gameObject == null)
        {
            initialized = true;
            return;
        }
        renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            initialized = true;
            return;
        }
        originalMaterials = renderer.sharedMaterials;
        instancedMaterials = renderer.materials;
        if (instancedMaterials.Length == 0)
        {
            initialized = true;
            return;
        }
        alphaPropertyNames = new string[instancedMaterials.Length];
        for (int i = 0; i < instancedMaterials.Length; i++)
        {
            alphaPropertyNames[i] = FindAlphaProperty(instancedMaterials[i]);
        }
        initialized = true;
    }
    private string FindAlphaProperty(Material material)
    {
        if (!string.IsNullOrEmpty(customAlphaPropertyName) && material.HasProperty(customAlphaPropertyName))
        {
            return customAlphaPropertyName;
        }
        Debug.LogError("Can't find fade property for: ", renderer.gameObject);
        
        return "_Alpha";
    }
    private void SetAlpha(float alpha)
    {
        for (int i = 0; i < instancedMaterials.Length; i++)
        {
            Material material = instancedMaterials[i];
            string alphaPropertyName = alphaPropertyNames[i];
            if (material != null)
            {
                if (!string.IsNullOrEmpty(alphaPropertyName) && material.HasProperty(alphaPropertyName))
                {
                    material.SetFloat(alphaPropertyName, alpha);
                }
                else
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }
        }
    }
    public override void OnPlayableDestroy(Playable playable)
    {
        if (renderer != null && originalMaterials != null)
        {
            renderer.materials = originalMaterials;
        }
    }
}