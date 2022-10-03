using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

    public float MinDistance;

    public int BeginSize;

    public float Speed = 1;
    public float RotationSpeed = 50;

    public GameObject BodyPrefab;

    private float Distance;
    private Transform CurrentBodyPart;
    private Transform PreviousBodyPart;

    public float TimeFromLastRetry;

    public GameObject DeadScreen;
    public GameObject WinScreen;
    public GameObject GameScreen;

    public Text CurrentScore;
    public Text CurrentLevel;
    public GameObject RestartButton;

    public bool IsAlive;

    public Game game;


    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            if (Input.GetKey(KeyCode.A) && (BodyParts[0].transform.position.x >= -4.5))
            {
                BodyParts[0].Translate(-BodyParts[0].right * (Speed * 3) * Time.smoothDeltaTime, Space.World);
            }

            if (Input.GetKey(KeyCode.D) && (BodyParts[0].transform.position.x <= 4.5))
            {
                BodyParts[0].Translate(BodyParts[0].right * (Speed * 3) * Time.smoothDeltaTime, Space.World);
            }

            Move();
            //Debug.Log(BodyParts[0]);
        }
    }

    public void StartLevel()
    {
        TimeFromLastRetry = Time.time;

        DeadScreen.SetActive(false);
        WinScreen.SetActive(false);
        RestartButton.SetActive(true); ;
        GameScreen.SetActive(true);
        

        for (int i = BodyParts.Count - 1; i > BeginSize; i--)
        {
            Destroy(BodyParts[i].gameObject);
            BodyParts.Remove(BodyParts[i]);
        }

        BodyParts[0].position = new Vector3(0.0f, 0.0f, 0.0f);
        BodyParts[0].rotation = Quaternion.identity;

        CurrentScore.gameObject.SetActive(true);
        CurrentScore.text = "Score: 0";
        IsAlive = true;


        for (int i = 0; i < BeginSize - 1; i++)
        {
            AddBodyPart();
        }

        BodyParts[0].position = new Vector3(0.0f, 0.0f, 1.0f);
    }

    public void Move()
    {
        float CurrentSpeed = Speed;

        BodyParts[0].Translate(BodyParts[0].forward * CurrentSpeed * Time.smoothDeltaTime, Space.World);
        BodyParts[0].position = new Vector3(BodyParts[0].position.x, 0.0f, BodyParts[0].position.z);

        for (int i = 1; i < BodyParts.Count; i++)
        {
            CurrentBodyPart = BodyParts[i];
            PreviousBodyPart = BodyParts[i - 1];

            Distance = Vector3.Distance(PreviousBodyPart.position, CurrentBodyPart.position);
            Vector3 NewPosition = PreviousBodyPart.position;

            NewPosition.y = BodyParts[0].position.y;

            float T = Time.deltaTime * Distance / MinDistance * CurrentSpeed;

            if (T > 0.5f)
            {
                T = 0.5f;
            }

            CurrentBodyPart.position = Vector3.Lerp(CurrentBodyPart.position, NewPosition, T);
            CurrentBodyPart.rotation = Quaternion.Lerp(CurrentBodyPart.rotation, PreviousBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform NewPart = (Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
        NewPart.SetParent(transform);
        BodyParts.Add(NewPart);
        CurrentScore.text = "Score: " + (BodyParts.Count - BeginSize).ToString();
    }
    public void RemoveBodyPart()
    {
        if (BodyParts.Count <= 1)
        {
            Die();
            return;
        }
        BodyParts.RemoveAt(BodyParts.Count - 1);
        CurrentScore.text = "Score: " + (BodyParts.Count - BeginSize).ToString();
    }

    public void Die()
    {
        IsAlive = false;

        CurrentScore.gameObject.SetActive(false);
        RestartButton.SetActive(false);
        DeadScreen.SetActive(true);
    }

    public void Win()
    {
        CurrentScore.gameObject.SetActive(false);
        RestartButton.SetActive(false);
        WinScreen.SetActive(true);
    }
}
