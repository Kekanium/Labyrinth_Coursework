using UnityEngine;
using UnityEngine.UI;

public class CheckForCorrectDataInput : MonoBehaviour
{
    public InputField input;

    public void checkForInt()
    {
        if ((int.Parse(input.text))<=0)
        {
            input.text = "Incorrect input";
        }
    }
}
