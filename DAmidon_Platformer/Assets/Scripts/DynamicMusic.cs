using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public int bpm;
    public int barsPerSegment;
    public int beatsPerBar;
    [Range(1, 8)] public int startDelay;
    float segmentLength;
    float lastStarted;
    float bps;

    public InstrumentGroup[] instrumentGroups;

    private void Start()
    {
        //Get beats per second
        bps = bpm / 60f;

        //get length of segment
        segmentLength = bps * barsPerSegment * beatsPerBar;

        //Start song
        lastStarted = segmentLength - startDelay;
    }

    private void Update()
    {
        //loop through random segments
        lastStarted += Time.deltaTime;
        if(lastStarted > segmentLength)
        {
            lastStarted = 0;
            PlayRandom();
        }
    }

    void PlayRandom()
    {
        foreach(InstrumentGroup group in instrumentGroups)
        {
            //Select random instrument per group
            int randomInstrument = UnityEngine.Random.Range(0, group.instruments.Length);
            Instrument selectedInstrument = group.instruments[randomInstrument];

            //Random Clip on selected instrument
            int randomClip = UnityEngine.Random.Range(0, selectedInstrument.instrumentClips.Length);

            //Play selected group:instrument:clip if exists
            if (selectedInstrument.instrumentClips[randomClip] != null)
            {
                //Create object as the clip needs time to taper off.
                AudioSource _source = Instantiate(SoundManager.instance.source, transform.position, Quaternion.identity).GetComponent<AudioSource>();

                //Set up clip
                _source.clip = selectedInstrument.instrumentClips[randomClip];
                _source.volume = SoundManager.instance.masterVolume * selectedInstrument.instrumentVolume;
                _source.name = $"{group.groupName}: {selectedInstrument.instrumentName}_{randomClip}";

                //Play
                DontDestroyOnLoad(_source.gameObject); //Doesn't cause breaks when scene load
                Destroy(_source.gameObject, _source.clip.length);
                _source.Play();
            }
        }
    }
}

[Serializable]
public class InstrumentGroup
{
    public string groupName;
    public Instrument[] instruments;
}

[Serializable]
public class Instrument 
{
    public string instrumentName;
    public float instrumentVolume;
    public AudioClip[] instrumentClips;
}

