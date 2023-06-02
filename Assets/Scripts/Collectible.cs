using UnityEngine;

namespace Modl.Demo
{
	public class Collectible : MonoBehaviour
	{
		//private Score score;
		void OnTriggerEnter(Collider playerCollider)
		{
			if (!playerCollider.CompareTag("Player")) return;
			
#if MODL_AUTOMATIC_TESTING
			//Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
			Modl.EventReporter.Report("PickedUpCollectible", this.gameObject.name);
#endif
				
			var score = playerCollider.gameObject.GetComponent(typeof(Score)) as Score;
			if (score != null) score.count += 1;
				
			gameObject.SetActive(false);
		}
	}
}
