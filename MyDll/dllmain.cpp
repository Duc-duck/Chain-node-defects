// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "Source_OpenCV.h"
#include "dllmain.h"

void convertToGray(cv::Mat& Mat_data, ImageInfo& imInfo)
{
    std::vector<unsigned char> bytes;
    imencode(".png", Mat_data, bytes);
    imInfo.size = bytes.size();
    imInfo.data = (unsigned char*)calloc(imInfo.size, sizeof(unsigned char));
    std::copy(bytes.begin(), bytes.end(), imInfo.data);
}

bool read_img(unsigned char* buffer_data, int Bitmap_width, int Bitmap_height, 
              double x, double x1, double y, double y1, ImageInfo& out_image)
{
    cv::Mat image = cv::Mat(Bitmap_height, Bitmap_width, CV_8UC1, buffer_data, cv::Mat::AUTO_STEP);    
    cv::Mat ROI(image, cv::Rect(x * Bitmap_width, y * Bitmap_height, (int)((x1 - x) * Bitmap_width),
        (int)((y1 - y) * Bitmap_height)));
    cv::Mat mat;
    ROI.copyTo(mat);
    //cv::Mat mat = image;
    //cv::cvtColor(image, mat, cv::COLOR_BGR2GRAY); // Mono image doesn't need to be convert to gray
    out_image.elementSize = mat.elemSize();
    out_image.size = mat.total() * mat.elemSize();
    out_image.data = (unsigned char*)calloc(out_image.size, sizeof(unsigned char));
    //memcpy(out_image.data, &mat.data[0], out_image.size);
    std::copy(mat.datastart, mat.dataend , out_image.data);
    // directly assign "mat.data" to "out_image.data" will cause memory read/write interuption in WritePixels process
    return 1;
}

double chain_node_area(unsigned char* imageBuffer, ImageInfo& out_img,
                       int Bitmap_width, int Bitmap_height, int row_cls, int col_cls, int row_ero,
                       int col_ero, double x, double x1, double y, double y1)
{
    // Convert the byte array to a cv::Mat object
    cv::Mat image = cv::Mat(Bitmap_height, Bitmap_width, CV_8UC1); // using mono camera
    image.data = imageBuffer;
    cv::Mat ROI(image, cv::Rect(x * Bitmap_width, y * Bitmap_height, 
                               (int)((x1 - x) * Bitmap_width), (int)((y1 - y) * Bitmap_height)));
    cv::Mat crop;
    ROI.copyTo(crop);
    cv::Mat inverted = cv::Scalar::all(255) - crop;
    //int range_end_rows = inverted.rows / (row_cls * 15);
    int range_end_rows = 20;
    cv::Mat img_cls, img_cls1, img_min1, img_min2, img_min;
    //img_cls = img_cls1 = img_min1 = img_min2 = img_min = cv::Mat::zeros(inverted.size(), inverted.type());
    img_cls = img_cls1 = img_min1 = img_min2 = img_min = inverted;
    cv::parallel_for_(cv::Range(0, range_end_rows), Parallel_process_dilate(inverted, img_cls,
        (2 * row_cls) + 1, (2 * col_cls * 2) + 1, range_end_rows));
    //cv::dilate(inverted, img_cls,
    //    getStructuringElement(cv::MORPH_RECT, cv::Size((2 * col_cls * 2) + 1, (2 * row_cls) + 1)));
    cv::parallel_for_(cv::Range(0, range_end_rows), Parallel_process_erode(img_cls, img_cls1,
        (2 * row_cls) + 1, (2 * col_cls * 2) + 1, range_end_rows));
    //cv::erode(img_cls, img_cls1,
    //    getStructuringElement(cv::MORPH_RECT, cv::Size((2 * col_cls * 2) + 1, (2 * row_cls) + 1)));

    cv::parallel_for_(cv::Range(0, range_end_rows), Parallel_process_erode(img_cls1, img_min1,
        (2 * col_ero) + 1, (2 * row_ero * 2) + 1, range_end_rows));
    //cv::erode(img_cls1, img_min1,
    //    getStructuringElement(cv::MORPH_RECT, cv::Size((2 * row_ero * 2) + 1, (2 * col_ero) + 1)));
    cv::parallel_for_(cv::Range(0, range_end_rows), Parallel_process_dilate(img_min1, img_min2,
        (2 * col_ero) + 1, (2 * row_ero * 2) + 1, range_end_rows));
    //cv::dilate(img_min1, img_min2,
    //    getStructuringElement(cv::MORPH_RECT, cv::Size((2 * row_ero * 2) + 1, (2 * col_ero) + 1)));

    cv::parallel_for_(cv::Range(0, range_end_rows), Parallel_process_erode(img_min2, img_min,
        (2 * row_ero) + 1, (2 * col_ero) + 1, range_end_rows));
    //cv::erode(img_min2, img_min,
    //    getStructuringElement(cv::MORPH_RECT, cv::Size((2 * col_ero) + 1, (2 * row_ero) + 1)));
    cv::Mat thresh;
    cv::threshold(img_min, thresh, 128, 255, cv::THRESH_BINARY);
    out_img.size = img_min.total() * img_min.elemSize();
    out_img.elementSize = img_min.elemSize();
    out_img.data = (unsigned char*)calloc(out_img.size, sizeof(unsigned char));
    std::copy(img_min.datastart, img_min.dataend, out_img.data);

    return cv::countNonZero(thresh);
}

void ReleaseMemoryFromC(unsigned char* buf)
{
    if (buf == NULL) return;
    free(buf);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
