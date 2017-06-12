using UnityEngine;
using System.Collections;

public class PainelDeEscolhaINicial : PainelStatus
{
    [SerializeField]private GameObject painelDosElementos;

    private CriatureBase[] criaturesIniciais;
    void OnEnable()
    {
        painelDosElementos.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        criaturesIniciais = new CriatureBase[3]
            {
                new CriatureBase(nomesCriatures.Florest),
                new CriatureBase(nomesCriatures.PolyCharm),
                new CriatureBase(nomesCriatures.Xuash)
            };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BtnMeuOutro(int indice)
    {
        indiceDoSelecionado = indice;
        painelDosElementos.SetActive(true);
        InserirDadosNoPainelPrincipal(criaturesIniciais[indice]);
        AbaSelecionada(indice);
    }

    public void BtnEscolher()
    {
        BtnsManager.DesligarBotoes(gameObject);
        GameController.g.HudM.Confirmacao.AtivarPainelDeConfirmacao(SimEscolhiEsse, AindaNaoEscolhi,
            string.Format(bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.certezaDeEscolhaInicial),
            criaturesIniciais[indiceDoSelecionado].NomeEmLinguas,
            tipos.NomeEmLinguas(criaturesIniciais[indiceDoSelecionado].CaracCriature.meusTipos[0]))
            );
    }

    void FinalizaEscolhaInicial()
    {
        gameObject.SetActive(false);
        CharacterManager manager = GameController.g.Manager;
        manager.Dados.CriaturesAtivos = new System.Collections.Generic.List<CriatureBase>(){ criaturesIniciais[indiceDoSelecionado]};
        GameController.g.MyKeys.MudaShift(KeyShift.estouNoTuto,false);
        GameObject G = InicializadorDoJogo.InstanciaCriature(manager.transform, manager.Dados.CriaturesAtivos[0]);
        InicializadorDoJogo.InsereCriatureEmJogo(G, manager);
    }

    void SimEscolhiEsse()
    {
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(FinalizaEscolhaInicial,string.Format(
            bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.voceRecebeuCriature), criaturesIniciais[indiceDoSelecionado].NomeEmLinguas
            ));
    }

    void AindaNaoEscolhi()
    {
        BtnsManager.ReligarBotoes(gameObject);
    }

}
