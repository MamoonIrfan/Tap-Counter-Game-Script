using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    Dictionary<string,int>Name=new Dictionary<string,int>();
    // Start is called before the first frame update
    void Start()
    {
        Name["Mamoon"] = 12;
        Name["Taimoor"] = 13;
        Name["Hammad"] = 14;
     

    }

    // Update is called once per frame
  
}
