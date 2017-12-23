using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*   folgende NuGets sind zu laden
     Alexa.NET" Version="1.4.0" 
     Newtonsoft.Json" Version="10.0.1"   keine höheren Versionen !!!
 * 
 * 
 */

using Alexa.NET.Request;          // erfordert NuGet Pakate   Alexa.Net Tim Heuer
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]


namespace RequestAnalyseAWSLambda
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

        public static string skillName = "RequestAnalyse";

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse skillResponse = new SkillResponse();
            skillResponse.Response = new ResponseBody
            {
                ShouldEndSession = false
            };

            IOutputSpeech outputSpeech = null;

            var log = context.Logger;

            var intentRequest = input.Request as IntentRequest;

            if (input.GetRequestType() == typeof(LaunchRequest))
            { // einfacher Aufruf des Skills ohne Aufgabenstellung ( Intent )

                log.LogLine("(Line:55) Der Skill " + skillName + " wurde gelaunched.");

                outputSpeech = new PlainTextOutputSpeech();
                (outputSpeech as PlainTextOutputSpeech).Text = "Ich bin bereit ";
            }
            else if (input.GetRequestType() == typeof(IntentRequest)) // hier werden alle nicht 'LaunchRequests' behandelt
            {
                var typeOfTntent = (IntentRequest)input.Request;
                log.LogLine("(Line:63)Ein " + typeOfTntent.ToString() + " wurde erkannt");
                switch (typeOfTntent.Intent.Name)
                {
                    case "CustomIntent":
                   
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text =
                                        "CustomIntent erkannt";
                        SimpleCard simpleCard = new SimpleCard();
                        simpleCard.Title = "Analyse";
                        simpleCard.Content = typeOfTntent.Intent.Name;
                        simpleCard.Content += typeOfTntent.Intent.ToString();
                        skillResponse.Response.Card = simpleCard;
                            
                            
                            
                            // Alexa.NET.ResponseBuilder.TellWithCard("CustomIntent", skillName, "Analyse");
                        
                        break;
                    case "AMAZON.CancelIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Jawohl, ich höre auf";
                        break;
                    case "AMAZON.StopIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Bis zum nächsten mal ";
                        break;
                    case "AMAZON.HelpIntent":
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = "Hilf dir selbst ";
                        break;
                    default:
                        outputSpeech = new PlainTextOutputSpeech();
                        (outputSpeech as PlainTextOutputSpeech).Text = " Das werde ich ignorieren "
                                                                        + typeOfTntent.ToString();
                        break;




                }

            }
            skillResponse.Response.OutputSpeech = outputSpeech;
            skillResponse.Version = "1.0";
            return skillResponse;



        }
    }
}
