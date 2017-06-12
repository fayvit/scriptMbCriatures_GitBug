using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCfalasCondicionais : NPCdeConversa
{
    [SerializeField]private FalasCondicionais[] falas;

    [System.Serializable]
    private class FalasCondicionais
    {
        public KeyShift chaveCondicionalDaConversa;
        public ChaveDeTexto chaveDeTextoDaConversa;
    }

    void VerificaQualFala()
    {
        
        for (int i = falas.Length; i > 0; i--)
            if (!GameController.g.MyKeys.VerificaAutoShift(falas[i-1].chaveCondicionalDaConversa))
                conversa = bancoDeTextos.RetornaListaDeTextoDoIdioma(falas[i-1].chaveDeTextoDaConversa).ToArray();
        // conversa é uma variavel protected da classe pai
        
    }

    override public void IniciaConversa(Transform Destrutivel)
    {
        VerificaQualFala();
        base.IniciaConversa(Destrutivel);
    }
}
