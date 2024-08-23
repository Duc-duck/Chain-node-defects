using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using Basler.Pylon;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace App_DongBo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap PictureBox1;
        private WriteableBitmap PictureBox5;
        private WriteableBitmap PictureBox6;

        private byte[] img_chain_buffer;
        public static byte[] img_chain_buffer1; 
        public bool a, allow_Stop, Pass, allow_SetOutClose;
        public static Camera camera = null;
        CancellationTokenSource source;
        CancellationToken token;
        PixelDataConverter converter = new PixelDataConverter();
        //IntPtr ptrBmp, ptrOutBmp;
        ImageInfo out_image = new ImageInfo();
        Stopwatch stopWatch = new Stopwatch();
        string Dir;
        public static int bitmapWidth = 640;
        public static int bitmapHeight = 512;
        bool user1_in_process;
        public MainWindow()
        {
            InitializeComponent();
            Edit_Button.Background = System.Windows.Media.Brushes.LimeGreen;
            Dir = AppDomain.CurrentDomain.BaseDirectory;
            CheckForIsRunningApplication();
            DataContext = this;            
        }
        private void CheckForIsRunningApplication()
        {
            //Get Current Process Name
            string strProcessName = Process.GetCurrentProcess().ProcessName;

            //Get All the running processes with same name
            Process[] strAllProcesses = Process.GetProcessesByName(strProcessName);

            //If process exists in task manager, kill current process with show message

            if (strAllProcesses.Length > 1)
            {
                MessageBox.Show(" Ứng dụng đã được khởi động !!!");
                Application.Current.Shutdown();
            }
        }
        #region Button Mode
        private void Run_Clicked(object sender, RoutedEventArgs e)
        {
            a = true;
            Edit_Button.Background = System.Windows.Media.Brushes.LightGray;
            Run_Button.Background = System.Windows.Media.Brushes.LimeGreen;
            Edit_Button.IsEnabled = true; Run_Button.IsEnabled = false;
            ClearPictureBox(User1.UserPictureBox1);
            ClearPictureBox(User1.UserPictureBox2);
            PictureBox5 = null;
            PictureBox5 = new WriteableBitmap((int)((double)bitmapWidth * (User1.x1_chain_day - User1.x_chain_day)),
                                                  (int)((double)bitmapHeight * (User1.y1_chain_day - User1.y_chain_day)),
                                                  96, 96, PixelFormats.Gray8, null);
            pictureBox5.ImageSource = PictureBox5;
            MainWindow_Run();
        }
        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            a = false;
            Edit_Button.Background = System.Windows.Media.Brushes.LimeGreen;
            Run_Button.Background = System.Windows.Media.Brushes.LightGray;
            Edit_Button.IsEnabled = false; Run_Button.IsEnabled = true;
            ClearPictureBox(PictureBox1);
            ClearPictureBox(PictureBox5);
            ClearPictureBox(PictureBox6);
            MainWindow_Edit();
        }
        #endregion

        #region Control menu
        private void btnToggle1_Click(object sender, RoutedEventArgs e)
        {
            if (btnToggle1.IsChecked == true)
            {
                // Destroy the old camera object
                if (camera != null)
                {
                    DestroyCamera();
                }
                try
                {
                    // Create a new camera object.
                    camera = new Camera();

                    //camera.CameraOpened += Configuration.AcquireSingleFrame;

                    // Register for the events of the image provider needed for proper operation.
                    camera.ConnectionLost += OnConnectionLost;
                    camera.CameraOpened += OnCameraOpened;
                    camera.CameraClosed += OnCameraClosed;
                    camera.StreamGrabber.GrabStarted += OnGrabStarted;
                    camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                    camera.StreamGrabber.GrabStopped += OnGrabStopped;
                    camera.Open();
                    camera.Parameters.Load(Dir + @"\chainn.pfs", ParameterPath.CameraDevice);
                    if (camera.IsConnected)
                    {
                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                        camera.Parameters[PLCamera.UserOutputValue].SetValue(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnToggle1.IsChecked = false;
                    DestroyCamera();
                }
            }
            if (btnToggle1.IsChecked == false)
            {
                if (source != null)
                {
                    source.Cancel();
                }
                DestroyCamera();
            }
        }

        #endregion

        #region Basler Camera
        private void EnableButtons(bool canGrab, bool canStop)
        {
            if (!a) User1.Oneshot_Button.IsEnabled = canGrab && IsSingleSupported();
            else User1.Oneshot_Button.IsEnabled = canGrab;
            User1.Continuous_Button.IsEnabled = canGrab;
            User1.Stop_Button.IsEnabled = canStop;
        }
        private bool IsSingleSupported()
        {
            // Camera can be null if not yet opened
            if (camera == null)
            {
                return false;
            }

            // Camera can be closed
            if (!camera.IsOpen)
            {
                return false;
            }

            bool canSet = camera.Parameters[PLCamera.AcquisitionMode].CanSetValue("SingleFrame");
            return canSet;
        }
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Dispatcher.Invoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }

            // Close the camera object.
            if (a)
            {
                a = false;
                if (source != null)
                {
                    source.Cancel();
                }
                MessageBox.Show("Camera mất kết nối hoặc dây bị lỏng !!!");
            }
            else
            {
                Task.Run(async () =>
                {
                    await Task.Delay(2000);
                    DestroyCamera();
                });
            }
        }
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Dispatcher.BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }
            //prepare the converter
            converter.OutputPixelFormat = PixelType.RGB8packed;
            converter.Parameters[PLPixelDataConverter.InconvertibleEdgeHandling].SetValue("Clip");

            string oldPixelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue();
            // The image provider is ready to grab. Enable the grab buttons.
            if (!a) EnableButtons(true, false);
        }
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Dispatcher.BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }
            // The camera connection is closed. Disable all buttons.
            if (!a) EnableButtons(false, false);
        }
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Dispatcher.BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }
            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.

            stopWatch.Reset();

            // Do not update the device list while grabbing to reduce jitter. Jitter may occur because the GUI thread is blocked for a short time when enumerating.
            // updateDeviceListTimer.Stop();

            // The camera is grabbing. Disable the grab buttons. Enable the stop button.
            if (!a) EnableButtons(false, true);
        }
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                Dispatcher.BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        if ((User1.UserPictureBox1 == null |
                            PictureBox1 == null
                            | PictureBox6 == null))
                        {
                            User1.UserPictureBox1 = new WriteableBitmap(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Gray8, null);
                            PictureBox1 = new WriteableBitmap(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Gray8, null);
                            PictureBox6 = new WriteableBitmap(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Gray8, null);

                            User1.pictureBox1.ImageSource = User1.UserPictureBox1;
                            pictureBox1.ImageSource = PictureBox1;
                            pictureBox6.ImageSource = PictureBox6;
                        }

                        converter.OutputPixelFormat = PixelType.Mono8;
                        if (img_chain_buffer == null)
                            img_chain_buffer = new byte[converter.GetBufferSizeForConversion(grabResult)];
                        converter.Convert(img_chain_buffer, grabResult);

                        if (!a)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                User1.UserPictureBox1.Lock();
                                User1.UserPictureBox1.WritePixels(new Int32Rect(0, 0, User1.UserPictureBox1.PixelWidth, User1.UserPictureBox1.PixelHeight),
                                                            img_chain_buffer, User1.UserPictureBox1.PixelWidth, 0);
                                User1.UserPictureBox1.Unlock();
                            });
                            img_chain_buffer1 = img_chain_buffer;
                        }
                        else if(camera!=null && a)
                        {
                            if (!user1_in_process)
                            {
                                try
                                {
                                    user1_in_process = true;
                                    if (img_chain_buffer != null) User1_Process();
                                    user1_in_process = false;
                                    camera.Parameters[PLCamera.LineSelector].SetValue(PLCamera.LineSelector.Line1);
                                    Pass = camera.Parameters[PLCamera.LineStatus].GetValue(); // Press Pass button (true) or not (false)
                                    if (Pass)
                                    {
                                        allow_Stop = false;
                                        Roller_status_text.Dispatcher.Invoke(() =>
                                        {
                                            if (Roller_status_text.Text == "")
                                            {
                                                Roller_status_text.Background = System.Windows.Media.Brushes.Yellow;
                                                Roller_status_text.Text = "BY PASS";
                                            }
                                        }); // Press Pass button, OK as default
                                    }
                                    else
                                    {
                                        Roller_status_text.Dispatcher.Invoke(() =>
                                        {
                                            if (Roller_status_text.Text == "BY PASS")
                                            {
                                                Roller_status_text.Background = System.Windows.Media.Brushes.Transparent;
                                                Roller_status_text.Text = "";
                                            }
                                        });
                                    }
                                    if (allow_Stop && allow_SetOutClose) // NG
                                    {
                                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                                        camera.Parameters[PLCamera.UserOutputValue].SetValue(true); // close output, stop the motor
                                        allow_SetOutClose = false;
                                    }
                                    else if (!allow_Stop && !allow_SetOutClose) // Pass button is pressed
                                    {
                                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                                        camera.Parameters[PLCamera.UserOutputValue].SetValue(false); // open output, enable the motor
                                        allow_SetOutClose = true;
                                    }
                                }
                                catch
                                {
                                    a = false;
                                }

                                if (token.IsCancellationRequested)
                                {
                                    a = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Dispatcher.BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
                return;
            }

            // Reset the stopwatch.
            stopWatch.Reset();

            // Re-enable the updating of the device list.
            //  updateDeviceListTimer.Start();

            // The camera stopped grabbing. Enable the grab buttons. Disable the stop button.
            if (!a) EnableButtons(true, false);

            // If the grabbed stop due to an error, display the error message.
            if (e.Reason != GrabStopReason.UserRequest)
            {
                MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error");
            }
        }
        private void DestroyCamera()
        {
            // Destroy the camera object.
            try
            {
                if (camera != null)
                {
                    if (camera.IsConnected)
                    {
                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                        camera.Parameters[PLCamera.UserOutputValue].SetValue(false);
                    }
                    camera.Close();
                    camera.Dispose();
                    camera = null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void OneShot_Run()
        {
            if ((camera != null) && (camera.StreamGrabber.IsGrabbing == false))
            {
                // Starts the grabbing of one image.
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
        }
        public static void OneShot_Edit()
        {
            if ((camera != null) && (camera.StreamGrabber.IsGrabbing == false))
            {
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
        }

        public static void ContinuousShot_Edit()
        {
            if ((camera != null) && (camera.StreamGrabber.IsGrabbing == false))
            {
                try
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        private void ContinuousShot_Run()
        {
            if ((camera != null) && (camera.StreamGrabber.IsGrabbing == false))
            {
                try
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        public static void Stop()
        {
            // Stop the grabbing.
            if ((camera != null))
            {
                try
                {
                    camera.StreamGrabber.Stop();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        #endregion

        #region Subroutine
        const string dllImport = @"D:\Desktop\DongBoChain_1_Cameras_Cpp\x64\Release\MyDll.dll"; // Release dll ones uses very less CPU resources compared to Debug ones
        //const string dllImport = @"D:\Desktop\DongBoChain_1_Cameras_Cpp\x64\Debug\MyDll.dll";
        [DllImport(dllImport, CallingConvention = CallingConvention.Cdecl)]
        private static extern double chain_node_area(byte[] imageBuffer, ref ImageInfo out_img,
                       int Bitmap_width, int Bitmap_height, int row_cls, int col_cls, int row_ero,
                       int col_ero, double x, double x1, double y, double y1);

        [DllImport("MyDll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ReleaseMemoryFromC(IntPtr buf);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        public static void ClearPictureBox(WriteableBitmap bitmap)
        {
            if (bitmap != null)
            {
                Int32Rect rect = new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight);
                int bytesPerPixel = bitmap.Format.BitsPerPixel / 8;
                byte[] empty = new byte[rect.Width * rect.Height * bytesPerPixel];
                int emptyStride = rect.Width * bytesPerPixel;
                bitmap.WritePixels(rect, empty, emptyStride, 0);
            }
        }
        private void MainWindow_Run()
        {
            if (source != null) source.Dispose();
            source = new CancellationTokenSource(); // khai bao lai source moi dau khi cancel
            token = source.Token;

            User1.Section1.Dispatcher.Invoke(() => { User1.Section1.IsEnabled = false; });
            User1.Section2.Dispatcher.Invoke(() => { User1.Section2.IsEnabled = false; });
            User1.Section4.Dispatcher.Invoke(() => { User1.Section4.IsEnabled = false; });

            if (camera != null)
            {
                Stop();
            }
            allow_Stop = false;
            allow_SetOutClose = true;
            user1_in_process = false;
            ContinuousShot_Run();
        }
        private void MainWindow_Edit()
        {
            if (source != null)
            {
                source.Cancel();
            }
            if (camera != null)
            {
                if (camera.IsConnected)
                {
                    camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                    camera.Parameters[PLCamera.UserOutputValue].SetValue(false); // open output, enable the motor
                }
                Stop();
            }
            User1.Section1.Dispatcher.Invoke(() => { User1.Section1.IsEnabled = true; });
            User1.Section2.Dispatcher.Invoke(() => { User1.Section2.IsEnabled = true; });
            User1.Section4.Dispatcher.Invoke(() => { User1.Section4.IsEnabled = true; });
        }
        private void User1_Process()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PictureBox1.Lock();
                    PictureBox1.WritePixels(new Int32Rect(0, 0, PictureBox1.PixelWidth, PictureBox1.PixelHeight),
                                                img_chain_buffer, PictureBox1.PixelWidth, 0);
                    PictureBox1.Unlock();
                });
                User1.Chain_area = chain_node_area(img_chain_buffer, ref out_image, bitmapWidth, bitmapHeight,
                                                   User1.row_cls, User1.col_cls, User1.row_ero, User1.col_ero,
                                                   User1.x_chain_day, User1.x1_chain_day, User1.y_chain_day, User1.y1_chain_day);

                if ((User1.ideal_area - User1.Chain_area) > User1.max_diff && User1.Chain_area > User1.max_diff * 3)
                {
                    Chain_status_color.Dispatcher.Invoke(() => { Chain_status_color.Background = System.Windows.Media.Brushes.Red; });
                    Chain_status_text.Dispatcher.Invoke(() => { Chain_status_text.Text = "NG"; });
                    if (!Pass) allow_Stop = true;

                    byte[] imagePixels = new byte[out_image.size];
                    Marshal.Copy(out_image.data, imagePixels, 0, out_image.size);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PictureBox5.Lock();
                        PictureBox5.WritePixels(new Int32Rect(0, 0, PictureBox5.PixelWidth, PictureBox5.PixelHeight),
                                                    imagePixels, PictureBox5.PixelWidth * out_image.elementSize, 0);
                        PictureBox5.Unlock();

                        PictureBox6.Lock();
                        PictureBox6.WritePixels(new Int32Rect(0, 0, bitmapWidth, bitmapHeight),
                                                    img_chain_buffer, bitmapWidth, 0);
                        PictureBox6.Unlock();
                    });
                }
                else
                {
                    Chain_status_color.Dispatcher.Invoke(() => { Chain_status_color.Background = System.Windows.Media.Brushes.Lime; });
                    Chain_status_text.Dispatcher.Invoke(() => { Chain_status_text.Text = "OK"; });
                }
                ReleaseMemoryFromC(out_image.data);
            }
            catch
            {
                if (source != null)
                {
                    source.Cancel();
                }
                MessageBox.Show("Chưa nhập đầy đủ thông số !!!");
            }
        }
        #endregion
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (source != null)
            {
                source.Cancel();
                a = false;
                source.Dispose();
                source = null;
            }
            if (camera != null)
            {
                Stop();
                DestroyCamera();
            }
        }
    }
}
