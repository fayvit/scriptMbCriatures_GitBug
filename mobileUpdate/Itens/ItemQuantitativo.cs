using UnityEngine;
using System.Collections;

public class ItemQuantitativo
{
    public static bool UsaItemDeRecuperacao(CriatureBase meuCriature)
    {
        Atributos A = meuCriature.CaracCriature.meusAtributos;
        if (A.PV.Corrente < A.PV.Maximo&&A.PV.Corrente>0)
            return true;
        else
            return false;
    }

    public static bool UsaItemDeEnergia(CriatureBase meuCriature)
    {
        
        Atributos A = meuCriature.CaracCriature.meusAtributos;
        if (A.PE.Corrente < A.PE.Maximo && A.PE.Corrente >= 0 && A.PV.Corrente>0)
            return true;
        else
            return false;
    }

    public static void RecuperaPV(Atributos meusAtributos, int tanto)
    {
        int contador = meusAtributos.PV.Corrente;
        int maximo = meusAtributos.PV.Maximo;

        if (contador + tanto < maximo)
            meusAtributos.PV.Corrente += tanto;
        else
            meusAtributos.PV.Corrente = meusAtributos.PV.Maximo;
    }

    public static void RecuperaPE(Atributos meusAtributos, int tanto)
    {
        int contador = meusAtributos.PE.Corrente;
        int maximo = meusAtributos.PE.Maximo;

        if (contador + tanto < maximo)
            meusAtributos.PE.Corrente += tanto;
        else
            meusAtributos.PE.Corrente = meusAtributos.PE.Maximo;
    }

    public static void AplicacaoDoItemComMenu(CharacterManager manager,CriatureBase C,TipoQuantitativo Q,int valor)
    {
        Atributos A = C.CaracCriature.meusAtributos;

        if (Q == TipoQuantitativo.PV)
            RecuperaPV(A, valor);
        else
        {
            RecuperaPE(A, valor);
        }
        
        PainelStatus ps = GameController.g.HudM.P_EscolheUsoDeItens;
        GameController.g.HudM.AtualizaHudHeroi(C);
        GameController.g.StartCoroutine(
            ParticulaDeCoisasBoas.ParticulasMaisBotao(ps.GetComponent<RectTransform>(), ps.ReligarMeusBotoes)
            );

        ps.DesligarMeusBotoes();
        ps.BtnMeuOutro(manager.Dados.CriaturesAtivos.IndexOf(C));
    }
}

public enum TipoQuantitativo
{
    PV,
    PE
}
