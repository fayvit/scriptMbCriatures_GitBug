﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public class AtualizadorDeImpactoAereo
{
    private Vector3 dirDeslocamento;
    private Vector3 posInicial;
    private Transform alvoProcurado;
    private CharacterController controle;
    private NavMeshAgent nav;

    private float tempoDecorrido = 0;
    private bool adview = false;
    private bool alcancouOApceDaAltura = false;
    private bool procurouAlvo = false;
    private bool estavaParada;
    

    public void ReiniciaAtualizadorDeImpactos(GameObject G)
    {
        if(nav==null)
            nav = G.GetComponent<NavMeshAgent>();

        estavaParada = nav.enabled;

        nav.enabled = false;

        posInicial = G.transform.position;

        if(controle == null)
            controle = G.GetComponent<CharacterController>();

        adview = false;
        tempoDecorrido = 0;
        alcancouOApceDaAltura = false;
        procurouAlvo = false;
    }

    public void ImpactoAtivo(GameObject G, IGolpeBase ativa, CaracteristicasDeImpactoComSalto caracteristica)
    {
        tempoDecorrido += Time.deltaTime;
        if (!adview)
        {
            if (!procurouAlvo)
            {
                alvoProcurado = CriaturesPerto.procureUmBomAlvo(G);
                procurouAlvo = true;
               // Debug.Log(alvoProcurado + "  esse é o alvo");
                AtualizadorDeImpactos.ajudaAtaque(alvoProcurado, G.transform);
                if (alvoProcurado != null)
                    ativa.DirDeREpulsao = (Vector3.ProjectOnPlane(alvoProcurado.position - G.transform.position,Vector3.up)).normalized;
            }
            
            ColisorDeGolpe.AdicionaOColisor(G, ativa, caracteristica.deImpacto, tempoDecorrido);
            

            adview = true;
            AnimadorCriature.AnimaAtaque(G, ativa.Nome.ToString());

        }

        if (adview)
        {
            if (caracteristica.final == ImpactoAereoFinal.MaisAltoQueOAlvo)
                MaisAltoQueOAlvo(alvoProcurado, G, ativa);
            else
                AvanceEPareAbaixo(alvoProcurado, G, ativa);
        }

        if (tempoDecorrido > ativa.TempoDeMoveMax)
            nav.enabled = estavaParada;
    }

    private void MaisAltoQueOAlvo(Transform alvo, GameObject G,IGolpeBase ativa)
    {

        Vector3 pontoAlvo = 3 * Vector3.up;

        if (alvo != null)
        {
            pontoAlvo += alvo.transform.position + 3 * Vector3.up + (G.transform.position - alvo.position).normalized;
        }


        Vector3 direcaoDoSalto = alvo == null 
            ? G.transform.forward + 0.6f * Vector3.up 
            : Vector3.Dot(
                    Vector3.ProjectOnPlane(alvo.position - G.transform.position, Vector3.up),
                    ativa.DirDeREpulsao) > 0 ? pontoAlvo - G.transform.position:G.transform.forward;



        if (Vector3.Distance(pontoAlvo, G.transform.position) < 0.35f
           ||
           Mathf.Abs(posInicial.y - G.transform.position.y) > 4
           )
        {
            alcancouOApceDaAltura = true;
            direcaoDoSalto = Vector3.ProjectOnPlane(direcaoDoSalto, Vector3.up);
        }

        

        if (Vector3.ProjectOnPlane(posInicial - G.transform.position, Vector3.up).sqrMagnitude > 25f/* distancia ao quadrado*/ &&
            (alcancouOApceDaAltura || tempoDecorrido > ativa.TempoDeMoveMax))
        {

            Vector3 descendo = Vector3.down;
            if (alvo != null)
            {

                if (Vector3.Dot(
                    Vector3.ProjectOnPlane(alvo.position - G.transform.position, Vector3.up),
                    ativa.DirDeREpulsao) < 0)
                {                    
                    descendo = descendo + ativa.DirDeREpulsao;
                }
                else
                  descendo = descendo+  0.1f*(alvo.position - G.transform.position);
                descendo.Normalize();
                
                G.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(descendo, Vector3.up));
            }
            controle.Move(descendo * ativa.VelocidadeDeGolpe * Time.deltaTime);
        }
        else
        {


            controle.Move(direcaoDoSalto.normalized * Time.deltaTime * ativa.VelocidadeDeGolpe);
        }


    }


    void AvanceEPareAbaixo(Transform alvo, GameObject G, IGolpeBase ativa)
    {
        float distanciaDeParada = 5.75f;
        float velocidadeAvante = ativa.VelocidadeDeGolpe;

        Vector3 V = posInicial+10*ativa.DirDeREpulsao;
        Vector3 project;
        if (alvo)
        {
            V = alvo.position;
            project = (Vector3.ProjectOnPlane(G.transform.position - V, Vector3.up));
          //  if (Vector3.Dot(project,G.transform.forward)>0)
            //    AtualizadorDeImpactos.ajudaAtaque(alvo, G.transform);
        }

        project = (Vector3.ProjectOnPlane(G.transform.position - V, Vector3.up));

        if ((project.magnitude > distanciaDeParada
            &&
            G.transform.position.y - posInicial.y < 4
            &&
            !alcancouOApceDaAltura)
           &&
           tempoDecorrido < ativa.TempoDeMoveMax/2)
        {
            dirDeslocamento = (velocidadeAvante * G.transform.forward + Vector3.up * 15);
        }
        else if (project.magnitude <= distanciaDeParada)
        {
            
            dirDeslocamento = Vector3.Lerp(dirDeslocamento, velocidadeAvante * G.transform.forward + Vector3.down * 9.8f, 20 * Time.deltaTime);
            alcancouOApceDaAltura = true;
        }
        else if (G.transform.position.y - posInicial.y > 4)
        {
            alcancouOApceDaAltura = true;
        }
        else
        {
                dirDeslocamento = Vector3.Lerp(dirDeslocamento, velocidadeAvante * G.transform.forward + Vector3.down * 18.8f, 20 * Time.deltaTime);
        }

        controle.Move(dirDeslocamento * Time.deltaTime);
    }

    public void ReligarNavMesh()
    {
        if (nav)
            nav.enabled = estavaParada;
    }
}

public enum ImpactoAereoFinal
{
    AvanceEPareAbaixo,
    MaisAltoQueOAlvo
}
