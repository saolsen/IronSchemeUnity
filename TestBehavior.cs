using UnityEngine;
using System;
using System.Collections;

using IronScheme;
using IronScheme.Runtime;

public class TestBehavior : MonoBehaviour {
	
  public Callable updateFn;
	
  void Start () {
	IronScheme.RuntimeExtensions.Eval("(import (ironscheme clr)) (clr-using UnityEngine)");
	IronScheme.RuntimeExtensions.Eval(String.Format("(library-path (cons \"{0}\" (library-path)))", Application.streamingAssetsPath));
    IronScheme.RuntimeExtensions.Eval("(import (testbehavior))");
    updateFn = IronScheme.RuntimeExtensions.Eval<Callable>("update");
  }
	
  void Update () {
    updateFn.Call();
  }
}