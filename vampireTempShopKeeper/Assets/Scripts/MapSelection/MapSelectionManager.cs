using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class MapSelectionManager : MonoBehaviour
{
    private Transform _selectableElementsParent;
    private BloodPactTracker _bloodPactTracker;
    private DealTracker _dealTracker;
    private TurnStateIcon _turnStateIcon;
    [SerializeField] private List<ISelection> mapEntries;
    private DayIcon _dayIcon;


    private void Start()
    {
        var selectionEntries = GetComponentsInChildren<ISelection>();
        for (var i = 0; i < selectionEntries.Length; i++)
        {
            mapEntries.Add(selectionEntries[i]);
        }

        _turnStateIcon = GetComponentInChildren<TurnStateIcon>();
        _dayIcon = GetComponentInChildren<DayIcon>();
    }

    public void SetupElement(LevelConfiguration.State state, int currentDate)
    {    
        _turnStateIcon.SetTurnAnimatorState(state);
        _dayIcon.SetDaySprite(currentDate);
    }
}
