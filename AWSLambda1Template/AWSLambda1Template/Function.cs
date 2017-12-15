using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;      // stellt SkillRequest   bereit
using Alexa.NET.Request.Type; // stellt Requesttypen wie LaunchRequest  bereit
using Alexa.NET.Response;     // stellt SkillResponse  bereit
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1Template
{
    public class Function
    {
        
        
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse antwort = new SkillResponse();
            antwort.Response = new ResponseBody
            {
                ShouldEndSession = false
            };
            IOutputSpeech interneAntwort = null;
            var log = context.Logger;
            var alleDaten = GetAntworten();
            var Daten = alleDaten.FirstOrDefault();

            if(input.GetRequestType() == typeof(LaunchRequest))    // erster Aufruf ohne Aufgabe
            {
                log.LogLine($"Skill Aufruf erfolgt");
                interneAntwort = new PlainTextOutputSpeech();
                (interneAntwort as PlainTextOutputSpeech).Text = Daten.AufrufMessage;
            }
            else if(input.GetRequestType() == typeof(IntentRequest)) // Aufruf mit Aufgabe
            {
                var aufgabenstellung = (IntentRequest)input.Request;
                switch (aufgabenstellung.Intent.Name)    // auswahl der gestellten Aufgabe als string
                {
                    case "AMAZON.CancelIntent":
                        log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                        interneAntwort = new PlainTextOutputSpeech();
                        (interneAntwort as PlainTextOutputSpeech).Text = Daten.StopMessage;
                        antwort.Response.ShouldEndSession = true;
                        break;
                    case "AMAZON.StopIntent":
                        log.LogLine($"AMAZON.StopIntent: send StopMessage");
                        interneAntwort = new PlainTextOutputSpeech();
                        (interneAntwort as PlainTextOutputSpeech).Text = Daten.StopMessage;
                        antwort.Response.ShouldEndSession = true;
                        break;
                    case "AMAZON.HelpIntent":
                        log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                        interneAntwort = new PlainTextOutputSpeech();
                        (interneAntwort as PlainTextOutputSpeech).Text = Daten.HelpMessage;
                        break;

                    // Hier die eigenen Aufgabenstellungen 

                    case "Aufgabe1":
                        log.LogLine($"eigene Aufgabe1");
                        interneAntwort = new PlainTextOutputSpeech();
                        (interneAntwort as PlainTextOutputSpeech).Text = Daten.Antwort1Message;
                        break;
                    // .....   Aufgaben

                    default:
                        log.LogLine($"Unknown intent: " + aufgabenstellung.Intent.Name);
                        interneAntwort = new PlainTextOutputSpeech();
                        (interneAntwort as PlainTextOutputSpeech).Text = Daten.HelpReprompt;
                        break;
                }
            }

            antwort.Response.OutputSpeech = interneAntwort;
            return antwort;
        }

        public List<Data> GetAntworten()
        {
            List<Data> antworten = new List<Data>();
            Data antwort = new Data
            {
                HelpMessage = "Dies ist die Hilfenachricht",
                StopMessage = "Dies ist die Stopnachricht",
                HelpReprompt = "Dies ist die HelpRepromptnachricht",
                Antwort1Message = "Dies ist die Antwort auf die erste Aufgabe",
                AufrufMessage = "Dies ist der Skillaufruf"
            };

            antworten.Add(antwort);

            return antworten;
        }
    }
}
