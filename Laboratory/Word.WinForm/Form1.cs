using MSWord = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Word.WinForm
{
    public partial class Form1 : Form
    {
        private object path = Environment.CurrentDirectory + "\\MyWord_Print.doc";

        object unite = MSWord.WdUnits.wdStory;

        MSWord.Application wordApp;
        MSWord.Document wordDoc;
        string strContent;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void butDone_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            GetWord();
            //GetWord(txtPath.Text);
            ((Button)sender).Enabled = true;
        }

        private void GetWord()
        {
            wordApp = new MSWord.ApplicationClass();
            //wordApp.Visible = true;

            try
            {

                if (File.Exists(path.ToString()))
                {
                    File.Delete(path.ToString());
                }
                object Nothing = Missing.Value;

                wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                #region 

                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;
                wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;

                //写入普通文本
                string t1 = "标题 1";
                string t2 = "标题 2";
                string t3 = "标题 3";
                string body = "正文";
                strContent = "标题一\n";
                wordDoc.Paragraphs.Last.Range.set_Style(t1);
                wordDoc.Paragraphs.Last.Range.Text = strContent;


                wordDoc.Paragraphs.Last.Range.set_Style(t2);
                wordDoc.Paragraphs.Last.Range.Text = "标题二\n";

                wordDoc.Paragraphs.Last.Range.set_Style(body);
                wordDoc.Paragraphs.Last.Range.Text = "正文\n";

                #endregion


                #region 

                int tableColumn = 4;
                int tableRow = 7;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("名称", "");
                dic.Add("类型", "");
                dic.Add("是否必填", "");
                dic.Add("备注", "");

                wordApp.Selection.EndKey(ref unite, ref Nothing);
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
                tableRow, tableColumn, ref Nothing, ref Nothing);

                table.Borders.Enable = 1;

                table.Columns[1].Width = 120;
                table.Columns[2].Width = 50;
                table.Columns[3].Width = 70;
                table.Columns[4].Width = 150;
                table.Cell(1, 1).Merge(table.Cell(1, 4));
                table.Cell(2, 2).Merge(table.Cell(2, 4));
                table.Cell(3, 2).Merge(table.Cell(3, 4));
                table.Cell(4, 2).Merge(table.Cell(4, 4));
                table.Cell(5, 2).Merge(table.Cell(5, 4));
                table.Cell(1, 1).Range.Text = "接口信息";

                table.Cell(2, 1).Range.Text = "URL";
                table.Cell(3, 1).Range.Text = "数据格式";
                table.Cell(4, 1).Range.Text = "请求方式";
                table.Cell(5, 1).Range.Text = "备注";

                table.Cell(6, 1).Merge(table.Cell(6, 4));
                table.Cell(6, 1).Range.Text = "输入参数";

                int col = 1;
                int iniNum = 0;
                foreach (var item in dic.Keys)
                {
                    table.Cell(7, col).Range.Text = item;

                    table.Cell(7, col).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                    col++;
                }

                var addCount = 4;
                iniNum = tableRow + 1;
                for (int i = 0; i < addCount; i++)
                {
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;
                    table.Cell(iniNum + i, 1).Range.Text = "";
                    table.Cell(iniNum + i, 2).Range.Text = "";
                    table.Cell(iniNum + i, 3).Range.Text = "";
                    table.Cell(iniNum + i, 4).Range.Text = "";
                }

                iniNum = tableRow + addCount + 1;
                var paramNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Cell(iniNum, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

                iniNum = iniNum + 1;
                var resultTagNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                table.Cell(iniNum, 1).Range.Text = "输出参数";

                iniNum = iniNum + 1;
                var resultNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                table.Cell(iniNum, 1).Range.Text = dic.Keys.ToList()[0];
                table.Cell(iniNum, 2).Range.Text = dic.Keys.ToList()[1];
                table.Cell(iniNum, 3).Range.Text = dic.Keys.ToList()[3];


                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                var addResultValueCount = 4;
                iniNum = iniNum + 1;
                for (int i = 0; i < addResultValueCount; i++)
                {
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;
                    table.Cell(iniNum + i, 1).Range.Text = "";
                    table.Cell(iniNum + i, 2).Range.Text = "";
                    table.Cell(iniNum + i, 3).Range.Text = "";
                    table.Cell(iniNum + i, 3).Merge(table.Cell(iniNum + i, 4));
                }

                table.Cell(paramNum, 1).Merge(table.Cell(paramNum, 4));
                table.Cell(resultTagNum, 1).Merge(table.Cell(resultTagNum, 4));
                table.Cell(resultTagNum, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Cell(resultNum, 3).Merge(table.Cell(resultNum, 4));

                iniNum = iniNum + addResultValueCount;
                table.Cell(iniNum, 2).Merge(table.Cell(iniNum, 4));
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                iniNum = iniNum + 1;
                table.Cell(iniNum, 1).Merge(table.Cell(iniNum, 2));

                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));

                table.Range.Font.Size = 10.5F;
                table.Range.Font.Bold = 0;
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                table.Rows[1].Range.Font.Bold = 1;//加粗
                table.Rows[1].Range.Font.Size = 12F;
                table.Cell(1, 1).Range.Font.Size = 10.5F;
                wordApp.Selection.Cells.Height = 30;//所有单元格的高度

                #endregion

                wordApp.Selection.EndKey(ref unite, ref Nothing);

                object format = MSWord.WdSaveFormat.wdFormatDocument;

                wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void GetWordModuel()
        {
            path = Environment.CurrentDirectory + "\\MyWord_Print.doc";
            wordApp = new MSWord.ApplicationClass();
            //wordApp.Visible = true;

            try
            {

                if (File.Exists(path.ToString()))
                {
                    File.Delete(path.ToString());
                }
                object Nothing = Missing.Value;

                wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);


                #region 页面设置、页眉图片和文字设置，最后跳出页眉设置

                //页面设置
                //wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
                //wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientPortrait;//排列方式为垂直方向
                //wordDoc.PageSetup.TopMargin = 57.0f;
                //wordDoc.PageSetup.BottomMargin = 57.0f;
                //wordDoc.PageSetup.LeftMargin = 57.0f;
                //wordDoc.PageSetup.RightMargin = 57.0f;
                //wordDoc.PageSetup.HeaderDistance = 30.0f;//页眉位置

                //设置页眉
                //wordApp.ActiveWindow.View.Type = MSWord.WdViewType.wdNormalView;//普通视图（即页面视图）样式
                //wordApp.ActiveWindow.View.SeekView = MSWord.WdSeekView.wdSeekPrimaryHeader;//进入页眉设置，其中页眉边距在页面设置中已完成
                //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;//页眉中的文字右对齐


                //插入页眉图片(测试结果图片未插入成功)
                //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                //string headerfile = @"C:\Users\xiahui\Desktop\OficeProgram\3.jpg";
                //MSWord.InlineShape shape1 = wordApp.ActiveWindow.ActivePane.Selection.InlineShapes.AddPicture(headerfile, ref Nothing, ref Nothing, ref Nothing);
                //shape1.Height = 5;//强行设置貌似无效，图片没有按设置的缩放——图片的比例并没有改变。
                //shape1.Width = 20;
                //wordApp.ActiveWindow.ActivePane.Selection.InsertAfter("  文档页眉");//在页眉的图片后面追加几个字

                //去掉页眉的横线
                //wordApp.ActiveWindow.ActivePane.Selection.ParagraphFormat.Borders[MSWord.WdBorderType.wdBorderBottom].LineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                //wordApp.ActiveWindow.ActivePane.Selection.Borders[MSWord.WdBorderType.wdBorderBottom].Visible = false;
                //wordApp.ActiveWindow.ActivePane.View.SeekView = MSWord.WdSeekView.wdSeekMainDocument;//退出页眉设置
                #endregion

                #region 页码设置并添加页码

                //为当前页添加页码
                //MSWord.PageNumbers pns = wordApp.Selection.Sections[1].Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
                //pns.NumberStyle = MSWord.WdPageNumberStyle.wdPageNumberStyleNumberInDash;//设置页码的风格，是Dash形还是圆形的
                //pns.HeadingLevelForChapter = 0;
                //pns.IncludeChapterNumber = false;
                //pns.RestartNumberingAtSection = false;
                //pns.StartingNumber = 0; //开始页页码？
                //object pagenmbetal = MSWord.WdPageNumberAlignment.wdAlignPageNumberCenter;//将号码设置在中间
                //object first = true;
                //wordApp.Selection.Sections[1].Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);

                #endregion

                #region 行间距与缩进、文本字体、字号、加粗、斜体、颜色、下划线、下划线颜色设置

                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
                wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度

                //写入普通文本
                string t1 = "标题 1";
                string t2 = "标题 2";
                string t3 = "标题 3";
                string body = "正文";
                strContent = "标题一\n";
                wordDoc.Paragraphs.Last.Range.set_Style(t1);
                wordDoc.Paragraphs.Last.Range.Text = strContent;


                wordDoc.Paragraphs.Last.Range.set_Style(t2);
                wordDoc.Paragraphs.Last.Range.Text = "标题二\n";

                //wordDoc.Background = Shape;
                //直接添加段，不是覆盖( += )

                wordDoc.Paragraphs.Last.Range.set_Style(body);
                wordDoc.Paragraphs.Last.Range.Text = "正文\n";


                //添加在此段的文字后面，不是新段落
                //wordDoc.Paragraphs.Last.Range.InsertAfter("这是后面的内容\n");

                //将文档的前4个字替换成"哥是替换文字"，并将其颜色设为红色
                //object start = 0;
                //object end = 4;
                //MSWord.Range rang = wordDoc.Range(ref start, ref end);
                //rang.Font.Color = MSWord.WdColor.wdColorRed;
                //rang.Text = "哥是替换文字";
                //wordDoc.Range(ref start, ref end);

                //写入黑体文本
                object unite = MSWord.WdUnits.wdStory;
                //wordApp.Selection.EndKey(ref unite, ref Nothing);//将光标移到文本末尾
                //wordApp.Selection.ParagraphFormat.FirstLineIndent = 0;//取消首行缩进的长度
                //strContent = "这是黑体文本\n";
                //wordDoc.Paragraphs.Last.Range.Font.Name = "黑体";
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入加粗文本
                //strContent = "这是粗体文本\n"; //
                //wordApp.Selection.EndKey(ref unite, ref Nothing);//这一句不加，有时候好像也不出问题，不过还是加了安全
                //wordDoc.Paragraphs.Last.Range.Font.Bold = 1;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入15号字体文本
                //strContent = "我这个文本的字号是15号，而且是宋体\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Size = 15;
                //wordDoc.Paragraphs.Last.Range.Font.Name = "宋体";
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入斜体文本
                //strContent = "我是斜体字文本\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Italic = 1;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入蓝色文本
                //strContent = "我是蓝色的文本\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Color = MSWord.WdColor.wdColorBlue;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入下划线文本
                //strContent = "我是下划线文本\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineThick;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //写入红色下画线文本
                //strContent = "我是点线下划线，并且下划线是红色的\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineDottedHeavy;
                //wordDoc.Paragraphs.Last.Range.Font.UnderlineColor = MSWord.WdColor.wdColorRed;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;

                //取消下划线，并且将字号调整为12号
                //strContent = "我他妈不要下划线了，并且设置字号为12号，黑色不要斜体\n";
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordDoc.Paragraphs.Last.Range.Font.Size = 12;
                //wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineNone;
                //wordDoc.Paragraphs.Last.Range.Font.Color = MSWord.WdColor.wdColorBlack;
                //wordDoc.Paragraphs.Last.Range.Font.Italic = 0;
                //wordDoc.Paragraphs.Last.Range.Text = strContent;


                #endregion


                #region 插入图片、居中显示，设置图片的绝对尺寸和缩放尺寸，并给图片添加标题

                //wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                ////图片文件的路径
                //string filename = Environment.CurrentDirectory + "\\6.jpg";
                ////要向Word文档中插入图片的位置
                //Object range = wordDoc.Paragraphs.Last.Range;
                ////定义该插入的图片是否为外部链接
                //Object linkToFile = false;               //默认,这里貌似设置为bool类型更清晰一些
                ////定义要插入的图片是否随Word文档一起保存
                //Object saveWithDocument = true;              //默认
                ////使用InlineShapes.AddPicture方法(【即“嵌入型”】)插入图片
                //wordDoc.InlineShapes.AddPicture(filename, ref linkToFile, ref saveWithDocument, ref range);
                //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//居中显示图片

                ////设置图片宽高的绝对大小

                ////wordDoc.InlineShapes[1].Width = 200;
                ////wordDoc.InlineShapes[1].Height = 150;
                ////按比例缩放大小

                //wordDoc.InlineShapes[1].ScaleWidth = 20;//缩小到20% ？
                //wordDoc.InlineShapes[1].ScaleHeight = 20;

                ////在图下方居中添加图片标题

                //wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                //wordApp.Selection.EndKey(ref unite, ref Nothing);
                //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                //wordApp.Selection.Font.Size = 10;//字体大小
                //wordApp.Selection.TypeText("图1 测试图片\n");

                //#endregion

                //#region 添加表格、填充数据、设置表格行列宽高、合并单元格、添加表头斜线、给单元格添加图片
                //wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                //wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                ////object WdLine2 = MSWord.WdUnits.wdLine;//换一行;  
                ////wordApp.Selection.MoveDown(ref WdLine2, 6, ref Nothing);//向下跨15行输入表格，这样表格就在文字下方了，不过这是非主流的方法

                ////设置表格的行数和列数
                int tableColumn = 4;
                int tableRow = 7;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("名称", "");
                dic.Add("类型", "");
                dic.Add("是否必填", "");
                dic.Add("备注", "");
                ////定义一个Word中的表格对象
                wordApp.Selection.EndKey(ref unite, ref Nothing);//将光标移到文本末尾
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
                tableRow, tableColumn, ref Nothing, ref Nothing);

                ////默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
                table.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等

                ////表格的索引是从1开始的。
                //wordDoc.Tables[1].Cell(1, 1).Range.Text = "列\n行";

                table.Columns[1].Width = 120;
                table.Columns[2].Width = 50;
                table.Columns[3].Width = 70;
                table.Columns[4].Width = 150;
                table.Cell(1, 1).Merge(table.Cell(1, 4));
                table.Cell(2, 2).Merge(table.Cell(2, 4));
                table.Cell(3, 2).Merge(table.Cell(3, 4));
                table.Cell(4, 2).Merge(table.Cell(4, 4));
                table.Cell(5, 2).Merge(table.Cell(5, 4));
                table.Cell(1, 1).Range.Text = "接口信息";

                table.Cell(2, 1).Range.Text = "URL";
                table.Cell(3, 1).Range.Text = "数据格式";
                table.Cell(4, 1).Range.Text = "请求方式";
                table.Cell(5, 1).Range.Text = "备注";

                table.Cell(6, 1).Merge(table.Cell(6, 4));
                table.Cell(6, 1).Range.Text = "输入参数";

                int col = 1;
                int iniNum = 0;
                foreach (var item in dic.Keys)
                {
                    table.Cell(7, col).Range.Text = item;

                    table.Cell(7, col).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                    col++;
                }


                //for (int i = 1; i < tableRow; i++)
                //{
                //    for (int j = 1; j < tableColumn; j++)
                //    {
                //        if (i == 1)
                //        {
                //            table.Cell(i, j + 1).Range.Text = "Column " + j;//填充每列的标题
                //        }
                //        if (j == 1)
                //        {
                //            table.Cell(i + 1, j).Range.Text = "Row " + i; //填充每行的标题
                //        }
                //        table.Cell(i + 1, j + 1).Range.Text = i + "行 " + j + "列";  //填充表格的各个小格子
                //    }
                //}


                //添加行
                var addCount = 4;
                iniNum = tableRow + 1;
                for (int i = 0; i < addCount; i++)
                {
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;//设置新增加的这行表格的高度
                    table.Cell(iniNum + i, 1).Range.Text = "";
                    table.Cell(iniNum + i, 2).Range.Text = "";
                    table.Cell(iniNum + i, 3).Range.Text = "";
                    table.Cell(iniNum + i, 4).Range.Text = "";
                }

                iniNum = tableRow + addCount + 1;
                var paramNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Cell(iniNum, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

                iniNum = iniNum + 1;
                var resultTagNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;//设置新增加的这行表格的高度
                table.Cell(iniNum, 1).Range.Text = "输出参数";

                iniNum = iniNum + 1;
                var resultNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;//设置新增加的这行表格的高度
                table.Cell(iniNum, 1).Range.Text = dic.Keys.ToList()[0];
                table.Cell(iniNum, 2).Range.Text = dic.Keys.ToList()[1];
                table.Cell(iniNum, 3).Range.Text = dic.Keys.ToList()[3];


                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;//设置新增加的这行表格的高度
                var addResultValueCount = 4;
                iniNum = iniNum + 1;
                for (int i = 0; i < addResultValueCount; i++)
                {
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;//设置新增加的这行表格的高度
                    table.Cell(iniNum + i, 1).Range.Text = "";
                    table.Cell(iniNum + i, 2).Range.Text = "";
                    table.Cell(iniNum + i, 3).Range.Text = "";
                    table.Cell(iniNum + i, 3).Merge(table.Cell(iniNum + i, 4));
                }

                table.Cell(paramNum, 1).Merge(table.Cell(paramNum, 4));
                table.Cell(resultTagNum, 1).Merge(table.Cell(resultTagNum, 4));
                table.Cell(resultTagNum, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Cell(resultNum, 3).Merge(table.Cell(resultNum, 4));

                iniNum = iniNum + addResultValueCount;
                table.Cell(iniNum, 2).Merge(table.Cell(iniNum, 4));
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;//设置新增加的这行表格的高度
                iniNum = iniNum + 1;
                table.Cell(iniNum, 1).Merge(table.Cell(iniNum, 2));


                //向新添加的行的单元格中添加图片
                //string FileName = Environment.CurrentDirectory + "\\6.jpg";//图片所在路径
                //object LinkToFile = false;
                //object SaveWithDocument = true;
                //object Anchor = table.Cell(tableRow + 1, tableColumn).Range;//选中要添加图片的单元格
                //wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);

                //由于是本文档的第2张图，所以这里是InlineShapes[2]
                //wordDoc.Application.ActiveDocument.InlineShapes[2].Width = 50;//图片宽度
                //wordDoc.Application.ActiveDocument.InlineShapes[2].Height = 35;//图片高度

                // 将图片设置为四周环绕型
                //MSWord.Shape s = wordDoc.Application.ActiveDocument.InlineShapes[2].ConvertToShape();
                //s.WrapFormat.Type = MSWord.WdWrapType.wdWrapSquare;


                //设置table样式
                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

                table.Range.Font.Size = 10.5F;
                table.Range.Font.Bold = 0;

                //table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本居中
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//文本垂直贴到底部

                //设置table边框样式
                //table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;//表格外框是双线
                //table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

                table.Rows[1].Range.Font.Bold = 1;//加粗
                table.Rows[1].Range.Font.Size = 12F;
                table.Cell(1, 1).Range.Font.Size = 10.5F;
                wordApp.Selection.Cells.Height = 30;//所有单元格的高度

                //除第一行外，其他行的行高都设置为20
                //for (int i = 2; i <= tableRow; i++)
                //{
                //    table.Rows[i].Height = 20;
                //}

                //将表格左上角的单元格里的文字（“行” 和 “列”）居右
                //table.Cell(1, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;
                //将表格左上角的单元格里面下面的“列”字移到左边，相比上一行就是将ParagraphFormat改成了Paragraphs[2].Format
                //table.Cell(1, 1).Range.Paragraphs[2].Format.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

                //table.Columns[1].Width = 50;//将第 1列宽度设置为50

                //将其他列的宽度都设置为75
                //for (int i = 2; i <= tableColumn; i++)
                //{
                //    table.Columns[i].Width = 75;
                //}


                //添加表头斜线,并设置表头的样式
                //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].Visible = true;
                //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].Color = MSWord.WdColor.wdColorRed;
                //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].LineWidth = MSWord.WdLineWidth.wdLineWidth150pt;

                //合并单元格
                //table.Cell(4, 4).Merge(table.Cell(4, 5));//横向合并

                //table.Cell(2, 3).Merge(table.Cell(4, 3));//纵向合并，合并(2,3),(3,3),(4,3)

                #endregion

                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾

                //wordDoc.Content.InsertAfter("\n");
                //wordDoc.Content.InsertAfter("就写这么多，算了吧！2016.09.27");



                //WdSaveFormat为Word 2003文档的保存格式
                object format = MSWord.WdSaveFormat.wdFormatDocument;// office 2007就是wdFormatDocumentDefault
                                                                     //将wordDoc文档对象的内容保存为DOCX文档
                wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                //关闭wordDoc文档对象

                //看是不是要打印
                //wordDoc.PrintOut();



                wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                //关闭wordApp组件对象
                wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
                //Console.WriteLine(path + " 创建完毕！");
                //Console.ReadKey();


                //我还要打开这个文档玩玩
                //MSWord.Application app = new MSWord.Application();
                //MSWord.Document doc = null;
                //try
                //{

                //    object unknow = Type.Missing;
                //    app.Visible = true;
                //    string str = Environment.CurrentDirectory + "\\MyWord_Print.doc";
                //    object file = str;
                //    doc = app.Documents.Open(ref file,
                //        ref unknow, ref unknow, ref unknow, ref unknow,
                //        ref unknow, ref unknow, ref unknow, ref unknow,
                //        ref unknow, ref unknow, ref unknow, ref unknow,
                //        ref unknow, ref unknow, ref unknow);
                //    string temp = doc.Paragraphs[1].Range.Text.Trim();
                //    Console.WriteLine("你他妈输出temp干嘛？");
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                //wordDoc = doc;
                //wordDoc.Paragraphs.Last.Range.Text += "我真的不打算再写了,就写这么多吧";

                //Console.ReadKey();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void GetWord(string Path)
        {

            MSWord.Application app = new MSWord.Application();
            MSWord.Document doc = null;
            try
            {

                object unknow = Type.Missing;
                app.Visible = true;
                string str = Environment.CurrentDirectory + "\\70b2216f105f4f15bbc78aeba0ecfc2e.docx"; //Environment.CurrentDirectory + "\\MyWord_Print.doc";
                object file = str;
                doc = app.Documents.Open(ref file,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow);
                string temp = doc.Paragraphs[1].Range.Text.Trim();
                Console.WriteLine("你他妈输出temp干嘛？");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            wordDoc = doc;
            wordDoc.Paragraphs.Last.Range.Text += "我真的不打算再写了,就写这么多吧";
        }

    }
}
