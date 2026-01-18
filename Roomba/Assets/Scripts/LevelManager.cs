using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("Bathroom")]
    public bool HasBandaid;
    public bool HasBatteries;
    public UnityEvent OnLevel1Finish;
    [Header("Bedroom")]
    public Transform Racket;
    public Transform Racket2;
    public Transform Record;
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

    public void AttachRacketToRecordPlayer()
    {
        StartCoroutine(WaitToAttachRacket());
    }
    IEnumerator WaitToAttachRacket()
    {
        yield return new WaitForSeconds(0.97f);
        Racket.gameObject.SetActive(false);
        Racket2.localPosition = new Vector3(0.001587136f, 0.001864903f, -0.001941625f);
        Racket2.gameObject.SetActive(true);
        Racket2.rotation = Quaternion.Euler(7.958f, 195.576f, -86.587f);
    }
}
