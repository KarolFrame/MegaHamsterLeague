using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarTorretasPlayer : MonoBehaviour
{
    public GameObject prefabTorreta;
    Player jugador;
    void Awake()
    {
        jugador = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (jugador.GetTorretas() > 0)
        {
            if (Input.GetButtonDown("Ataque2"))
            {
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(rayo, out hit))
                {
                    if (hit.collider.tag == "Floor")
                    {
                        print("a");
                        GameObject clonTorreta = Instantiate(prefabTorreta);
                        clonTorreta.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        clonTorreta.transform.rotation = prefabTorreta.transform.rotation;

                        jugador.SetTorretas(jugador.GetTorretas()-1);

                    }
                }
            }
        }
    }
}
