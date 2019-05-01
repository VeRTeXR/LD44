using UnityEngine;
using UnityEngine.EventSystems;

public class ApartmentSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator _animator;
    private int _bloodRestore;
    void Start()
    {
        _animator = GetComponent<Animator>();
       
        Debug.LogError("Start:: ");
    }

    private void GoToApartmentSequence()
    {
       Debug.LogError("GotoApartSequence");
       GameStateManager.Instance.AddBlood(_bloodRestore);    
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Selected",true);
        Debug.LogError("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Selected",false);
        Debug.LogError("exit");
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        GoToApartmentSequence();
    }
}
