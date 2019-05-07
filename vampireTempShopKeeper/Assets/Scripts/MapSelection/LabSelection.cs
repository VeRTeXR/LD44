using UnityEngine;
using UnityEngine.EventSystems;

public class LabSelection : ISelection
{
    
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public override void Setup(LevelConfiguration.State state, int currentDate)
    {
        Debug.LogError("Lab Setup");
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        Animator.SetBool("Selected", true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Animator.SetBool("Selected", false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        GoToLabSequence();
    }

    private void GoToLabSequence()
    {
        throw new System.NotImplementedException();
    }
}