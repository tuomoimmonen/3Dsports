
using TMPro;
using UnityEngine;

public class DistanceJumpedText : MonoBehaviour
{
    [SerializeField] TMP_Text distanceJumpedtext;
    private void Awake()
    {
        SetDistanceJumpedText(0);
    }

    private void Start()
    {
        SetDistanceJumpedText(0);
    }

    void SetDistanceJumpedText(float distance)
    {
        distanceJumpedtext.text = "DistanceJumped: " + distance.ToString("F") + "m";
    }

    public void UpdateDistanceJumped(Component sender, object data)
    {
        if(data is float)
        {
            float distance = (float)data;
            SetDistanceJumpedText(distance);
        }
    }
}
