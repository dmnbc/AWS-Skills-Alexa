using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;          // erfordert NuGet Pakate   Alexa.Net Tim Heuer
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AddingSlotsAWSLambda
{
    public class Function
    {

        public static string skillName = "AddingSlots ";
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var intentRequest = input.Request as IntentRequest;
            SkillResponse skillResponse = new SkillResponse();

           // it is not possibe tocreate additional slots
           // the voice recognition needs a fixed interaction modell at start !!!!!!

            // maybe this will change in future releases. There is a way to do this
            // in home autmation skills


            skillResponse.Response = new ResponseBody
            {
                ShouldEndSession = false
            };
            skillResponse.Version = "1.0";
            return skillResponse;
        }
    }
}
