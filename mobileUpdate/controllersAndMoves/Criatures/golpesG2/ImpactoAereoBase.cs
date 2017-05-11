using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ImpactoAereoBase : GolpeBase
{
    protected CaracteristicasDeImpactoComSalto carac;
    private AtualizadorDeImpactoAereo aImpacto = new AtualizadorDeImpactoAereo();

    public ImpactoAereoBase(ContainerDeCaracteristicasDeGolpe C) : base(C) { }

    public override void IniciaGolpe(GameObject G)
    {
        NavMeshAgent nav = G.GetComponent<NavMeshAgent>();

        if (nav.enabled)
            nav.Stop();

        aImpacto.ReiniciaAtualizadorDeImpactos(G);
        DirDeREpulsao = G.transform.forward;
        AnimadorCriature.AnimaAtaque(G, "emissor");

        GameObject instancia = elementosDoJogo.el.retorna(carac.prepara.ToString());
        MonoBehaviour.Destroy(
        MonoBehaviour.Instantiate(instancia, G.transform.position, instancia.transform.rotation), 5);
    }

    public override void UpdateGolpe(GameObject G)
    {
        aImpacto.ImpactoAtivo(G, this, carac);
    }
}


[System.Serializable]
public struct CaracteristicasDeImpactoComSalto
{
    public NoImpacto noImpacto;
    public Trails trail;
    public ToqueAoChao toque;
    public PreparaSalto prepara;
    public ImpactoAereoFinal final;
    public bool parentearNoOsso;

    public CaracteristicasDeImpactoComSalto(
        NoImpacto noImpacto,
        Trails trail,
        ToqueAoChao toque,
        PreparaSalto prepara,
        ImpactoAereoFinal final,
        bool parentearNoOsso = true
        )
    {
        this.noImpacto = noImpacto;
        this.trail = trail;
        this.toque = toque;
        this.prepara = prepara;
        this.final = final;
        this.parentearNoOsso = parentearNoOsso;
    }

    public CaracteristicasDeImpacto deImpacto
    {
        get
        {
            return new CaracteristicasDeImpacto()
            {
                noImpacto = this.noImpacto.ToString(),
                nomeTrail = this.trail.ToString(),
                parentearNoOsso = this.parentearNoOsso
            };
        }
    }
}

public enum ToqueAoChao
{
    nulo,
    impactoAoChao,
    sobreSalto,
    chuvaVenenosa,
    impactoDePedraAoChao,
    aguaAoChao,
    fogoAoChao,
    eletricidadeAoChao
}

public enum PreparaSalto
{
    nulo,
    preparaImpactoAoChao
}
