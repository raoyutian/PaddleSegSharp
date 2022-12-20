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
	//ͨ�ò���
	bool use_gpu;//�Ƿ�ʹ��GPU��Ĭ��false
	int gpu_id;//GPU id��ʹ��GPUʱ��Ч��Ĭ��0;
	int gpu_mem;//�����GPU�ڴ�;Ĭ��4000
	int cpu_math_library_num_threads;//CPUԤ��ʱ���߳������ڻ����������������£���ֵԽ��Ԥ���ٶ�Խ�죻Ĭ��10
	bool enable_mkldnn;//�Ƿ�ʹ��mkldnn�⣻Ĭ��true

	MattingParameter()
	{
		//ͨ�ò���
		use_gpu = false;
		gpu_id = 0;
		gpu_mem = 4000;
		cpu_math_library_num_threads = 10;
		enable_mkldnn = true;

	}
};

#pragma pack(pop) 

