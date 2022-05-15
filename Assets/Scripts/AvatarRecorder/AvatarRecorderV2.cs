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


public partial class AvatarRecorderV2 : MonoBehaviour
{
    [SerializeField] private MicRecorder mic;

    public string savePath = "Assets/Animations/";

    [SerializeField] float playbackSpeed = 1f;

    public AnimationClip animClip;
    private GameObjectRecorder m_Recorder;
    public GameObject clonePool;
    [SerializeField] private GameObject cloneInstance;
    [SerializeField] private PlayableDirector cloneDirector;
    private TimelineAsset timeline;
    [SerializeField] private Animator cloneAnimator;
    [SerializeField] private AudioSource cloneAudioSource;
    [SerializeField] private AudioClip cloneClip;
    [SerializeField] private int count = 0;

    [SerializeField]  private List<Transform> childrenList;

    public UnityEvent OnStartRecording;
    public UnityEvent OnStopRecording;
    public UnityEvent OnPlayRecording;

    private void Start()
    {
        // Gets the pool of clones
        if (clonePool)
        {
            childrenList = new List<Transform>();

            for(int i = 0; i < clonePool.transform.childCount; i++)
            {
                childrenList.Add(clonePool.transform.GetChild(i));
                clonePool.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


    private AnimationClip CreateNewClip()
    {
        AnimationClip c = new AnimationClip();
        c.name = "Clone" + count;
        return c;
    }

    [EasyButtons.Button]
    public void StartRecording()
    {
        //Animation
        animClip = CreateNewClip();

        // Create recorder and record the script GameObject. It records this game object
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);

        // Audio
        // capture mic audio
        if (mic) mic.StartCapture();

        OnStartRecording.Invoke();
    }

    [EasyButtons.Button]
    public void StopRecording()
    {
        if (animClip == null)
            return;

        if (m_Recorder.isRecording)
        {
            // Animation
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(animClip); // if (animClip.Count < count) 
            // Save the animation clip into a file
            string clipname = savePath + animClip.name + ".anim";
            AssetDatabase.CreateAsset(animClip, clipname);
            AssetDatabase.SaveAssets();

            // Audio
            // Stop mic
            if (mic) mic.StopCapture();

            OnStopRecording.Invoke();

            // Find and cache components
            if (childrenList != null)
            {
                Transform clone = childrenList[count];
                cloneInstance = clone.gameObject;

                // Get and cache the playable director component from the instance clone
                if (cloneInstance.GetComponentInChildren<PlayableDirector>()) cloneDirector = cloneInstance.GetComponentInChildren<PlayableDirector>();

                // try get the clone instance's animator component
                if (cloneInstance.GetComponentInChildren<Animator>()) cloneAnimator = cloneInstance.GetComponentInChildren<Animator>();    
                
                // Try to get the clone's audio source component
                if (cloneInstance.GetComponentInChildren<AudioSource>()) cloneAudioSource = cloneInstance.GetComponentInChildren<AudioSource>();                
            }
            count++;
        }
    }

    [EasyButtons.Button]
    private void LoadLastAudioFile()
    {
        StartCoroutine(GetAudioClip());

        if (cloneClip) cloneAudioSource.clip = cloneClip;
    }

    [EasyButtons.Button]
    public void PlayRecording()
    {
        // Create and set an instance of a timeline asset for the director
        timeline = new TimelineAsset();
        cloneDirector.playableAsset = timeline;

        // Load the last file captures by the mic
        LoadLastAudioFile();

        CreateAnimationTimelineAsset();
        CreateAudioTimelineAsset();

        cloneInstance.SetActive(true);

        // 4. Play the timeline
        cloneDirector?.Play();

        OnPlayRecording.Invoke();
    }

    //[EasyButtons.Button]
    private void CreateAnimationTimelineAsset()
    {
        // 1. Create new Animation Track in director's TimelineAsset
        //TimelineAsset timeline = cloneDirector?.playableAsset as TimelineAsset;

        // Note - we're deleting the track if it exists already, since we want to generate everything on the spot for this example
        foreach (TrackAsset track in timeline.GetOutputTracks())
            if (track.name == cloneInstance.name)
                timeline.DeleteTrack(track);
        AnimationTrack newTrack = timeline.CreateTrack<AnimationTrack>(cloneInstance.name + "Anim");

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
        //TimelineAsset timeline = cloneDirector?.playableAsset as TimelineAsset;

        // Note - we're deleting the track if it exists already, since we want to generate everything on the spot for this example
        foreach (TrackAsset line in timeline.GetOutputTracks())
            if (line.name == cloneInstance.name)
                timeline.DeleteTrack(line);
        /// Create new track
        AudioTrack track = timeline.CreateTrack<AudioTrack>(cloneInstance.name + "Audio");

        /// Create an audio clip in the track
        var audioAsset = (AudioPlayableAsset)(track.CreateClip<AudioPlayableAsset>().asset);
        audioAsset.name = "Clone" + count + "AudioAsset"; /// Not entirely sure if this is necessary
        if (cloneClip) audioAsset.clip = cloneClip; /// This is an AudioClip type object which works when adding it to a regular AudioSource component not related to the timeline
        audioAsset.clip.name = "Clone" + count + "AudioClip";

        /// Bind the audio clip to the audio player gameobject
        /// Note: this seems to be optional, but without doing this, the Audio Source field in your timeline track will still say "None" as pictured in my original post
        cloneDirector?.SetReferenceValue("Clone" + count + "Audio", cloneAudioSource.gameObject);
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
                cloneClip = DownloadHandlerAudioClip.GetContent(www);
            }
        }
        //yield return new WaitForSeconds(10);
    }

    void LateUpdate()
    {
        if (animClip == null)
            return;

        // Take a snapshot and record all the bindings values for every frame.
        if (m_Recorder) m_Recorder.TakeSnapshot(Time.deltaTime);
    }

}
