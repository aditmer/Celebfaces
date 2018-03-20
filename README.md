# Celebfaces

A sample app to show how to use the Face API from Microsoft Cognitive Services:  https://azure.microsoft.com/en-us/services/cognitive-services/face/

There are 2 apps in this repo:
1. An Azure Function to train the Face API LargePersonGroup.  The Function is triggered by uploading an image to Blob Storage.  The image name needs to match the person's name.
2. A Xamarin.Forms (iOS, Android, UWP) app that passes images to the Face API to identify people by trained faces.
