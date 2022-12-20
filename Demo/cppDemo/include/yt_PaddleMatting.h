#pragma once
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

#include <include/yt_MattingParameter.h>
using namespace std;
namespace  PaddleMatting
{
	//µ¼³öCº¯Êý
	extern "C" {
#ifdef _WIN64
		__declspec(dllexport) void MattingInit(char* modelPath, MattingParameter  parameter);
		__declspec(dllexport) void MattingSetbackground(int r, int g, int b);
		__declspec(dllexport) int  MattingSegFile(char* imagefile, char* outfile);
		__declspec(dllexport) int  MattingSegByte(char* imagebytedata, size_t* size, char* outfile);
		__declspec(dllexport) int  MattingSegBase64(char* imagebase64, char* outfile);

#else

#endif 

	}
}

