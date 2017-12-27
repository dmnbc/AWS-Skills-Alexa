using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*   folgende NuGets sind zu laden
    
     Newtonsoft.Json" Version="10.0.1"   keine h�heren Versionen !!!

     Alexa.NET" Version="1.4.0" 
 * 
 * 
 */

using Alexa.NET.Request;          // erfordert NuGet Pakate   Alexa.Net Tim Heuer
using Alexa.NET.Request.Type;     // getestet mit Version 1.4.0
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

          // gleich dem Projektnamen
namespace OnLaunchAWSLambda
{
    public class Function
    {
        public static string skillName = "OnLaunchAWSLambda"
            public static bool logging = true;

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
                     // Objekte vor Aufruf erzeugen
        SkillResponse  skillResponse = new SkillResponse();
                       skillResponse.Response = new ResponseBody{ShouldEndSession = false};
        IOutputSpeech  outputSpeech = null;
        IntentRequest  intentRequest = input.Request as IntentRequest;
        ILambdaLogger  lambdaLogger = context.Logger;

            if (logging)
            {
                lambdaLogger.LogLine("Log started");
                lambdaLogger.LogLine("FunctionName:" + context.FunctionName);
            }
           




            // (Sprach)ausgabe erstellen
            skillResponse.Response.OutputSpeech = outputSpeech;

            skillResponse.Version = "1.0";
            return skillResponse;

        }
    }
}