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
        foreach (MeshRenderer r in renderers)
        {
            Debug.Log(r.gameObject.name);
        }
        StartCoroutine(FadeOut(renderers));
    }

    IEnumerator FadeOut(List<MeshRenderer> renderers)
    {
        float timePassed = 0f;
        while (timePassed < 3f)
        {
            Debug.Log("Decrementing color");
            foreach (MeshRenderer r in renderers)
            {
                if (r == null) continue;
                //Color c = r.material.color;
                //c.a -= 0.03f;
                //Mathf.Clamp(c.a, 0, 255);
                Color c = Color.Lerp(r.material.color, Color.clear, (timePassed/9f));
                r.material.color = c;
            }
            Debug.Log("made it past the first foreach");
            timePassed += 0.01f;
            Debug.Log("timepassed = " + timePassed);
            yield return null;
            
            
        }
        
        
        foreach (MeshRenderer r in renderers)
        {
            r.gameObject.SetActive(false) ;
        }
    }
}
