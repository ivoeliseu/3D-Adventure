using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    public float duration;
    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    //Corrotina que ir� fazer a anima��o da Vignette;
    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        //out -> Pega a variavel (tmp) e passa a variavel para dentro da fun��o abaixo como ela, sem criar uma nova
        {
            _vignette = tmp;
        }

        //Post processing utiliza como paramentro de cor value
        ColorParameter c = new ColorParameter();
        c.value = Color.red;

        float time = 0; //Tempo que ir� levar para a tela piscar.

        //La�o de repeti��o que fara a transi��o entre as cores
        while(time < duration)
        {
            c.value = Color.Lerp(Color.white, Color.red, time / duration);
            time += Time.deltaTime;

            //Override: Sobrescreve a atual cor.
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0; //Tempo que ir� levar para a tela piscar.

        //La�o de repeti��o que fara a transi��o entre as cores
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.white, time / duration);
            time += Time.deltaTime;

            //Override: Sobrescreve a atual cor.
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }



    }
    
}
