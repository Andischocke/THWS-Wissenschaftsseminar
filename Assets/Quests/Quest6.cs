using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quest6 : Quest
{
    [field: SerializeField]
    private GameObject WrongSheepContainer { get; set; }
    [field: SerializeField]
    private GameObject[] WrongSheep { get; set; }
    [field: SerializeField]
    private GameObject Desk { get; set; }
    [field: SerializeField]
    private GameObject ClawMachine { get; set; }
    [field: SerializeField]
    private GameObject RightSheep { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Description = "Finde das fehlende Schaf";
        IsActive = false;
        IsDone = false;

        // Disable sheep container
        WrongSheepContainer.SetActive(false);
        // Disable desk
        Desk.SetActive(false);
        // Disable claw machine
        ClawMachine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Display all wrong sheep
        if (IsActive && !IsDone)
        {
            WrongSheepContainer.SetActive(true);
            Desk.SetActive(true);
        }

        // Disable wrong sheep if XR grabbed
        foreach (GameObject sheep in WrongSheep)
        {
            if (sheep.GetComponent<XRGrabInteractable>().isSelected)
            {
                sheep.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsDone = true;
        Desk.SetActive(false);
        ClawMachine.SetActive(true);
    }
}
