using UnityEngine;

public class Quest1 : Quest
{
    [field: SerializeField]
    private GameObject[] Checkpoints { get; set; }
    private int Index { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        Description = "Folge den Checkpunkten";
        IsActive = false;
        IsDone = false;

        // Disable all checkpoints
        foreach (GameObject checkpoint in Checkpoints)
        {
            checkpoint.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // All Checkpoints face the camera (only y rotation)
        foreach (GameObject checkpoint in Checkpoints)
        {
            checkpoint.transform.LookAt(Camera.main.transform);
            checkpoint.transform.eulerAngles = new Vector3(0, checkpoint.transform.eulerAngles.y, 0);
        }

        // Enable first checkpoint
        if (IsActive && !IsDone && Index == 0)
        {
            Checkpoints[0].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Next();
    }

    private void Next()
    {
        Checkpoints[Index].SetActive(false);

        //Check if this was the last checkpoint
        if (Index == Checkpoints.Length - 1)
        {
            IsDone = true;
        }
        else
        {
            Index++;
            Checkpoints[Index].SetActive(true);
        }
    }
}