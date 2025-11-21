using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    int IsWalking = Animator.StringToHash("isWalking");
    int IsIdle = Animator.StringToHash("isIdle");
    bool isWalking = true;
    bool isIdle = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isIdle = true;
            isWalking = false;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            isIdle = false;
            isWalking = true;
        }
        Animate();
    }
    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 24;

        GUI.color = Color.yellow;
        GUI.Label(new Rect(20, 20, 200, 32), "1 for Idle");

        GUI.color = Color.red;
        GUI.Label(new Rect(20, 40, 200, 32), "2 for Walk");
    }
}
