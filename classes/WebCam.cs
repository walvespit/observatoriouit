using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ObservatorioUIT
{
    class WebCam
    {
        private MediaCapture mediaCapture;
        private StorageFile photoFile;

        private bool isPreviewing;

        public WebCam()
        {
            isPreviewing = false;


        }

        public async Task Cleanup()
        {
            if (mediaCapture != null)
            {
                if (isPreviewing)
                {
                    await mediaCapture.StopPreviewAsync();
                    isPreviewing = false;
                }

                mediaCapture.Dispose();
                mediaCapture = null;
            }
        }

        public async Task init()
        {
            try
            {
                if (mediaCapture != null)
                {
                    if (isPreviewing)
                    {
                        await mediaCapture.StopPreviewAsync();
                        isPreviewing = false;
                    }

                    mediaCapture.Dispose();
                    mediaCapture = null;
                }

                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();

                var captureElement = new CaptureElement();
                captureElement.Source = mediaCapture;

                mediaCapture.Failed += new MediaCaptureFailedEventHandler(mediaCapture_Failed);
                await mediaCapture.StartPreviewAsync();
                isPreviewing = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to initialize camera:" + ex.Message);
            }
        }

        public async Task takePicture(String fileName)
        {
            await init();
            try
            {
                photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                    fileName + ".jpg", CreationCollisionOption.GenerateUniqueName);
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
                await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);

                IRandomAccessStream photoStream = await photoFile.OpenReadAsync();
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(photoStream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error: " + ex.Message);
            }
            await Cleanup();
        }


        private async void mediaCapture_Failed(MediaCapture currentCaptureObject, MediaCaptureFailedEventArgs currentFailure)
        {

            Debug.WriteLine("\nCheck if camera is diconnected.");

        }


    }
}
