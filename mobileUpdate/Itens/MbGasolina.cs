[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da Gasolina
/// </summary>
public class MbGasolina : ItemDeEnergiaBase
{
    public MbGasolina(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.gasolina)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Fogo;
        valorDeRecuperacao = 40;
    }

    /*
    [System.NonSerialized]private CreatureManager CriatureAlvoDoItem;
    private const float TEMPO_DE_ANIMA_CURA_1 = 1.5f;
    private const nomeTipos recuperaDoTipo = nomeTipos.Fogo;

    public MbGasolina(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.gasolina)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
    }

    public override void IniciaUsoDeMenu(GameObject dono)
    {
        GameController.g.HudM.P_EscolheUsoDeItens.AtivarParaItem(EscolhiEmQuemUsar);
        PainelMensCriature.p.AtivarNovaMens(string.Format(
            bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),
            item.nomeEmLinguas(ID)
            ), 26);
    }

    void EscolhiEmQuemUsar(int indice)
    {
        CharacterManager manager = GameController.g.Manager;
        CriatureBase C = manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        if (ItemQuantitativo.UsaItemDeEnergia(C) && C.CaracCriature.TemOTipo(recuperaDoTipo))
        {
            RetirarUmItem(manager, this, 1, FluxoDeRetorno.menuHeroi);
            ItemQuantitativo.AplicacaoDoItemComMenu(manager, C, TipoQuantitativo.PE, 40);
        }
        else if (A.PE.Corrente <= 0)
        {
            MensDeUsoDeItem.MensDeMorto(C.NomeEmLinguas);
        }
        else if (A.PE.Corrente >= A.PE.Maximo)
        {
            MensDeUsoDeItem.MensDeNaoPrecisaDesseItem(C.NomeEmLinguas);
        }
        else if (!C.CaracCriature.TemOTipo(recuperaDoTipo))
        {
            MensDeUsoDeItem.MensNaoTemOTipo(recuperaDoTipo.ToString());
        }
    }

    public override bool AtualizaUsoDeMenu()
    {
        return false;
    }

    public override void IniciaUsoComCriature(GameObject dono)
    {
        IniciaUsoDaMaca(dono);
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDaMaca();
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        IniciaUsoDaMaca(dono);
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDaMaca();
    }

    private void IniciaUsoDaMaca(GameObject dono)
    {
        Manager = GameController.g.Manager;
        CriatureAlvoDoItem = Manager.CriatureAtivo;
        if (ItemQuantitativo.UsaItemDeEnergia(CriatureAlvoDoItem.MeuCriatureBase) 
            && CriatureAlvoDoItem.MeuCriatureBase.CaracCriature.TemOTipo(recuperaDoTipo) 
            && RetirarUmItem(Manager, this, 1))
        {
            InicializacaoComum(dono, Manager.CriatureAtivo.transform);
            Estado = EstadoDeUsoDeItem.animandoBraco;
        }
        else
        {
            Estado = EstadoDeUsoDeItem.finalizaUsaItem;
            if (!CriatureAlvoDoItem.MeuCriatureBase.CaracCriature.TemOTipo(recuperaDoTipo))
                PainelMensCriature.p.AtivarNovaMens(string.Format(
               bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[3], recuperaDoTipo.ToString()), 30, 5);
            else if (!ItemQuantitativo.UsaItemDeEnergia(CriatureAlvoDoItem.MeuCriatureBase))
                PainelMensCriature.p.AtivarNovaMens(string.Format(
                    bancoDeTextos.falacoes[heroi.lingua]["mensLuta"][2],
                    CriatureAlvoDoItem.MeuCriatureBase.NomeEmLinguas), 30, 5);
        }
    }

    private bool AtualizaUsoDaMaca()
    {
        
        switch (Estado)
        {
            case EstadoDeUsoDeItem.animandoBraco:
                if (!AnimaB.AnimaTroca(true))
                {
                    Estado = EstadoDeUsoDeItem.aplicandoItem;
                    Manager.Mov.Animador.ResetaTroca();
                    AuxiliarDeInstancia.InstancieEDestrua(DoJogo.animaPE,CriatureAlvoDoItem.transform.position, 1);
                }
            break;
            case EstadoDeUsoDeItem.aplicandoItem:
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TEMPO_DE_ANIMA_CURA_1)
                {

                    ItemQuantitativo.RecuperaPE(CriatureAlvoDoItem.MeuCriatureBase.CaracCriature.meusAtributos, 40);
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
    }*/
}
