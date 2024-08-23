#include "pch.h" // This header must the place on the top
#include "Source_OpenCV.h"
 void Parallel_process_dilate::operator()(const cv::Range& range) const 
 {
     for (int i = range.start; i < range.end; i++) // range = 10, for loop run from value 0 to 9 (10 threads), i width
     {
         cv::Mat in(img, cv::Rect(0, (img.rows / diff) * i,
             img.cols, i == (range.end - 1) ? ((img.rows / diff) + (img.rows % diff)) : (img.rows / diff)));
         cv::Mat out(retVal, cv::Rect(0, (retVal.rows / diff) * i,
             retVal.cols, i == (range.end - 1) ? ((retVal.rows / diff) + (retVal.rows % diff)) : (retVal.rows / diff)));
         cv::dilate(in, out,
             getStructuringElement(cv::MORPH_RECT, cv::Size(cols, rows)));
     }
 }

 void Parallel_process_erode::operator()(const cv::Range& range) const
 {
     for (int i = range.start; i < range.end; i++) // range = 10, for loop run from value 0 to 9 (10 threads), i width
     {
         cv::Mat in(img, cv::Rect(0, (img.rows / diff) * i,
             img.cols, i == (range.end - 1) ? ((img.rows / diff) + (img.rows % diff)) : (img.rows / diff)));
         cv::Mat out(retVal, cv::Rect(0, (retVal.rows / diff) * i,
             retVal.cols, i == (range.end - 1) ? ((retVal.rows / diff) + (retVal.rows % diff)) : (retVal.rows / diff)));
         cv::erode(in, out,
             getStructuringElement(cv::MORPH_RECT, cv::Size(cols, rows)));
     }
 }
