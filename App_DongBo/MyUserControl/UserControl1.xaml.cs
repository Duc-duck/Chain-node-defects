using System;
using System.Windows;
using System.Windows.Controls;
using App_DongBo.Properties;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace App_DongBo.MyUserControl
{
    // <summary>
    // Interaction logic for UserControl1.xaml
    // </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public WriteableBitmap UserPictureBox1;
        public WriteableBitmap UserPictureBox2;
        public int row_cls, col_cls, row_ero, col_ero;
        public double max_diff, ideal_area, Chain_area;
        public double x_chain_day, y_chain_day, x1_chain_day, y1_chain_day;
        private string chain_id;
        ImageInfo out_image = new ImageInfo();
        ContentControl contentcontrol;
        System.Windows.Shapes.Rectangle rectangle;
        public string Chain_ID
        {
            get { return chain_id; }
            set { chain_id = value; OnPropertyChanged("chain_id"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public UserControl1()
        {
            InitializeComponent();
            Load_Setting();
            Save_Button.IsEnabled = false;            
            Choose_model.Text = "Xích dày";
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(contentcontrol != null)
            {
                if(x_chain_day > 0 && y_chain_day >0)
                {
                    contentcontrol.Width = (x1_chain_day - x_chain_day) * canvasControl1.ActualWidth;
                    contentcontrol.Height = (y1_chain_day - y_chain_day) * canvasControl1.ActualHeight;
                    Canvas.SetTop(contentcontrol, y_chain_day * canvasControl1.ActualHeight);
                    Canvas.SetLeft(contentcontrol, x_chain_day * canvasControl1.ActualWidth);
                }
                else
                {
                    contentcontrol.Width = canvasControl1.ActualWidth * 0.18;
                    contentcontrol.Height = canvasControl1.ActualHeight * 0.18;
                    Canvas.SetTop(contentcontrol, canvasControl1.ActualHeight * 0.2);
                    Canvas.SetLeft(contentcontrol, canvasControl1.ActualWidth * 0.2);
                }
            }
        }
        #region Section1
        private void Button_OneShot(object sender, RoutedEventArgs e)
        {
            MainWindow.OneShot_Edit();        
        }
        private void Continuous_Button_Click(object sender, RoutedEventArgs e)
        {
                MainWindow.ContinuousShot_Edit();
        }
        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
                MainWindow.Stop();
        }

        private void Choose_Model_Code(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog Browse_Model = new Microsoft.Win32.OpenFileDialog();
            Browse_Model.DefaultExt = ".json";
            Browse_Model.Filter = "chain model (.json)|*.json";
            Browse_Model.InitialDirectory = "C:\\Users\\HPC\\Desktop\\Mã xích";
            Nullable<bool> result = Browse_Model.ShowDialog();
            if (result == true)
            {
                string json = System.IO.File.ReadAllText(Browse_Model.FileName);
                parameters_data_node jsonObj = JsonConvert.DeserializeObject<parameters_data_node>(json);

                row_cls = jsonObj.row_cls_node;
                col_cls = jsonObj.colum_cls_node;
                row_ero = jsonObj.row_ero_node;
                col_ero = jsonObj.colum_ero_node;
                max_diff = jsonObj.max_diff_node;
                ideal_area = jsonObj.ideal_area_node;

                Row_cls.Text = jsonObj.row_cls_node.ToString();
                Col_cls.Text = jsonObj.colum_cls_node.ToString();
                Row_ero.Text = jsonObj.row_ero_node.ToString();
                Col_ero.Text = jsonObj.colum_ero_node.ToString();
                Max_diff.Text = jsonObj.max_diff_node.ToString();
                Ideal_Area.Text = jsonObj.ideal_area_node.ToString();
                Chain_ID = Browse_Model.SafeFileName.Split('.')[0];                
            }
        }
        #endregion

        #region Section2
        private void Button_Draw(object sender, RoutedEventArgs e)
        {
            if(!Save_Button.IsEnabled) Save_Button.IsEnabled = true;
            // Draw rectangle for "Xích dày" and "Xích mỏng"
            switch (Choose_model.Text)
            {
                case "Xích dày":
                    if (canvasControl1.Children.Contains(contentcontrol)) canvasControl1.Children.Remove(contentcontrol);

                    if (x_chain_day > 0 && y_chain_day > 0)
                    {
                        rectangle = null;
                        contentcontrol = null;
                        contentcontrol = new ContentControl();
                        contentcontrol.Width = (x1_chain_day - x_chain_day) * canvasControl1.ActualWidth;
                        contentcontrol.Height = (y1_chain_day - y_chain_day) * canvasControl1.ActualHeight;
                        Canvas.SetTop(contentcontrol, y_chain_day * canvasControl1.ActualHeight);
                        Canvas.SetLeft(contentcontrol, x_chain_day * canvasControl1.ActualWidth);
                        Selector.SetIsSelected(contentcontrol, true);
                        contentcontrol.Style = Application.Current.Resources["DesignerItemStyle"] as Style;
                        rectangle = new System.Windows.Shapes.Rectangle();
                        rectangle.Fill = System.Windows.Media.Brushes.Transparent;
                        rectangle.IsHitTestVisible = false;
                        rectangle.Stroke = System.Windows.Media.Brushes.Red;
                        rectangle.StrokeThickness = 1;
                        rectangle.Stretch = Stretch.Fill;
                        contentcontrol.Content = rectangle;
                        canvasControl1.Children.Add(contentcontrol);
                    }
                    else
                    {
                        rectangle = null;
                        contentcontrol = null;
                        contentcontrol = new ContentControl();
                        contentcontrol.Width = canvasControl1.ActualWidth * 0.18;
                        contentcontrol.Height = canvasControl1.ActualHeight * 0.18;
                        Canvas.SetTop(contentcontrol, canvasControl1.ActualHeight * 0.2);
                        Canvas.SetLeft(contentcontrol, canvasControl1.ActualWidth * 0.2);
                        Selector.SetIsSelected(contentcontrol, true);
                        contentcontrol.Style = Application.Current.Resources["DesignerItemStyle"] as Style;
                        rectangle = new System.Windows.Shapes.Rectangle();
                        rectangle.Fill = System.Windows.Media.Brushes.Transparent;
                        rectangle.IsHitTestVisible = false;
                        rectangle.Stroke = System.Windows.Media.Brushes.Red;
                        rectangle.StrokeThickness = 1;
                        rectangle.Stretch = Stretch.Fill;
                        contentcontrol.Content = rectangle;
                        canvasControl1.Children.Add(contentcontrol);
                    }
                    break;
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            Password connection_window = new Password();
            Window parentWindow = Window.GetWindow(this);
            connection_window.Owner = parentWindow;
            var result = connection_window.ShowDialog();
            if (result == true)
            {
                switch (Choose_model.Text)
                {
                    case "Xích dày":
                        // Save rectangle
                        rectangle.Stroke = System.Windows.Media.Brushes.Blue;
                        x_chain_day = Canvas.GetLeft(contentcontrol);
                        y_chain_day = Canvas.GetTop(contentcontrol);
                        x1_chain_day = Canvas.GetLeft(contentcontrol) + contentcontrol.Width;
                        y1_chain_day = Canvas.GetTop(contentcontrol) + contentcontrol.Height;

                        if (x1_chain_day < 0) { x1_chain_day = (x1_chain_day - x_chain_day); x_chain_day = 1; }
                        if (x_chain_day < 0) x_chain_day = 1;
                        if (x_chain_day > canvasControl1.ActualWidth) { x_chain_day = canvasControl1.ActualWidth - (x1_chain_day - x_chain_day); x1_chain_day = canvasControl1.ActualWidth - 1; }
                        if (x1_chain_day > canvasControl1.ActualWidth) x1_chain_day = canvasControl1.ActualWidth - 1;

                        if (y1_chain_day < 0) { y1_chain_day = (x1_chain_day - x_chain_day); y_chain_day = 1; }
                        if (y_chain_day < 0) y_chain_day = 1;
                        if (y_chain_day > canvasControl1.ActualHeight) { y_chain_day = canvasControl1.ActualHeight - (y1_chain_day - y_chain_day); y1_chain_day = canvasControl1.ActualHeight - 1; }
                        if (y1_chain_day > canvasControl1.ActualHeight) y1_chain_day = canvasControl1.ActualHeight - 1;

                        x_chain_day = x_chain_day / canvasControl1.ActualWidth;
                        y_chain_day = y_chain_day / canvasControl1.ActualHeight;
                        x1_chain_day = x1_chain_day / canvasControl1.ActualWidth;
                        y1_chain_day = y1_chain_day / canvasControl1.ActualHeight;

                        Settings.Default.x_chain_day = x_chain_day;
                        Settings.Default.y_chain_day = y_chain_day;
                        Settings.Default.x1_chain_day = x1_chain_day;
                        Settings.Default.y1_chain_day = y1_chain_day;

                        Settings.Default.Save();
                        Task.Run(async () => {
                            await Task.Delay(500);
                            rectangle.Dispatcher.Invoke(() => { rectangle.Stroke = System.Windows.Media.Brushes.Red; });
                        });
                        break;
                    case "Xích mỏng":
                        // Save rectangle

                        Settings.Default.Save();

                        break;
                    default:
                        System.Windows.MessageBox.Show("Chưa chọn loại xích!");
                        break;
                }
            }            

            if(MainWindow.img_chain_buffer1 != null)
            {
                UserPictureBox2 = null;
                UserPictureBox2 = new WriteableBitmap((int)((double)MainWindow.bitmapWidth * (x1_chain_day - x_chain_day)),
                                                      (int)((double)MainWindow.bitmapHeight * (y1_chain_day - y_chain_day)),
                                                      96, 96, PixelFormats.Gray8, null);
                pictureBox2.ImageSource = UserPictureBox2;
            }
        }
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Bạn có chắc muốn xóa thông số \"QUAN TRỌNG\" không???", "CẢNH BÁO", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    x_chain_day = 0;
                    y_chain_day = 0;
                    break;
                case MessageBoxResult.No:                   
                    break;
            }
        }
        private void Get_Area_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MainWindow.img_chain_buffer1 != null)
                {
                    if (Choose_model.Text == "Xích dày")
                    {
                        Chain_area = chain_node_area(MainWindow.img_chain_buffer1, ref out_image,
                                                    UserPictureBox1.PixelWidth,
                                                    UserPictureBox1.PixelHeight,
                                                    row_cls, col_cls, row_ero, col_ero,
                                                    x_chain_day, x1_chain_day, y_chain_day, y1_chain_day);

                        //------------ we use "read_img" for display testing ------------
                        //bool ab = read_img(MainWindow.img_chain_buffer1,
                        //                   UserPictureBox1.PixelWidth, UserPictureBox1.PixelHeight,
                        //                   x_chain_day, x1_chain_day, y_chain_day, y1_chain_day,
                        //                   ref out_image);
                        //ImageInfo alo = out_image;

                        byte[] imagePixels = new byte[out_image.size];
                        Marshal.Copy(out_image.data, imagePixels, 0, out_image.size);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            UserPictureBox2.Lock();
                            UserPictureBox2.WritePixels(new Int32Rect(0, 0, UserPictureBox2.PixelWidth, UserPictureBox2.PixelHeight),
                                                        imagePixels, UserPictureBox2.PixelWidth * out_image.elementSize, 0);
                            UserPictureBox2.Unlock();
                        });                       
                        ReleaseMemoryFromC(out_image.data);
                        Ideal_Area.Text = Chain_area.ToString();
                        ideal_area = Convert.ToDouble(Ideal_Area.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            try
            {
                // Save parameters
                row_cls = Convert.ToInt16(Row_cls.Text);
                col_cls = Convert.ToInt16(Col_cls.Text);
                row_ero = Convert.ToInt16(Row_ero.Text);
                col_ero = Convert.ToInt16(Col_ero.Text);
                max_diff = Convert.ToDouble(Max_diff.Text);
                ideal_area = Convert.ToDouble(Ideal_Area.Text);
                Task.Run(async () =>
                {
                    Row_cls.Dispatcher.Invoke(() => { Row_cls.Foreground = System.Windows.Media.Brushes.Blue; });
                    Col_cls.Dispatcher.Invoke(() => { Col_cls.Foreground = System.Windows.Media.Brushes.Blue; });
                    Row_ero.Dispatcher.Invoke(() => { Row_ero.Foreground = System.Windows.Media.Brushes.Blue; });
                    Col_ero.Dispatcher.Invoke(() => { Col_ero.Foreground = System.Windows.Media.Brushes.Blue; });
                    Max_diff.Dispatcher.Invoke(() => { Max_diff.Foreground = System.Windows.Media.Brushes.Blue; });
                    Ideal_Area.Dispatcher.Invoke(() => { Ideal_Area.Foreground = System.Windows.Media.Brushes.Blue; });
                    await Task.Delay(500);
                    Row_cls.Dispatcher.Invoke(() => { Row_cls.Foreground = System.Windows.Media.Brushes.Black; });
                    Col_cls.Dispatcher.Invoke(() => { Col_cls.Foreground = System.Windows.Media.Brushes.Black; });
                    Row_ero.Dispatcher.Invoke(() => { Row_ero.Foreground = System.Windows.Media.Brushes.Black; });
                    Col_ero.Dispatcher.Invoke(() => { Col_ero.Foreground = System.Windows.Media.Brushes.Black; });
                    Max_diff.Dispatcher.Invoke(() => { Max_diff.Foreground = System.Windows.Media.Brushes.Black; });
                    Ideal_Area.Dispatcher.Invoke(() => { Ideal_Area.Foreground = System.Windows.Media.Brushes.Black; });
                });
            }
            catch { }
        }
        #endregion

        #region Section3

        #endregion

        #region Section4
        private void Button_Save_1(object sender, RoutedEventArgs e)
        {
            try
            {
                row_cls = Convert.ToInt16(Row_cls.Text);
                col_cls = Convert.ToInt16(Col_cls.Text);
                row_ero = Convert.ToInt16(Row_ero.Text);
                col_ero = Convert.ToInt16(Col_ero.Text);
                max_diff = Convert.ToDouble(Max_diff.Text);
                ideal_area = Convert.ToDouble(Ideal_Area.Text);

                Microsoft.Win32.SaveFileDialog model_name = new Microsoft.Win32.SaveFileDialog();
                model_name.InitialDirectory = "C:\\Users\\HPC\\Desktop\\Mã xích";
                model_name.FileName = "Chain-node-1"; // Default file name
                model_name.DefaultExt = ".json"; // Default file extension
                model_name.Filter = "chain code (.json)|*.json"; // Filter files by extension

                Nullable<bool> result = model_name.ShowDialog();
                if (result == true)
                {
                    parameters_data_node _data = new parameters_data_node
                    {
                        row_cls_node = row_cls,
                        colum_cls_node = col_cls,
                        row_ero_node = row_ero,
                        colum_ero_node = col_ero,
                        max_diff_node = max_diff,
                        ideal_area_node = ideal_area
                    };
                    string json = JsonConvert.SerializeObject(_data);
                    System.IO.File.WriteAllText(model_name.FileName, json);
                }
                else Chain_ID = "?????";
            }
            catch
            {
                System.Windows.MessageBox.Show("Không thể lưu. Chưa đủ cài đặt đủ thông số!!!");
            }
        }
        private void btnToggle1_Click(object sender, RoutedEventArgs e)
        {
            if (btnToggle1.IsChecked == false)
            {
                MainWindow.ClearPictureBox(UserPictureBox1);
                MainWindow.ClearPictureBox(UserPictureBox2);
            }
        }
        #endregion

        #region Subroutine
        const string dllImport = @"D:\Desktop\DongBoChain_1_Cameras_Cpp\x64\Release\MyDll.dll";
        //const string dllImport = @"D:\Desktop\DongBoChain_1_Cameras_Cpp\x64\Debug\MyDll.dll";
        [DllImport(dllImport, CallingConvention = CallingConvention.Cdecl)]
        private static extern double chain_node_area(byte[] imageBuffer, ref ImageInfo out_img,
                       int Bitmap_width, int Bitmap_height, int row_cls, int col_cls, int row_ero,
                       int col_ero, double x, double x1, double y, double y1);

        [DllImport(dllImport, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ReleaseMemoryFromC(IntPtr buf);

        [DllImport(dllImport, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool read_img(byte[] data, int Bitmap_width, int Bitmap_height,
                                            double x, double x1, double y, double y1, ref ImageInfo imInfo);
        private void Load_Setting()
        {
            x_chain_day = Settings.Default.x_chain_day;
            y_chain_day = Settings.Default.y_chain_day;
            x1_chain_day = Settings.Default.x1_chain_day;
            y1_chain_day = Settings.Default.y1_chain_day;

            //UserPictureBox2 = new WriteableBitmap((MainWindow.bitmapWidth * (int)(x1_chain_day - x_chain_day)),
            //                          (MainWindow.bitmapHeight * (int)(y1_chain_day - y_chain_day)),
            //                          96, 96, PixelFormats.Gray8, null);
            //pictureBox2.ImageSource = UserPictureBox2;
        }
        #endregion
    }
}
