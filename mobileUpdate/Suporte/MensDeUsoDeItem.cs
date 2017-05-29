using UnityEngine;
using System.Collections;

public class MensDeUsoDeItem
{
    static void ApresentaMensagem(string mens)
    {
        GameController.g.HudM.P_EscolheUsoDeItens.DesligarMeusBotoes();
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(
            GameController.g.HudM.P_EscolheUsoDeItens.ReligarMeusBotoes, mens);            
    }

    public static void MensDeMorto(string nomeDoCriatureBase)
    {
        ApresentaMensagem(string.Format(bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[2], nomeDoCriatureBase));
    }

    public static void MensDeNaoPrecisaDesseItem(string nomeDele)
    {
        ApresentaMensagem(string.Format(bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[9], nomeDele));
    }

    public static void MensNaoTemOTipo(string nomeDoTipo)
    {
        ApresentaMensagem(string.Format(bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[3], nomeDoTipo));
    }
    
}