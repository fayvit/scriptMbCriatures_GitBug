using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitialSceneManager : MonoBehaviour
{
    [SerializeField]private GameObject btnNovoJogo;
    [SerializeField]private GameObject btnCarregarJogo;
    [SerializeField]private GameObject btnPrimeiroJogo;
    [SerializeField]private InputTextDoCriandoNovoJogo pDoInput;
    [SerializeField]private ContainerDeLoadSaves containerDeLoads;

    private LoadAndSaveGame salvador = new LoadAndSaveGame();
    private List<PropriedadesDeSave> lista;
    // Use this for initialization
    void Start()
    {
        lista = (List<PropriedadesDeSave>)(salvador.CarregarArquivo("criaturesGames.ori"));
        lista.Sort();

        bool primeiro = true;

        if(lista!=null)
            if (lista.Count > 0)
                primeiro = false;

        btnNovoJogo.SetActive(!primeiro);
        btnCarregarJogo.SetActive(!primeiro);
        btnPrimeiroJogo.SetActive(primeiro);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotaoNovoJogo()
    {
        BtnsManager.DesligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        pDoInput.Iniciar();
    }

    public void BotaoCarregarJogo()
    {
        BtnsManager.DesligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        containerDeLoads.IniciarHud(EscolhiSave, lista.ToArray());
    }

    void EscolhiSave(int indice)
    {

    }

    public void FecharLoadContainer()
    {
        BtnsManager.ReligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        containerDeLoads.FinalizarHud();
        Start();
    }

    public void FecharInputText()
    {
        BtnsManager.ReligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        pDoInput.gameObject.SetActive(false);
        Start();
    }
}

[System.Serializable]
public struct PropriedadesDeSave:System.IComparable
{
    public string nome;
    public System.DateTime ultimaJogada;

    public int CompareTo(object obj)
    {
        return System.DateTime.Compare(((PropriedadesDeSave)obj).ultimaJogada,ultimaJogada);
    }
}
