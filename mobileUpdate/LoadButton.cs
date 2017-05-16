using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private Text nomeDoSave;
    [SerializeField] private Text labelDoSave;

    private System.Action<int> acao;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetarBotao(System.Action<int> acao,PropriedadesDeSave prop,int indice)
    {
        this.acao += acao;
        nomeDoSave.text = prop.nome;
        labelDoSave.text = "Save " + indice;
    }

    public void BotaoCarregar()
    {

    }

    public void BotaoExcluir()
    {

    }
}
