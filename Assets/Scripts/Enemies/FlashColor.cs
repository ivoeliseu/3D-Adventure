using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    //Items que vão mudar de cor
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    // private Color _defaultColor;
    private Tween _currTween;

    //Valida o MeshRenderer e SkinnedMeshRenderer
    private void OnValidate()
    {
        if (meshRenderer == null ) meshRenderer = GetComponent<MeshRenderer>();
        if (skinnedMeshRenderer == null ) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        // Salva a cor de emissão padrão do objeto
        // _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        //Se não for nulo E não tiver outra animação ocorrendo no momento, executa
        if (meshRenderer != null && !_currTween.IsActive()) 
            _currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
            
        if (skinnedMeshRenderer != null && !_currTween.IsActive())
            _currTween = skinnedMeshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
