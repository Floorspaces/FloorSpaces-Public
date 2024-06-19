using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Media;
using System;
using System.Text;

public class DataModel : MonoBehaviour
{
    public List<byte[]> DataStream = new List<byte[]>();
    public List<char> TypeStream = new List<char>();
    public List<int[]> ImageSizes = new List<int[]>();

    void AddBytes(byte[] bytes, char type)
    {
        DataStream.Add(bytes);
        TypeStream.Add(type);
    }

    void AddBytes(byte[] bytes, char type, int Width, int Height)
    {
        int[] Size = new int[] {Width, Height};
        ImageSizes.Add(Size);
        AddBytes(bytes, type);
    }

    UniversalData ConvertToData(byte[] data, char type)
    {
        var ret = new UniversalData();
        if (type == 'v')
        {
            // Video Convert
            throw new System.Exception("Not Implemented yet");
        }
        else if (type == 'i')
        {
            // Image Convert
            var size = ImageSizes[ImageSizes.Count - 1];
            ImageSizes.RemoveAt(ImageSizes.Count - 1);
            var Image = new Texture2D(size[0], size[1]);
            ImageConversion.LoadImage(Image, data);
            ret.Image = Image;
        }
        else if (type == 't')
        {
            // Text Convert
            var Message = Encoding.UTF8.GetString(data);
            ret.message = Message;
        }
        else if (type == 'a')
        {
            // Audio Convert
            float[] AudioStream = new float[data.Length / 4];
            for(int i = 0; i != data.Length; i += 4)
            {
                AudioStream[i / 4] = BitConverter.ToSingle(data, i);
            }
            AudioClip clip = AudioClip.Create("Clip", AudioStream.Length, 1, 44100, true);
            ret.Audio = clip;
        }
        else
        {
            throw new System.Exception("Error invalid data type provided");
        }
        return ret;
    }

    struct UniversalData
    {
        #nullable enable
        public Texture2D? Image;
        public string? message;
        public AudioClip? Audio;
        #nullable disable
    }
}
