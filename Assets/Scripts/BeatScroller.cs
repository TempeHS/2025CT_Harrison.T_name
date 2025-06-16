using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float beatTempo;

    public bool hasStarted;

    public Transform[] laneTargets;

    public GameObject leftArrowPrefab;
    public GameObject downArrowPrefab;
    public GameObject upArrowPrefab;
    public GameObject rightArrowPrefab;

    public float noteHitY = -3.5f;
    public float originalNoteSpawnY = 5.0f;

    private float _noteFallDuration;
    private float nextBeatSpawnTime;
    private int currentBeatIndex = 0;

    [System.Serializable]
    public class NoteSpawnInfo
    {
        public int laneIndex;

        public NoteSpawnInfo(int lane)
        {
            laneIndex = lane;
        }
    }

    private List<List<NoteSpawnInfo>> beatMap = new List<List<NoteSpawnInfo>>
    {
         // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
        // --- END OF REPEATING SEGMENT (16 Beats) --- // --- START OF REPEATING SEGMENT (16 Beats) ---
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
                                                                                                                            // --- END OF REPEATING SEGMENT (16 Beats) ---
         new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
                                                                                                                            // --- END OF REPEATING SEGMENT (16 Beats) ---
         new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
                                                                                                                            // --- END OF REPEATING SEGMENT (16 Beats) ---
         new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
                                                                                                                            // --- END OF REPEATING SEGMENT (16 Beats) ---
         new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1) }, // Left & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // Up & Right Chord
        new List<NoteSpawnInfo> { },                                         // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(3) }, // Left & Right Chord

        new List<NoteSpawnInfo> { new NoteSpawnInfo(1) }, // Down
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3) }, // Right
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0) }, // Left
        new List<NoteSpawnInfo> { new NoteSpawnInfo(2) }, // Up

        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2) }, // Triple Note Chord
        new List<NoteSpawnInfo> { },                                                                  // Empty beat
        new List<NoteSpawnInfo> { new NoteSpawnInfo(3), new NoteSpawnInfo(1) },                         // Right & Down Chord
        new List<NoteSpawnInfo> { new NoteSpawnInfo(0), new NoteSpawnInfo(1), new NoteSpawnInfo(2), new NoteSpawnInfo(3) }, // All Four Notes!
                                                                                                                            // --- END OF REPEATING SEGMENT (16 Beats) ---
        // --- END OF REPEATING SEGMENT (16 Beats) ---
        
    };

    void Start()
    {
        beatTempo = beatTempo / 60f;


        float trueFixedFallDistance = originalNoteSpawnY - noteHitY;

        if (beatTempo > 0)
        {
            _noteFallDuration = trueFixedFallDistance / beatTempo;
        }
        else
        {
            Debug.LogError("BeatTempo is zero or negative!");
            _noteFallDuration = 1f;
        }


        nextBeatSpawnTime = Time.time;
        currentBeatIndex = 0;
    }



    // Update is called once per frame
    void Update()
    {

        if (!hasStarted)
        {
            return; // Don't update if the game hasn't started
        }
        if (Time.time >= nextBeatSpawnTime && currentBeatIndex < beatMap.Count)
        {
            SpawnNotesForBeat(beatMap[currentBeatIndex]);
            nextBeatSpawnTime += (60f / (beatTempo * 60f));
            currentBeatIndex++;
        }
    }

    void SpawnNotesForBeat(List<NoteSpawnInfo> notesToSpawn)
    {
        float currentVisualSpawnY = originalNoteSpawnY;
        if (GameManager.instance != null)
        {
            float effectiveVisualFallDistance = (originalNoteSpawnY - noteHitY) * GameManager.instance.visualScrollSpeedMultiplier;
            currentVisualSpawnY = noteHitY + effectiveVisualFallDistance;
        }


        float noteSpeedPerSecond = (currentVisualSpawnY - noteHitY) / _noteFallDuration;

        foreach (NoteSpawnInfo noteInfo in notesToSpawn)
        {
            if (laneTargets.Length <= noteInfo.laneIndex)
            {
                Debug.LogWarning($"Lane index {noteInfo.laneIndex} out of bounds for laneTargets array. Skipping note spawn.");
                continue;
            }


            GameObject notePrefabToUse = null;
            float noteSpawnX = laneTargets[noteInfo.laneIndex].position.x;

            switch (noteInfo.laneIndex)
            {
                case 0:
                    notePrefabToUse = leftArrowPrefab; break;
                case 1:
                    notePrefabToUse = downArrowPrefab; break;
                case 2:
                    notePrefabToUse = upArrowPrefab; break;
                case 3:
                    notePrefabToUse = rightArrowPrefab; break;
                default:
                Debug.LogWarning($"Invalid lane index {noteInfo.laneIndex}. Skipping note spawn.");
                notePrefabToUse = null; // Reset to null if the lane index is invalid
                break; // Skip this iteration if the lane index is invalid
            }

            if (notePrefabToUse == null)
            {
                Debug.LogError("No valid note prefab found for lane index " + noteInfo.laneIndex + ". Skipping this note.");
                continue;
            }

            GameObject noteGo = Instantiate(notePrefabToUse, new Vector3(noteSpawnX, currentVisualSpawnY, transform.position.z), Quaternion.identity);
            noteGo.transform.SetParent(this.transform);

            NoteObject noteObj = noteGo.GetComponent<NoteObject>();
            if (noteObj != null)
            {
                noteObj.SetupMovement(noteSpeedPerSecond);
            }
            else
            {
                Debug.LogError("NoteObject component not found on the spawned note prefab.");
            }
        }

    }
} 
