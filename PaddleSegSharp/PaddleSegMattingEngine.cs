// Copyright (c) 2022 raoyutian Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using System.Reflection;

namespace PaddleSegSharp
{
    /// <summary>
    /// PaddleOCR识别引擎对象
    /// </summary>
    public class PaddleSegMattingEngine : EngineBase
    {
        #region API
        [DllImport("PaddleSeg.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern void MattingInit(string modelPath, MattingParameter parameter);
        [DllImport("PaddleSeg.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern void MattingSetbackground(int r, int g, int b);

        [DllImport("PaddleSeg.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int MattingSegFile(string imagefile, string result);
      
        [DllImport("PaddleSeg.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int MattingSegByte(byte[] imagebytedata, long size, string result);
        [DllImport("PaddleSeg.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int MattingSegBase64(string imagebase64, string result);
      
        #endregion

        #region 分割
        /// <summary>
        /// 初始化参数与模型
        /// </summary>
        /// <param name="modelPath">模型绝对路径，如果为空则按默认值inference\ppmatting_human_512</param>
        /// <param name="parameter">识别参数，为空均按缺省值</param>
        public void Init(string modelPath, MattingParameter parameter)
        { 
            if (IsCPUSupport()<=0) throw new NotSupportedException($"当前CPU的指令集不支持PaddleSeg");

            if (parameter == null) parameter = new MattingParameter();
            if (string.IsNullOrEmpty(modelPath))
            {
                string root= System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                modelPath = root + @"\inference\modnet-mobilenetv2";
            }
            MattingInit(modelPath, parameter);
        }
        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <exception cref="NotSupportedException"></exception>
        public void Setbackground(int r, int g, int b)
        {
           
            if (IsCPUSupport() <= 0) throw new NotSupportedException($"当前CPU的指令集不支持PaddleSeg");
            MattingSetbackground(r, g, b);
        }
        /// <summary>
        /// 图像文件分割
        /// </summary>
        /// <param name="imagefile"></param>
        /// <param name="outfile"></param>

        public void Seg(string imagefile,string outfile)
        {
            if (!System.IO.File.Exists(imagefile)) throw new Exception($"文件{imagefile}不存在");
            MattingSegFile(imagefile, outfile);
        }

        /// <summary>
        /// 图像文件字节流分割
        /// </summary>
        /// <param name="imagebyte"></param>
        /// <param name="outfile"></param>
        public void Seg(byte[] imagebyte, string outfile)
        {
            if (imagebyte==null) throw new ArgumentNullException("imagebyte");
            MattingSegByte(imagebyte, imagebyte.LongLength, outfile);
        }
        /// <summary>
        /// 图像分割
        /// </summary>
        /// <param name="image"></param>
        /// <param name="outfile"></param>
        public void Seg(Image image, string outfile)
        {
            if (image == null) throw new ArgumentNullException("image");
            byte[] imagebyte = ImageToBytes(image);
            Seg(imagebyte, outfile);
        }
        /// <summary>
        /// 图像base64分割
        /// </summary>
        /// <param name="imagebase64"></param>
        /// <param name="outfile"></param>
        public void SegBase64(string imagebase64, string outfile)
        {
            if (imagebase64 == null || imagebase64 == "") throw new ArgumentNullException("imagebase64");
            MattingSegBase64(imagebase64, outfile);  
        }
        #endregion
        #region Dispose
        #endregion
    }
}