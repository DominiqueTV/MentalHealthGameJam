using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class AvatarRecorderV2 : MonoBehaviour
{
    [SerializeField] float playbackSpeed = 1f;

    public AnimationClip animClip;
    private GameObjectRecorder m_Recorder;
    public GameObject clonePool;
    [SerializeField] private GameObject cloneInstance;
    [SerializeField] private PlayableDirector cloneDirector;
    [SerializeField] private Animator cloneAnimator;
    [SerializeField] private int count = 0;

    [SerializeField]  private List<Transform> childrenList;

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


    // Cant create an animation clip at runtine
    private AnimationClip CreateNewClip()
    {
        AnimationClip c = new AnimationClip();
        c.name = "Clone" + count;
        return c;
    }



    [EasyButtons.Button]
    public void StartRecording()
    {
        animClip = CreateNewClip();

        // Create recorder and record the script GameObject. It records this game object
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    [EasyButtons.Button]
    public void StopRecording()
    {
        if (animClip == null)
            return;

        if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(animClip); // if (animClip.Count < count) 
       
            if (childrenList != null)
            {
                Transform clone = childrenList[count];
                cloneInstance = clone.gameObject;

                // Get and cache the playable director component from the instance clone
                if (cloneInstance.GetComponentInChildren<PlayableDirector>()) cloneDirector = cloneInstance.GetComponentInChildren<PlayableDirector>();

                // try get the clone instance's animator component
                if (cloneInstance.GetComponentInChildren<Animator>()) cloneAnimator = cloneInstance.GetComponentInChildren<Animator>();                
            }
                               
            PlayRecording();
            count++;
        }
    }

    [EasyButtons.Button]
    private void PlayRecording()
    {
        // 1. Create new Animation Track in director's TimelineAsset
        TimelineAsset asset = cloneDirector?.playableAsset as TimelineAsset;

        // Note - we're deleting the track if it exists already, since we want to generate everything on the spot for this example
        foreach (TrackAsset track in asset.GetOutputTracks())
            if (track.name == cloneInstance.name)
                asset.DeleteTrack(track);
        AnimationTrack newTrack = asset.CreateTrack<AnimationTrack>(cloneInstance.name);

        // 2. Make the created animation track reference the animationToAdd
        TimelineClip clip = newTrack.CreateClip(animClip);
        clip.start = 0.1f;
        clip.timeScale = playbackSpeed;
        clip.duration = clip.duration / clip.timeScale;

        // 3. Edit the director's TimelineInstance and configure the bindings to reference objectToAnimate
        cloneDirector?.SetGenericBinding(newTrack, cloneAnimator);

        // 4. Play the timeline
        cloneDirector?.Play();

        // set position and rotation
        cloneInstance.transform.position = transform.position;
        cloneInstance.transform.rotation = transform.rotation;
        
        cloneInstance.SetActive(true);
    }


    void LateUpdate()
    {
        if (animClip == null)
            return;

        // Take a snapshot and record all the bindings values for every frame.
        if (m_Recorder) m_Recorder.TakeSnapshot(Time.deltaTime);
    }

}
