using UnityEngine;

public class InfoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    const float Speed = 6f;
    [SerializeField]
    Transform SectionInfo;
    Vector3 desiredScale = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * Speed);
    }


    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }
    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}
