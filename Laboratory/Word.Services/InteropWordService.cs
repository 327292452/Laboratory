using MSWord = Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Word.Services
{
    public class InteropWordService
    {
        private object path = Environment.CurrentDirectory + "\\MyWord_Print.doc";

        object unite = MSWord.WdUnits.wdStory;

        MSWord.Application wordApp;
        MSWord.Document wordDoc;
        string strContent;
        string Tag;
        Dictionary<string, string> dic = new Dictionary<string, string>();

        object Nothing = Missing.Value;
        public InteropWordService()
        {
            dic.Add("名称", "");
            dic.Add("类型", "");
            dic.Add("是否必填", "");
            dic.Add("备注", "");
        }

        public void GetWord(List<ApiModuleDTO> apiModules)
        {
            wordApp = new MSWord.ApplicationClass();
            //wordApp.Visible = true;

            Dictionary<string, DTOModule> dicDTO = new Dictionary<string, DTOModule>();
            try
            {
                if (apiModules == null || !apiModules.Any()) throw new Exception("数据为null，不能生成文档！");

                if (File.Exists(path.ToString()))
                {
                    File.Delete(path.ToString());
                }

                wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;
                wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;

                apiModules.ForEach(f =>
                {
                    //if (f.ApiUrl.Equals("/api/DetectTypes"))
                    //{
                    //}
                    if (f.ComponentSubs != null && !dicDTO.ContainsKey(f.ComponentSubCode))
                    {
                        dicDTO.Add(f.ComponentSubCode, f.ComponentSubs);
                    }
                    OpetionWord(f);

                });
                OpetionWord_Appendix(dicDTO);


                object format = MSWord.WdSaveFormat.wdFormatDocument;

                wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (wordDoc != null) wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                if (wordApp != null) wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            }


        }
        private void OpetionWord(ApiModuleDTO apiModule)
        {
            try
            {


                #region 


                //写入普通文本
                string t1 = "标题 1";
                string t2 = "标题 2";
                string t3 = "标题 3";
                string body = "正文";
                strContent = "标题一\n";

                if (!apiModule.ApiTag.Equals(Tag))
                {
                    Tag = apiModule.ApiTag;
                    wordDoc.Paragraphs.Last.Range.set_Style(t1);
                    wordDoc.Paragraphs.Last.Range.Text = apiModule.ApiTagDec + "\n";
                }


                wordDoc.Paragraphs.Last.Range.set_Style(t2);
                wordDoc.Paragraphs.Last.Range.Text = apiModule.ApiName + "\n";

                #endregion


                #region 

                wordDoc.Paragraphs.Last.Range.set_Style(body);
                int tableColumn = 4;
                int tableRow = 7;

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

                table.Cell(2, 1).Range.Text = dic.Keys.ToList()[0];
                table.Cell(3, 1).Range.Text = dic.Keys.ToList()[1];
                table.Cell(4, 1).Range.Text = dic.Keys.ToList()[2];
                table.Cell(5, 1).Range.Text = dic.Keys.ToList()[3];

                table.Cell(2, 2).Range.Text = apiModule.ApiUrl;
                table.Cell(3, 2).Range.Text = apiModule.DataFormat;
                table.Cell(4, 2).Range.Text = apiModule.ApiWay;
                table.Cell(5, 2).Range.Text = apiModule.ApiExplain;

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
                iniNum = tableRow;

                var addCount = apiModule.ApiParame.Count;
                if (addCount > 0)
                {
                    iniNum += 1;
                }

                for (int i = 0; i < apiModule.ApiParame.Count; i++)
                {
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;
                    table.Rows[iniNum + i].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(iniNum + i, 1).Range.Text = apiModule.ApiParame[i].Name;
                    table.Cell(iniNum + i, 2).Range.Text = apiModule.ApiParame[i].Schema.type;
                    table.Cell(iniNum + i, 3).Range.Text = apiModule.ApiParame[i].Schema.nullable ? "必填" : string.Empty;
                    table.Cell(iniNum + i, 4).Range.Text = apiModule.ApiParame[i].Schema.description;
                }
                iniNum = tableRow + addCount;
                if (apiModule.IsRequestBody)
                {
                    iniNum = iniNum + 1;
                }

                var paramNum = 0;
                if (apiModule.IsRequestBody)
                {
                    paramNum = iniNum;
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(iniNum, 1).Range.Text = apiModule.ApiParameFormet;
                }

                iniNum = iniNum + 1;
                var resultTagNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                table.Cell(iniNum, 1).Range.Text = "输出参数";

                iniNum = iniNum + 1;
                var resultNum = iniNum;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                table.Rows[iniNum].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(iniNum, 1).Range.Text = dic.Keys.ToList()[0];
                table.Cell(iniNum, 2).Range.Text = dic.Keys.ToList()[1];
                table.Cell(iniNum, 3).Range.Text = dic.Keys.ToList()[3];


                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                var addResultValueCount = apiModule.Components != null ? apiModule.Components.properties.Keys.Count : 0;


                iniNum = iniNum + 1;

                for (int i = 0; i < addResultValueCount; i++)
                {
                    var key = apiModule.Components.properties.ToList()[i].Key;
                    var format = string.IsNullOrEmpty(apiModule.Components.properties[key].format) ? apiModule.Components.properties[key].type : apiModule.Components.properties[key].format;
                    var desc = apiModule.Components.properties[key].items != null ? "见附录：" + apiModule.ComponentSubDescription + string.Format("（{0}）", apiModule.Components.properties[key].items.RefPath.Substring(apiModule.Components.properties[key].items.RefPath.LastIndexOf("/") + 1)) : apiModule.Components.properties[key].description;
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;
                    table.Rows[iniNum + i].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(iniNum + i, 1).Range.Text = key;
                    table.Cell(iniNum + i, 2).Range.Text = format;
                    table.Cell(iniNum + i, 3).Range.Text = desc;
                    table.Cell(iniNum + i, 3).Merge(table.Cell(iniNum + i, 4));
                }

                if (paramNum > 0) table.Cell(paramNum, 1).Merge(table.Cell(paramNum, 4));
                table.Cell(resultTagNum, 1).Merge(table.Cell(resultTagNum, 4));
                table.Cell(resultTagNum, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Cell(resultNum, 3).Merge(table.Cell(resultNum, 4));

                iniNum = iniNum + addResultValueCount;
                table.Cell(iniNum, 2).Merge(table.Cell(iniNum, 4));
                table.Cell(iniNum, 1).Range.Text = apiModule.ResultCode.Keys.ToList().Count > 0 ? apiModule.ResultCode.Keys.ToList()[0] : string.Empty;
                table.Cell(iniNum, 2).Range.Text = apiModule.ResultCode.Keys.ToList().Count > 0 ? apiModule.ResultCode.Values.ToList()[0].description : string.Empty;
                table.Rows.Add(ref Nothing);
                table.Rows[iniNum].Height = 17;
                iniNum = iniNum + 1;
                table.Cell(iniNum, 1).Merge(table.Cell(iniNum, 2));
                table.Cell(iniNum, 1).Range.Text = apiModule.ResultCode.Values.ToList().Count > 0 ? apiModule.ResultCode.Values.ToList()[0].content : string.Empty;

                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));

                table.Range.Font.Size = 10.5F;
                table.Range.Font.Bold = 0;
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                table.Rows[1].Range.Font.Bold = 1;//加粗
                table.Rows[1].Range.Font.Size = 12F;
                table.Cell(1, 1).Range.Font.Size = 10.5F;
                wordApp.Selection.Cells.Height = 30;//所有单元格的高度

                wordApp.Selection.EndKey(ref unite, ref Nothing);
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void OpetionWord_Appendix(Dictionary<string, DTOModule> dtoModules)
        {
            string t1 = "标题 1";
            string t2 = "标题 2";
            string t3 = "标题 3";
            string body = "正文";

            wordDoc.Paragraphs.Last.Range.set_Style(t1);
            wordDoc.Paragraphs.Last.Range.Text = "附录\n";


            dtoModules.Keys.ToList().ForEach(f =>
            {
                var dto = dtoModules[f];
                int iniNum = 0;
                wordDoc.Paragraphs.Last.Range.set_Style(t2);
                wordDoc.Paragraphs.Last.Range.Text = dto.description + f + "\n";
                var dtoCount = dto.properties.Keys.Count();

                wordApp.Selection.EndKey(ref unite, ref Nothing);

                wordDoc.Paragraphs.Last.Range.set_Style(body);
                int tableColumn = 4;
                int tableRow = 1;
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
                tableRow, tableColumn, ref Nothing, ref Nothing);

                table.Borders.Enable = 1;

                table.Columns[1].Width = 120;
                table.Columns[2].Width = 50;
                table.Columns[3].Width = 70;
                table.Columns[4].Width = 150;

                iniNum = tableRow;
                table.Rows[iniNum].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(iniNum, 1).Range.Text = dic.Keys.ToList()[0];
                table.Cell(iniNum, 2).Range.Text = dic.Keys.ToList()[1];
                table.Cell(iniNum, 3).Range.Text = dic.Keys.ToList()[3];
                table.Cell(iniNum, 3).Merge(table.Cell(iniNum, 4));

                iniNum += 1;
                for (int i = 0; i < dtoCount; i++)
                {
                    var key = dto.properties.ToList()[i].Key;
                    var format = string.IsNullOrEmpty(dto.properties[key].format) ? dto.properties[key].type : dto.properties[key].format;
                    var desc = dto.properties[key].description;
                    table.Rows.Add(ref Nothing);
                    table.Rows[iniNum + i].Height = 17;
                    table.Rows[iniNum + i].Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    table.Cell(iniNum + i, 1).Range.Text = key;
                    table.Cell(iniNum + i, 2).Range.Text = format;
                    table.Cell(iniNum + i, 3).Range.Text = desc;
                }

                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));

                table.Range.Font.Size = 10.5F;
                table.Range.Font.Bold = 0;
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                table.Rows[1].Range.Font.Bold = 1;//加粗
                table.Rows[1].Range.Font.Size = 12F;
                table.Cell(1, 1).Range.Font.Size = 10.5F;
                wordApp.Selection.Cells.Height = 30;//所有单元格的高度

                wordApp.Selection.EndKey(ref unite, ref Nothing);
            });

        }
    }
    public class ApiModuleDTO : ApiBaseDTO
    {
        /// <summary>
        /// 接口参数
        /// </summary>
        public List<Parameter> ApiParame { get; set; }
        /// <summary>
        /// 接口返回结果说明
        /// </summary>
        public List<Parameter> ApiResultExplain { get; set; }

        public DTOModule Components { get; set; }

        public DTOModule ComponentSubs { get; set; }

        public string ComponentSubDescription { get; set; }

        public string ComponentSubCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ApiRemark { get; set; }
        public string RefPath { get; set; }

        public Dictionary<string, ResultForme> ResultCode { get; set; }
    }

    public class ApiBaseDTO
    {
        /// <summary>
        /// 接口标记
        /// </summary>
        public string ApiTag { get; set; }
        /// <summary>
        /// 接口标记说明
        /// </summary>
        public string ApiTagDec { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ApiName { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public string DataFormat { get; set; }
        /// <summary>
        /// 接口说明
        /// </summary>
        public string ApiExplain { get; set; }

        /// <summary>
        /// 接口请求方式
        /// </summary>
        public string ApiWay { get; set; }
        /// <summary>
        /// 接口返回结果
        /// </summary>
        public string ApiResult { get; set; }
        /// <summary>
        /// 参数子项
        /// </summary>
        public string ApiSubParame { get; set; }
        /// <summary>
        /// 参数子项
        /// </summary>
        public string ApiParameFormet { get; set; }
        /// <summary>
        /// 返回值结构
        /// </summary>
        public string ApiResultFormet { get; set; }
        public bool IsRequestBody { get; set; }
    }

    public class DTOModule
    {
        public string type { get; set; }
        public Dictionary<string, AttrModule> properties { get; set; }
        public bool additionalProperties { get; set; }
        public string description { get; set; }
        public string[] required { get; set; }

    }

    public class AttrModule
    {
        public string type { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public bool nullable { get; set; }
        public ContentSchema items { get; set; }
    }
    public class ContentSchema : ItemRefPath
    {
        public string Type { get; set; }
        public ItemRefPath Items { get; set; }
    }
    public class ItemRefPath
    {
        public string RefPath { get; set; }
    }
    public class Parameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public ParameterSchema Schema { get; set; }
    }
    public class ParameterSchema
    {
        public string type { get; set; }
        public string description { get; set; }
        public bool nullable { get; set; }
        public string format { get; set; }
    }

    public class ResultForme
    {
        public string description { get; set; }
        public string content { get; set; }
    }
}
