using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaphone : MonoBehaviour
{
    public int gainedVoters;
    public void UseMegaphone()
    {
        GameManager.Instance.AddVoters(gainedVoters);
    }
}
