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
using System.Runtime.InteropServices;
namespace PaddleSegSharp
{
    /// <summary>
    /// 参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class MattingParameter
    {

        #region 通用参数
        /// <summary>
        /// 是否使用GPU；默认false
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] 
		public bool use_gpu { get; set; } = false;
		/// <summary>
		/// GPU id，使用GPU时有效；默认0;
		/// </summary>
		public int gpu_id { get; set; } = 0;
		/// <summary>
		/// 申请的GPU内存;默认4000
		/// </summary>
		public int gpu_mem { get; set; } = 4000;
		/// <summary>
		/// CPU预测时的线程数，在机器核数充足的情况下，该值越大，预测速度越快；默认10
		/// </summary>
		public int numThread { get; set; } = 10;
		/// <summary>
		/// 是否使用mkldnn库；默认true
		/// </summary>
		[field:MarshalAs(UnmanagedType.I1)]
		public bool Enable_mkldnn { get; set; } =true;
        #endregion

	}
}


