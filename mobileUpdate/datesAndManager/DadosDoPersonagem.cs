using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DadosDoPersonagem
{
    private List<CriatureBase> criaturesAtivos = new List<CriatureBase>();
    private List<CriatureBase> criaturesArmagedados = new List<CriatureBase>();
    private List<MbItens> itens = new List<MbItens>();
    private UltimoArmagedomVisitado ultimoArmagedom = new UltimoArmagedomVisitado(new Vector3(483, 1.2f, 755),NomesCenas.MbInfinity);
    private int cristais = 0;
    private int criatureSai = 1;
    public int itemSai = 0;    
    public int maxCarregaveis = 5;

    public List<CriatureBase> CriaturesAtivos
    {
        get { return criaturesAtivos; }
        set { criaturesAtivos = value; }
    }

    public List<CriatureBase> CriaturesArmagedados
    {
        get { return criaturesArmagedados; }
        set { criaturesArmagedados = value; }
    }

    public List<MbItens> Itens
    {
        get { return itens; }
        set { itens = value; }
    }

    public int CriatureSai
    {
        get { return criatureSai; }
        set { criatureSai = value; }
    }

    public UltimoArmagedomVisitado UltimoArmagedom
    {
        get { return ultimoArmagedom; }
        set { ultimoArmagedom = value; }
    }

    public int Cristais
    {
        get { return cristais; }
        set { cristais = value; }
    }

    public void InicializadorDosDados()
    {
        CriaturesAtivos = new List<CriatureBase>() {
             new CriatureBase(nomesCriatures.Florest,7)
        };

        //CriaturesAtivos[1].CaracCriature.meusAtributos.PV.Corrente = 0;
        //CriaturesAtivos[2].CaracCriature.meusAtributos.PV.Corrente = 2;

        

        Itens = new List<MbItens>()
        {
            PegaUmItem.Retorna(nomeIDitem.maca,10),
            PegaUmItem.Retorna(nomeIDitem.regador,10)
        };
        /*
        itens.Add(new item(nomeIDitem.maca) { estoque = 20 });
        itens.Add(new item(nomeIDitem.cartaLuva) { estoque = 3 });
        itens.Add(new item(nomeIDitem.pergArmagedom) { estoque = 7 });
        itens.Add(new item(nomeIDitem.pergSabre) { estoque = 5 });
        itens.Add(new item(nomeIDitem.pergSaida) { estoque = 5 });
        itens.Add(new item(nomeIDitem.pergGosmaDeInseto) { estoque = 8 });
        itens.Add(new item(nomeIDitem.pergGosmaAcida) { estoque = 8 });
        itens.Add(new item(nomeIDitem.pergMultiplicar) { estoque = 7 });
        itens.Add(new item(nomeIDitem.estatuaMisteriosa) { estoque = 1 });
        */


    }

    public bool TemCriatureVivo()
    {
        bool retorno = false;
        for (int i = 0; i < CriaturesAtivos.Count; i++)
        {
            if (CriaturesAtivos[i].CaracCriature.meusAtributos.PV.Corrente > 0)
                retorno = true;
        }

        return retorno;
    }

    public void TodosCriaturesPerfeitos()
    {
        for (int i = 0; i < CriaturesAtivos.Count; i++)
        {
            CriatureBase.EnergiaEVidaCheia(CriaturesAtivos[i]);
        }
    }

    public int TemItem(nomeIDitem nome)
    {
        int tanto = 0;
        for (int i = 0; i < Itens.Count; i++)
        {
            if (Itens[i].ID == nome)
                tanto += Itens[i].Estoque;
        }

        return tanto;
    }

    public void AdicionaItem(nomeIDitem nomeItem, int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            AdicionaItem(nomeItem);
        }
    }

    public void AdicionaItem(nomeIDitem nomeItem)
    {
        MbItens I = PegaUmItem.Retorna(nomeItem);
        bool foi = false;
        if (I.Acumulavel > 1)
        {

            int ondeTem = -1;
            for (int i = 0; i < Itens.Count; i++)
            {
                if (Itens[i].ID == I.ID)
                {
                    if (Itens[i].Estoque < Itens[i].Acumulavel)
                    {
                        if (!foi)
                        {
                            ondeTem = i;
                            foi = true;
                        }
                    }
                }
            }

            if (foi)
            {
                Itens[ondeTem].Estoque++;
            }
            else
            {
                Itens.Add(PegaUmItem.Retorna(nomeItem));
            }
        }
        else
        {
            Itens.Add(PegaUmItem.Retorna(nomeItem));
        }
    }

    public void ZeraUltimoUso()
    {
        for (int i = 0; i < criaturesAtivos.Count; i++)
        {
            for (int j = 0; j < criaturesAtivos[i].GerenteDeGolpes.meusGolpes.Count; j++)
            {
                criaturesAtivos[i].GerenteDeGolpes.meusGolpes[j].UltimoUso = 0;
            }
        }

        for (int i = 0; i < criaturesArmagedados.Count; i++)
        {
            for (int j = 0; j < criaturesArmagedados[i].GerenteDeGolpes.meusGolpes.Count; j++)
            {
                criaturesArmagedados[i].GerenteDeGolpes.meusGolpes[j].UltimoUso = 0;
            }
        }

    }
}

[System.Serializable]
public struct UltimoArmagedomVisitado
{
    private  NomesCenas nomeDaCena;
    private  float[] V;

    public UltimoArmagedomVisitado(Vector3 pos, NomesCenas cena)
    {
        V = new float[3] { pos[0], pos[1], pos[2] };
        nomeDaCena = cena;
    }

    public NomesCenas NomeDaCena
    {
        get { return nomeDaCena; }
    }

    public Vector3 posHeroi
    {
        private set { }
        get
        {
            Vector3 V2 = new Vector3(V[0], V[1], V[2]);
            return V2;
        }
    }
}