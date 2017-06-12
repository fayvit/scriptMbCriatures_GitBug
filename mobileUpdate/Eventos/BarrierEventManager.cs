using UnityEngine;
using System.Collections;

public class BarrierEventManager : EventoComGolpe
{    
    [SerializeField]private GameObject barreira;
    [SerializeField]private GameObject acaoEfetivada;
    [SerializeField]private GameObject finalizaAcao;
    [SerializeField]private nomesGolpes[] ativaveis;
    [SerializeField]private nomeTipos tipoParaAtivar = nomeTipos.nulo;
    [SerializeField]private KeyShift chaveEspecial = KeyShift.nula;
    [SerializeField]private string chave;
    [SerializeField]private int indiceDaMensagem = 0;
    [SerializeField]private bool todoDoTipo = false;

    private BarrierEventsState estado = BarrierEventsState.emEspera;
    private bool jaIniciaou = false;
    private float tempoDecorrido = 0;
    private float tempoDeEfetivaAcao = 2.5f;
    private float tempoDoFinalizaAcao = 1.75f;

    private enum BarrierEventsState
    {
        emEspera,
        mensAberta,
        ativou,
        apresentaFinalizaAcao
    }

    // Use this for initialization
    void Start()
    {
        if (GameController.g)
        {
            if (GameController.g.MyKeys.VerificaAutoShift(chave))
            {
                gameObject.SetActive(false);
            }
            jaIniciaou = true;
        }

        SempreEstaNoTrigger();
    }

    void VoltarAoFLuxoDeJogo()
    {
        GameController g = GameController.g;
        AndroidController.a.LigarControlador();

        g.Manager.AoHeroi();
        g.HudM.ligarControladores();
    }

    new void Update()
    {
        if (jaIniciaou)
        {
            switch (estado)
            {
                case BarrierEventsState.mensAberta:
                    if (Input.GetMouseButtonDown(0))
                    {
                        estado = BarrierEventsState.emEspera;
                        PainelMensCriature.p.EsconderMensagem();

                        VoltarAoFLuxoDeJogo();
                    }
                break;
                case BarrierEventsState.ativou:
                    tempoDecorrido += Time.deltaTime;
                    if (tempoDecorrido > tempoDeEfetivaAcao)
                    {
                        tempoDecorrido = 0;
                        finalizaAcao.SetActive(true);
                        barreira.SetActive(false);
                        estado = BarrierEventsState.apresentaFinalizaAcao;
                    }
                break;
                case BarrierEventsState.apresentaFinalizaAcao:
                    tempoDecorrido += Time.deltaTime;
                    if (tempoDecorrido > tempoDoFinalizaAcao)
                    {
                        gameObject.SetActive(false);
                        VoltarAoFLuxoDeJogo();
                    }
                break;
            }
            base.Update();
        }
        else
            Start();
    }

    private bool VerificaGolpeNaLista(nomesGolpes nomeDoGolpe)
    {
        for (int i = 0; i < ativaveis.Length; i++)
            if (ativaveis[i] == nomeDoGolpe)
                return true;

        return false;
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        if (todoDoTipo)
            if (PegaUmGolpeG2.RetornaGolpe(nomeDoGolpe).Tipo == tipoParaAtivar)
                estado = BarrierEventsState.ativou;
        if (VerificaGolpeNaLista(nomeDoGolpe))
            estado = BarrierEventsState.ativou;

        if (estado == BarrierEventsState.ativou)
        {
            FluxoDeBotao();
            acaoEfetivada.SetActive(true);
            tempoDecorrido = 0;
            GameController.g.MyKeys.MudaAutoShift(chave, true);
            GameController.g.MyKeys.MudaShift(chaveEspecial, true);
            AplicadorDeCamera.cam.Basica.NovoFoco(transform,10,10);
        }
    }

    public void BotaoInfo()
    {
        FluxoDeBotao();
        PainelMensCriature.p.AtivarNovaMens(
            bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.barreirasDeGolpes)[indiceDaMensagem]
            , 25);
        estado = BarrierEventsState.mensAberta;
        
    }
}
