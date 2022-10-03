using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshPro healthText;
    void Start()
    {
        healthText.text = Random.Range(5, 10).ToString(); ;
    }
}
