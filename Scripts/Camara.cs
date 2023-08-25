using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Player player;
    public float smooth = 0.3f;

    Vector3 velocity = Vector3.zero;
    Transform tr;

    public float height, zpos;

    //SHAKE
    




    void Start()
    {
        if(tr == null)
            tr = transform;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.transform.position.x;
        pos.z = player.transform.position.z-zpos;
        pos.y = player.transform.position.y + height;
        tr.position = Vector3.SmoothDamp(tr.position, pos, ref velocity, smooth);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = tr.position;
        float elapsed = 0f;
        while(elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            tr.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        tr.position = originalPosition;

    }
}
