using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AvatarRecorder
{
    [RequireComponent(typeof(MicRecorder))]
    public partial class AvatarRecorderV3 : MonoBehaviour
    {
        [SerializeField] private bool debug = false;

        [Tooltip("Place a copy of the avatar in here, we use this to instantiate a clone. It can be a stripped down version as long as the hierarchy is the same")]
        [SerializeField] private GameObject clonePrefab;
     
        private GameObjectRecorder m_Recorder;

        [Space(10)]
        [Header("File Destinations")]

        [Tooltip("The folder that the animation clips will be saved into")]
        public string animationSavePath = "Assets/AvatarRecorder/Clones/Animations/";

        [Tooltip("The folder that the audio clips will be saved into")]
        public string audioClipSavePath = "Assets/AvatarRecorder/Clones/AudioClips/";

        [Tooltip("The folder that the timelines will be saved into")]
        public string timelineSavePath = "Assets/AvatarRecorder/Clones/Timelines/";

        [Space(10)]
        [Header("Control Variables")]
        [SerializeField] private bool recordMicAudio = true;
        [SerializeField] private bool playAfterRecording = true;
        [SerializeField] private bool loop = true;
        [SerializeField] private float playbackSpeed = 1f;

        // Messages
        public delegate void StartedRecording();
        public static event StartedRecording onStartRecording;

        public delegate void StoppedRecording();
        public static event StoppedRecording onStopRecording;

        public delegate void PlayedRecording();
        public static event PlayedRecording onPlayRecording;

        // Events
        [Space(10)]
        [Header("Unity Events")]
        public UnityEvent OnStartRecording;
        public UnityEvent OnStopRecording;
        public UnityEvent OnPlayRecording;


        // Variables
        [Space(10)]
        [Header("Debug Variables")]
        [SerializeField] private MicRecorder mic;
        [SerializeField] private List<GameObject> cloneInstances;

        [SerializeField] private GameObject cloneInstance;
        [SerializeField] private TimelineAsset cloneTimeline;
        [SerializeField] private AnimationClip animClip;
        [SerializeField] private PlayableDirector cloneDirector;
        [SerializeField] private Animator cloneAnimator;
        [SerializeField] private AudioSource cloneAudioSource;
        [SerializeField] private AudioClip loadedAudioClip;


        //[SerializeField] private int count = 0;

        private void Start()
        {
            // Caches the mic
            if (!mic) mic = GetComponent<MicRecorder>();
            mic.saveFolder = audioClipSavePath;

            if (debug)
            {
                if (!clonePrefab) Debug.LogError("The Clone Prefab is missing on the AvatarRecorder");
            }
        }

        [EasyButtons.Button]
        public void StartRecording()
        {
            // Create recorder and record the script GameObject. It records this game object
            m_Recorder = new GameObjectRecorder(gameObject);

            // Bind all the Transforms on the GameObject and all its children.
            m_Recorder.BindComponentsOfType<Transform>(gameObject, true);

            // Audio
            // capture mic audio
            if (recordMicAudio)
                if (mic) mic.StartCapture();

            OnStartRecording?.Invoke();
            onStartRecording?.Invoke();
        }

        [EasyButtons.Button]
        public void StopRecording()
        {
            if (m_Recorder.isRecording)
            {
                // Animation
                //Create the animation clip
                animClip = new AnimationClip();
                // Save the recorded session to the clip.
                m_Recorder.SaveToClip(animClip); // if (animClip.Count < count) 
                                                 // Save the animation clip into a file
                string clipname = animationSavePath + "CloneAnim" + cloneInstances.Count + ".anim";
                AssetDatabase.CreateAsset(animClip, clipname);
                AssetDatabase.SaveAssets();

                // Audio
                // Stop mic
                if (recordMicAudio)
                    if (mic) mic.StopCapture();

                OnStopRecording?.Invoke();
                onStopRecording?.Invoke();

                // Do we want to play recording afterwards?
                if (playAfterRecording)
                    PlayRecording();



                /*
                // Find and cache components
                if (cloneInstances != null)
                {
                    Transform clone = cloneInstances[cloneInstances.Length].transform;

                    // Get and cache the playable director component from the instance clone
                    if (clone.GetComponentInChildren<PlayableDirector>()) cloneDirector = cloneInstance.GetComponentInChildren<PlayableDirector>();

                    // try get the clone instance's animator component
                    if (clone.GetComponentInChildren<Animator>()) cloneAnimator = cloneInstance.GetComponentInChildren<Animator>();    

                    // Try to get the clone's audio source component
                    if (clone.GetComponentInChildren<AudioSource>()) cloneAudioSource = cloneInstance.GetComponentInChildren<AudioSource>();                
                }
                */
            }
        }

        [EasyButtons.Button]
        private void LoadLastAudioFile()
        {
            StartCoroutine(GetAudioClip());

            //if (cloneClip) cloneAudioSource.clip = cloneClip;
        }

        [EasyButtons.Button]
        public void PlayRecording()
        {
            // Create instance from prefab
            if (clonePrefab) cloneInstance = Instantiate(clonePrefab);
            else return;

            cloneInstances.Add(cloneInstance);

            // Check to see if there are the wanted components: Animator, Playable Director, AudioSource
            // If not, the create them
            if (!cloneInstance.GetComponentInChildren<Animator>())
                cloneAnimator = cloneInstance.AddComponent<Animator>();
            else
                cloneAnimator = cloneInstance.GetComponentInChildren<Animator>();

            if (!cloneInstance.GetComponentInChildren<PlayableDirector>())
                cloneDirector = cloneInstance.AddComponent<PlayableDirector>();
            else
                cloneDirector = cloneInstance.GetComponentInChildren<PlayableDirector>();

            if (!cloneInstance.GetComponentInChildren<AudioSource>())
                cloneAudioSource = cloneInstance.AddComponent<AudioSource>();
            else
                cloneAudioSource = cloneInstance.GetComponentInChildren<AudioSource>();

            // Create Timeline asset
            cloneTimeline = new TimelineAsset();

            // Populate the director if needed
            if (!cloneDirector.playableAsset) cloneDirector.playableAsset = cloneTimeline;

            // Loop?
            if (loop)
                cloneDirector.extrapolationMode = DirectorWrapMode.Loop;




            // Create Animation Timeline Asset from last saved animation clip
            CreateAnimationTimelineAsset();

            // Load the last file captured by the mic
            LoadLastAudioFile();
            // Create Audio Timeline Asset from the last saved audio clip
            CreateAudioTimelineAsset();

            // Save the timeline
            string timelineName = timelineSavePath + "CloneTimeline" + cloneInstances.Count + ".playable";
            AssetDatabase.CreateAsset(cloneTimeline, timelineName);
            AssetDatabase.SaveAssets();

            // Play the timeline
            cloneInstance.SetActive(true);


            // Play the timeline
            cloneDirector?.Play();

            OnPlayRecording?.Invoke();
            onPlayRecording?.Invoke();
        }

        //[EasyButtons.Button]
        private void CreateAnimationTimelineAsset()
        {
            // Note - we're deleting the track if it exists already, since we want to generate everything on the spot for this example
            foreach (TrackAsset track in cloneTimeline.GetOutputTracks())
                if (track.name == cloneInstance.name)
                    cloneTimeline.DeleteTrack(track);

            AnimationTrack newTrack = cloneTimeline.CreateTrack<AnimationTrack>(cloneInstance.name + "Anim");

            // 2. Make the created animation track reference the animationToAdd
            TimelineClip clip = newTrack.CreateClip(animClip);
            clip.start = 0.0f;
            clip.timeScale = playbackSpeed;
            clip.duration = clip.duration / clip.timeScale;

            // 3. Edit the director's TimelineInstance and configure the bindings to reference objectToAnimate
            cloneDirector?.SetGenericBinding(newTrack, cloneAnimator);

            // set position and rotation
            cloneInstance.transform.position = transform.position;
            cloneInstance.transform.rotation = transform.rotation;
        }

        //[EasyButtons.Button]
        private void CreateAudioTimelineAsset()
        {
            // Note - we're deleting the track if it exists already, since we want to generate everything on the spot for this example
            foreach (TrackAsset line in cloneTimeline.GetOutputTracks())
                if (line.name == cloneInstance.name)
                    cloneTimeline.DeleteTrack(line);
            /// Create new track
            AudioTrack track = cloneTimeline.CreateTrack<AudioTrack>(cloneInstance.name + "Audio");

            /// Create an audio clip in the track
            var audioAsset = (AudioPlayableAsset)(track.CreateClip<AudioPlayableAsset>().asset);
            if (loadedAudioClip) audioAsset.clip = loadedAudioClip; /// This is an AudioClip type object which works when adding it to a regular AudioSource component not related to the timeline

                                                                    /// Bind the audio clip to the audio player gameobject
                                                                    /// Note: this seems to be optional, but without doing this, the Audio Source field in your timeline track will still say "None" as pictured in my original post
            cloneDirector?.SetReferenceValue(cloneInstance.name, cloneAudioSource.gameObject);
            cloneDirector?.SetGenericBinding(track, cloneAudioSource);
        }


        private IEnumerator GetAudioClip()
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(mic.GetLastAudioClip(), AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    loadedAudioClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
            //yield return new WaitForSeconds(10);
        }

        void LateUpdate()
        {
            // Take a snapshot and record all the bindings values for every frame.
            if (m_Recorder) m_Recorder.TakeSnapshot(Time.deltaTime);
        }
    }
}
