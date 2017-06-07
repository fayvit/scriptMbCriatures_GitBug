using UnityEngine;
using System.Collections;

public class TriggerSegundaConversaDoJogo : TriggerDeConversaEspecial
{
    [SerializeField]private Transform primeiraPosHeroi;
    [SerializeField]private Transform segundaPosHeroi;

    private FasesDesseTrigger fase = FasesDesseTrigger.emEspera;

    private enum FasesDesseTrigger
    {
        emEspera,
        movimentoDoHeroi,
        movimentoDeCamera,
        movimentoAteAPedra,
        empurrando,
        falas
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
                    Manager.Estado = EstadoDePersonagem.parado;
                    fase = FasesDesseTrigger.empurrando;
                }
            break;
        }
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
