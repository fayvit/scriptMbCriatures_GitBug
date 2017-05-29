using UnityEngine;
using System.Collections;

public class PegaUmItem
{
    public static MbItens Retorna(nomeIDitem nomeItem, int estoque = 1)
    {
        MbItens retorno = new MbItens(new ContainerDeCaracteristicasDeItem());
        switch (nomeItem)
        {
            case nomeIDitem.maca:
                retorno = new MbMaca(estoque);
            break;
            case nomeIDitem.cartaLuva:
                retorno = new MbCartaLuva(estoque);
            break;
            case nomeIDitem.gasolina:
                retorno = new MbGasolina(estoque);
            break;
            case nomeIDitem.aguaTonica:
                retorno = new MbAguaTonica(estoque);
            break;
            case nomeIDitem.aura:
                retorno = new MbAura(estoque);
            break;
            case nomeIDitem.regador:
                retorno = new MbRegador(estoque);
            break;
            case nomeIDitem.ventilador:
                retorno = new MbVentilador(estoque);
            break;
            case nomeIDitem.inseticida:
                retorno = new MbInseticida(estoque);
            break;
            case nomeIDitem.pilha:
                retorno = new MbPilha(estoque);
            break;
            case nomeIDitem.estrela:
                retorno = new MbEstrela(estoque);
            break;
            case nomeIDitem.seiva:
                retorno = new MbSeiva(estoque);
            break;
            case nomeIDitem.quartzo:
                retorno = new MbQuartzo(estoque);
            break;
            case nomeIDitem.adubo:
                retorno = new MbAdubo(estoque);
            break;
            case nomeIDitem.repolhoComOvo:
                retorno = new MbRepolhoComOvo(estoque);
            break;
        }
        return retorno;
    }
}
