using UnityEngine;
using System.Collections;

public class LoadBar : MonoBehaviour
{
    [SerializeField]private RectTransform bar;

    private float posOriginalMaxDaAncora;
    private float posOriginalMinDaAncora;

    //[Range(0,1)]public float teste = 1;
    // Use this for initialization
    void Awake()
    {
        posOriginalMaxDaAncora = bar.anchorMax.x;
        posOriginalMinDaAncora = bar.anchorMin.x;
    }

    // Update is called once per frame
    public void ValorParaBarra(float x)
    {
        gameObject.SetActive(true);
        PercentagemDeBarraNoY(bar,x);
    }

    void PercentagemDeBarraNoY(RectTransform barra, float percentagem)
    {
        barra.anchorMax = new Vector2(
            (posOriginalMaxDaAncora - posOriginalMinDaAncora) * percentagem + posOriginalMinDaAncora,
            barra.anchorMax.y
            );
    }
}
