using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Alexa.NET.Request;          // erfordert NuGet Pakate   Alexa.Net Tim Heuer
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

using Amazon.Lambda.Core;
using Amazon.RDS;
using Amazon.RDS.Model;
// using static Amazon.Internal.RegionEndpointProviderV2;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaRDSInstanceLister
{
    public class Function
    {
        
       
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
            outputSpeech = new PlainTextOutputSpeech();

            if (input.GetRequestType() == typeof(LaunchRequest))
            { // einfacher Aufruf des Skills ohne Aufgabenstellung ( Intent )

                log.LogLine("39: Launch ");

                (outputSpeech as PlainTextOutputSpeech).Text = "Ergebnis der Suche nach Datenbanken: ";
                AmazonRDSClient amazonRDSClient = new AmazonRDSClient(StaticValues.AWS_ACCESS_KEY, StaticValues.AWS_SECRET_KEY,Amazon.RegionEndpoint.EUCentral1);
                try
                {
                   
                    DescribeDBInstancesRequest request = new DescribeDBInstancesRequest();
                   // request.DBInstanceIdentifier = "opensandbox";
                    var response = amazonRDSClient.DescribeDBInstancesAsync(request);
                    foreach (var instance in response.Result.DBInstances)
                    {

                        log.LogLine(instance.DBName);
                        (outputSpeech as PlainTextOutputSpeech).Text += instance.DBName;
                        (outputSpeech as PlainTextOutputSpeech).Text += instance.Engine + " Version: " + instance.EngineVersion;

                    }
                }
                catch (Exception e)
                {
                    log.LogLine("58:" + e.Message);
                }

                
            }

            skillResponse.Response.OutputSpeech = outputSpeech;
            skillResponse.Version = "1.0";
            return skillResponse;
        }
    }
}
