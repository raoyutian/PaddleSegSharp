#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include "string"
#include <string.h>
#include <include/yt_MattingParameter.h>
#include <io.h> 
#include <chrono>
#include <vector>
using namespace std;
using namespace chrono;
#pragma comment (lib,"PaddleSeg.lib")
extern "C" {
	__declspec(dllimport) void MattingInit(char* modelPath, MattingParameter  parameter);
	__declspec(dllimport) void MattingSetbackground(int r, int g, int b);
	__declspec(dllimport) int  MattingSegFile(char* imagefile, char* outfile);
	__declspec(dllimport) int  MattingSegByte(char* imagebytedata, size_t* size, char* outfile);
	__declspec(dllimport) int  MattingSegBase64(char* imagebase64, char* outfile);
};

void getFiles(string path, vector<string>& files)
{
	intptr_t   hFile = 0;//文件句柄，过会儿用来查找
	struct _finddata_t fileinfo;//文件信息
	string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
		//如果查找到第一个文件
	{
		do
		{
			if ((fileinfo.attrib & _A_SUBDIR))//如果是文件夹
			{
				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
					getFiles(p.assign(path).append("\\").append(fileinfo.name), files);
			}
			else//如果是文件
			{
				files.push_back(p.assign(path).append("\\").append(fileinfo.name));
			}
		} while (_findnext(hFile, &fileinfo) == 0);	//能寻找到其他文件

		_findclose(hFile);	//结束查找，关闭句柄
	}
}

int main()
{
	MattingParameter parameter;
	parameter.enable_mkldnn = true;
	parameter.cpu_math_library_num_threads = 10;
	 
	char path[MAX_PATH];

	GetCurrentDirectoryA(MAX_PATH, path);

	string modelpath(path);
	modelpath += "\\inference\\modnet-mobilenetv2"; 
	string imagepath(path);
	imagepath += "\\image";
	string outpath(path);
	outpath += "\\out\\";
	vector<string> images;
	getFiles(imagepath, images);
	 
	MattingInit(const_cast<char*>(modelpath.c_str()),parameter);
	MattingSetbackground(45,145,255);
	std::wcout.imbue(std::locale("chs"));
	if (images.size() > 0)
	{
		for (size_t i = 0; i < images.size(); i++)
		{
			auto	start = system_clock::now();
			int pos= images[i].find_last_of( "\\");
			string filename(images[i].substr(pos + 1));
			 
			 MattingSegFile( const_cast<char*>(images[i].c_str()), const_cast<char*>((outpath+ filename).c_str()));
			auto	end = system_clock::now();
			auto duration = duration_cast<milliseconds>(end - start);

			std::cout << duration.count() * 0.001 << "s" << endl;
		}
	}
	std::cin.get();
}

