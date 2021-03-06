﻿using UnityEngine;
using System.Collections;

public class AtivadorDeBotao : MonoBehaviour
{

   [SerializeField]protected GameObject btn;
   [SerializeField]protected float distanciaParaAcionar = 4.6f;
   private bool estaNoTrigger = false;    

    // Use this for initialization
    void Start()
    {

    }

    protected void FluxoDeBotao()
    {
        GameController.EntrarNoFluxoDeTexto();

        GameController.g.Manager.transform.rotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(transform.position - GameController.g.Manager.transform.position, Vector3.up));

        
        Update();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (GameController.g)
            if (GameController.g.Manager)
                if (Vector3.Distance(GameController.g.Manager.transform.position, transform.position) < distanciaParaAcionar
                    &&
                    estaNoTrigger
                    &&
                    GameController.g.Manager.Estado == EstadoDePersonagem.aPasseio
                    )
                {
                    btn.SetActive(true);
                }
                else
                {
                    btn.SetActive(false);
                }
    }
    protected void SempreEstaNoTrigger()
    {
        estaNoTrigger = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            estaNoTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            estaNoTrigger = false;
        }
    }
}
