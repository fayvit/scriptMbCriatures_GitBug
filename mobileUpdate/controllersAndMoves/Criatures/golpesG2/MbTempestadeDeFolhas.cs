using UnityEngine;

[System.Serializable]
public class MbTespestadeDeFolhas : ImpactoAereoBase
{


    public MbTespestadeDeFolhas() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.tempestadeDeFolhas,
        tipo = nomeTipos.Planta,
        carac = caracGolpe.colisao,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 18f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.65f,//74
        tempoDeMoveMax = 1.3f,
        tempoDeDestroy = 0.85f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoComum,
            Trails.tempestadeDeFolhas,
            ToqueAoChao.impactoAoChao,
            PreparaSalto.preparaImpactoAoChao,
            ImpactoAereoFinal.AvanceEPareAbaixo,
            false
            )
            ;

    }


}
