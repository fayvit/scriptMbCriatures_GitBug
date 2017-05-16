using UnityEngine;

[System.Serializable]
public class ContainerDeLoadSaves : UiDeOpcoes
{
    private System.Action<int> acao;
    private PropriedadesDeSave[] lista;

    public void IniciarHud(
        System.Action<int> acao,
        PropriedadesDeSave[] lista
        )
    {
        //this.opcoes = txDeOpcoes;
        this.acao += acao;
        this.lista = lista;
        IniciarHUD(lista.Length, TipoDeRedimensionamento.horizontal);
    }

    public override void SetarComponenteAdaptavel(GameObject G, int indice)
    {
        G.GetComponent<LoadButton>().SetarBotao(acao,lista[indice],indice);
    }

    protected override void FinalizarEspecifico()
    {
        acao = null;
    }
}
