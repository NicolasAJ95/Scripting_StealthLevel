using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectReference : MonoBehaviour
{
    public Shader postEffect;
    private Material mat;

    public Color whiteReplacement;
    public Color blackReplacement;

    // Use this for initialization
    void Start()
    {
        this.mat = new Material(this.postEffect);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetColor ("_WhiteReplacement", whiteReplacement);
        mat.SetColor ("_BlackReplacement", blackReplacement);
        Graphics.Blit(source, destination, mat);
    }

}