using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Face;

namespace CelebFaces
{
    public static class AddFaces
    {
        [FunctionName("AddFaces")]
        public static async System.Threading.Tasks.Task Run([BlobTrigger("trainingimages/{fileName}", Connection = "AzureWebJobsStorage")]Stream myBlob, string fileName, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            var faceServiceClient = new FaceServiceClient(Keys.FaceAPIKey);
            string personGroupId = "celebs";
            string nameNoExt = System.IO.Path.GetFileNameWithoutExtension(fileName);

            var celeb = await faceServiceClient.GetPersonInPersonGroupAsync(personGroupId, new System.Guid(nameNoExt));
            

            
            // Detect faces in the image and add to the celebrity
            await faceServiceClient.AddPersonFaceInPersonGroupAsync(
                personGroupId, celeb.PersonId, myBlob);

            
        }
    }
}
