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
       public static string Content(IntentRequest input)
        {
        string content  = "Analyse des Requests: \n";
               content += "input.DialogState:" + input.DialogState + "\n";
               content += "input.Locale:" + input.Locale + "\n";
               content += "input.RequestId:" + input.RequestId + "\n";

            return content; 
        }

    }
}
