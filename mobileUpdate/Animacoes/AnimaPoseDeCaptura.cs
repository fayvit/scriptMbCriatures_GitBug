﻿using UnityEngine;
using System.Collections;

public class AnimaPoseDeCaptura
{
    private CriatureBase oCapturado;
    private Animator animator;
    private FaseDoAnimaPose fase = FaseDoAnimaPose.inicia;
    private bool foiParaArmagedom = false;
    private float tempoDecorrido = 0;

    private const int TEMPO_DE_MENS_DE_CAPTURA = 10;
    private enum FaseDoAnimaPose
    {
        inicia,
        brilho2,
        insereInfos,
        mensDoArmagedom,
        finaliza
    }
    public AnimaPoseDeCaptura(CriatureBase oCapturado)
    {
        this.oCapturado = oCapturado;
        animator = GameController.g.Manager.GetComponent<Animator>();

        DadosDoPersonagem dados = GameController.g.Manager.Dados;

        if (dados.CriaturesAtivos.Count < dados.maxCarregaveis)
        {
            dados.CriaturesAtivos.Add(oCapturado);
            foiParaArmagedom = false;
        }
        else
        {
            dados.CriaturesArmagedados.Add(oCapturado);
            /*
			linhas para encher a vida e retirar status quando o Criature for para o Armagedom
			 */

            // statusTemporarioBase.limpaStatus(oCapturado, -1);
            Atributos A = oCapturado.CaracCriature.meusAtributos;
            A.PV.Corrente = A.PV.Maximo;
            A.PE.Corrente = A.PE.Maximo;

            /**************************************************/
            foiParaArmagedom = true;
        }


        animator.SetBool("travar", true);
        animator.SetBool("chama", false);
        animator.Play("capturou");
        
    }

    public bool Update()
    {
        tempoDecorrido += Time.deltaTime;
        switch (fase)
        {
            case FaseDoAnimaPose.inicia:
                AplicadorDeCamera.cam.FocarPonto(10);
                if (tempoDecorrido > 1)
                {
                    InsereBrilho();
                    tempoDecorrido = 0;
                    fase = FaseDoAnimaPose.brilho2;
                }
            break;
            case FaseDoAnimaPose.brilho2:
                if (tempoDecorrido > 1.1f)
                {
                    InsereBrilho();
                    tempoDecorrido = 0;
                    fase = FaseDoAnimaPose.insereInfos;
                }
            break;
            case FaseDoAnimaPose.insereInfos:
                if (tempoDecorrido > 0.4f)
                {
                    PainelDeCriature PC = GameController.g.HudM.P_Criature;
                    PainelMensCriature.p.AtivarNovaMens(
                        bancoDeTextos.falacoes[heroi.lingua]["tentaCapturar"][5]+
                        oCapturado.NomeEmLinguas
                        , 25);
                    PC.gameObject.SetActive(true);
                    PC.InserirDadosNoPainelPrincipal(oCapturado);

                    if (foiParaArmagedom)
                    {
                        fase = FaseDoAnimaPose.mensDoArmagedom;
                    }
                    else
                    {
                        fase = FaseDoAnimaPose.finaliza;
                        tempoDecorrido = 0;
                    }
                }
            break;
            case FaseDoAnimaPose.mensDoArmagedom:
                if (Input.GetMouseButton(0) || tempoDecorrido > TEMPO_DE_MENS_DE_CAPTURA)
                {
                    GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(() => {
                        tempoDecorrido = 11;// para finalizar imediatamente
                        fase = FaseDoAnimaPose.finaliza;
                    },string .Format(bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.foiParaArmagedom),
                    GameController.g.Manager.Dados.maxCarregaveis,
                    oCapturado.NomeEmLinguas,
                    oCapturado.CaracCriature.mNivel.Nivel
                    ));
                }
            break;
            case FaseDoAnimaPose.finaliza:
                if (Input.GetMouseButton(0) || tempoDecorrido > TEMPO_DE_MENS_DE_CAPTURA)
                {
                    animator.SetBool("travar", false);
                    PainelMensCriature.p.EsconderMensagem();
                    GameController.g.HudM.P_Criature.gameObject.SetActive(false);
                    return false;
                }
            break;
        }
        return true;
    }

    void InsereBrilho()
    {
        Vector3 maoDoHeroi = GameController.g.Manager.transform
            .Find("metarig/hips/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/palm_04_R")
                .transform.position;//+0.2f*transform.forward;
        MonoBehaviour.Instantiate(elementosDoJogo.el.retorna("luz1captura"), maoDoHeroi, Quaternion.identity);
    }
}
