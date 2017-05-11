using UnityEngine;
using System.Collections;

public class AtualizadorDeImpactoAereo
{
    [System.NonSerialized]private Vector3 posInicial;
    [System.NonSerialized]private Transform alvoProcurado;
    [System.NonSerialized]CharacterController controle;

    private float tempoDecorrido = 0;
    private bool adview = false;
    private bool alcancouOApceDaAltura = false;
    private bool procurouAlvo = false;

    public void ReiniciaAtualizadorDeImpactos(GameObject G)
    {
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
        if (tempoDecorrido > ativa.TempoDeMoveMin && !adview)
        {
            if (!procurouAlvo)
            {
                alvoProcurado = CriaturesPerto.procureUmBomAlvo(G);
                procurouAlvo = true;
                Debug.Log(alvoProcurado + "  esse é o alvo");
                AtualizadorDeImpactos.ajudaAtaque(alvoProcurado, G.transform);
                if (alvoProcurado != null)
                    ativa.DirDeREpulsao = (alvoProcurado.position - G.transform.position).normalized;
            }

            if (!adview)
            {
                ColisorDeGolpe.AdicionaOColisor(G, ativa, caracteristica.deImpacto, tempoDecorrido);
            }

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
    }

    private void MaisAltoQueOAlvo(Transform alvo, GameObject G,IGolpeBase ativa)
    {
        //umC.ataqueComPulo = true;
        if (!controle)
            controle = G.GetComponent<CharacterController>();

        /*
		float distanciaDeParada = 1.75f;

		float subindo = Y.gravidade;
*/





        Vector3 pontoAlvo = 3 * Vector3.up;

        if (alvo != null)
        {
            pontoAlvo += alvo.transform.position + 3 * Vector3.up - 0.35f * (G.transform.position - alvo.position);
        }


        Vector3 direcaoDoSalto = alvo == null ? G.transform.forward + 0.6f * Vector3.up : pontoAlvo - G.transform.position;



        if (Vector3.Distance(pontoAlvo, G.transform.position) < 0.5f
           ||
           Mathf.Abs(posInicial.y - G.transform.position.y) > 4
           )
        {
            alcancouOApceDaAltura = true;
        }


        if (Vector3.Distance(posInicial, G.transform.position) > 2&&
            (alcancouOApceDaAltura || tempoDecorrido > ativa.TempoDeMoveMax))
        {

            Vector3 descendo = Vector3.down;
            if (alvo != null)
            {
                descendo = descendo + 8 * (alvo.position - G.transform.position);
                descendo.Normalize();
                G.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(descendo, Vector3.up));
            }
            controle.Move(descendo * ativa.VelocidadeDeGolpe * Time.deltaTime);
            Debug.Log("descendo");
        }
        else
        {
            Debug.Log("subindo" + alvo);


            controle.Move(direcaoDoSalto.normalized * Time.deltaTime * ativa.VelocidadeDeGolpe);
        }


    }


    void AvanceEPareAbaixo(Transform alvo,GameObject G,IGolpeBase ativa)
    {
        float distanciaDeParada = 1.75f;
        float subindo = 9.8f;
        float velocidadeAvante = ativa.VelocidadeDeGolpe;
        Vector3 V = Vector3.zero;

        if (alvo)
        {
            V = alvo.position;
            AtualizadorDeImpactos.ajudaAtaque(alvo,G.transform);
        }

        if (((G.transform.position - V).magnitude > distanciaDeParada
            &&
            G.transform.position.y - posInicial.y < 4
            &&
            !alcancouOApceDaAltura)
           ||
           tempoDecorrido > 1.25f)
        {
            subindo = -11;
        }
        else if ((G.transform.position - V).magnitude <= distanciaDeParada)
        {
            velocidadeAvante = 0;
            alcancouOApceDaAltura = true;
        }
        else
        {
            alcancouOApceDaAltura = true;
        }

        controle.Move((velocidadeAvante * G.transform.forward + Vector3.down * subindo) * Time.deltaTime);
    }
}

public enum ImpactoAereoFinal
{
    AvanceEPareAbaixo,
    MaisAltoQueOAlvo
}
