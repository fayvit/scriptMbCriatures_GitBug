﻿using UnityEngine;

[System.Serializable]
public class MbBastao : ImpactoBase
{
    public MbBastao() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.bastao,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 1,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.25f,//74
        tempoDeMoveMax = 0.5f,
        tempoDeDestroy = 1f,
        velocidadeDeGolpe = 28
    }
        )
    {
        carac = new CaracteristicasDeImpacto()
        {
            noImpacto = NoImpacto.impactoComum.ToString(),
            nomeTrail = Trails.umCuboETrail.ToString(),
            parentearNoOsso = true
        };
    }
}
