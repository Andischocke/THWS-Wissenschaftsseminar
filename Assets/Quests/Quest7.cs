public class Quest7 : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        Description = "Kehre ans Whiteboard zur�ck";
        IsActive = false;
        IsDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            IsDone = true;
        }
    }
}