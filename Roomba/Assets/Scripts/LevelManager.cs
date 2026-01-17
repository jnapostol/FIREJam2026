using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("Bathroom")]
    public bool HasBandaid;
    public bool HasBatteries;
    public UnityEvent OnLevel1Finish;
    public void Level1Check()
    {
        if (HasBandaid && HasBatteries)
        {
            OnLevel1Finish.Invoke();
        }
    }
    public void NotifyPickUpBandaid()
    {
        HasBandaid = true;
    }
    public void NotifyPickUpBatteries()
    {
        HasBatteries = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
