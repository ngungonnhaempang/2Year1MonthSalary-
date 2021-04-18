using System;
using System.Data;
//using System.Drawing;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Shawoo.Common;
using log4net;

namespace FEPVWebApiOwinHost.QC
{

        public class ChartCreater
        {
            protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
            NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
            public DataTable GetChartDataRecent(string sampleName, string lot_no, string line, string propertyName, DateTime e)
            {
                DataTable dt = gate.ExecuteStoredProcedure("i_GetChartDataRecent", new string[] { "SampleName", "LOT_NO", "LINE", "PropertyName", "DateE" }, new object[] { sampleName, lot_no, line, propertyName, e }).Tables[0];

                dt.Columns.Add("Order");
                int dtCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Order"] = (dtCount - i).ToString();
                }
                return dt;
                //return db.ExecuteDataSet(cmd).Tables[0];
            }

            public DataTable GetChartDataNormal(string sampleName, string lot_no, string line, string propertyName, DateTime b, DateTime e)
            {

                DataTable dt = gate.ExecuteStoredProcedure("i_GetChartDataNormal", new string[] { "SampleName", "LOT_NO", "LINE", "PropertyName", "DateB", "DateE" }, new object[] { sampleName, lot_no, line, propertyName, b, e }).Tables[0];
                dt.Columns.Add("Order");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Order"] = (i + 1).ToString();
                }
                return dt;
            }

            #region 私有变量
            //ChartServers dal = new ChartServers();

            decimal mUSL, mLSL,
                    mUCL, mLCL,
                    mLUSL, mLLSL,
                    mX1, mX2,
                //mS1,
                    mTARGET,
                    mCL,
                    mMR1;

            //*************
            decimal[][] eightScores;
            //
            decimal[] testScores;

            int[] Numbers;
            //
            decimal[] overTestScores;
            int[] overNumbers;
            decimal[] overCLScores;
            int[] overCLNumbers;
            // * **********/
            int MainDataCount = 0;
            //
            //
            #endregion
            public string mHeader;
            public string X, S, CV, CP, CPK, Zbench;
            int prec;

            public DataTable MainData = new DataTable("Main");

            protected void InitLineData()
            {
                prec = 0;
                eightScores = null;
                testScores = null;
                Numbers = null;
                overTestScores = null;
                overNumbers = null;
                //
                overCLNumbers = null;
                overCLScores = null;
                //
                MainDataCount = 0;
                /////
                mUSL = decimal.MinValue;
                mLSL = decimal.MinValue;

                mLUSL = decimal.MinValue;
                mLLSL = decimal.MinValue;

                mUCL = decimal.MinValue;
                mLCL = decimal.MinValue;

                mX1 = decimal.MinValue;
                mX2 = decimal.MinValue;
                //mS1 = decimal.MinValue;
                mTARGET = decimal.MinValue;
                mCL = decimal.MinValue;
                mMR1 = decimal.MinValue;
                /////
                mHeader = "*";
                //
                X = "*";
                S = "*";
                CV = "*";
                CP = "*";
                CPK = "*";
                Zbench = "*";
            }
            /// <summary>
            /// 以系统时间为止的 80 个数据
            /// </summary>
            /// <param name="SampleName"></param>
            /// <param name="lot_no"></param>
            /// <param name="line"></param>
            /// <param name="property"></param>
            /// <returns></returns>
            public int FillData(string SampleName, string lot_no, string line, string property)
            {
                InitLineData();
                mHeader = SampleName + " " + lot_no + "  LINE:" + line + "  " + property;
                MainData = GetChartDataRecent(SampleName, lot_no, line, property, DateTime.Now);
                MainDataCount = MainData.Rows.Count;

                if (MainDataCount == 0) return 0;
                ///////////////////
                try
                {
                    this.mTARGET = Convert.ToDecimal((string)MainData.Rows[0]["MidValue"]);
                }
                catch
                {
                    this.mTARGET = decimal.MinValue;
                }

                try
                {
                    this.mUSL = Convert.ToDecimal((string)MainData.Rows[0]["MaxValue"]);
                }
                catch
                {
                    this.mUSL = decimal.MinValue;
                }

                try
                {
                    this.mLSL = Convert.ToDecimal((string)MainData.Rows[0]["MinValue"]);
                }
                catch
                {
                    this.mLSL = decimal.MinValue;
                }
                ///////////////////
                int OverCount = 0;
                testScores = (decimal[])Array.CreateInstance(typeof(decimal), MainDataCount);

                //
                for (int i = 0; i < MainDataCount; i++)
                {
                    testScores[i] = Convert.ToDecimal((string)MainData.Rows[MainDataCount - 1 - i]["Result"]);

                    if (mUCL != decimal.MinValue && testScores[i] > mUCL) OverCount++;
                    if (mLCL != decimal.MinValue && testScores[i] < mLCL) OverCount++;
                }
                //
                Numbers = (int[])Array.CreateInstance(typeof(int), testScores.Length);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i;
                //
                if (OverCount != 0)
                {
                    overNumbers = (int[])Array.CreateInstance(typeof(int), OverCount);
                    overTestScores = (decimal[])Array.CreateInstance(typeof(decimal), OverCount);

                    int num = 0;
                    for (int i = 0; i < testScores.Length; i++)
                    {
                        if (mUCL != decimal.MinValue && testScores[i] > mUCL)
                        {
                            overNumbers[num] = i + 1;
                            overTestScores[num] = testScores[i];
                            num++;
                        }
                        else if (mLCL != decimal.MinValue && testScores[i] < mLCL)
                        {
                            overNumbers[num] = i + 1;
                            overTestScores[num] = testScores[i];
                            num++;
                        }
                    }
                }
                ///////////////////
                return MainDataCount;

            }
            //
            /// <summary>
            /// 填充数据 若是poy 填充特别数据
            /// </summary>
            /// <param name="SampleName"></param>
            /// <param name="lot_no"></param>
            /// <param name="line"></param>
            /// <param name="property"></param>
            /// <param name="b"></param>
            /// <param name="e"></param>
            /// <returns></returns>
            public int FillData(string SampleName, string lot_no, string line, string property, DateTime b, DateTime e, int ab)
            {
                InitLineData();
                string side = "";
                #region
                //if (SampleName.Trim() == "棉物性")
                //{
                //    if (ab == 0)
                //        side = "   B边";
                //    else if (ab == 1)
                //        side = "   A边";
                //}
                #endregion
                mHeader = b.ToString("yyMMdd HH:mm") + "~" +
                          e.ToString("yyMMdd HH:mm") + " : " +
                          SampleName + " " + lot_no + "  LINE:" + line + "  " + property
                          + side;
                MainData = GetChartDataNormal(SampleName, lot_no, line, property, b, e);
                MainDataCount = MainData.Rows.Count;

                if (MainDataCount == 0) return 0;
                ///////////////////
                try
                {
                    this.mTARGET = Convert.ToDecimal((string)MainData.Rows[0]["MidValue"]);
                }
                catch
                {
                    this.mTARGET = decimal.MinValue;
                }

                try
                {
                    this.mUSL = Convert.ToDecimal((string)MainData.Rows[0]["MaxValue"]);
                }
                catch
                {
                    this.mUSL = decimal.MinValue;
                }

                try
                {
                    this.mLSL = Convert.ToDecimal((string)MainData.Rows[0]["MinValue"]);
                }
                catch
                {
                    this.mLSL = decimal.MinValue;
                }

                try
                {
                    this.mLUSL = Convert.ToDecimal((string)MainData.Rows[0]["UCL"]);
                }
                catch
                {
                    this.mLUSL = decimal.MinValue;
                }

                try
                {
                    this.mLLSL = Convert.ToDecimal((string)MainData.Rows[0]["LCL"]);
                }
                catch
                {
                    this.mLLSL = decimal.MinValue;
                }


                int OverCount = 0;
                int OverCLCount = 0;
                decimal SumTestScores = 0;

                testScores = (decimal[])Array.CreateInstance(typeof(decimal), MainDataCount);
                //
                eightScores = new decimal[8][];
               if (SampleName.Trim().ToUpper().IndexOf("POY") != -1 && MainDataCount != 0)
               {
                    for (int i = 0; i < 8; i++)
                    {
                        eightScores[i] = (decimal[])Array.CreateInstance(typeof(decimal), MainDataCount);
                    }
                    //C1---C8  eightScores数组
                    #region
                    for (int i = 0; i < MainDataCount; i++)
                    {
                        try
                        {
                            eightScores[0][i] = Convert.ToDecimal((string)MainData.Rows[i]["C1"]);
                        }
                        catch
                        {
                            eightScores[0][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[1][i] = Convert.ToDecimal((string)MainData.Rows[i]["C2"]);
                        }
                        catch
                        {
                            eightScores[1][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[2][i] = Convert.ToDecimal((string)MainData.Rows[i]["C3"]);
                        }
                        catch
                        {
                            eightScores[2][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[3][i] = Convert.ToDecimal((string)MainData.Rows[i]["C4"]);
                        }
                        catch
                        {
                            eightScores[3][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[4][i] = Convert.ToDecimal((string)MainData.Rows[i]["C5"]);
                        }
                        catch
                        {
                            eightScores[4][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[5][i] = Convert.ToDecimal((string)MainData.Rows[i]["C6"]);
                        }
                        catch
                        {
                            eightScores[5][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[6][i] = Convert.ToDecimal((string)MainData.Rows[i]["C7"]);
                        }
                        catch
                        {
                            eightScores[6][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                        try
                        {
                            eightScores[7][i] = Convert.ToDecimal((string)MainData.Rows[i]["C8"]);
                        }
                        catch
                        {
                            eightScores[7][i] = Convert.ToDecimal((string)MainData.Rows[i]["CMAX"]);
                        }
                    }
                }
                    #endregion
                //
                for (int i = 0; i < MainDataCount; i++)
                {
                    testScores[i] = Convert.ToDecimal((string)MainData.Rows[i]["Result"]);
                    SumTestScores += testScores[i];
                }

                try
                {
                    mX1 = SumTestScores / MainDataCount;
                }
                catch
                {
                    mX1 = decimal.MinValue;
                }
                //
                //UCL LCL 计算
                decimal args1 = decimal.MinValue;
                decimal args2 = 1.880m;
                if (SampleName.Trim().ToUpper().IndexOf("POY") == -1)
                {
                    args1 = CalculateMR();
                    args2 = 2.66m;
                }
                else
                {
                    args1 = CalculateS();
                    args2 = 0.373m;
                }

                if (args1 != decimal.MinValue)
                {
                    //                                      // _        _ 
                    mUCL = Math.Round(mX1 + args2 * args1, 6); // X + A2 * R
                    mLCL = Math.Round(mX1 - args2 * args1, 6); //   - 
                    //
                }
                // end 
                //LUSL LLSL 的获取 MoveToUp
                //mLUSL = mUCL;
                //mLLSL = mLCL;   
                //
                for (int i = 0; i < MainDataCount; i++)
                {
                    //testScores[i] = Convert.ToDecimal((string)MainData.Rows[i]["Result"]);
                    //SumTestScores += testScores[i];
                    if (mUSL != decimal.MinValue && testScores[i] > mUSL) OverCount++;
                    if (mLSL != decimal.MinValue && testScores[i] < mLSL) OverCount++;
                    //
                    if (mLUSL != decimal.MinValue && testScores[i] > mLUSL) OverCLCount++;
                    if (mLLSL != decimal.MinValue && testScores[i] < mLLSL) OverCLCount++;
                }
                ////////////////////////////////////////////////////////////////////////
                Numbers = (int[])Array.CreateInstance(typeof(int), testScores.Length);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i;
                //
                if (OverCount != 0)
                {
                    overNumbers = (int[])Array.CreateInstance(typeof(int), OverCount);
                    overTestScores = (decimal[])Array.CreateInstance(typeof(decimal), OverCount);

                    int num = 0;
                    for (int i = 0; i < testScores.Length; i++)
                    {
                        if (mUSL != decimal.MinValue && testScores[i] > mUSL)
                        {
                            overNumbers[num] = i + 1;
                            overTestScores[num] = testScores[i];
                            num++;
                        }
                        else if (mLSL != decimal.MinValue && testScores[i] < mLSL)
                        {
                            overNumbers[num] = i + 1;
                            overTestScores[num] = testScores[i];
                            num++;
                        }
                    }
                }
                ////
                if (OverCLCount != 0)
                {
                    overCLNumbers = (int[])Array.CreateInstance(typeof(int), OverCLCount);
                    overCLScores = (decimal[])Array.CreateInstance(typeof(decimal), OverCLCount);

                    int num = 0;
                    for (int i = 0; i < testScores.Length; i++)
                    {
                        if (mLUSL != decimal.MinValue && testScores[i] > mLUSL)
                        {
                            overCLNumbers[num] = i + 1;
                            overCLScores[num] = testScores[i];
                            num++;
                        }
                        else if (mLLSL != decimal.MinValue && testScores[i] < mLLSL)
                        {
                            overCLNumbers[num] = i + 1;
                            overCLScores[num] = testScores[i];
                            num++;
                        }
                    }
                }
                //计算X平均,S,CV% CP CPK Zbench
                if (MainDataCount >= 7)
                {
                    ArrayList aryScores = new ArrayList();
                    decimal nTotalScore = 0m;
                    int nTotalCount = 0;
                    if (SampleName.Trim().ToUpper().IndexOf("POY") == -1)
                    {
                        foreach (DataRow row in MainData.Rows)
                        {
                            decimal tmp = Convert.ToDecimal((string)row["Result"]);
                            nTotalScore += tmp;
                            nTotalCount++;
                            aryScores.Add(tmp);

                        }
                    }
                    else
                    {
                        foreach (DataRow row in MainData.Rows)
                        {
                            #region
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C1"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C2"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C3"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C4"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C5"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C6"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C7"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            try
                            {
                                decimal tmp = Convert.ToDecimal((string)row["C8"]);
                                nTotalScore += tmp;
                                nTotalCount++;
                                aryScores.Add(tmp);
                            }
                            catch
                            { }
                            #endregion
                        }
                    }
                    /////////////////////
                    prec = (int)MainData.Rows[0]["Prec"];

                    decimal iX = nTotalScore / nTotalCount;
                    //
                    mX1 = iX;
                    mX2 = iX;
                    //
                    decimal iS = 0m;
                    for (int i = 0; i < nTotalCount; i++)
                    {
                        iS += (decimal)Math.Pow((double)((decimal)aryScores[i] - iX), 2);
                    }
                    iS = iS / (nTotalCount - 1);
                    iS = (decimal)Math.Round(Math.Sqrt((double)iS), 9);

                    if (iX != 0m)
                    {
                        decimal iCV = Math.Round(iS / iX * 100, 2);
                        this.CV = iCV.ToString();
                    }
                    else this.CV = "*";
                    ////////////
                    this.X = Math.Round(iX, prec + 1).ToString();
                    this.S = Math.Round(iS, 4).ToString();
                    //有上限和下限值
                    if (mUSL != decimal.MinValue &&
                        mLSL != decimal.MinValue &&
                        iS != 0m)
                    {
                        this.CP = Math.Round((mUSL - mLSL) / (6 * iS), 2).ToString();
                        //
                        decimal tmp1 = Math.Round((mUSL - iX) / (3 * iS), 2);
                        decimal tmp2 = Math.Round((iX - mLSL) / (3 * iS), 2);
                        if (tmp1 >= tmp2)
                            this.CPK = tmp2.ToString();
                        else
                            this.CPK = tmp1.ToString();
                        //
                        tmp1 =
                                Math.Round((mUSL - iX) / iS, 2);   //Z u
                        tmp2 =
                                Math.Round((iX - mLSL) / iS, 2);//Z L

                        //双边的
                        this.Zbench = MathBenchvalue(Convert.ToDouble(tmp1), Convert.ToDouble(tmp2)).ToString();
                        //

                    } //有上限没下限
                    else if (mUSL != decimal.MinValue &&
                        mLSL == decimal.MinValue &&
                        iS != 0m)
                    {
                        this.CP = "*";
                        decimal tmp1 =
                                Math.Round((mUSL - iX) / (3 * iS), 2); //CPU

                        this.CPK = tmp1.ToString();

                        decimal tmp2 =
                                Math.Round((mUSL - iX) / iS, 2);

                        this.Zbench = tmp2.ToString();

                    } //有下限没上限
                    else if (mUSL == decimal.MinValue &&
                       mLSL != decimal.MinValue &&
                       iS != 0m)
                    {
                        decimal tmp2 = Math.Round((iX - mLSL) / (3 * iS), 2);  //CPL

                        this.CPK = tmp2.ToString();// tmp1.ToString();

                        this.CP = "*";// tmp1.ToString();
                        decimal tmp1 =
                              Math.Round((iX - mLSL) / iS, 2);//Z L

                        this.Zbench = tmp1.ToString();

                    }



                }
                ///////////////////
                return MainDataCount;

            }
            private void GetZBenchCPKValue(decimal iS, decimal iX)
            {

                //有上限和下限值
                if (mUSL != decimal.MinValue &&
                    mLSL != decimal.MinValue &&
                    iS != 0m)
                {
                    this.CP = Math.Round((mUSL - mLSL) / (6 * iS), 2).ToString();
                    //
                    decimal tmp1 = Math.Round((mUSL - iX) / (3 * iS), 2);
                    decimal tmp2 = Math.Round((iX - mLSL) / (3 * iS), 2);
                    if (tmp1 >= tmp2)
                        this.CPK = tmp2.ToString();
                    else
                        this.CPK = tmp1.ToString();
                    //
                    tmp1 =
                            Math.Round((mUSL - iX) / iS, 2);   //Z u
                    tmp2 =
                            Math.Round((iX - mLSL) / iS, 2);//Z L

                    //双边的
                    this.Zbench = MathBenchvalue(Convert.ToDouble(tmp1), Convert.ToDouble(tmp2)).ToString();
                    //

                } //有上限没下限
                else if (mUSL != decimal.MinValue &&
                    mLSL == decimal.MinValue &&
                    iS != 0m)
                {
                    this.CP = "*";
                    decimal tmp1 =
                            Math.Round((mUSL - iX) / (3 * iS), 2); //CPU

                    this.CPK = tmp1.ToString();

                    decimal tmp2 =
                            Math.Round((mUSL - iX) / iS, 2);

                    this.Zbench = tmp2.ToString();

                } //有下限没上限
                else if (mUSL == decimal.MinValue &&
                   mLSL != decimal.MinValue &&
                   iS != 0m)
                {
                    decimal tmp2 = Math.Round((iX - mLSL) / (3 * iS), 2);  //CPL

                    this.CPK = tmp2.ToString();// tmp1.ToString();

                    this.CP = "*";// tmp1.ToString();
                    decimal tmp1 =
                          Math.Round((iX - mLSL) / iS, 2);//Z L

                    this.Zbench = tmp1.ToString();

                }
            }
            //带特代的棉物性查询

            public double MathBenchvalue(double zu, double zl)
            {
                try
                {

                    Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    double dd = excel.WorksheetFunction.NormSInv(excel.WorksheetFunction.NormSDist(zu) + excel.WorksheetFunction.NormSDist(zl) - 1);

                    return dd;
                }
                catch (Exception ex)
                {
                    Loger.Error(ex);
                    //LogService.Trace(ex);
                    if (zu >= zl)
                        return zl;
                    else
                        return zu;

                }

            }


            /// <summary>
            /// IMR Chart
            /// </summary>
            /// <param name="chart"></param>
            /// <param name="chart2"></param>
            /// <returns></returns>
            private decimal CalculateMR()
            {   //移动极差
                decimal[] testScores = (decimal[])Array.CreateInstance(typeof(decimal), this.testScores.Length - 1);
                for (int i = 1; i < this.testScores.Length; i++)
                {
                    testScores[i - 1] = Math.Abs(this.testScores[i] - this.testScores[i - 1]);
                }
                int[] Numbers = (int[])Array.CreateInstance(typeof(int), this.testScores.Length - 1);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i + 1;
                //
                decimal total = 0m;
                for (int i = 0; i < testScores.Length; i++)
                {
                    total += testScores[i];
                }

                try
                {
                    mMR1 = total / testScores.Length;
                }
                catch
                {
                    mMR1 = decimal.MinValue;
                }
                return mMR1;
            }
            private decimal CalculateS()
            {
                decimal[] testScores = (decimal[])Array.CreateInstance(typeof(decimal), this.testScores.Length);
                for (int i = 0; i < this.testScores.Length; i++)
                {
                    testScores[i] =
                        // Math.Abs(
                        Convert.ToDecimal((string)MainData.Rows[MainDataCount - 1 - i]["CMAX"])
                      - Convert.ToDecimal((string)MainData.Rows[MainDataCount - 1 - i]["CMIN"]);
                    // );
                }
                int[] Numbers = (int[])Array.CreateInstance(typeof(int), this.testScores.Length);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i;
                //
                decimal total = 0m;
                for (int i = 0; i < testScores.Length; i++)
                {
                    total += testScores[i];
                }

                try
                {
                    mMR1 = total / testScores.Length;
                }
                catch
                {
                    mMR1 = decimal.MinValue;
                }
                return mMR1;
            }


            public bool SingleTrendChart(ref TrendChart dic)
            {
                //int nScores = testScores.Length;
                dic = new TrendChart();

                #region 绘制控制线
                /*************************
             *        USL
             * **********************/
                if (mUSL != decimal.MinValue)
                {
                    dic.USL = mUSL;
                    string USL_Lable = "USL=" + dic.USL.ToString();
                    dic.Label_USL = USL_Lable;
                }
                /*************************
                 *        LSL
                 * **********************/
                if (mLSL != decimal.MinValue)
                {
                    dic.LSL = mLSL;
                    string LSL_Lable = "LSL=" + dic.LSL.ToString();
                    dic.Label_LSL = LSL_Lable;
                }
                /*************************
                 *        LUCL
                 * **********************/
                if (mLUSL != decimal.MinValue)
                {
                    dic.LUCL = mLUSL;
                    string LUCL_Lable = "LUCL=" + dic.LUCL.ToString();
                    dic.Label_LUCL = LUCL_Lable;
                }
                /*************************
                *        LLCL
                * ***********************/
                if (mLLSL != decimal.MinValue)
                {
                    dic.LLCL = mLLSL;
                    string LLCL_Lable = "LLCL=" + dic.LLCL.ToString();
                    dic.Label_LLCL = LLCL_Lable;
                }
                /*************************
                 *        _ 
                 *        X
                 * **********************/
                if (mX1 != decimal.MinValue)
                {
                    dic.X = mX1;
                    string X_Lable = " X =" + Math.Round(dic.X, prec + 1).ToString();
                    dic.X_Lab = X_Lable;
                }
                // 
                #endregion

                dic.Number = Numbers;
                dic.testScores = testScores;
                dic.eightScores = eightScores;
                dic.overCLNumbers = overCLNumbers;
                dic.overCLScores = overCLScores;
                dic.overNumbers = overNumbers;
                dic.overTestScores = overTestScores;
                ///////////////////////////////////////////////////////////////////////////
                return false;
            }
            public bool MutiTrendChart(ref TrendChart dic)
            {
                int nScores = testScores.Length;
                //  dic = new Dictionary<string, object>(); 
                dic = new TrendChart();
                //labels.DefaultLabelStyle.BackColor = chart.Style.BackColor;
                #region 绘制控制线
                int index = -1;
                /*************************
                 *        USL
                 * **********************/
                if (mUSL != decimal.MinValue)
                {
                    dic.USL = mUSL;
                    string USL_Lable = "USL=" + dic.USL.ToString();
                    dic.Label_USL = USL_Lable;
                }
                /*************************
                 *        LSL
                 * **********************/
                if (mLSL != decimal.MinValue)
                {
                    dic.LSL = mLSL;
                    string LSL_Lable = "LSL=" + dic.LSL.ToString();
                    dic.Label_LSL = LSL_Lable;

                }
                /*************************
                  *        LUCL
                  * **********************/
                if (mLUSL != decimal.MinValue)
                {
                    dic.LUCL = mLUSL;
                    string LUCL_Lable = "LUCL=" + dic.LUCL.ToString();
                    dic.Label_LUCL = LUCL_Lable;

                }
                /*************************
                *        LLCL
                * ***********************/
                if (mLLSL != decimal.MinValue)
                {
                    dic.LLCL = mLLSL;
                    string LLCL_Lable = "LLCL=" + dic.LLCL.ToString();
                    dic.Label_LLCL = LLCL_Lable;

                }
                /*************************
                 *        _ 
                 *        X
                 * **********************/
                if (mX1 != decimal.MinValue)
                {
                    dic.X = mX1;
                    string X_Lable = " X =" + Math.Round(dic.X, prec + 1).ToString();
                    dic.X_Lab = X_Lable;
                }
                // 
                #endregion
                dic.Number = Numbers;
                dic.testScores = testScores;
                dic.eightScores = eightScores;

                dic.overCLNumbers = overCLNumbers;
                dic.overCLScores = overCLScores;
                dic.overNumbers = overNumbers;
                dic.overTestScores = overTestScores;


                return false;
            }

            public bool X1Chart(ref TrendChart dic)
            {
                dic = new TrendChart();

                #region 绘制控制线
                if (mUCL != decimal.MinValue)
                {
                    dic.UCL = Math.Round(mUCL, prec + 1);
                    dic.Label_UCL = "UCL=" + dic.UCL.ToString();
                }
                if (mLCL != decimal.MinValue)
                {
                    dic.LCL = Math.Round(mLCL, prec + 1);
                    dic.Label_LCL = "LCL=" + dic.LCL.ToString();
                }
                if (mLUSL != decimal.MinValue)
                {
                    dic.LUCL = mLUSL;
                    dic.Label_LUCL = "LUCL=" + dic.LUCL.ToString();
                }
                if (mLLSL != decimal.MinValue)
                {
                    dic.LLCL = mLLSL;
                    dic.Label_LLCL = "LLCL=" + dic.LLCL.ToString();
                }
                if (mUSL != decimal.MinValue)
                {
                    dic.USL = mUSL;
                    dic.Label_USL = "USL=" + dic.USL.ToString();
                }
                if (mLSL != decimal.MinValue)
                {
                    dic.LSL = mLSL;
                    dic.Label_LSL = "LSL=" + dic.LSL.ToString();
                }
                if (mTARGET != decimal.MinValue)
                {
                    dic.TARGET = mTARGET;
                    dic.Label_TARGET = "TAG=" + dic.TARGET.ToString();
                }
                if (mX1 != decimal.MinValue)
                {
                    dic.X = Math.Round(mX1, prec + 1);
                    dic.X_Lab = "X=" + dic.X.ToString();
                }
                #endregion

                dic.Number = Numbers;
                dic.testScores = testScores;
                dic.eightScores = eightScores;
                dic.overCLNumbers = overCLNumbers;
                dic.overCLScores = overCLScores;
                dic.overNumbers = overNumbers;
                dic.overTestScores = overTestScores;

                return false;
            }
            public bool RChart(ref TrendChart dic)
            {
                dic = new TrendChart();

                decimal[] testScores = (decimal[])Array.CreateInstance(typeof(decimal), this.testScores.Length);
                for (int i = 0; i < this.testScores.Length; i++)
                {
                    testScores[i] =
                        // Math.Abs(
                        Convert.ToDecimal((string)MainData.Rows[i]["CMAX"])
                      - Convert.ToDecimal((string)MainData.Rows[i]["CMIN"]);
                    // );
                }
                int[] Numbers = (int[])Array.CreateInstance(typeof(int), this.testScores.Length);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i;

                decimal total = 0m;
                for (int i = 0; i < testScores.Length; i++)
                {
                    total += testScores[i];
                }

                try
                {
                    mMR1 = total / testScores.Length;
                }
                catch
                {
                    mMR1 = decimal.MinValue;
                }

                #region 绘制控制线
                //UCL
                dic.UCL = Math.Round((1.863m * mMR1), prec + 2);
                dic.Label_UCL = "UCL=" + dic.UCL.ToString();

                //LCL
                dic.LCL = Math.Round((0.136m * mMR1), prec + 2);
                dic.Label_LCL = "LCL=" + dic.LCL.ToString();

                //MR
                dic.MR = Math.Round(mMR1, prec + 2);
                dic.Label_MR = "MR=" + dic.MR.ToString();
                #endregion

                dic.Number = Numbers;
                dic.testScores = testScores;

                return false;
            }
            public bool RChart4DTY(ref TrendChart dic)
            {
                dic = new TrendChart();

                decimal[] testScores = (decimal[])Array.CreateInstance(typeof(decimal), this.testScores.Length);
                for (int i = 0; i < this.testScores.Length; i++)
                {
                    testScores[i] =
                        // Math.Abs(
                        Convert.ToDecimal((string)MainData.Rows[i]["CMAX"])
                      - Convert.ToDecimal((string)MainData.Rows[i]["CMIN"]);
                    // );
                }
                int[] Numbers = (int[])Array.CreateInstance(typeof(int), this.testScores.Length);
                int nScores = testScores.Length;
                for (int i = 1; i <= nScores; i++)
                    Numbers[i - 1] = i;

                decimal total = 0m;
                for (int i = 0; i < testScores.Length; i++)
                {
                    total += testScores[i];
                }

                try
                {
                    mMR1 = total / testScores.Length;
                }
                catch
                {
                    mMR1 = decimal.MinValue;
                }

                #region 绘制控制线
                //UCL
                dic.UCL = Math.Round((2.114m * mMR1), prec + 2);
                dic.Label_UCL = "UCL=" + dic.UCL.ToString();

                //LCL
                dic.LCL = Math.Round((0m * mMR1), prec + 2);
                dic.Label_LCL = "LCL=" + dic.LCL.ToString();

                //MR
                dic.MR = Math.Round(mMR1, prec + 2);
                dic.Label_MR = "MR=" + dic.MR.ToString();
                #endregion

                dic.Number = Numbers;
                dic.testScores = testScores;

                return false;
            }

            public bool IChart(ref TrendChart dic)
            {
                dic = new TrendChart();

                #region 绘制控制线
                if (mUCL != decimal.MinValue)
                {
                    dic.UCL = Math.Round(mUCL, prec + 1);
                    dic.Label_UCL = "UCL=" + dic.UCL.ToString();
                }
                if (mLCL != decimal.MinValue)
                {
                    dic.LCL = Math.Round(mLCL, prec + 1);
                    dic.Label_LCL = "LCL=" + dic.LCL.ToString();
                }
                if (mLUSL != decimal.MinValue)
                {
                    dic.LUCL = mLUSL;
                    dic.Label_LUCL = "LUCL=" + dic.LUCL.ToString();
                }
                if (mLLSL != decimal.MinValue)
                {
                    dic.LLCL = mLLSL;
                    dic.Label_LLCL = "LLCL=" + dic.LLCL.ToString();
                }
                if (mUSL != decimal.MinValue)
                {
                    dic.USL = mUSL;
                    dic.Label_USL = "USL=" + dic.USL.ToString();
                }
                if (mLSL != decimal.MinValue)
                {
                    dic.LSL = mLSL;
                    dic.Label_LSL = "LSL=" + dic.LSL.ToString();
                }
                if (mTARGET != decimal.MinValue)
                {
                    dic.CL = Math.Round(mTARGET, prec + 1);
                    dic.Label_CL = "CL=" + dic.CL.ToString();
                }
                if (mX1 != decimal.MinValue)
                {
                    dic.X = Math.Round(mX1, prec + 1);
                    dic.X_Lab = "X=" + dic.X.ToString();
                }
                #endregion

                dic.Number = Numbers;
                dic.testScores = testScores;
                dic.overCLNumbers = overCLNumbers;
                dic.overCLScores = overCLScores;
                dic.overNumbers = overNumbers;
                dic.overTestScores = overTestScores;

                return false;
            }
            public bool MRChart(ref TrendChart dic)
            {
                dic = new TrendChart();


                try
                {
                    decimal[] testScores = (decimal[])Array.CreateInstance(typeof(decimal), this.testScores.Length - 1);
                    for (int i = 1; i < this.testScores.Length; i++)
                    {
                        testScores[i - 1] = Math.Abs(this.testScores[i] - this.testScores[i - 1]);
                    }
                    int[] Numbers = (int[])Array.CreateInstance(typeof(int), this.testScores.Length - 1);
                    int nScores = testScores.Length;
                    for (int i = 1; i <= nScores; i++)
                        Numbers[i - 1] = i + 1;
                    //
                    decimal total = 0m;
                    for (int i = 0; i < testScores.Length; i++)
                    {
                        total += testScores[i];
                    }

                    try
                    {
                        mMR1 = total / testScores.Length;
                    }
                    catch
                    {
                        mMR1 = decimal.MinValue;
                    }

                    #region 绘制控制线
                    //UCL
                    dic.UCL = Math.Round((3.267m * mMR1), 6);
                    dic.Label_UCL = "UCL=" + dic.UCL.ToString();

                    //LCL
                    dic.LCL = 0;
                    dic.Label_LCL = "LCL";

                    //MR
                    dic.MR = Math.Round(mMR1, 6);
                    dic.Label_MR = "MR=" + dic.MR.ToString();
                    #endregion

                    dic.Number = Numbers;
                    dic.testScores = testScores;


                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                    Loger.Error(ex);
                }
            }
        }
        
        public class TrendChart
        {
            public TrendChart()
            {
                USL = decimal.MinValue;
                LSL = decimal.MinValue;
                LUCL = decimal.MinValue;
                LLCL = decimal.MinValue;
                UCL = decimal.MinValue;
                LCL = decimal.MinValue;
                TARGET = decimal.MinValue;
                MR = decimal.MinValue;
                CL = decimal.MinValue;
                X = decimal.MinValue;
            }
            public string Label_USL { get; set; }
            public string Label_LSL { get; set; }
            public string Label_LUCL { get; set; }
            public string Label_LLCL { get; set; }

            public string Label_UCL { get; set; }
            public string Label_LCL { get; set; }
            public string Label_TARGET { get; set; }
            public string Label_MR { get; set; }
            public string Label_CL { get; set; }

            public decimal USL { get; set; }
            public decimal LSL { get; set; }
            public decimal LUCL { get; set; }
            public decimal LLCL { get; set; }

            public decimal UCL { get; set; }
            public decimal LCL { get; set; }
            public decimal TARGET { get; set; }
            public decimal MR { get; set; }
            public decimal CL { get; set; }

            public decimal X { get; set; }
            public string X_Lab { get; set; }
            public int[] Number { get; set; }
            public decimal[] testScores { get; set; }

            public decimal[][] eightScores { get; set; }

            public int[] overCLNumbers { get; set; }
            public decimal[] overCLScores { get; set; }
            public int[] overNumbers { get; set; }
            public decimal[] overTestScores { get; set; }
        }

    }

