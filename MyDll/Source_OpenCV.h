#pragma once
#include <opencv2/core.hpp>
#include <opencv2/imgcodecs.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/highgui.hpp>

class Parallel_process_dilate : public cv::ParallelLoopBody
{
private:
    cv::Mat img;
    cv::Mat& retVal;
    int rows;
    int cols;
    int diff;

public:
    Parallel_process_dilate(cv::Mat inputImgage, cv::Mat& outImage,
        int rowsVal, int colsVal, int diffVal)
        : img(inputImgage), retVal(outImage),
        rows(rowsVal), cols(colsVal), diff(diffVal) {}

    virtual void operator()(const cv::Range& range) const;
};

class Parallel_process_erode : public cv::ParallelLoopBody
{
private:
    cv::Mat img;
    cv::Mat& retVal;
    int rows;
    int cols;
    int diff;


public:
    Parallel_process_erode(cv::Mat inputImgage, cv::Mat& outImage,
        int rowsVal, int colsVal, int diffVal)
        : img(inputImgage), retVal(outImage), 
        rows(rowsVal), cols(colsVal), diff(diffVal) {}

    virtual void operator()(const cv::Range& range) const;
};
