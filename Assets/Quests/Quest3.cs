using UnityEngine;

public class Quest3 : Quest
{
    [field: SerializeField]
    private GameObject Note { get; set; }
    [field: SerializeField]
    private GameObject Projection { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Description = "Finde den Zettel mit weiten Anweisungen";
        IsActive = false;
        IsDone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActive)
        {
            IsDone = true;
        }
        // If you enter the collider, the projection will play
        Projection.SetActive(true);
    }

    // If you leave the collider, the projection will be disabled
    private void OnTriggerExit(Collider other)
    {
        Projection.SetActive(false);
    }

}
