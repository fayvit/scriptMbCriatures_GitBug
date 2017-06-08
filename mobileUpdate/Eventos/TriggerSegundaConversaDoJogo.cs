using UnityEngine;
using System.Collections;

public class TriggerSegundaConversaDoJogo : TriggerDeConversaEspecial
{
    [SerializeField]private Transform primeiraPosHeroi;
    [SerializeField]private Transform segundaPosHeroi;
    [SerializeField]private Transform segundoPontoParaNPC;
    [SerializeField]private Transform posFinalDoNPC;
    [SerializeField]private Transform pedraRemovivel;
    [SerializeField]private Transform posParaPedra;
    [SerializeField]private GameObject conjuntoDePoeira;

    private MovimentoControladoParaNPC movNPC;

    private FasesDesseTrigger fase = FasesDesseTrigger.emEspera;

    private enum FasesDesseTrigger
    {
        emEspera,
        movimentoDoHeroi,
        movimentoDeCamera,
        movimentoAteAPedra,
        empurrando,
        falas,
        falaPraSeguirDenovo,
        NpcParaNovoPonto,
        finalizar
    }

    // Use this for initialization
    new void Start()
    {
        T = bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.segundaConversaDaEntradinha).ToArray();
        EstaKey = KeyShift.fezSegundaFalaDeTuto;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        switch (fase)
        {
            case FasesDesseTrigger.movimentoDoHeroi:
                CaminheHeroi(primeiraPosHeroi.position);                
            break;
            case FasesDesseTrigger.movimentoDeCamera:
                CameraAteFalas();
            break;
            case FasesDesseTrigger.falas:
                if (DisparaT.UpdateDeTextos(T, Fota))
                {
                    Manager.Estado = EstadoDePersonagem.movimentoDeFora;
                    fase = FasesDesseTrigger.movimentoAteAPedra;
                }
            break;
            case FasesDesseTrigger.movimentoAteAPedra:
                Vector3 pos = segundaPosHeroi.position;
                if (Vector3.Distance(Manager.transform.position, pos) > 0.5f)
                {
                    Manager.Mov.AplicadorDeMovimentos((pos - Manager.transform.position).normalized);
                }
                else
                {
                    EntrarNaFaseDoEmpurrao();
                    conjuntoDePoeira.SetActive(true);
                }
            break;
            case FasesDesseTrigger.empurrando:
                if (Vector3.Distance(pedraRemovivel.position, posParaPedra.position) > 0.5f)
                    pedraRemovivel.position += Vector3.forward * Time.deltaTime;
                else
                {
                    Manager.transform.parent = null;
                    Alvo.parent = null;
                    Manager.Mov.Animador.ForcarPadrao();
                    movNPC.Mov.Animador.ForcarPadrao();
                    Destroy(conjuntoDePoeira);
                    DisparaT.IniciarDisparadorDeTextos();
                    T = bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.continueMeSeguindo).ToArray();
                    fase = FasesDesseTrigger.falaPraSeguirDenovo;
                }
            break;
            case FasesDesseTrigger.falaPraSeguirDenovo:
                if (DisparaT.UpdateDeTextos(T, Fota))
                {
                    AplicadorDeCamera.cam.FocarBasica(Alvo, 10, 10);
                    movNPC.InsereElementosDeControle(Alvo.gameObject, segundoPontoParaNPC);
                    fase = FasesDesseTrigger.NpcParaNovoPonto;
                }
            break;
            case FasesDesseTrigger.NpcParaNovoPonto:
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
        Alvo.rotation = Quaternion.LookRotation(Vector3.left);
    }

    void EntrarNaFaseDoEmpurrao()
    {
        Manager.Estado = EstadoDePersonagem.parado;
        Manager.Mov.AplicadorDeMovimentos(Vector3.zero);
        Manager.Mov.Animador.AnimaEmpurra();
        Manager.transform.rotation = Quaternion.identity;
        AplicadorDeCamera.cam.FocarBasica(Manager.transform, 10, 7);
        fase = FasesDesseTrigger.empurrando;

        Alvo.rotation = Quaternion.identity;
        Vector3 V = Alvo.position;
        Alvo.position = new Vector3(V.x, V.y, Manager.transform.position.z);
        

        movNPC = new MovimentoControladoParaNPC();
        movNPC.InsereElementosDeControle(Alvo.gameObject, segundoPontoParaNPC);
        //movNPC.Mov.AplicadorDeMovimentos(Vector3.zero);
        movNPC.Mov.Animador.AnimaEmpurra();

        Manager.transform.SetParent(pedraRemovivel);
        Alvo.parent = pedraRemovivel;

        
    }
    protected override void MudarParaFaseDasFalas()
    {
        fase = FasesDesseTrigger.falas;
    }
    protected override void FasePosMOvimentoDoHeroi()
    {
        fase = FasesDesseTrigger.movimentoDeCamera;
    }

    protected override void FaseDoTrigger()
    {
        fase = FasesDesseTrigger.movimentoDoHeroi;
    }
}
