using UnityEngine;
using System.Collections.Generic;

public class ListaDeTextosPT_BR
{
    static Dictionary<ChaveDeTexto, List<string>> txt;
    public static Dictionary<ChaveDeTexto, List<string>> Txt
    {
        get {
            if (txt == null)
            {
                txt = new Dictionary<ChaveDeTexto, List<string>>();

                ColocaTextos(ref txt, TextosChaveEmPT_BR.txt);
                ColocaTextos(ref txt, TextosInfinity.txt);
                ColocaTextos(ref txt, TextosIve.txt);
                ColocaTextos(ref txt, TextosDeBarreirasPT_BR.txt);
            }

            return txt;
        }
    }

    static void ColocaTextos(ref Dictionary<ChaveDeTexto, List<string>>  retorno, Dictionary<ChaveDeTexto, List<string>> inserir)
    {
        foreach (ChaveDeTexto k in inserir.Keys)
        {
            retorno[k] = inserir[k];
        }
    }
    
}