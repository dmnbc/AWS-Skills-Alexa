using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;          // erfordert NuGet Pakate   Alexa.Net Tim Heuer
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace RequestAnalyseAWSLambda
{
    public static class Analyse
    {
        public static bool HasSlots(IntentRequest input)
        {
            if (input.Intent.Slots.Count > 0)
            { return true; }
            return false;
        }

        public static string Content(IntentRequest input)
        {
        string content  = "Analyse des Requests: \n";
            //   content += "input.DialogState:" + input.DialogState + "\n";
            //   content += "input.Locale:" + input.Locale + "\n";
            //   content += "input.RequestId:" + input.RequestId + "\n";
            //   content += "input.Timestamp:" + input.Timestamp.ToString() + "\n";
            //   content += "input.Type:" + input.Type+ "\n";
           //    content += "input.GetType:" + input.GetType() + "\n";
           //    content += "input.ToString:" + input.ToString() + "\n";
               content += "---------- Intent ------------ \n";
           //    content += "Intent.ToString():" + input.Intent.ToString() + "\n";
          //     content += "Intent.ConfirmationStatus:" + input.Intent.ConfirmationStatus + "\n";
          //     content += "Intent.GetType():" + input.Intent.GetType() + "\n";
               content += "Intent.Name:" + input.Intent.Name + "\n";
         //      content += "Intent.Signature:" + input.Intent.Signature.ToString() + "\n";
               content += "Intent.Slots:" + input.Intent.Slots.ToString() + "\n";
               content += "---------- Slots ------------ \n";
            if (HasSlots(input))
            {
                IDictionary<string, Slot> slots = input.Intent.Slots;
                content += "Intent.Slots.Count:" + slots.Count + "\n";
                for (int i = 0; i < slots.Count; i++)
                {
                    try
                    {
                    content += "In slot[" + i + "] übergeben:\n";
                    content += "  Key: " + slots.ElementAt(i).Key + "\n";
                        if (slots.ElementAt(i).Value.Value != null)
                        { content += "  Value:>" + slots.ElementAt(i).Value.Value + "<\n"; }
                        else
                        { content += "  Value: leer\n"; }
                //    content += "  Name: " + slots.ElementAt(i).Value.Name + "\n";
                //    content += "  Resolution: " + slots.ElementAt(i).Value.Resolution.ToString() + "\n";
                
                    }
                    catch(Exception e)
                    {
                        content += "Fehler bei der Analyse : " + e.Message;
                    }
                }
            }
            else
            {
                content += "hat keine Slots\n";
            }




            return content; 
        }

    }
}
