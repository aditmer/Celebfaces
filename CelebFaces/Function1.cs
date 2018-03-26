
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using System;
using Microsoft.ProjectOxford.Face.Contract;

namespace CelebFaces
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            string sErrorMessage = "";
            string sErrorCode = "";

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var faceServiceClient = new FaceServiceClient(Keys.FaceAPIKey);
            CreatePersonResult celeb1;

            // Create an empty PersonGroup
            string personGroupId = "celebs";
            try
            {
                
                await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Celebs");
            }
            catch (Exception ex)
            {
                log.Info($"Error creating PersonGroup: {ex.Message}");
                
            }

            
                
                
                    // Define celeb
                    celeb1 = await faceServiceClient.CreatePersonInPersonGroupAsync(
                        // Id of the PersonGroup that the person belonged to
                        personGroupId,
                        // Name of the person (passed into the fumction)
                        name
                    );
                    
                //}
                //catch (Exception ex)
                //{
                //    log.Info($"Error creating person: {ex.Message}");
                //    sErrorMessage = ex.Message;
                //}
            

            if (sErrorMessage == "")
            {
                return name != null
                ? (ActionResult) new OkObjectResult($"We succesfully created a record for {name}.  Please now upload photos of them so we can train the model with thier face.  The photos need to have this id as the file name:  {celeb1.PersonId}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }
            else return new BadRequestObjectResult(sErrorMessage);
        }
    }
}
