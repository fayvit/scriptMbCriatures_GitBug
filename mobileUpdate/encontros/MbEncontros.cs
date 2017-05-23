﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MbEncontros
{
    [SerializeField]private int minEncontro = 50;
    [SerializeField]private int maxEncontro = 300;
    [SerializeField]private float andado = 0;
    [SerializeField]private float proxEncontro = 100;

    private Vector3 posHeroi;
    private Vector3 posAnterior = Vector3.zero;
    private CharacterManager manager;
    private encontravel encontrado;
    private EncounterManager gerenteDeEncontro;
    private bool luta = false;

    public bool Luta
    {
        get{return luta;}
    }

    public CreatureManager InimigoAtivo
    {
        get { return gerenteDeEncontro.Inimigo; }
    }
    // Use this for initialization
    public void Start()
    {
        manager = MonoBehaviour.FindObjectOfType<CharacterManager>();
        posAnterior = manager.transform.position;
        gerenteDeEncontro = new EncounterManager();
    }

    // Update is called once per frame
    public void Update()
    {
        
        if (!pausaJogo.pause)
        {
            if (!luta)
                posHeroi = manager.transform.position;

            //  if (!heroiMB)
            //    heroiMB = tHeroi.GetComponent<movimentoBasico>();

            if (!lugarSeguro() && !luta && comandos.noChaoS(manager.Mov.Controle,0.01f))
            {
                andado += (posHeroi - posAnterior).magnitude;
                posAnterior = posHeroi;
            }


            if (!luta && andado >= proxEncontro)
            {
                IniciaEncontro();
            }

            if (gerenteDeEncontro.Update() && luta)
            {
                GameController.g.Salvador.SalvarAgora();         
                RetornaParaModoPasseio();
                AplicadorDeCamera.cam.GetComponent<Camera>().farClipPlane = 300;
                System.GC.Collect();
                Resources.UnloadUnusedAssets();
            }


            Debug.DrawRay(posHeroi - 40f * manager.transform.forward, 1000f * Vector3.up, Color.yellow);
        }
    }

    public void FinalizaEncontro()
    {
        gerenteDeEncontro.FinalizarEncontro();
    }

    void IniciaEncontro()
    {
        luta = true;
        andado = 0;
        proxEncontro = SorteiaPassosParaEncontro();
        encontrado = criatureEncontrado();
        gerenteDeEncontro.InicializarEncounterManager(InsereInimigoEmCampo.RetornaInimigoEmCampo(encontrado, manager), manager);
        AplicadorDeCamera.cam.GetComponent<Camera>().farClipPlane = 100;
        GameController.g.HudM.MenuDeI.FinalizarHud();
        GameController.g.HudM.Btns.btnParaCriature.interactable = false;
        InsereElementosDoEncontro.encontroPadrao(manager);
    }

    void RetornaParaModoPasseio()
    {
        MonoBehaviour.Destroy(GameObject.Find("cilindroEncontro"));
        GameController.g.HudM.Btns.btnParaCriature.interactable = true;
        heroi.emLuta = false;
        luta = false;        
        manager.AoHeroi();        
        manager.transform.position = posHeroi;
        
    }

    protected virtual bool lugarSeguro()
    {
        if ((
            /*CIDADE DE INFINITY*/
            manager.transform.position.z < 804
            &&
            manager.transform.position.z > 685
            &&
            manager.transform.position.x > 430
            &&
            manager.transform.position.x < 570
            /* FIM DE INFINITY */
            ) ||
            (
            /*CIDADE DE Ive*/
            manager.transform.position.z < 400
            &&
            manager.transform.position.z > 280
            &&
            manager.transform.position.x > 400
            &&
            manager.transform.position.x < 560
            /* FIM DE Ive */
            )
            )
            return true;
        return false;
    }

    protected float SorteiaPassosParaEncontro()
    {
        return Random.Range(minEncontro, maxEncontro);
    }

    protected virtual List<encontravel> listaEncontravel()
    {
        
        return new List<encontravel>() { //new encontravel(nomesCriatures.Nessei, 1, 8, 10),
            new encontravel(nomesCriatures.Steal, 1, 8, 10)
        };
        
        /*
        return new List<encontravel>() { new encontravel(nomesCriatures.PolyCharm,1,11,13),
            new encontravel(nomesCriatures.Urkan,1,11,13),
            new encontravel(nomesCriatures.Xuash,1,11,13),
            new encontravel(nomesCriatures.Florest,1,11,13),
            new encontravel(nomesCriatures.Arpia,1,11,13),
            new encontravel(nomesCriatures.Iruin,1,11,13),
            new encontravel(nomesCriatures.Serpente, 1, 11, 13),
            new encontravel(nomesCriatures.Nessei, 1, 11, 13),
            new encontravel(nomesCriatures.Cracler, 1, 11, 13),
            new encontravel(nomesCriatures.Flam, 1, 11, 13),
            new encontravel(nomesCriatures.Rocketler, 1, 11, 13),
            new encontravel(nomesCriatures.Aladegg, 1, 11, 13),
            new encontravel(nomesCriatures.Steal, 1, 11, 13),
            new encontravel(nomesCriatures.Babaucu, 1, 11, 13),
            new encontravel(nomesCriatures.Marak, 1, 11, 13),
            new encontravel(nomesCriatures.Escorpion, 1, 11, 13),
            new encontravel(nomesCriatures.Baratarab, 1, 11, 13)
        };*/
    }

    encontravel criatureEncontrado()
    {
        List<encontravel> encontraveis = listaEncontravel();//secaoEncontravel[IndiceDoLocal()];
        float maiorAleatorio = 0;
        for (int i = 0; i < encontraveis.Count; i++)
        {
            maiorAleatorio += encontraveis[i].taxa;
        }
        //		print(maiorAleatorio);
        float roleta = Random.Range(0, maiorAleatorio);
        //		print(roleta);
        float sum = 0;
        int retorno = -1;
        for (int i = 0; i < encontraveis.Count; i++)
        {
            sum += encontraveis[i].taxa;

            if (roleta <= sum && retorno == -1)
                retorno = i;
        }

        return encontraveis[retorno];
    }

    public void ZeraPosAnterior()
    {
        posAnterior = manager.transform.position;
        posHeroi = manager.transform.position;
        andado = 0;
    }


    /*
    Funções originalmente criadas para testes
        */

    #region TestRegion
    public void ZerarPassosParaProxEncontro()
    {
        proxEncontro = 0;
    }

    public void ColocarUmPvNoInimigo()
    {
        gerenteDeEncontro.Inimigo.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente = 1;
    }

    #endregion
}
