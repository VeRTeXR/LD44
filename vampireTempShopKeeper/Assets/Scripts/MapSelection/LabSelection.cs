using UnityEngine;
using UnityEngine.EventSystems;

public class LabSelection : ISelection
{
    
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void Setup()
    {
        
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