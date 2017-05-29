using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da maçã
/// </summary>
public class ItemDeAtributoConsumivelBase : MbItens
{
    [System.NonSerialized]protected CreatureManager CriatureAlvoDoItem;
    private const float TEMPO_DE_ANIMA_CURA_1 = 1.5f;

    public ItemDeAtributoConsumivelBase(ContainerDeCaracteristicasDeItem C) : base(C) { }

    public override void IniciaUsoDeMenu(GameObject dono)
    {
        GameController.g.HudM.P_EscolheUsoDeItens.AtivarParaItem(EscolhiEmQuemUsar);
        PainelMensCriature.p.AtivarNovaMens(string.Format(
            bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),
            item.nomeEmLinguas(ID)
            ), 26);
    }

    protected virtual void EscolhiEmQuemUsar(int indice)
    {
        throw new System.NotImplementedException();
    }

    protected void EscolhiEmQuemUsar(
        int indice,
        bool vaiUsar,
        bool tipoCerto, 
        TipoQuantitativo tipoQ,
        int valor,
        int corrente,
        int maximo,
        nomeTipos recuperaDoTipo)
    {
        CharacterManager manager = GameController.g.Manager;
        CriatureBase C = manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        if (vaiUsar && tipoCerto)
        {
            RetirarUmItem(manager, this, 1, FluxoDeRetorno.menuHeroi);
            ItemQuantitativo.AplicacaoDoItemComMenu(manager, C, tipoQ, valor);
        }
        else if (!tipoCerto)
        {
            MensDeUsoDeItem.MensNaoTemOTipo(recuperaDoTipo.ToString());
        }
        else if (corrente >= maximo)
        {
            MensDeUsoDeItem.MensDeNaoPrecisaDesseItem(C.NomeEmLinguas);
        }
        else if (corrente <= 0)
        {
            MensDeUsoDeItem.MensDeMorto(C.NomeEmLinguas);
        }
    }

    public override bool AtualizaUsoDeMenu()
    {
        return false;
    }

    protected void IniciaUsoDesseItem(GameObject dono,bool podeUsar,bool temTipo = true,nomeTipos nomeDoTipo = nomeTipos.nulo)
    {
        Manager = GameController.g.Manager;
        CriatureAlvoDoItem = Manager.CriatureAtivo;
        if (podeUsar && temTipo && RetirarUmItem(Manager, this, 1))
        {
            InicializacaoComum(dono, Manager.CriatureAtivo.transform);
            Estado = EstadoDeUsoDeItem.animandoBraco;
        }
        else
        {
            Estado = EstadoDeUsoDeItem.finalizaUsaItem;
            if(!temTipo)
                PainelMensCriature.p.AtivarNovaMens(string.Format(
                    bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[3], nomeDoTipo), 30, 5);
            else if (!podeUsar)
                PainelMensCriature.p.AtivarNovaMens(string.Format(
                    bancoDeTextos.falacoes[heroi.lingua]["mensLuta"][2],
                    CriatureAlvoDoItem.MeuCriatureBase.NomeEmLinguas), 30, 5);
        }
    }

    protected bool AtualizaUsoDesseItem(DoJogo particula)
    {

        switch (Estado)
        {
            case EstadoDeUsoDeItem.animandoBraco:
                if (!AnimaB.AnimaTroca(true))
                {
                    Estado = EstadoDeUsoDeItem.aplicandoItem;
                    Manager.Mov.Animador.ResetaTroca();
                    AuxiliarDeInstancia.InstancieEDestrua(particula, CriatureAlvoDoItem.transform.position, 1);
                }
            break;
            case EstadoDeUsoDeItem.aplicandoItem:
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TEMPO_DE_ANIMA_CURA_1)
                {
                    Recuperacao();

                    GameController.g.HudM.AtualizaHudHeroi(CriatureAlvoDoItem.MeuCriatureBase);
                    Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                    return false;
                }
            break;
            case EstadoDeUsoDeItem.finalizaUsaItem:
                return false;
            //break;
            case EstadoDeUsoDeItem.nulo:
                Debug.Log("alcançou estado nulo para " + ID.ToString());
            break;
        }
        return true;
    }

    public virtual void Recuperacao()
    {
        throw new System.NotImplementedException();
    }
}
