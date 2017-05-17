using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitialSceneManager : MonoBehaviour
{
    public static InitialSceneManager i;

    [SerializeField]private GameObject btnNovoJogo;
    [SerializeField]private GameObject btnCarregarJogo;
    [SerializeField]private GameObject btnPrimeiroJogo;
    [SerializeField]private InputTextDoCriandoNovoJogo pDoInput;
    [SerializeField]private ContainerDeLoadSaves containerDeLoads;
    [SerializeField]private PainelUmaMensagem umaMensagem;
    [SerializeField]private PainelDeConfirmacao confirmacao;
    [SerializeField]private SceneLoader loadScene;

    private LoadAndSaveGame salvador = new LoadAndSaveGame();
    private List<PropriedadesDeSave> lista;

    
    public SceneLoader LoadScene
    {
        get { return loadScene; }
    }
    
    public PainelDeConfirmacao Confirmacao
    {
        get { return confirmacao; }
    }

    public PainelUmaMensagem UmaMensagem
    {
        get { return umaMensagem; }
    }

    // Use this for initialization
    void Start()
    {        
        i = this;        
        AtualizaLista();
    }

    public void AtualizaLista()
    {
        lista = (List<PropriedadesDeSave>)(salvador.CarregarArquivo("criaturesGames.ori"));

        bool primeiro = true;

        if (lista != null)
            if (lista.Count > 0)
            {
                primeiro = false;
                lista.Sort();
            }

        btnNovoJogo.SetActive(!primeiro);
        btnCarregarJogo.SetActive(!primeiro);
        btnPrimeiroJogo.SetActive(primeiro);
    }

    // Update is called once per frame
    void Update()
    {
        loadScene.Update();
    }

    public void BotaoNovoJogo()
    {
        BtnsManager.DesligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        pDoInput.Iniciar();
    }

    public void BotaoCarregarJogo()
    {
        BtnsManager.DesligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        containerDeLoads.IniciarHud(EscolhiSave,EscolhiDelete, lista.ToArray());
    }

    void EscolhiDelete(int indice)
    {
        FecharLoadContainer();

        PropriedadesDeSave p = lista[indice];
        lista = (List<PropriedadesDeSave>)(salvador.CarregarArquivo("criaturesGames.ori"));

        salvador.ExcluirArquivo("criatures.ori" + p.indiceDoSave);

        lista.Remove(p);

        
        salvador.SalvarArquivo("criaturesGames.ori", lista);

        lista.Sort();

        if (lista.Count > 0)
            BotaoCarregarJogo();
        else
            AtualizaLista();

    }

    void EscolhiSave(int indice)
    {
        PropriedadesDeSave p = lista[indice];

        lista = (List<PropriedadesDeSave>)(salvador.CarregarArquivo("criaturesGames.ori"));
        indice = lista.IndexOf(p);

        lista[indice] =new PropriedadesDeSave()
        {
            ultimaJogada = System.DateTime.Now,
            nome = lista[indice].nome,
            indiceDoSave = lista[indice].indiceDoSave
        };

        salvador.SalvarArquivo("criaturesGames.ori", lista);
        LoadScene.IniciarCarregamento(lista[indice].indiceDoSave);
    }

    public void FecharLoadContainer()
    {
        BtnsManager.ReligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        containerDeLoads.FinalizarHud();
        AtualizaLista();
    }

    public void FecharInputText()
    {
        BtnsManager.ReligarBotoes(btnCarregarJogo.transform.parent.gameObject);
        pDoInput.gameObject.SetActive(false);
        AtualizaLista();
    }
}

[System.Serializable]
public struct PropriedadesDeSave:System.IComparable
{
    public string nome;
    public int indiceDoSave;
    public System.DateTime ultimaJogada;

    public int CompareTo(object obj)
    {
        return System.DateTime.Compare(((PropriedadesDeSave)obj).ultimaJogada,ultimaJogada);
    }
}
