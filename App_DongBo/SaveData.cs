using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace App_DongBo
{
    class parameters_data_node
    {
        public int row_cls_node;
        public int colum_cls_node;
        public int row_ero_node;
        public int colum_ero_node;
        public double max_diff_node;
        public double ideal_area_node;
    }
    class parameters_data_roller
    {
        public int row_cls_roller;
        public int colum_cls_roller;
        public int row_ero_roller;
        public int colum_ero_roller;
        public double min_score_roller;
    }
    public struct ImageInfo
    {
        public IntPtr data; // IntPtr type because we'll free it later in C++ source 
        public int size;
        public int elementSize;
    };
}
