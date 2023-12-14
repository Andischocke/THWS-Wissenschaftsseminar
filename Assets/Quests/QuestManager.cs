using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Attributes
    [field: SerializeField]
    private GameObject MotionSicknessGlasses { get; set; }
    [field: SerializeField]
    private bool MotionSicknessGlassesEnabled { get; set; } = true;
    [field: SerializeField]
    private TextMeshPro QuestBoardTitle { get; set; }

    [field: SerializeField]
    private TextMeshPro Timer { get; set; }
    private float TimerInSeconds { get; set; } = 0f;
    private bool TimerIsRunning { get; set; } = false;

    [field: SerializeField]
    private TextMeshPro QuestDescription { get; set; }
    [field: SerializeField]
    private GameObject[] Quests { get; set; }

    [field: SerializeField]
    private TextMeshPro Acknowledgment { get; set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set the motion sickness glasses to the value of the variable on start
        MotionSicknessGlasses.SetActive(MotionSicknessGlassesEnabled);
        QuestBoardTitle.text = MotionSicknessGlassesEnabled ? "Deine Aufgabenliste für die VR-Studie (Gruppe B)" : "Deine Aufgabenliste für die VR-Studie (Gruppe A)";
        // Activate the first quest
        Quests[0].GetComponent<Quest>().IsActive = true;

        //Quests[6] = new Quest("Kehre ans Whiteboard zurück", true, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerIsRunning)
        {
            TimerInSeconds += Time.deltaTime;
            Timer.text = TimerInSeconds.ToString("F2") + " Sekunden";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If the quest is done, activate the next quest
        for (int i = 0; i < Quests.Length; i++)
        {
            if (Quests[i].GetComponent<Quest>().IsDone && i < Quests.Length - 1)
            {
                Quests[i + 1].GetComponent<Quest>().IsActive = true;
            }
        }

        PrintQuests();
    }

    // Print the quests to the quest board
    private void PrintQuests()
    {
        string questDescriptions = "";

        for (int i = 0; i < Quests.Length; i++)
        {
            if (Quests[i].GetComponent<Quest>().IsActive)
            {
                questDescriptions += string.Format("{0}. {1}\n", i + 1, Quests[i].GetComponent<Quest>().Description);
            }
        }
        QuestDescription.text = questDescriptions;
    }

    // Stop the timer if you enter the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // Check if it was the last quest
        if (Quests[Quests.Length - 1].GetComponent<Quest>().IsDone)
        {
            TimerIsRunning = false;
            Acknowledgment.text = "Herzlichen Glückwunsch, du hast es geschafft!\nVielen Dank für die Teilnahme!";
        }
    }

    // Start the timer if you leave the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (Quests[0].GetComponent<Quest>().IsActive && !Quests[0].GetComponent<Quest>().IsDone)
        {
            TimerIsRunning = true;
        }
    }
}