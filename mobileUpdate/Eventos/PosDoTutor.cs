using UnityEngine;
using System.Collections;

public class PosDoTutor : MonoBehaviour
{
    [SerializeField]private Transform pos2;
    [SerializeField]private Transform pos3;

    // Use this for initialization
    void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            KeyVar keys = GameController.g.MyKeys;
            if (keys.VerificaAutoShift(KeyShift.fezPrimeiraFalaDeTuto))
                transform.position = pos2.position;

            if (keys.VerificaAutoShift(KeyShift.fezSegundaFalaDeTuto))
                transform.position = pos3.position;
        }
    }
}
