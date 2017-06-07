using UnityEngine;
public class TriggerPrimeiraFalaDoJogo : TriggerDeConversaEspecial
{
    [SerializeField]private Transform alvoDaCaminhada;
    [SerializeField]private Transform posFinalDoNPC;

    private MovimentoControladoParaNPC movNPC;
    private FasesDesseTrigger fase = FasesDesseTrigger.emEspera;

    private enum FasesDesseTrigger
    {
        emEspera,
        movimentoDePersonagem,
        movimentoDeCamera,
        falas,
        npcEmMovimento,
        finalizar
    }

    new void Start()
    {
        T = bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.entradinha).ToArray();
        EstaKey = KeyShift.fezPrimeiraFalaDeTuto;
        base.Start();
    }

    void Update()
    {
        switch (fase)
        {
            case FasesDesseTrigger.movimentoDePersonagem:
                CaminheHeroi(transform.position);
            break;
            case FasesDesseTrigger.movimentoDeCamera:
                CameraAteFalas(); 
            break;
            case FasesDesseTrigger.falas:
                if (DisparaT.UpdateDeTextos(T,Fota))
                {
                    movNPC = new MovimentoControladoParaNPC();
                    movNPC.InsereElementosDeControle(Alvo.gameObject, alvoDaCaminhada);
                    AplicadorDeCamera.cam.FocarBasica(Alvo, 10, 10);
                    fase = FasesDesseTrigger.npcEmMovimento;
                }
            break;
            case FasesDesseTrigger.npcEmMovimento:
                if (movNPC.UpdatePosition())
                {
                    AplicadorDeCamera.cam.FocarBasica(Manager.transform, 10, 10);
                    Invoke("MudaPosDoNPC", 0.25f);
                    Invoke("VoltaControleDoHeroi", 0.3f);
                    Destroy(Destrutivel.gameObject);
                    fase = FasesDesseTrigger.finalizar;
                }
            break;
        }
    }

    void MudaPosDoNPC()
    {
        Alvo.position = new melhoraPos().novaPos(posFinalDoNPC.position);
        Alvo.rotation = Quaternion.identity;
    }

    protected override void FasePosMOvimentoDoHeroi()
    {
        fase = FasesDesseTrigger.movimentoDeCamera;
    }

    protected override void MudarParaFaseDasFalas()
    {
        fase = FasesDesseTrigger.falas;
    }

    protected override void FaseDoTrigger()
    {
        fase = FasesDesseTrigger.movimentoDePersonagem;
    }
}

