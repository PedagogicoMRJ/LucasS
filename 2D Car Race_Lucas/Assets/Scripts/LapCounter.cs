using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LapCounter : MonoBehaviour
{
    int nCheckpoint;
    float timeChackpointPassed;
    int nPassedCheckpoints;

    int lapsCompleted;
    bool isRaceCompleted = false;
    const int allLaps = 2;
    int carPosition = 0;

    public event Action<LapCounter> OnPassCheckpoint;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNCheckpointPassed()
    {
        return nPassedCheckpoints;
    }

    public float GetTimeCheckpointPassed()
    {
        return timeChackpointPassed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Checkpoint"))
        {
            if (isRaceCompleted)
                return;

            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
            if(nCheckpoint + 1 == checkpoint.nCheckpoint)
            {
                nCheckpoint = checkpoint.nCheckpoint;
                nPassedCheckpoints++;
                timeChackpointPassed = Time.time;
                

                if(checkpoint.isFinish)
                {
                    nCheckpoint = 0;
                    lapsCompleted++;
                    Debug.Log($"Evento: {gameObject.name} completou a corrida em {carPosition.ToString()} posicao");
                }
            }
            OnPassCheckpoint?.Invoke(this);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
