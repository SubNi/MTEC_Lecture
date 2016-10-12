using UnityEngine;
using System.Collections;

public class MyComponent : MonoBehaviour {
    public int intVariable;
    public float floatVariable;
    public GameObject[] gameObjects;

    [SerializeField]    //프라이빗이지만 시리얼라이즈픨드 사용할고임
    private int _intVar;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public int IntVar
    {
        get
        {
            return _intVar;
        }
        set
        {
            _intVar = value;
        }
    }

    public void DoSomething()
    {
        _intVar += 2;
    }
}
