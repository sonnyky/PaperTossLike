using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface PaperTypeInterface : IEventSystemHandler {

	void OnPaperTypeChange (int paperTypeNum);

}
