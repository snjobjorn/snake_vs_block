using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlocksBehaviour : MonoBehaviour
{
    //public Material material;
    // Start is called before the first frame update
    void Start()
    {
        SetNumber();
        SetColor();
    }

    void SetNumber()
    {
        this.GetComponent<TextMeshPro>().text = Random.Range(1, 20).ToString();
    }
    void SetColor()
    {
        GameObject Cube = this.transform.GetChild(0).gameObject;
        Renderer renderer = Cube.GetComponent<Renderer>();
        int blockHealthNumber = int.Parse(this.GetComponent<TextMeshPro>().text);
        if (blockHealthNumber > 0 && blockHealthNumber < 5)
        {
            renderer.material.color = new Color(1.0f, 0.6075472f, 0.8704214f);
        }
        if (blockHealthNumber >= 5 && blockHealthNumber < 10)
        {
            renderer.material.color = new Color(1.0f, 0.0f, 0.6700001f);
        }
        if (blockHealthNumber >= 10 && blockHealthNumber < 15)
        {
            renderer.material.color = new Color(1.0f, 0.0f, 0.2633753f);
        }
        if (blockHealthNumber >= 15 && blockHealthNumber < 20)
        {
            renderer.material.color = new Color(0.5660378f, 0.0f, 0.1997415f);
        }
        if (blockHealthNumber >= 25)
        {
            renderer.material.color = new Color(0.245283f, 0.0f, 0.08687114f);
        }
    }

}
