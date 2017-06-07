using UnityEngine;
using System.Collections;

public class ExistenciaDoController
{
    public static bool AgendaExiste(PainelUmaMensagem.RetornarParaAntecessor r, MonoBehaviour m)
    {
        GameController g = GameController.g;
        if (g!=null)
            return true;
        else
        {
            m.StartCoroutine(Rotina(r));
            return false;
        }
    }

    static IEnumerator Rotina(PainelUmaMensagem.RetornarParaAntecessor r)
    {
        yield return new WaitForSecondsRealtime(0.15f);
        r();
    }
}