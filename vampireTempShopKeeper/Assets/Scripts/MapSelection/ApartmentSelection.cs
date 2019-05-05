using UnityEngine;
using UnityEngine.EventSystems;

public class ApartmentSelection : ISelection
{
 
    private int _bloodRestore;
    void Start()
    {
        Animator = GetComponent<Animator>();       
    }

    private void GoToApartmentSequence()
    {
       GameStateManager.Instance.AddBlood(_bloodRestore);    
    }
    

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Animator.SetBool("Selected",true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Animator.SetBool("Selected",false);
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        GoToApartmentSequence();
    }
}
