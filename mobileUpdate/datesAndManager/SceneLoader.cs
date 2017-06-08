using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class SceneLoader:MonoBehaviour
{

    [SerializeField]private LoadBar loadBar;

    private SaveDates S;
    private AsyncOperation[] a2;
    private FasesDoLoad fase = FasesDoLoad.emEspera;
    private bool podeIr = false;
    private int indiceDoJogo = 0;
    private float tempo = 0;

    private const float tempoMin = 1;

    private enum FasesDoLoad
    {
        emEspera,
        carregando,
        escurecendo,
        clareando
    }

    public void CenaDoCarregamento(int indice)
    {
        DontDestroyOnLoad(gameObject);
        indiceDoJogo = indice;
        SceneManager.LoadScene("CenaDeCarregamento");
        SceneManager.sceneLoaded += IniciarCarregamento;
    }

    void IniciarCarregamento(Scene cena,LoadSceneMode mode)
    {
        Debug.Log("vez");
        loadBar = FindObjectOfType<LoadBar>();

        SceneManager.LoadSceneAsync("comunsDeFase", LoadSceneMode.Additive);
        SceneManager.sceneLoaded -= IniciarCarregamento;
        SceneManager.sceneLoaded += CarregouComuns;
    }

    void CarregouComuns(Scene cena, LoadSceneMode mode)
    {
        ComunsCarregado();
    }

    void ComunsCarregado()
    {
        if (ExistenciaDoController.AgendaExiste(ComunsCarregado, this))
            {
            fase = FasesDoLoad.carregando;
            S = new LoadAndSaveGame().Load(indiceDoJogo);
            if (S == null)
            {
                a2 = new AsyncOperation[1];
                a2[0] = SceneManager.LoadSceneAsync("MbInfinity", LoadSceneMode.Additive);
            }
            else
            {

                a2 = new AsyncOperation[S.VariaveisChave.CenasAtivas.Count];
                for (int i = 0; i < a2.Length; i++)
                {
                    a2[i] = SceneManager.LoadSceneAsync(S.VariaveisChave.CenasAtivas[i].ToString(), LoadSceneMode.Additive);
                }
            }
            Time.timeScale = 0;

            SceneManager.sceneLoaded -= CarregouComuns;
            SceneManager.sceneLoaded += SetarCenaPrincipal;
        }
    }

    void ComoPode()
    {

        if (ExistenciaDoController.AgendaExiste(ComoPode, this))
        {
            Debug.Log(GameController.g+"  segunda vez");
            CharacterManager manager = GameController.g.Manager;
            AplicadorDeCamera.cam.transform.position = S.Posicao + new Vector3(0, 12, -10);//new Vector3(483, 12f, 745);
            manager.transform.position = S.Posicao;//new Vector3(483,1.2f,755);  
            manager.transform.rotation = S.Rotacao;
            GameController.g.ReiniciarContadorDeEncontro();

            if (manager.CriatureAtivo != null)
            {
                MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
                manager.InserirCriatureEmJogo();
                manager.CriatureAtivo.transform.position = S.Posicao + new Vector3(0, 0, 1);//new Vector3(483, 1.2f, 756);
            }

            manager.Dados = S.Dados;
            manager.Dados.ZeraUltimoUso();
            GameController.g.MyKeys = S.VariaveisChave;
            GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);

            podeIr = true;
        }
    }

    void SetarCenaPrincipal(Scene scene, LoadSceneMode mode)
    {
        if (S != null)
        {
            Debug.Log(S.VariaveisChave.CenaAtiva.ToString()+" : "+ scene.name);
            if (scene.name == S.VariaveisChave.CenaAtiva.ToString() && GameController.g)
            {
                InvocarSetScene(scene);
                SceneManager.sceneLoaded -= SetarCenaPrincipal;

                Debug.Log(GameController.g);

                ComoPode();

                if (scene.name == NomesCenas.cavernaInicial.ToString())
                {
                    Debug.Log("cavernaInicial");
                }

                
            }
        }
        else
        if (scene.name != "comunsDeFase")
        {
            podeIr = true;
            InvocarSetScene(scene);
            SceneManager.sceneLoaded -= SetarCenaPrincipal;


            CharacterManager manager = GameController.g.Manager;
            AplicadorDeCamera.cam.transform.position = new Vector3(483, 12f, 745);
            manager.transform.position = new Vector3(483, 1.2f, 755);
            GameController.g.ReiniciarContadorDeEncontro();

            if (manager.CriatureAtivo != null)
            {
                MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
                manager.InserirCriatureEmJogo();
                manager.CriatureAtivo.transform.position = new Vector3(483, 1.2f, 756);
            }

            GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);
        }

        
    }

    IEnumerator setarScene(Scene scene)
    {
        yield return new WaitForSeconds(0.5f);
        InvocarSetScene(scene);
    }

    public void InvocarSetScene(Scene scene)
    {
        Debug.Log(scene.name);
        SceneManager.SetActiveScene(scene);
        Debug.Log(GameController.g+" : "+scene.name);
        if (SceneManager.GetActiveScene() != scene)
            StartCoroutine(setarScene(scene));

        Debug.Log("nomeAtiva: " + SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        switch (fase)
        {
            case FasesDoLoad.carregando:
                
                tempo += Time.fixedDeltaTime;

                float progresso = 0;

                for (int i = 0; i < a2.Length; i++)
                {
                    progresso += a2[i].progress;
                }

                progresso /= a2.Length;

                //Debug.Log(progresso + " : " + (tempo / tempoMin) + " : " + Mathf.Min(progresso, tempo / tempoMin, 1));

                loadBar.ValorParaBarra(Mathf.Min(progresso, tempo / tempoMin, 1));

                if (podeIr && tempo >= tempoMin)
                {
                    SceneManager.MoveGameObjectToScene(GameObject.Find("EventSystem"), SceneManager.GetSceneByName("comunsDeFase"));
                    pretoMorte pm = GameController.g.gameObject.AddComponent<pretoMorte>();
                    pm.vel = 2;
                    fase = FasesDoLoad.escurecendo;
                    tempo = 0;
                }
                
            break;
            case FasesDoLoad.escurecendo:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.95f)
                {
                    GameObject.FindObjectOfType<pretoMorte>().entrando = false;
                    FindObjectOfType<Canvas>().enabled = false;
                    fase = FasesDoLoad.clareando;
                    SceneManager.SetActiveScene(
                       SceneManager.GetSceneByName(GameController.g.MyKeys.CenaAtiva.ToString()));
                    GameController.g.Salvador.SalvarAgora();
                    Time.timeScale = 1;
                    tempo = 0;
                }
            break;
            case FasesDoLoad.clareando:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.5f)
                {
                    SceneManager.UnloadSceneAsync("CenaDeCarregamento");
                    Destroy(gameObject);
                }
            break;
        }
    }
}
