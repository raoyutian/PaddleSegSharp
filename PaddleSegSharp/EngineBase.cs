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
using System.Drawing.Imaging;
using System.IO;

namespace PaddleSegSharp
{
    /// <summary>
    /// 引擎基类
    /// </summary>
    public abstract class EngineBase : IDisposable
    {
        #region yt_CPUCheck API
        [DllImport("yt_CPUCheck.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int IsCPUSupport();
        #endregion
        #region private

        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        internal protected byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Guid == ImageFormat.Jpeg.Guid)
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Guid == ImageFormat.Png.Guid)
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Guid == ImageFormat.Bmp.Guid)
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Guid == ImageFormat.Gif.Guid)
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Guid == ImageFormat.Icon.Guid)
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                else
                {
                    image.Save(ms, ImageFormat.Png);
                }
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        #endregion

        public virtual void Dispose()
        {  
        }
    }
}