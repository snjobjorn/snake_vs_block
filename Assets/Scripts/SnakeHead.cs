using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeHead : MonoBehaviour
{
    public SnakeMovement Movement;
    public ParticleSystem ParticleSystem;
    public AudioSource AudioSource;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "health")
        {
            int healthNumber = int.Parse(collision.gameObject.GetComponent<TextMeshPro>().text);
            ParticleSystem.Play();
            AudioSource.Play();
            for (int i = 0; i < healthNumber; i++)
            {
                Movement.AddBodyPart();
            }
            Destroy(collision.gameObject);


        }

        if (collision.gameObject.tag == "block")
        {
            StartCoroutine(BlockDestruction(collision.gameObject));
        }

        if (collision.gameObject.tag == "Finish")
        {
            StartCoroutine(Finish(collision.gameObject));
        }
    }

    public IEnumerator BlockDestruction(GameObject block)
    {
        int blockHealthNumber = int.Parse(block.GetComponent<TextMeshPro>().text);
        //Debug.Log(blockHealthNumber);
        while (blockHealthNumber > 0)
        {
            Movement.Speed = 0;
            Movement.RemoveBodyPart();
            block.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            block.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            block.GetComponent<TextMeshPro>().text = (blockHealthNumber - 1).ToString();
            blockHealthNumber = int.Parse(block.GetComponent<TextMeshPro>().text);
        }
        Destroy(block);
        Movement.Speed = 6;
    }

    public IEnumerator Finish(GameObject block)
    {
        Movement.Speed = 0;
        yield return new WaitForSeconds(1f);
        Movement.Win();
    }
}
