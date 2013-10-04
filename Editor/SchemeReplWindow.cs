using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SchemeReplWindow : EditorWindow {
	
	[MenuItem("Window/Repl")]
	public static void ShowSchemeReplWindow() {
		SchemeReplWindow window = EditorWindow.GetWindow<SchemeReplWindow>();
		window.title = "Repl";
	}
	
	// Repl
	List<string> replHistory = new List<string>();
	string currentForm = "";
	string currentLine = "";
	
	bool parensMatch(string form) {
		int lefts = form.Split('(').Length - 1;
		int rights = form.Split(')').Length - 1;
		return lefts == rights;
	}
	
	string Eval(string form) {
		try {
			var result = IronScheme.RuntimeExtensions.Eval(form);
			return result.ToString ();
		} catch (Exception e) {
			return e.ToString();
		}	
	}
	
	void OnEnable() {
		IronScheme.RuntimeExtensions.Eval("(import (ironscheme clr)) (clr-using UnityEngine)");
		IronScheme.RuntimeExtensions.Eval(String.Format("(library-path (cons \"{0}\" (library-path)))", Application.streamingAssetsPath));
	}
	
	Vector2 scroll_position = Vector2.zero;
	
	void OnGUI () {
		GUILayout.Label("Scheme Repl", EditorStyles.boldLabel);
		scroll_position = GUILayout.BeginScrollView(scroll_position, false, true);
		
		foreach (string line in replHistory) {
			GUILayout.Label(line);	
		}
		
		GUILayout.EndScrollView();
		
		GUI.SetNextControlName("repl-input");
		currentLine = GUILayout.TextField(currentLine);
		
		if (Event.current.isKey && Event.current.keyCode == KeyCode.Return) {
			replHistory.Add(currentLine);
			currentForm = currentForm + "\n" + currentLine;
			currentLine = "";
			if (parensMatch(currentForm) && currentForm != "") {
				replHistory.Add(">" + Eval(currentForm));
				currentForm = "";
			}
			scroll_position.y = 100000;
		}
	}
	
	void OnInspectorUpdate() {
		// Redraw more often so we see results when they are evaluated.
		Repaint();
	}
	
	void OnFocus() {
		// Give keyboard focus to the repl textbox.
		GUI.FocusControl("repl-input");
	}
}
