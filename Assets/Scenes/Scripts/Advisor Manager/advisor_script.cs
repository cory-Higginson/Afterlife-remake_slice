using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class advisor_script : MonoBehaviour
{

    public TextAsset adviceJson;

    public AdviceList json_advice_array = new AdviceList();

    public List<Advice> advice_list = new List<Advice>();

    public Animator devil_sprite;
    public Animator angel_sprite;

    static public Color devil_text_colour = new Color(255, 47, 106, 255);
    static public Color angel_text_colour = new Color(255, 228, 155, 255);

    private string devil_text_hex = "FF0065";
    private string angel_text_hex = "FFE49B";

    private bool is_start_read_next_frame;
    

    public TextMeshProUGUI subtitle_text;
    
    public int read_speed = 10;
    
    private bool is_reading = false;

    private List<string> advice_sentences = new List<string>();
    private List<AudioClip> voice_lines = new List<AudioClip>();

    // Start is called before the first frame update

    void Start()
    {
        json_advice_array = JsonUtility.FromJson<AdviceList>(adviceJson.text);

        devil_sprite.StopPlayback();
        angel_sprite.StopPlayback();
        
        StartReading();

    }

    private void Update()
    {
        inputs();
        
        if (is_start_read_next_frame)
        {
            is_start_read_next_frame = false;
            StartReading();
        }

        if (is_reading)
        {
            Read();
        }
        return;
    }

    private void FixedUpdate()
    {
        inputs();
    }

    private void inputs()
    {
        if (Input.GetKeyDown("o"))
        {
            if (is_reading && read_letter_index_int > 0)
            {
                NextSentence();
            }
            
            
            if (advice_list.Count == 0)
            {
                TriggerSpecificAdvice("no path");
            }

        }
        
    }

    void StartReading()
    {
        BuildSentences();
        is_reading = true;
        UpdateSubtitles();
    }

    void StopReading()
    {
        is_reading = false;
        read_sentence_index = 0;
        read_letter_index_int = 0;
        read_letter_index_float = 0;
        advice_sentences.Clear();
        
        devil_sprite.Play("devil",0,0);
        angel_sprite.Play("angel", 0, 0);
        
        UpdateSubtitles();
    }

    void NextSentence()
    {
        if ((read_sentence_index + 1) < advice_sentences.Count)
        {
            read_sentence_index++;
            read_letter_index_int = 0;
            read_letter_index_float = 0;
            UpdateSubtitles();
        }
        else
        {
            StopReading();
        }
    }
    
    //Read variables

    private float read_letter_index_float = 0f;
    private int read_letter_index_int = 0;
    private int read_sentence_index = 0;

    enum Speaker
    {
        Devil,
        Angel
    };

    private Speaker current_speaker = Speaker.Devil;

    static private List<char> mouth_m = new List<char> { 'm', 'n', 'p', 'v', 'b', 'f' };
    static private List<char> mouth_o = new List<char> {'o', 'h', 'w' };
    static private List<char> mouth_a = new List<char> { 'a', 'e', 'i' };
    static private List<char> mouth_c = new List<char> { 'c', 'd', 'g', 'k', 's', 't', 'x', 'y', 'z', 'q', 'j',};
    static private List<char> mouth_u = new List<char> { 'r', 'u', 'l' };
    static private List<char> do_not_display_letters = new List<char> { '`', '¬' };

    private List<List<char>> mouth_shapes = new List<List<char>> { mouth_m, mouth_o, mouth_a, mouth_c, mouth_u };



    void Read()
    {
        if (advice_sentences.Count > 0)
        {
            if (read_letter_index_int < advice_sentences[read_sentence_index].Length)
            {
                AnimateSprites();
                read_letter_index_float += (read_speed * Time.deltaTime);
                read_letter_index_int = Mathf.FloorToInt(read_letter_index_float);
            }
        }
    }


    void AnimateSprites()
    {

        char cur_letter = advice_sentences[read_sentence_index][read_letter_index_int];

        if (cur_letter == '`')
        {
            current_speaker = Speaker.Angel;
        }
        else if (cur_letter == '¬')
        {
            current_speaker = Speaker.Devil;
        }


        float mouth_frame = 0f;
        for (int i = 0; i < mouth_shapes.Count; i++)
        {
            mouth_frame = (float)i / 4;
            if (mouth_shapes[i].Contains(cur_letter))
            {
                break;
            }

            mouth_frame = 0;
        }



        if (current_speaker == Speaker.Devil)
        {
            devil_sprite.Play("devil",0,mouth_frame);
            devil_sprite.StopPlayback();
            angel_sprite.Play("angel",0,0);
        }
        
        else if (current_speaker == Speaker.Angel)
        {
            angel_sprite.Play("angel", 0, mouth_frame);
            angel_sprite.StopPlayback();
            devil_sprite.Play("devil",0,0);
        }


    }
    

    void UpdateSubtitles()
    {
        
        
        
        string display_text = "";

        if (advice_sentences.Count > 0)
        {

            string current_sentence = advice_sentences[read_sentence_index];

            for (int i = 0; i < current_sentence.Length; i++)
            {
                char cur_letter = current_sentence[i];

                if (i == 0)
                {
                    if (current_speaker == Speaker.Angel)
                    {
                        display_text += ("<color=#" + angel_text_hex + ">");
                    }
                    else if (current_speaker == Speaker.Devil)
                    {
                        display_text += ("<color=#" + devil_text_hex + ">");
                    }
                }


                if (do_not_display_letters.Contains(cur_letter))
                {
                    if (cur_letter == '`')
                    {
                        display_text += ("</color><color=#" + angel_text_hex + ">");
                    }
                    else if (cur_letter == '¬')
                    {
                        display_text += ("</color><color=#" + devil_text_hex + ">");
                    }


                }
                else
                {
                    display_text += current_sentence[i];
                }
            }
        }

        subtitle_text.text = display_text;
        
    }

    void BuildSentences()
    {
        if (advice_list.Count > 0)
        {
            string advice = advice_list[0].advice_text;
            advice_sentences.Clear();

            string sentence = "";
            for (int i = 0; i < advice.Length; i++)
            {
                if (advice[i] == '/')
                {
                    advice_sentences.Add(sentence);
                    sentence = "";
                }


                else
                {
                    sentence += advice[i];
                }
            }

            advice_sentences.Add(sentence);

        }

    }
    
    

    void BuildVoiceFiles()
    {
        List<string> filenames = new List<string>();
        string long_filename_string = advice_list[0].voice_filenames;

        string filename = "";
        
        
        //Get individual filenames
        for (int i = 0; i < long_filename_string.Length; i++)
        {
            char cur_letter = long_filename_string[i];
            if (cur_letter == ',')
            {
                filenames.Add(filename);
                filename = "";
            }
            else
            {
                filename += cur_letter;
            }
        }
        
        filenames.Add(filename);
        
        //Load sounds
        for (int i = 0; i < filenames.Count; i++)
        {
            AudioClip voice_line;

        }

    }

    public void AddAdviceFromJson(string name)
    {
        for (int a = 0; a < json_advice_array.advice.Length; a++)
        {
            if (json_advice_array.advice[a].advice_name == name)
            {
                advice_list.Add(json_advice_array.advice[a]);
                SortAdvice();
                break;
            }
        }
    }
    
    public void AddAdviceFromJson(string name, int override_priority)
    {
        for (int a = 0; a < json_advice_array.advice.Length; a++)
        {
            Debug.Log(json_advice_array.advice[a].advice_name);
            Debug.Log(name.ToString());
            if (json_advice_array.advice[a].advice_name.ToString() == name.ToString())
            {
                Advice new_advice = json_advice_array.advice[a];
                new_advice.advice_priority = override_priority.ToString();
                advice_list.Add(new Advice());
                SortAdvice();
            }
        }
    }

    public void TriggerSpecificAdvice(string name)
    {
        for (int a = 0; a < json_advice_array.advice.Length; a++)
        {
            if (json_advice_array.advice[a].advice_name == name)
            {
                Advice new_advice = json_advice_array.advice[a];
                new_advice.advice_priority = "-1";
                advice_list.Insert(0, new_advice);
                break;
            }
        }

        is_start_read_next_frame = true;
    }

    public void PlayAdvice()
    {
        StartReading();
    }

    void SortAdvice()
    {
        advice_list = advice_list.OrderBy(x => int.Parse(x.advice_priority)).ToList();

    }









}




//Advice item classes

[Serializable]
public class Advice
{
    public string advice_name;
    public string advice_priority;
    public string advice_text;
    public string voice_filenames;
}

[Serializable]
public class AdviceList
{
    public Advice[] advice;
}

