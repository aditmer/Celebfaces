using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Face;

namespace CelebFaces
{
    public static class TrainFaces
    {
        [FunctionName("TrainFaces")]
        public static async System.Threading.Tasks.Task RunAsync([BlobTrigger("trainingimages//{name}", Connection = "celebfaces9b36_STORAGE")]Stream myBlob, string fileName, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            var faceServiceClient = new FaceServiceClient(Keys.FaceAPIKey);
            string personGroupId = "celebs";

            var celeb = await faceServiceClient.GetPersonInLargePersonGroupAsync(personGroupId, new System.Guid(fileName));
            

            
            // Detect faces in the image and add to the celebrity
            await faceServiceClient.AddPersonFaceAsync(
                personGroupId, celeb.PersonId, myBlob);
                
           
        }
    }
}
