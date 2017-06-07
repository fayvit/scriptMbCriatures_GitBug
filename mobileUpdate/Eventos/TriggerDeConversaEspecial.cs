using UnityEngine;
using System.Collections;

public class TriggerDeConversaEspecial : MonoBehaviour
{
    [SerializeField]private Transform alvo;
    [SerializeField]private Sprite fota;

    private KeyShift estaKey;
    private CharacterManager manager;
    private DisparaTexto disparaT;
    private Transform destrutivel;
    private string[] t;

    protected KeyShift EstaKey
    {
        get { return estaKey; }
        set { estaKey = value; }
    }

    protected CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    protected DisparaTexto DisparaT
    {
        get { return disparaT; }
        set { disparaT = value; }
    }

    protected Transform Destrutivel
    {
        get { return destrutivel; }
        set { destrutivel = value; }
    }

    protected Transform Alvo
    {
        get { return alvo; }
        set { alvo = value; }
    }

    public Sprite Fota
    {
        get { return fota; }
        set { fota = value; }
    }

    public string[] T
    {
        get { return t; }
        set { t = value; }
    }

    protected void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            if (GameController.g.MyKeys.VerificaAutoShift(EstaKey))
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (GameController.g.MyKeys.VerificaAutoShift(EstaKey))
            {
                Destroy(gameObject);
            }
            else
            {
                GameController.EntrarNoFluxoDeTexto();
                GameController.g.MyKeys.MudaShift(EstaKey, true);
                Manager = GameController.g.Manager;
                Manager.Estado = EstadoDePersonagem.movimentoDeFora;
                FaseDoTrigger();
            }
        }      
    }

    protected void CameraAteFalas()
    {
        if (AplicadorDeCamera.cam.FocarPonto(3, 15, 5))
        {
            MudarParaFaseDasFalas();
            DisparaT = GameController.g.HudM.DisparaT;
            DisparaT.IniciarDisparadorDeTextos();
        }
    }

    protected void CaminheHeroi(Vector3 pos)
    {
        if (Vector3.Distance(Manager.transform.position, pos) > 2.5f)
        {
            Manager.Mov.AplicadorDeMovimentos((pos - Manager.transform.position).normalized);
        }
        else
        {
            Manager.Estado = EstadoDePersonagem.parado;
            Manager.transform.rotation = Quaternion.LookRotation(Alvo.position - Manager.transform.position);
            Destrutivel = TransformPosDeConversa.MeAjude(Alvo);
            AplicadorDeCamera.cam.InicializaCameraExibicionista(Destrutivel, 1f, true);
            FasePosMOvimentoDoHeroi();
        }
    }

    protected void VoltaControleDoHeroi()
    {
        AndroidController.a.LigarControlador();
        GameController.g.HudM.ligarControladores();
        GameController.g.HudM.HudCriatureAtivo.container.transform.parent.gameObject.SetActive(false);
        GameController.g.Manager.Estado = EstadoDePersonagem.aPasseio;
    }

    protected virtual void FaseDoTrigger()
    {
        // sobrecarregar
    }

    protected virtual void FasePosMOvimentoDoHeroi()
    {
        //sobrecarregar
    }

    protected virtual void MudarParaFaseDasFalas()
    {
        //sobrecarregar
    }
}
