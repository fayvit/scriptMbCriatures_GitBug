using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntervaloDeGolpe : MonoBehaviour
{
    [SerializeField]private Text texto;
    [SerializeField]private Image imgDoTexto;
    // Update is called once per frame
    void Update()
    {
        if (GameController.g)
        {
            GerenciadorDeGolpes gg = GameController.g.Manager.Dados.CriaturesAtivos[0].GerenteDeGolpes;
            GolpeBase gb = gg.meusGolpes[gg.golpeEscolhido];
            if (-Time.time + gb.UltimoUso + gb.TempoDeReuso > 0)
            {
                imgDoTexto.gameObject.SetActive(true);
                texto.text = comandos.mostradorDeTempo(-Time.time + gb.UltimoUso + gb.TempoDeReuso, "s", false);
                texto.text = (texto.text == "0") ? "0." : texto.text;
            }
            else
            {
                imgDoTexto.gameObject.SetActive(false);

            }
        }
    }
}
