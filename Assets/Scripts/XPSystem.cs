using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    public int Level;
    public float Total;

    public TMP_Text XPCount;
    public TMP_Text LevelCount;

    // Update is called once per frame
    void Update()
    {
        if (Level != 0)
        {
            GetComponent<Slider>().maxValue = 25 * Level;
        }
        else
        {
            GetComponent<Slider>().maxValue = 25;
        }

        GetComponent<Slider>().value = Total;

        XPCount.text = Total + " /" + GetComponent<Slider>().maxValue;
        LevelCount.text = Level.ToString();

        if(Total >= GetComponent<Slider>().maxValue)
        {
            Level += 1;
            Total = Total - GetComponent<Slider>().maxValue;
        }
    }
}
