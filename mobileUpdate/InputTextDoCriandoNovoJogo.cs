using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputTextDoCriandoNovoJogo:MonoBehaviour
{
    [SerializeField]private InputField input;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Iniciar()
    {
        gameObject.SetActive(true);
    }

    public void CriandoJogo()
    {
        PropriedadesDeSave prop = new PropriedadesDeSave() { nome = input.text, ultimaJogada = System.DateTime.Now };
        LoadAndSaveGame salvador = new LoadAndSaveGame();
        List<PropriedadesDeSave> lista = (List<PropriedadesDeSave>)(salvador.CarregarArquivo("criaturesGames.ori"));

        if (lista != null)
            lista.Add(prop);
        else
            lista = new List<PropriedadesDeSave>() { prop };

        salvador.SalvarArquivo("criaturesGames.ori", lista);
    }

    public void Voltar()
    {
        FindObjectOfType<InitialSceneManager>().FecharInputText();
    }
}
