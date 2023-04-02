using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainlog : MonoBehaviour
{
    public static Mainlog Instance;

    public Web Web;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
    }


}
