using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Aktivierung_AWSLambda
{
    public class Function
    {

        /// <summary>
        /// Die Verarbeitungslogik mit den verschiedenen Reaktionen
        /// auf die möglichen Request-Types
        /// </summary>
        /// <param name="input"></param> mit input.GetRequestType() 
        /// <param name="context"></param>
        /// <returns></returns>
        /// 

        public static string skillName = "Aktivierung ";

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse skillResponse = new SkillResponse();
            skillResponse.Response = new ResponseBody
            {
                ShouldEndSession = false
            };

            IOutputSpeech outputSpeech = null;

            var log = context.Logger;

            if (input.GetRequestType() == typeof(LaunchRequest))
            { // einfacher Aufruf des Skills ohne Aufgabenstellung ( Intent )

              //  log.LogLine("(Line:45) Der Skill " + skillName + " wurde gelaunched.");

                outputSpeech = new PlainTextOutputSpeech();
                (outputSpeech as PlainTextOutputSpeech).Text = "Ich bin bereit ";
            }
            else if(input.GetRequestType() == typeof(IntentRequest)) // hier werden alle nicht 'LaunchRequests' behandelt
            {
                var typeOfTntent = (IntentRequest)input.Request;
                log.LogLine("(Line:53)Ein "+typeOfTntent.ToString()+" wurde erkannt");
                switch(typeOfTntent.Intent.Name)
                {
                    // case "CustomIntent":
                    // 
                    //
                    // break;
                    case "AMAZON.CancelIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Jawohl, ich höre auf";
                        break;
                    case   "AMAZON.StopIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Bis zum nächsten mal ";
                        break;
                    case   "AMAZON.HelpIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Hilf dir selbst ";
                        break;
                    default:
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = typeOfTntent.ToString() + "als "+ typeOfTntent.Intent.Name+" erkannt."+ " Das werde ich ignorieren "; break;




                }

            }
            skillResponse.Response.OutputSpeech = outputSpeech;
            skillResponse.Version = "1.0";
            return skillResponse;
        }
    }
}
