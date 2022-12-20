
#### 3. 使用示例
```
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            string modelPath = null;
            //参数
            MattingParameter parameter = new MattingParameter();
            //初始化引擎
            PaddleSegMattingEngine engine = new PaddleSegMattingEngine();
            engine.Init(modelPath, parameter);
            //设置背景颜色
            engine.Setbackground(45, 145, 255);
            //分割后的文件
            string outfile =  "C:\\" + Guid.NewGuid().ToString() + ".bmp";
            engine.Seg(ofd.FileName, outfile);

```