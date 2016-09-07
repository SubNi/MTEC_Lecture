using UnityEngine;
using System.Collections;

[AddComponentMenu("My Menu/My Component Script")] //컴포넌트 스크립트는 무조건 컴포넌트에..
public class MyComponent : MonoBehaviour {

    public int intNum;
    public float floatNum;
 //   public GameObject gameObjectList;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int IntVar // Propety!
    {
        get
        {
            return intNum;
        }
        set
        {
            intNum = value;
        }
    }

    public void DoSomething()
    {
        intNum++;
    }

}
