public class Quest4 : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        Description = "Setze dich hin und zähle die Schafe an der Leinwand";
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
