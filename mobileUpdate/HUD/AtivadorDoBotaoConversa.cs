using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoConversa : AtivadorDeBotao
{
    [SerializeField]protected NPCdeConversa npc;
    private Vector3 forwardInicialDoBotao;

    // Use this for initialization
    protected void Start()
    {
        npc.Start(transform);
        forwardInicialDoBotao = btn.transform.parent.forward;
        SempreEstaNoTrigger();
    }

    new protected void Update()
    {
        base.Update();

        if (btn.activeSelf)
            btn.transform.parent.forward = forwardInicialDoBotao;

        if (npc.Update())
        {
            GameController.g.Manager.AoHeroi();
        }
    }

    public void BotaoConversa()
    {
        FluxoDeBotao();

        Transform T = TransformPosDeConversa.MeAjude(transform);

        npc.IniciaConversa(T);
        
        AplicadorDeCamera.cam.InicializaCameraExibicionista(T, 1,true);
    }
}
