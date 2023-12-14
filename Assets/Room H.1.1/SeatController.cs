using UnityEngine;

public class SeatController : MonoBehaviour
{
    #region Variables
    private Animator Animator { get; set; }
    #endregion


    // Start is called before the first frame update
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // 
    private void OnTriggerEnter(Collider other)
    {
        Animator.SetBool("Occupied", true);
    }

    private void OnTriggerExit(Collider other)
    {
        Animator.SetBool("Occupied", false);
    }
}