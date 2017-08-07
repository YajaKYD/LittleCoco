using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[RequireComponent (typeof(AudioSource))]

public class Play_Tutorial_Ending : MonoBehaviour {

	//public MovieTexture _Ending_movie;
	//public Text _text;
	//private AudioSource audio;
	//private bool xxx = false;

//	public MovieTexture video;
//	private AudioSource aa;

	//public bool aa = false;


	// Use this for initialization
	void Start () {

//		GetComponent<Renderer> ().material.mainTexture = video as MovieTexture;
//		aa = GetComponent<AudioSource> ();
//		video.Play ();

		//_text.gameObject.SetActive (false);
		//GetComponent<RawImage> ().texture = _Ending_movie as MovieTexture;
		//audio = GetComponent<AudioSource> ();
		//audio.clip = _Ending_movie.audioClip;
		//_Ending_movie.Play ();
		//audio.Play ();
		//print(Handheld.PlayFullScreenMovie ("Tutorial_ending.mp4",Color.black,FullScreenMovieControlMode.CancelOnInput));

		//temporary delete for building
		//Handheld.PlayFullScreenMovie ("Tutorial_ending.mp4",Color.black,FullScreenMovieControlMode.Hidden);

	}


	// Update is called once per frame
	void Update () {

		//print(Handheld.PlayFullScreenMovie ("Tutorial_ending.mp4",Color.black,FullScreenMovieControlMode.Hidden));
		/*
		if (Input.GetKeyDown (KeyCode.Space) && _Ending_movie.isPlaying) {
			_Ending_movie.Pause ();
		} else if (Input.GetKeyDown (KeyCode.Space) && !_Ending_movie.isPlaying) {
			_Ending_movie.Play ();
		}
		*/
		/*
		if (!_Ending_movie.isPlaying) {
			_text.gameObject.SetActive (true);
			xxx = true;
		}
		if (xxx && Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene (6);
		}
		*/

//		if (aa) {
//			if (Input.GetMouseButtonDown (0)) {
//				SceneManager.LoadScene (3);
//			}
//		}
	}

//	IEnumerator PlayVideo(){
//		
//		yield return new WaitForEndOfFrame ();
//		print ("SS");
//		aa = true;
//	}
}
