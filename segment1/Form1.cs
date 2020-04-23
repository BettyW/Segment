using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PanGu;

using JiebaNet.Segmenter;


namespace segment1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //初始化库
            //Segment.Init();

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            //string str = "盘古分词demo2";
            //Segment segment = new Segment();
            //ICollection<WordInfo> words = segment.DoSegment(str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Original = txt_Original.Text;// "盘古分词demo2";
            Segment segment = new Segment();
            ICollection<WordInfo> words = segment.DoSegment(Original);
            string strs = "";
            foreach (WordInfo v in words)
            {
                strs += v.ToString() + "\r\n";
            }
            txt_Result.Text = strs;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Original = txt_Original.Text;//用户输入的文字段
            string strs = "";//分词结果


            var segmenter = new JiebaSegmenter();

            if(radioButton1.Checked == true)
            {
                var segments = segmenter.Cut(Original, cutAll: true);//我来到北京清华大学
                //Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));
                strs = "【全模式】：" + string.Join("/ ", segments);
            }
            


            // 默认为精确模式
            if (radioButton2.Checked == true)
            {
                var segments = segmenter.Cut(Original);      //我来到北京清华大学
                //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));
                strs = "【精确模式】：" + string.Join("/ ", segments);
            }
        

            // 默认为精确模式，同时也使用HMM模型
            if (radioButton3.Checked == true)
            {
                var segments = segmenter.Cut(Original);//他来到了网易杭研大厦  
                //Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));
                strs = "【新词识别】：" + string.Join("/ ", segments);
            }
          

            // 搜索引擎模式
            if (radioButton4.Checked == true)
            {
                var segments = segmenter.CutForSearch(Original); //小明硕士毕业于中国科学院计算所，后在日本京都大学深造
                                                                 //Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

                //匹配这些中文标点符号 。 ？ ！ ， 、 ； ： “ ” ‘ ' （ ） 《 》 〈 〉 【 】 『 』 「 」 ﹃ ﹄ 〔 〕 … — ～ ﹏ ￥
                var reg = "/[\u3002|\uff1f|\uff01|\uff0c|\u3001|\uff1b|\uff1a|\u201c|\u201d|\u2018|\u2019|\uff08|\uff09|\u300a|\u300b|\u3008|\u3009|\u3010|\u3011|\u300e|\u300f|\u300c|\u300d|\ufe43|\ufe44|\u3014|\u3015|\u2026|\u2014|\uff5e|\ufe4f|\uffe5] /";
                 
            
                for(var i = 0; i < segments.ToArray().Count(); i++)
                {

                }
                strs = "【搜索引擎模式】：" + string.Join("/ ", segments);
                
            }
           


            // 歧义消除
            if (radioButton5.Checked == true)
            {
                var segments = segmenter.Cut(Original);//结过婚的和尚未结过婚的
                //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));
                strs = "【歧义消除】：" + string.Join("/ ", segments);
            }


            txt_Result.Text = strs;
        }
    }
}
