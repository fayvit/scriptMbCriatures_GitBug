using UnityEngine;
using System.Collections;

public class MexeNoOffset : MonoBehaviour
{
    MeshRenderer M;
    public float vel = 1;
    // Use this for initialization
    void Start()
    {
        M = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        M.material.mainTextureOffset += (new Vector2(1, 1)) * vel * Time.deltaTime;
    }
}
