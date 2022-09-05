using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    public GameObject cam;
    [SerializeField] float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float tmp = (cam.transform.position.x) * (1 - parallaxEffect);
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (tmp > startPos + length)
            startPos += length;
        else if (tmp < startPos - length)
            startPos -= length;


    }
}
