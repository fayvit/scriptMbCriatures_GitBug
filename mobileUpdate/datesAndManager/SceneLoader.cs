using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class SceneLoader
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

    public void IniciarCarregamento(int indice)
    {
        fase = FasesDoLoad.carregando;
        indiceDoJogo = indice;

        SceneManager.LoadSceneAsync("comunsDeFase", LoadSceneMode.Additive);
        
        S = new LoadAndSaveGame().Load(indice);
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
        SceneManager.sceneLoaded += SetarCenaPrincipal;
    }

    void SetarCenaPrincipal(Scene scene, LoadSceneMode mode)
    {
        if (S != null)
        {
            if (scene.name == S.VariaveisChave.CenaAtiva.ToString())
            {
                podeIr = true;
                InvocarSetScene(scene);
                SceneManager.sceneLoaded -= SetarCenaPrincipal;

                CharacterManager manager = GameController.g.Manager;
                AplicadorDeCamera.cam.transform.position = S.Posicao + new Vector3(0, 12, -10);//new Vector3(483, 12f, 745);
                manager.transform.position = S.Posicao;//new Vector3(483,1.2f,755);  
                manager.transform.rotation = S.Rotacao;
                GameController.g.ReiniciarContadorDeEncontro();
                MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
                manager.Dados = S.Dados;
                manager.InserirCriatureEmJogo();
                manager.CriatureAtivo.transform.position = S.Posicao + new Vector3(0, 0, 1);//new Vector3(483, 1.2f, 756);
                manager.Dados.ZeraUltimoUso();
                GameController.g.MyKeys = S.VariaveisChave;
                GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);
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
            MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
            manager.InserirCriatureEmJogo();
            manager.CriatureAtivo.transform.position = new Vector3(483, 1.2f, 756);
            GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);
        }

        
    }

    static IEnumerator setarScene(Scene scene)
    {
        yield return new WaitForSeconds(0.5f);
        InvocarSetScene(scene);
    }

    public static void InvocarSetScene(Scene scene)
    {
        Debug.Log(scene.name);
        SceneManager.SetActiveScene(scene);
        if (SceneManager.GetActiveScene() != scene)
            GameController.g.StartCoroutine(setarScene(scene));
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

                Debug.Log(progresso + " : " + (tempo / tempoMin) + " : " + Mathf.Min(progresso, tempo / tempoMin, 1));

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
                    InitialSceneManager.i.GetComponent<Canvas>().enabled = false;
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
                    SceneManager.UnloadSceneAsync("Inicial");
                    
                }
            break;
        }
        
        if (fase == FasesDoLoad.carregando)
        {
           
        }
    }
}
