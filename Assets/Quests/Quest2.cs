using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quest2 : Quest
{
    [field: SerializeField]
    private GameObject Clock { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Description = "Hebe die Uhr auf und lege sie auf den Tisch";
        IsActive = false;
        IsDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Clock.GetComponent<Rigidbody>().useGravity = true;
            // Check if clock is XR grabbed
            if (Clock.GetComponent<XRGrabInteractable>().isSelected)
            {
                IsDone = true;
            }
        }
    }
}
