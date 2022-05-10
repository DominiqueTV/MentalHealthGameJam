using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class AudioRecorder : MonoBehaviour
{

    private string absolutePath = "./Audio/Recordings"; // relative path to where the app is running



    public bool powerOn;
    private bool recording;
    private bool Playing;
    private bool Paused;
    public Text RecTimeSelect;
    public float tmeleft;
    public float tmerec;
    public float tmestart;
    public float tmeend;
    private bool done;
    public float tmeprdct;
    public float pctleft;
    public float pctcmplt;
    public Color TextColor;
    private bool micRecording = false;
    private int minFreq;
    private int maxFreq;
    private static int HEADER_SIZE;
    public AudioSource goAudioSource;

    //Use this for initialization
    void Start()
    {
        done = false;
        TextColor = RecTimeSelect.color;
        HEADER_SIZE = 44;


    }

    void Update()
    {
        micRecording = Microphone.IsRecording(" ");

        if (micRecording)
        {
            tmeend = Time.time;
            tmeleft = tmeprdct - tmeend;
            done = false;

            pctleft = (int)(tmeleft / tmerec * 100);
            if (pctleft < 0.5f)
                pctleft = 0;
            pctcmplt = 100 - pctleft;
        }
        else
        {
            recording = false;

            if (!done)
            {
                tmeend = Time.time;

                tmeleft = tmeend - tmestart;
                done = true;
            }

            tmeend = Time.time;
            tmeleft = tmeprdct - tmeend;

            pctleft = (int)(tmeleft / tmerec * 100);
            if (pctleft < 0.5f)
                pctleft = 0;
            pctcmplt = 100 - pctleft;
        }
    }

    [EasyButtons.Button]
    public void Record()
    {
        micRecording = true;
        Playing = false;
        Paused = false;
        done = false;
        tmestart = Time.time;

        if (tmerec == 0f)
        {
            RecTimeSelect.text = ("TIME REQUIRED");
            TextColor = RecTimeSelect.color;
            RecTimeSelect.color = Color.red;
        }
        else
        {
            goAudioSource.clip = Microphone.Start(null, false, (int)tmerec, 44100);
        }
    }

    [EasyButtons.Button]
    public void StopRecord()
    {
        micRecording = false;
        tmeend = Time.time;
        Microphone.End(null); //Stop the audio recording

    }

    [EasyButtons.Button]
    public void Play()
    {
        if (micRecording)
            StopRecord();

        Playing = true;
        if (Paused)
        {
            Paused = false;
        }
        goAudioSource.Play(); //Playback the recorded audio
    }

    [EasyButtons.Button]
    public void StopPlay()
    {
        Playing = false;
        goAudioSource.Stop(); //Playback the recorded audio
    }

    [EasyButtons.Button]
    public void Stop()
    {

        Playing = false;
        Paused = false;

        if (micRecording)
            StopRecord();
        else
        {
            StopPlay();
            Playing = false;
        }
        micRecording = false;
    }

    [EasyButtons.Button]
    public void Pause()
    {
        if (!micRecording & Playing)
        {
            if (!Paused)
            {
                goAudioSource.Pause(); //Pause Playback
                Paused = true;

            }
            else
            {
                goAudioSource.Play(); //Playback the recorded audio
                Paused = false;
            }
        }
        if (micRecording)
        {
            Stop();
            Paused = false;
        }
    }

    [EasyButtons.Button]
    public void SaveToDisk()
    {
        AudioRecorder.Save("myFile", goAudioSource.clip);
    }

    public void RecordTime1m()
    {
        tmerec = 5;
        RecTimeSelect.text = ("Time Selected 5 S");
        TimeSelected();
    }

    public void RecordTime5m()
    {
        tmerec = 300;
        RecTimeSelect.text = ("Time Selected 5 M");
        TimeSelected();
    }

    public void RecordTime15m()
    {
        tmerec = 900;
        RecTimeSelect.text = ("Time Selected 15 M");
        TimeSelected();
    }

    public void RecordTime30m()
    {
        tmerec = 1800;
        RecTimeSelect.text = ("Time Selected 30 M");
        TimeSelected();
    }

    public void RecordTime60m()
    {
        tmerec = 3600;
        RecTimeSelect.text = ("Time Selected 60 M");
        TimeSelected();
    }

    public void RecordTime2h()
    {
        tmerec = 7200;
        RecTimeSelect.text = ("Time Selected 2 H");
        TimeSelected();
    }

    public void RecordTime4h()
    {
        tmerec = 14400;
        RecTimeSelect.text = ("Time Selected 4 H");
        TimeSelected();
    }

    public void RecordTime8h()
    {
        tmerec = 28800;
        RecTimeSelect.text = ("Time Selected 8 H");
        TimeSelected();
    }

    public void TimeSelected()
    {
        RecTimeSelect.color = TextColor;
    }

    public static bool Save(string filename, AudioClip clip)
    {

        if (!filename.ToLower().EndsWith(".wav"))
        {

            filename += ".wav";

        }



        var filepath = Path.Combine(Application.streamingAssetsPath, filename);



        Debug.Log(filepath);



        // Make sure directory exists if user is saving to sub dir.

        Directory.CreateDirectory(Path.GetDirectoryName(filepath));



        using (var fileStream = CreateEmpty(filepath))
        {



            ConvertAndWrite(fileStream, clip);



            WriteHeader(fileStream, clip);

        }



        return true; // TODO: return false if there's a failure saving the file

    }

    public static AudioClip TrimSilence(AudioClip clip, float min)
    {

        var samples = new float[clip.samples];



        clip.GetData(samples, 0);



        return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);

    }

    public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz)
    {

        return TrimSilence(samples, min, channels, hz, false, false);

    }

    public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream)
    {

        int i;



        for (i = 0; i < samples.Count; i++)
        {

            if (Mathf.Abs(samples[i]) > min)
            {

                break;

            }

        }



        samples.RemoveRange(0, i);



        for (i = samples.Count - 1; i > 0; i--)
        {

            if (Mathf.Abs(samples[i]) > min)
            {

                break;

            }

        }



        samples.RemoveRange(i, samples.Count - i);



        var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);



        clip.SetData(samples.ToArray(), 0);



        return clip;

    }

    static FileStream CreateEmpty(string filepath)
    {

        var fileStream = new FileStream(filepath, FileMode.Create);

        byte emptyByte = new byte();



        for (int i = 0; i < HEADER_SIZE; i++)
        { //preparing the header

            fileStream.WriteByte(emptyByte);

        }



        return fileStream;

    }

    static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {



        var samples = new float[clip.samples];



        clip.GetData(samples, 0);



        Int16[] intData = new Int16[samples.Length];

        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]



        Byte[] bytesData = new Byte[samples.Length * 2];

        //bytesData array is twice the size of

        //dataSource array because a float converted in Int16 is 2 bytes.



        int rescaleFactor = 32767; //to convert float to Int16



        for (int i = 0; i < samples.Length; i++)
        {

            intData[i] = (short)(samples[i] * rescaleFactor);

            Byte[] byteArr = new Byte[2];

            byteArr = BitConverter.GetBytes(intData[i]);

            byteArr.CopyTo(bytesData, i * 2);

        }



        fileStream.Write(bytesData, 0, bytesData.Length);

    }

    static void WriteHeader(FileStream fileStream, AudioClip clip)
    {



        var hz = clip.frequency;

        var channels = clip.channels;

        var samples = clip.samples;



        fileStream.Seek(0, SeekOrigin.Begin);



        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");

        fileStream.Write(riff, 0, 4);



        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);

        fileStream.Write(chunkSize, 0, 4);



        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");

        fileStream.Write(wave, 0, 4);



        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");

        fileStream.Write(fmt, 0, 4);



        Byte[] subChunk1 = BitConverter.GetBytes(16);

        fileStream.Write(subChunk1, 0, 4);



        UInt16 two = 2;

        UInt16 one = 1;



        Byte[] audioFormat = BitConverter.GetBytes(one);

        fileStream.Write(audioFormat, 0, 2);



        Byte[] numChannels = BitConverter.GetBytes(channels);

        fileStream.Write(numChannels, 0, 2);



        Byte[] sampleRate = BitConverter.GetBytes(hz);

        fileStream.Write(sampleRate, 0, 4);



        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2

        fileStream.Write(byteRate, 0, 4);



        UInt16 blockAlign = (ushort)(channels * 2);

        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);



        UInt16 bps = 16;

        Byte[] bitsPerSample = BitConverter.GetBytes(bps);

        fileStream.Write(bitsPerSample, 0, 2);



        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");

        fileStream.Write(datastring, 0, 4);



        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);

        fileStream.Write(subChunk2, 0, 4);



        //        fileStream.Close();

    }

}