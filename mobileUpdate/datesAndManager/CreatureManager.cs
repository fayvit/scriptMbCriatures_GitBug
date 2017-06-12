﻿using UnityEngine;
using System.Collections;

public class CreatureManager : MonoBehaviour
{
    [SerializeField]private CreatureState estado = CreatureState.seguindo;
    [SerializeField]private Transform tDono;
    [SerializeField]private CriatureBase meuCriatureBase;
    [SerializeField]private IA_Base ia;
    [SerializeField]private MovimentacaoBasica mov;

    private AndroidController controle;
    
    public enum CreatureState
    {
        aPasseio,
        seguindo,
        morto,
        selvagem,
        aplicandoGolpe,
        emDano,
        emLuta,
        parado
    }

    public CriatureBase MeuCriatureBase
    {
        get { return meuCriatureBase; }
        set { meuCriatureBase = value; }
    }

    public Transform TDono
    {
        get { return tDono; }
        set { tDono = value; }
    }

    public CreatureState Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public IA_Base IA
    {
        get { return ia; }
        set { ia = value; }
    }

    public MovimentacaoBasica Mov
    {
        get {
            if (mov == null)
                SetaMov();
            return mov;

        }
    }

    public void MudaParaEstouEmDano()
    {
        estado = CreatureState.emDano;
        ia.SuspendeNav();
    }


    public bool MudaAplicaGolpe()
    {
        bool retorno = false;
        if (estado == CreatureState.aPasseio || estado==CreatureState.emLuta ||estado==CreatureState.selvagem)
        {
            estado = CreatureState.aplicandoGolpe;
            retorno = true;
        }else
            Debug.LogError("estado indefinido");

        return retorno;
    }
    public bool LiberaMovimento(CreatureState esseEstado)
    {
        if (estado == esseEstado)
        {
            //Debug.Log("libera MOvimento: "+gameObject.name);

            if (name == "CriatureAtivo")
                estado = GameController.g.estaEmLuta ? CreatureState.emLuta : CreatureState.aPasseio;
            else
                estado = CreatureState.selvagem;

            return true;
        }
        else
            return false;
    }

    // Use this for initialization
    void Start()
    {
        mov = null;
        ia.Start(this);
        //personagemG2.Start();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case CreatureState.parado:
            case CreatureState.emDano:
            case CreatureState.aplicandoGolpe:
                if (mov != null)
                    mov.AplicadorDeMovimentos(Vector3.zero, meuCriatureBase.CaracCriature.distanciaFundamentadora, transform);
                else
                    SetaMov();
            break;
            case CreatureState.seguindo:
            case CreatureState.selvagem:
                ia.Update();
            break;
            case CreatureState.aPasseio:
            case CreatureState.emLuta:
                Vector3 dir = Vector3.zero;
                if (controle)
                {
                    dir = controle.ValorParaEixos();
                    if (estado == CreatureState.emLuta)
                        dir = direcaoInduzida(dir.x, dir.z);
                }else
                    controle = FindObjectOfType<AndroidController>();

                if (mov == null)
                {
                    SetaMov();
                }
                else
                {
                    mov.AplicadorDeMovimentos(dir, meuCriatureBase.CaracCriature.distanciaFundamentadora,transform);
                }

            break;
        }
    }

    void SetaMov()
    {
        mov = new MovimentacaoBasica(
                       meuCriatureBase.Mov, new ElementosDeMovimentacao()
                       {
                           animador = new AnimadorHumano(GetComponent<Animator>()),
                           controle = GetComponent<CharacterController>(),
                           transform = transform
                       }
                        );
    }

    Vector3 direcaoInduzida(float h, float v)
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 retorno = cameraTransform.TransformDirection(Vector3.forward);
        retorno = Vector3.ProjectOnPlane(retorno, Vector3.up).normalized;
        retorno = v * retorno - h * (Vector3.Cross(retorno, Vector3.up));
        
        return retorno;
    }

    void AplicaGolpe()
    {
        Atributos A = MeuCriatureBase.CaracCriature.meusAtributos;
        IGolpeBase gg = meuCriatureBase.GerenteDeGolpes.meusGolpes[meuCriatureBase.GerenteDeGolpes.golpeEscolhido];

        if ((mov.NoChao(meuCriatureBase.CaracCriature.distanciaFundamentadora) || gg.PodeNoAr))
        {
            if (!DisparadorDoGolpe.Dispara(meuCriatureBase, gameObject))
            {
                string[] textos = bancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usoDeGolpe).ToArray();
                if (gg.UltimoUso + gg.TempoDeReuso >= Time.time)
                    PainelMensCriature.p.AtivarNovaMens(
                        string.Format(textos[0], comandos.mostradorDeTempo(gg.UltimoUso - (Time.time - gg.TempoDeReuso)))
                        , 25, 2);
                else if (A.PE.Corrente < gg.CustoPE)
                    PainelMensCriature.p.AtivarNovaMens(textos[1], 25, 2);
            }
        }
    }

    public void ComandoDeAtacar()
    {
        if (estado == CreatureState.aPasseio || estado == CreatureState.emLuta)
            AplicaGolpe();
    }

    public void IniciaPulo()
    {
        
        if (!meuCriatureBase.Mov.caracPulo.estouPulando && (estado == CreatureState.aPasseio ||estado==CreatureState.emLuta))
            mov._Pulo.IniciaAplicaPulo();
    }

    public void PararCriatureNoLocal()
    {
        estado = CreatureState.parado;
        ia.SuspendeNav();
    }
}


