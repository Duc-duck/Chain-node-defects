#pragma once
#define EXPORTED_METHOD extern "C" __declspec(dllexport)

struct ImageInfo
{
    unsigned char* data;
    int size;
    int elementSize;
};

EXPORTED_METHOD double chain_node_area(unsigned char* imageBuffer, ImageInfo& out_img,
                       int Bitmap_width, int Bitmap_height, int row_cls, int col_cls, int row_ero,
                       int col_ero, double x, double x1, double y, double y1);

EXPORTED_METHOD void ReleaseMemoryFromC(unsigned char* buf);

EXPORTED_METHOD bool read_img(unsigned char* buffer_data, int Bitmap_width, int Bitmap_height,
                             double x, double x1, double y, double y1, ImageInfo& imInfo);
