using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;
using NAudio;
using System.Collections;
using DSP;

namespace TestFFT
{
    class Program
    {
        static void Main(string[] args)
        {
            /*WaveFileReader wavReader = new WaveFileReader(@"D:\Priv\ProjektInz\InzApp\TestFFT\SoundSamples\vocal2.wav");
            ArrayList wavArray = new ArrayList();
            for (int i = 0; i < wavReader.Length; i++){
                     
               int newArrayElement =   wavReader.
                wavArray.Add(newArrayElement);
            }
             */
            byte[] byteArray = File.ReadAllBytes(@"D:\Priv\ProjektInz\InzApp\TestFFT\SoundSamples\vocal2.wav");
            StreamWriter writer = new StreamWriter(@"wav.txt");
            foreach (int x in byteArray)
            {
                writer.Write(x + " ");
            }

            double[] sampleBuffer = new Double[
           
            Console.ReadKey();

        }
    }
}
