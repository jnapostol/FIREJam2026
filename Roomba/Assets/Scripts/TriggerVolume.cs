using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;

public class TriggerVolume : MonoBehaviour
{
    public UnityEvent OnPlayerOverlap;
    bool _isTriggered = false;
    public List<SmartObject> RoomSmartObjects = new List<SmartObject>();
    //[SerializeField] bool _isNewRoom;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _isTriggered == false) //if player enters
        {
            //call the event and turn itself off to avoid more collisions
            OnPlayerOverlap.Invoke();
            //this.gameObject.SetActive(false);
            _isTriggered = true;
        }
        
    }

    /// <summary>
    /// Fades object and all its children out if possible
    /// </summary>
    /// <param name="obj"></param>
    public void Fade3DObjectOut(GameObject obj)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        List<MeshRenderer> renderers = new List<MeshRenderer>();
        if (renderer != null)
        {
            renderers.Add(renderer);
        }

        renderers.AddRange(obj.GetComponentsInChildren<MeshRenderer>());
        StartCoroutine(FadeOut(renderers));
    }

    IEnumerator FadeOut(List<MeshRenderer> renderers)
    {
        float timePassed = 0f;
        while (timePassed < 3f)
        {
            foreach (MeshRenderer r in renderers)
            {
                if (r == null) continue;
                //Color c = r.material.color;
                //c.a -= 0.03f;
                //Mathf.Clamp(c.a, 0, 255);
                Color c = Color.Lerp(r.material.color, Color.clear, (timePassed/9f));
                r.material.color = c;
            }
            timePassed += 0.01f;
            yield return null;
            
            
        }
        
        
        foreach (MeshRenderer r in renderers)
        {
            r.gameObject.SetActive(false) ;
        }
    }


    public void RepopulateSmartObjs()
    {
        SelectionManager.Instance.RepopulateSmartObjects(RoomSmartObjects, 0);
    }
}
