using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForCorrectDataInput : MonoBehaviour
{
    public InputField input;

    public void checkForInt()
    {
        int result;
        if ((result = int.Parse(input.text))<=0)
        {
            input.text = "Incorrect input";
        }
    }
}
