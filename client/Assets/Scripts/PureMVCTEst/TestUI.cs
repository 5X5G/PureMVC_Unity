using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour {

    public GameObject canvas;

	void Start ()
    {
        new TestFacade(canvas);
	}
}
