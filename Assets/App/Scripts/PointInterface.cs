using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface PointInterface : IEventSystemHandler {

	void OnSuccess ();
	void OnFailure ();

}
