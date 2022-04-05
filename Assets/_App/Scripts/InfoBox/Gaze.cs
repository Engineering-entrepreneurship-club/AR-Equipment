using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    List<InfoBehavior> infos = new List<InfoBehavior>();
    void Start()
    {
    }
    void Update()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
        Debug.DrawLine(transform.position, transform.forward + transform.position, Color.cyan);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("containInfo"))
            {
                OpenInfo(go.GetComponent<InfoBehavior>());
            }
            else
            {
                CloseAllInfo();
            }
        }
    }

    void OpenInfo(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAllInfo()
    {
        foreach (InfoBehavior info in infos)
        {
            info.CloseInfo();
        }

    }
}
