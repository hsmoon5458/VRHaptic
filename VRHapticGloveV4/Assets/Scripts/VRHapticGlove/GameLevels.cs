using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this scrpit should be attached to LevelMenu prefab.
public class GameLevels : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip song1, song2, song3, tutorial; //put songs

    public GameObject[] spawning; // position of the cube
    public GameObject cube1, cube2, cube3, cube4, cube5; // 5 prefabs
    public Quaternion cube_rotation = new Quaternion(0f, 0f, 0f, 1f);
    private Vector3 p1, p2, p3, p4, p5; // position of spawing for convinence
    private float beat_89 = 0.674157f;
    private float beat_90 = 0.666667f; // time between each beat for 90bpm
    private float song3_beat = 0.6665f;
    private float start_time_offset_song1 = 0.15f,
                    start_time_offset_song2 = -0.12f,
                    start_time_offset_song3 = -0.19f,
                    start_time_offset_tutorial = 0.03f;
    public static bool song_play_status = false;

    void Start()
    {
        // set position as variables to easily hardcode the levels
        p1 = spawning[0].transform.position;
        p2 = spawning[1].transform.position;
        p3 = spawning[2].transform.position;
        p4 = spawning[3].transform.position;
        p5 = spawning[4].transform.position;

        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {

        // song play stauts is for avoiding the play to be played multiple times
        if (LaunchPadFingerTrigger.song1_lv1 && song_play_status == false || Input.GetKeyDown("1"))
        {
            // reinitilaize the value for other song to be saved
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S1L1";
            song_play_status = true;
            Song1LV1();
            StartCoroutine(WaitASecSong(song1, 5.45f));
            LaunchPadFingerTrigger.song1_lv1 = false;
            StartCoroutine(WaitStatus()); // wait 3 seconds to avoid button pushed multiple times
        }

        if (LaunchPadFingerTrigger.song1_lv2 && song_play_status == false || Input.GetKeyDown("2"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S1L2";
            song_play_status = true;
            Song1LV2();
            StartCoroutine(WaitASecSong(song1, 5.45f));
            LaunchPadFingerTrigger.song1_lv2 = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.song2_lv1 && song_play_status == false || Input.GetKeyDown("3"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S2L1";
            song_play_status = true;
            Song2LV1();
            StartCoroutine(WaitASecSong(song2, 3.45f));
            LaunchPadFingerTrigger.song2_lv1 = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.song2_lv2 && song_play_status == false || Input.GetKeyDown("4"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S2L2";
            song_play_status = true;
            Song2LV2();
            StartCoroutine(WaitASecSong(song2, 3.45f));
            LaunchPadFingerTrigger.song2_lv2 = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.song3_lv1 && song_play_status == false || Input.GetKeyDown("5"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S3L1";
            song_play_status = true;
            Song3LV1();
            PlayAudio(song3);
            LaunchPadFingerTrigger.song3_lv1 = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.song3_lv2 && song_play_status == false || Input.GetKeyDown("6"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            CubeHit.song_title = "S3L2";
            song_play_status = true;
            Song3LV2();
            PlayAudio(song3);
            LaunchPadFingerTrigger.song3_lv2 = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.tutorial && song_play_status == false || Input.GetKeyDown("7"))
        {
            CubeHit.hit_count = 0;
            CubeHit.combo_count = 0;
            CubeHit.score_calculated = 0;
            CubeHit.deviation = new float[200];
            CubeHit.deviation_sum = 0;
            CubeHit.deviation_index = 0;

            song_play_status = true;
            Tutorial();
            StartCoroutine(WaitASecSong(tutorial, 4.45f));
            LaunchPadFingerTrigger.tutorial = false;
            StartCoroutine(WaitStatus());
        }

        if (LaunchPadFingerTrigger.stop_button)
        {
            StopCoroutine("WaitASec"); // it is not working now.
            StopAudio();
            LaunchPadFingerTrigger.stop_button = false;
        }


    }

    private void Tutorial() // done but need to calibrate the timing
    {
        CubeFalling.cube_falling_speed = 2.5f;
        StartCoroutine(WaitASec(beat_90 * 1 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 2 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 3 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 4 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 5 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 6 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 7 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 8 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 9 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 10 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 11 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 12 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 13 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 14 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 15 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 16 + start_time_offset_tutorial, cube1, p1));

        StartCoroutine(WaitASec(beat_90 * 17 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 18 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 19 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 20 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 21 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 22 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 23 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 24 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 25 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 26 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 27 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 28 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 29 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 30 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 31 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 32 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 32 + start_time_offset_tutorial, cube5, p5));//

        StartCoroutine(WaitASec(beat_90 * 33 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 34 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 35 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 36 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 37 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 38 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 39 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 40 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 41 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 42 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 43 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 44 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 45 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 46 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 47 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 48 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 48 + start_time_offset_tutorial, cube5, p5));//

        StartCoroutine(WaitASec(beat_90 * 49 + start_time_offset_tutorial, cube2, p2));//
        StartCoroutine(WaitASec(beat_90 * 49 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 50 + start_time_offset_tutorial, cube2, p2));//
        StartCoroutine(WaitASec(beat_90 * 50 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 51 + start_time_offset_tutorial, cube2, p2));//
        StartCoroutine(WaitASec(beat_90 * 51 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 52 + start_time_offset_tutorial, cube2, p2));//
        StartCoroutine(WaitASec(beat_90 * 52 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 52.5f + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 53 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 53 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 54 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 54 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 55 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 55 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 56 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 56 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 56.5f + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 57 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 58 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 59 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 60 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 60.5f + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 61 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 61 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 62 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 62 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 63 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 63 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 64 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 64 + start_time_offset_tutorial, cube5, p5));//
        StartCoroutine(WaitASec(beat_90 * 64.5f + start_time_offset_tutorial, cube4, p4));

        StartCoroutine(WaitASec(beat_90 * 65 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 65 + start_time_offset_tutorial, cube3, p3));//
        StartCoroutine(WaitASec(beat_90 * 66 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 66 + start_time_offset_tutorial, cube3, p3));//
        StartCoroutine(WaitASec(beat_90 * 67 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 67 + start_time_offset_tutorial, cube3, p3));//
        StartCoroutine(WaitASec(beat_90 * 68 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 68 + start_time_offset_tutorial, cube3, p3));//
        StartCoroutine(WaitASec(beat_90 * 68.5f + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 69 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 69 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 70 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 70 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 71 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 71 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 72 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 72 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 72.5f + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 73 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 74 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 75 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 76 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 76.5f + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 77 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 77 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 78 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 78 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 79 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 79 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 80 + start_time_offset_tutorial, cube1, p1));//
        StartCoroutine(WaitASec(beat_90 * 80 + start_time_offset_tutorial, cube4, p4));//
        StartCoroutine(WaitASec(beat_90 * 80.5f + start_time_offset_tutorial, cube3, p3));

        StartCoroutine(WaitASec(beat_90 * 83 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 84 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 85 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 86 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 87 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 88 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 89 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 90 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 91 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 92 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 93 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 94 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 95 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 96 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 97 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 98 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 99 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 100 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 101 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 102 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 103 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 104 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 105 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 106 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 107 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 108 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 109 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 110 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 111 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 112 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 113 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 114 + start_time_offset_tutorial, cube2, p2));

        StartCoroutine(WaitASec(beat_90 * 115 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 116 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 117 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 118 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 119 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 120 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 121 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 122 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 123 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 124 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 125 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 126 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 127 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 128 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 129 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 130 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 131 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 132 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 133 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 134 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 135 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 136 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 137 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 138 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 139 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 140 + start_time_offset_tutorial, cube5, p5));
        StartCoroutine(WaitASec(beat_90 * 141 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 142 + start_time_offset_tutorial, cube3, p3));
        StartCoroutine(WaitASec(beat_90 * 143 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 144 + start_time_offset_tutorial, cube4, p4));
        StartCoroutine(WaitASec(beat_90 * 145 + start_time_offset_tutorial, cube1, p1));
        StartCoroutine(WaitASec(beat_90 * 146 + start_time_offset_tutorial, cube2, p2));
        StartCoroutine(WaitASec(beat_90 * 147 + start_time_offset_tutorial, cube2, p2));
    }


    private void Song1LV1() // done but need to calibrate the timing
    {
        CubeFalling.cube_falling_speed = 1.5f;
        StartCoroutine(WaitASec((beat_89 * 0f) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * 1f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 2f) + start_time_offset_song1, cube5, p5));
        //StartCoroutine(WaitASec((beat_89 * 2.75f) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * 3.5f) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * 5.5f) + start_time_offset_song1, cube4, p4));
        //StartCoroutine(WaitASec((beat_89 * 6.75f) + start_time_offset_song1, cube2, p2));
        //StartCoroutine(WaitASec((beat_89 * 7.5f) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * 8.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 10.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 12.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 14.5f) + start_time_offset_song1, cube2, p2));

        StartCoroutine(WaitASec((beat_89 * (15.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (15.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (15.7f + 2f)) + start_time_offset_song1, cube5, p5));
        //StartCoroutine(WaitASec((beat_89 * (16 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (16 + 3.5f)) + start_time_offset_song1, cube3, p3));
        //StartCoroutine(WaitASec((beat_89 * (16 + 5.0f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 5.5f)) + start_time_offset_song1, cube4, p4));
        //StartCoroutine(WaitASec((beat_89 * (16 + 6.75f)) + start_time_offset_song1, cube2, p2));
        //StartCoroutine(WaitASec((beat_89 * (16 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (16 + 8.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 13.5f)) + start_time_offset_song1, cube4, p4));

        StartCoroutine(WaitASec((beat_89 * (16 + 15.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (32 + 1.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (32 + 3.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (32 + 5.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (32 + 7.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (32 + 9.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (32 + 11.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (32 + 13.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (48 -0.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (48 + 1.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (48 + 3.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (48 + 5.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (48 + 7.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (48 + 9.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (48 + 11.5f)) + start_time_offset_song1, cube4, p4));




        StartCoroutine(WaitASec((beat_89 * (63.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (63.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (63.7f + 2f)) + start_time_offset_song1, cube5, p5));
        //StartCoroutine(WaitASec((beat_89 * (64 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (64 + 3.5f)) + start_time_offset_song1, cube3, p3));
        //StartCoroutine(WaitASec((beat_89 * (64 + 5.0f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 5.5f)) + start_time_offset_song1, cube4, p4));
        //StartCoroutine(WaitASec((beat_89 * (64 + 6.75f)) + start_time_offset_song1, cube2, p2));
        //StartCoroutine(WaitASec((beat_89 * (64 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (64 + 8.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 12f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (64 + 14f)) + start_time_offset_song1, cube4, p4));
        
        StartCoroutine(WaitASec((beat_89 * (79.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (79.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (79.7f + 2f)) + start_time_offset_song1, cube5, p5));
        //StartCoroutine(WaitASec((beat_89 * (80 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (80 + 3.5f)) + start_time_offset_song1, cube3, p3));
        //StartCoroutine(WaitASec((beat_89 * (80 + 5.0f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 5.5f)) + start_time_offset_song1, cube4, p4));
        //StartCoroutine(WaitASec((beat_89 * (80 + 6.75f)) + start_time_offset_song1, cube2, p2));
        //StartCoroutine(WaitASec((beat_89 * (80 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (80 + 8.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 12f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (80 + 14f)) + start_time_offset_song1, cube4, p4));

        StartCoroutine(WaitASec((beat_89 * (95.8f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (95.8f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (95.8f + 2f)) + start_time_offset_song1, cube5, p5));
        //StartCoroutine(WaitASec((beat_89 * (96 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (96 + 3.5f)) + start_time_offset_song1, cube3, p3));

        StartCoroutine(WaitASec((beat_89 * (96 + 5.75f)) + start_time_offset_song1, cube3, p3));

        StartCoroutine(WaitASec((beat_89 * (96 + 7.75f)) + start_time_offset_song1, cube2, p2));
    }
    private void Song1LV2()
    {
        CubeFalling.cube_falling_speed = 1.5f;
        StartCoroutine(WaitASec((beat_89 * 0f) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * 1f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 2f) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * 2.75f) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * 3.5f) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * 5.5f) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * 6.75f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 7.5f) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * 10.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 10.5f) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * 12.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 12.5f) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * 14.5f) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * 14.5f) + start_time_offset_song1, cube5, p5));

        StartCoroutine(WaitASec((beat_89 * (15.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (15.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (15.7f + 2f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (16 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (16 + 3.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (16 + 5.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (16 + 6.75f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (16 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 10.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (16 + 12.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (16 + 12.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (16 + 14.5f)) + start_time_offset_song1, cube3, p3));

        StartCoroutine(WaitASec((beat_89 * (16 + 15.5f)) + start_time_offset_song1, cube2, p2));

        StartCoroutine(WaitASec((beat_89 * (32 + 1.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (32 + 3.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (32 + 5.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (32 + 5.5f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (32 + 7.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (32 + 9.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (32 + 11.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (32 + 13.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (32 + 13.5f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (48 - 0.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (48 + 1.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (48 + 3.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (48 + 5.5f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (48 + 5.5f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (48 + 7.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (48 + 9.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (48 + 11.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (48 + 11.5f)) + start_time_offset_song1, cube2, p2));




        StartCoroutine(WaitASec((beat_89 * (63.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (63.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (63.7f + 2f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (64 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (64 + 3.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (64 + 5.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (64 + 6.75f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (64 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 10.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (64 + 12.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 12.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (64 + 14.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (64 + 14.5f)) + start_time_offset_song1, cube5, p5));

        StartCoroutine(WaitASec((beat_89 * (79.7f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (79.7f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (79.7f + 2f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (80 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (80 + 3.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (80 + 5.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (80 + 6.75f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 7.5f)) + start_time_offset_song1, cube3, p3));
        StartCoroutine(WaitASec((beat_89 * (80 + 10.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 10.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (80 + 12.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 12.5f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (80 + 14.5f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (80 + 14.5f)) + start_time_offset_song1, cube5, p5));

        StartCoroutine(WaitASec((beat_89 * (95.8f + 0f)) + start_time_offset_song1, cube1, p1));
        StartCoroutine(WaitASec((beat_89 * (95.8f + 1f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (95.8f + 2f)) + start_time_offset_song1, cube5, p5));
        StartCoroutine(WaitASec((beat_89 * (96 + 2.75f)) + start_time_offset_song1, cube4, p4));
        StartCoroutine(WaitASec((beat_89 * (96 + 3.5f)) + start_time_offset_song1, cube3, p3));

        StartCoroutine(WaitASec((beat_89 * (96 + 5.75f)) + start_time_offset_song1, cube3, p3));

        StartCoroutine(WaitASec((beat_89 * (96 + 7.75f)) + start_time_offset_song1, cube2, p2));
        StartCoroutine(WaitASec((beat_89 * (96 + 7.75f)) + start_time_offset_song1, cube4, p4));
    }

    private void Song2LV1()
    {
        CubeFalling.cube_falling_speed = 2.5f;
        StartCoroutine(WaitASec(song3_beat * 3 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 4 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 5 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 6 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 7 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 8 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 9 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 10 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 11 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 12 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 13 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 14 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 15 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 16 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 17 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 18 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 19 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 20 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 21 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 22 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 23 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 24 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 25 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 26 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 27 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 28 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 29 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 30 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 31 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 32 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 33 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 34 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 35 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 36 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 37 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 38 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 39 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 40 + start_time_offset_song2, cube2, p2));

        StartCoroutine(WaitASec(song3_beat * 41 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 42 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 43 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 44 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 45 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 46 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 47 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 48 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 50 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 51 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 52 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 53 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 54 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 55 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 56 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 57 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 58 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 59 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 60 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 62 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 63 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 64 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 65 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 66 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 67 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 68 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 69 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 70 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 71 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 72 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 73 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 74 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 75 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 76 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 77 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 78 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 79 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 80 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 81 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 82 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 83 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 84 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 85 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 86 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 87 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 88 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 89 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 90 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 91 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 92 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 93 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 94 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 95 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 96 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 97 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 98 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 99 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 100 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 101 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 102 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 103 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 104 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 105 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 106 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 107 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 108 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 109 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 110 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 111 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 112 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 113 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 114 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 115 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 116 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 117 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 118 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 120 + start_time_offset_song2, cube3, p3));
    }

    private void Song2LV2()
    {
        CubeFalling.cube_falling_speed = 2.5f;
        StartCoroutine(WaitASec(song3_beat * 3 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 4 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 5 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 6 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 7 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 8 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 9 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 9.5f + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 10 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 11 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 11.5f + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 12 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 13 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 14 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 15 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 16 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 17 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 17.5f + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 18 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 19 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 19.5f + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 20 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 21 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 22 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 23 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 24 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 25 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 25.5f + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 26 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 27 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 27.5f + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 28 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 29 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 29.5f + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 30 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 31 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 31.5f + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 32 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 33 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 34 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 35 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 36 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 37 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 38 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 39 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 40 + start_time_offset_song2, cube2, p2));


        StartCoroutine(WaitASec(song3_beat * 41 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 42 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 43 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 44 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 45 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 46 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 47 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 48 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 50 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 50 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 51 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 51.5f + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 52 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 53 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 53 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 54 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 54 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 55 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 56 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 57 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 58 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 59 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 60 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 62 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 63 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 63 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 64 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 65 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 66 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 66 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 67 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 68 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 68 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 69 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 70 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 71 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 71.5f + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 72 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 73 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 73 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 74 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 74 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 75 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 76 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 76 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 77 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 78 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 79 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 80 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 81 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 81 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 82 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 82 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 83 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 84 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 84 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 85 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 86 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 87 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 88 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 89 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 89 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 90 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 90 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 91 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 92 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 92 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 93 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 94 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 95 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 96 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 97 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 97 + start_time_offset_song2, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 98 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 99 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 100 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 101 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 102 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 102.5f + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 103 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 104 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 104.5f + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 105 + start_time_offset_song2, cube4, p4));

        StartCoroutine(WaitASec(song3_beat * 106 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 107 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 108 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 109 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 110 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 110.5f + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 111 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 112 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 113.5f + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 113 + start_time_offset_song2, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 114 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 115 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 116 + start_time_offset_song2, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 117 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 118 + start_time_offset_song2, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 118 + start_time_offset_song2, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song2, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song2, cube2, p2));

        StartCoroutine(WaitASec(song3_beat * 120 + start_time_offset_song2, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 120 + start_time_offset_song2, cube4, p4));
    }

    private void Song3LV1()
    {
        CubeFalling.cube_falling_speed = 2.5f;
        StartCoroutine(WaitASec(song3_beat * 3 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 5 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 7 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 9 + start_time_offset_song3, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 11 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 13 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 15 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 17 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 19 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 21 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 23 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 25 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 27 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 27 + start_time_offset_song3, cube5, p5));//
        StartCoroutine(WaitASec(song3_beat * 29 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 31 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 32 + start_time_offset_song3, cube3, p3));

        //StartCoroutine(WaitASecSave((song3_beat * 32 + start_time_offset_song3)));

        StartCoroutine(WaitASec(song3_beat * 35 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 37 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 39 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 41 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 43 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 45 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 47 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 51 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 53 + start_time_offset_song3, cube5, p5));//
        StartCoroutine(WaitASec(song3_beat * 55 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 57 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 59 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 63 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 65 + start_time_offset_song3, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 67 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 69 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 71 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 72 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 73 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 75 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 77 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 79 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 80 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 81 + start_time_offset_song3, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 83 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 85 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 87 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 89 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 91 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 93 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 95 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 97 + start_time_offset_song3, cube3, p3));

        StartCoroutine(WaitASec(song3_beat * 99 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 101 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 103 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 105 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 107 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 109 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 111 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 113 + start_time_offset_song3, cube3, p3));

        StartCoroutine(WaitASec(song3_beat * 115 + start_time_offset_song3, cube2, p2));//
        StartCoroutine(WaitASec(song3_beat * 115 + start_time_offset_song3, cube4, p4));//
        StartCoroutine(WaitASec(song3_beat * 117 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 117 + start_time_offset_song3, cube5, p5));//
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song3, cube2, p2));//
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song3, cube4, p4));//
        StartCoroutine(WaitASec(song3_beat * 121 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 121 + start_time_offset_song3, cube5, p5));//



    }

    private void Song3LV2()
    {
        CubeFalling.cube_falling_speed = 2.5f;
        StartCoroutine(WaitASec(song3_beat * 3 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 5 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 7 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 9 + start_time_offset_song3, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 10 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 11 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 12 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 13 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 14 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 15 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 16 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 17 + start_time_offset_song3, cube2, p2));//
        StartCoroutine(WaitASec(song3_beat * 17 + start_time_offset_song3, cube4, p4));//

        StartCoroutine(WaitASec(song3_beat * 18 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 19 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 20 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 21 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 22 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 23 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 24 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 25 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 25 + start_time_offset_song3, cube5, p5));//

        StartCoroutine(WaitASec(song3_beat * 26 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 27 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 28 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 29 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 30 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 30.5f + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 31 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 31.5f + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 32 + start_time_offset_song3, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 34 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 35 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 36 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 37 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 38 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 39 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 40 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 41 + start_time_offset_song3, cube2, p2));//
        StartCoroutine(WaitASec(song3_beat * 41 + start_time_offset_song3, cube4, p4));//

        StartCoroutine(WaitASec(song3_beat * 42 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 43 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 44 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 45 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 46 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 47 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 48 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 49 + start_time_offset_song3, cube5, p5));//

        StartCoroutine(WaitASec(song3_beat * 50 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 51 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 52 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 53 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 54 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 54.5f + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 55 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 55.5f + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 56 + start_time_offset_song3, cube5, p5));

        StartCoroutine(WaitASec(song3_beat * 58 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 58 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 59 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 59 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 60 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 60 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 61 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 62 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 63 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 64 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 65 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 66 + start_time_offset_song3, cube1, p1));


        StartCoroutine(WaitASec(song3_beat * 67 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 68 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 69 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 70 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 71 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 72 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 73 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 74 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 75 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 76 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 77 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 78 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 78.5f + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 79 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 79.5f + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 80 + start_time_offset_song3, cube1, p1));

        StartCoroutine(WaitASec(song3_beat * 82 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 84 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 86 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 88 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 90 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 92 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 94 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 96 + start_time_offset_song3, cube3, p3));

        StartCoroutine(WaitASec(song3_beat * 98 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 99 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 100 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 101 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 102 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 103 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 104 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 105 + start_time_offset_song3, cube2, p2));//
        StartCoroutine(WaitASec(song3_beat * 105 + start_time_offset_song3, cube4, p4));//

        StartCoroutine(WaitASec(song3_beat * 106 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 107 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 108 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 109 + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 110 + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 111 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 112 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 113 + start_time_offset_song3, cube1, p1));//
        StartCoroutine(WaitASec(song3_beat * 113 + start_time_offset_song3, cube5, p5));//

        StartCoroutine(WaitASec(song3_beat * 114 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 115 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 116 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 117 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 118 + start_time_offset_song3, cube1, p1));
        StartCoroutine(WaitASec(song3_beat * 118.5f + start_time_offset_song3, cube2, p2));
        StartCoroutine(WaitASec(song3_beat * 119 + start_time_offset_song3, cube3, p3));
        StartCoroutine(WaitASec(song3_beat * 119.5f + start_time_offset_song3, cube4, p4));
        StartCoroutine(WaitASec(song3_beat * 120 + start_time_offset_song3, cube5, p5));
        StartCoroutine(WaitASec(song3_beat * 120.5f + start_time_offset_song3, cube3, p3));

    }

    IEnumerator WaitASec(float waitTime, GameObject cube, Vector3 spawning_location)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(cube, spawning_location, cube_rotation);

    }
    /*
    IEnumerator WaitASecSave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CubeHit.saveDataToObject();
    }
    */
    IEnumerator WaitASecSong(AudioClip song, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayAudio(song);

    }

    IEnumerator WaitStatus()
    {
        yield return new WaitForSeconds(3f);
        song_play_status = false;
    }

    public void PlayAudio(AudioClip clipToPlay)
    {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}

