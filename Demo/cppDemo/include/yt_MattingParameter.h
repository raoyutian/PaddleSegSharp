// Copyright (c) 2021 raoyutian Authors. All Rights Reserved.
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

#pragma once
#pragma pack(push,1)
 
struct MattingParameter
{
	//通用参数
	bool use_gpu;//是否使用GPU；默认false
	int gpu_id;//GPU id，使用GPU时有效；默认0;
	int gpu_mem;//申请的GPU内存;默认4000
	int cpu_math_library_num_threads;//CPU预测时的线程数，在机器核数充足的情况下，该值越大，预测速度越快；默认10
	bool enable_mkldnn;//是否使用mkldnn库；默认true

	MattingParameter()
	{
		//通用参数
		use_gpu = false;
		gpu_id = 0;
		gpu_mem = 4000;
		cpu_math_library_num_threads = 10;
		enable_mkldnn = true;

	}
};

#pragma pack(pop) 

