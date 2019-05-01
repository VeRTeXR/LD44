using UnityEngine;
using UnityEngine.EventSystems;

public class LabSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator _animator;
    
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Selected", false);
    }

    public void Setup()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Selected", true);
        Debug.LogError("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Selected", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogError("GotoLabSequence");
    }
}