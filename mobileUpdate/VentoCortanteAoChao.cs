using UnityEngine;
using System.Collections.Generic;

public class VentoCortanteAoChao : MonoBehaviour
{
    [SerializeField]private GameObject trail;
    private float tempoDecorrido;
    private List<Transform> instanciados = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        trail.SetActive(false);
        InstanciaNovo();   
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < instanciados.Count; i++)
        {
            if (instanciados[i] != null)
                instanciados[i].RotateAround(transform.position, instanciados[i].up, 20);// ( Vector3.up, 10);
        }

        int x = instanciados.Count;

        for (int i = x - 1; i >= 0; i--)
            if (instanciados[i] == null)
                instanciados.Remove(instanciados[i]);

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido > 0.025f)
        {
            InstanciaNovo();
            tempoDecorrido = 0;
        }
    }

    void InstanciaNovo()
    {
        GameObject G = Instantiate(trail);
        G.SetActive(true);
        G.transform.parent = transform;
        G.transform.rotation = Quaternion.Euler(Random.Range(-20,20),0,Random.Range(-20,20));
        G.transform.localPosition = SorteiaPos();
        Destroy(G, 0.35f);
        instanciados.Add(G.transform);
    }

    Vector3 SorteiaPos()
    {
        Vector2 s = Random.insideUnitCircle;
        s = Random.Range(1.25f, 1.95f) * s.normalized;
        return new Vector3(s.x, Random.Range(-0.1f,0.5f), s.y);
    }
}
