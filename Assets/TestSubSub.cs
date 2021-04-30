using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubSub : TestSub
{
    // Start is called before the first frame update
    void Start()
    {
        Chat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Chat()
    {
        base.Chat();
        Debug.Log("CHATCHATCHAT");
    }
}
