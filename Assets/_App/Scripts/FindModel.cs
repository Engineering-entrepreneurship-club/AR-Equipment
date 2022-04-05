using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindModel : MonoBehaviour
{

    Vector3 desiredScale = Vector3.zero;
    Vector3 originalScale = Vector3.zero;
    const float speed = 6f;
    bool toggle = false;
    public void OnClick()
    {
        GameObject[] models;
        models = GameObject.FindGameObjectsWithTag("equipment");
        desiredScale = toggle?Vector3.one:Vector3.zero;
        toggle = !toggle;
        ScreenLog.Log("Set object to " + toggle);


        foreach(GameObject model in models)
        {
            ScreenLog.Log("current scale: " + model.transform.localScale.ToString() + model.name);
            if(model.transform.localScale != desiredScale)
            {
                // desiredScale = model.transform.localScale;
                StartCoroutine(ScaleTo(model, desiredScale, speed));
            }

        }

    }

    IEnumerator ScaleTo(GameObject model, Vector3 desiredScale, float speed)
    {
        float t = 0f;
        while(t <= 1)
        {
            t += Time.deltaTime * speed;
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, desiredScale, t);
            yield return null;
            
        }
    }


}
