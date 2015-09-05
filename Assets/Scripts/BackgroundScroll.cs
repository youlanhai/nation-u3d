using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	public float scrollSpeed_ = -0.1f;
	public float tileSizeZ_ = 7.4f;
	
	private Vector3 startPosition_;
	
	void Start ()
	{
		startPosition_ = transform.position;
	}
	
	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed_, tileSizeZ_);
		transform.position = new Vector3(startPosition_.x, startPosition_.y + newPosition, startPosition_.z);
	}
}
