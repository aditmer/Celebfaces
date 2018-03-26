
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.ProjectOxford.Face;
using System.Threading.Tasks;

namespace CelebFaces
{
    public static class TrainFaces
    {
        [FunctionName("TrainFaces")]
        public static async  Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function to train the face model has been triggered.");

            var faceServiceClient = new FaceServiceClient(Keys.FaceAPIKey);
            string personGroupId = "celebs";

            string sReturn = "";

            //var trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);

            ////if trianing is not already in progress, then train the model with the new images
            //if (trainingStatus.Status != Microsoft.ProjectOxford.Face.Contract.Status.Running)
            //{
                await faceServiceClient.TrainPersonGroupAsync(personGroupId);
                sReturn = "We have started training the model.";
            //}
            //else
            //{
            //    sReturn = "The model training is already underway.";
            //}


            return (ActionResult) new OkObjectResult(sReturn);
                
               
        }
    }
}
