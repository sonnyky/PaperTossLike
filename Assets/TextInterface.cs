using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface TextInterface : IEventSystemHandler {
	
	void OnChange ();
}
