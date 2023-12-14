using UnityEngine;

public class Quest5 : Quest
{
    [field: SerializeField]
    private GameObject Checkpoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Description = "Gehe ans Fenster und zähle die Schafe im Freien";
        IsActive = false;
        IsDone = false;

        // Disable checkpoint
        Checkpoint.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Checkpoint face the camera (only y rotation)
        Checkpoint.transform.LookAt(Camera.main.transform);
        Checkpoint.transform.eulerAngles = new Vector3(0, Checkpoint.transform.eulerAngles.y, 0);

        // Enable first checkpoint
        if (IsActive && !IsDone)
        {
            Checkpoint.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsDone = true;
        Checkpoint.SetActive(false);
    }
}
