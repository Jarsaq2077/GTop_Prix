using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText, recordText, placeText;
    private float timer = 0f, record;
    private bool timerRunning = false;
    public Transform waypointsList;
    public Transform[] waypoints;
    public Transform[] racers;
    private int totalCars;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        /*if (PlayerPrefs.HasKey("record"))
        {
            record = PlayerPrefs.GetFloat("record");
        }
        else
        {
            record = 0f;
        }
        DisplayRecord(record);*/
        record = 0f;
        DisplayRecord(record);
        waypoints = new Transform[waypointsList.childCount];

        for (int i = 0; i < waypointsList.childCount; i++)
        {
            waypoints[i] = waypointsList.GetChild(i);
        }
        totalCars = racers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);
        }
        else
        {
            DisplayTime(timer);
        }
        var ranking = racers.OrderByDescending(c => nextCheckpoint(c)).ToList();
        int place;
        

        for (int i = 0; i < ranking.Count; i++)
        {
            if (ranking[i].name == "bmw")
            {
                place = i + 1;
                placeText.text = place.ToString() + " / " + totalCars.ToString();
            }
            Debug.Log((i + 1) + "° lugar: " + ranking[i].name);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milisecs = Mathf.FloorToInt((timeToDisplay * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milisecs);
    }

    public void StartTimer()
    {
        timer = 0f;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
        if (timer < record || record == 0f)
        {
            record = timer;
            PlayerPrefs.SetFloat("record", record);
        }

        DisplayRecord(record);
    }

    private void DisplayRecord(float _record)
    {
        float minutes = Mathf.FloorToInt(_record / 60);
        float seconds = Mathf.FloorToInt(_record % 60);
        float milisecs = Mathf.FloorToInt((_record * 1000f) % 1000f);

        recordText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milisecs);
    }

    float nextCheckpoint(Transform racers)
    {
        int closest = 0;
        float closestDist = Mathf.Infinity;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float dist = Vector3.Distance(racers.position, waypoints[i].position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = i;
            }
            
        }
        return closest * 1000f - closestDist;
    }

}