using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color _defaultColor;
    private Tween _currTween;

    private void Start()
    {
        // Salva a cor de emissão padrão do objeto
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currTween.IsActive()) 
            _currTween = meshRenderer.material.DOColor(Color.red, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
