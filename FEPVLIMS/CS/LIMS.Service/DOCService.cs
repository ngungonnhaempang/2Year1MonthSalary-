using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using System.Data.Common;
using System.IO;
using System.Configuration;
using NBear.Data;

namespace LIMS.Service
{
	/// <summary>
	/// feis
	/// </summary>
	public class DOCService : MarshalByRefObject, IDOC
	{
		//FEPV LIMS
		protected static Gateway gate = new Gateway("LIMS");
		readonly DB db = new DB("LIMS");
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public byte[] DocAssistSearch(DateTime B, DateTime E, string par, string userid)
		{
			return null;

		}

		public byte[] GetvProperties(string voucherID)
		{

			Console.WriteLine("DocService-GetvProperties():" + DateTime.Now.ToString());
			string docType = "";
			try
			{
				docType = voucherID.Trim().Substring(0, 1);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			DataSet ds = gate.ExecuteStoredProcedure("QE31_GetPropertyByVoucherID", new string[] { "UserID", "VoucherID" }, new object[] { DB.User, voucherID });
			return DataFormatter.GetBinaryFormatDataCompress(ds);

		}
		/// <summary>
		/// Create by Isaac 2018-11-12
		/// </summary>
		/// <param name="voucherID"></param>
		/// <param name="lotno_new"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public bool DocUpdateMaterialNo(string voucherID, string lotno_new, string reason)
		{
			Console.WriteLine("DOCDAL-DocUpdateMaterialNo():{0} -->{1} {2} ", voucherID, lotno_new, DateTime.Now);
			try
			{
				gate.ExecuteStoredProcedure("QE31_UpdateMaterialNO", new string[] { "VoucherID", "LOT_NO", "Remark" }, new object[] { voucherID, lotno_new, reason });
				return true;
			}
			catch (Exception e)
			{
				
				
				throw new Exception(e.Message);
			}
		}

		public bool SaveDocRemark(string voucherID, string remark)
		{
			Console.WriteLine("DocService-SaveDocRemark():" + DateTime.Now.ToString());
			string docConfCmd = "";
			Console.WriteLine("voucherID:" + voucherID);
			string _DocType = voucherID.Substring(0, 2);
			if (_DocType == "SP")
			{
				docConfCmd = @"UPDATE Receive SET Remark = @Remark WHERE State = '1' AND VoucherID = @VoucherID";
			}
			else
			{
				string DocType = voucherID.Substring(0, 1);
				switch (DocType)
				{
					case "R":
						docConfCmd = @"UPDATE Requisition SET Remark = @Remark WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "P":
						docConfCmd = @"UPDATE Plans SET Remark = @Remark WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "S":
						docConfCmd = @"UPDATE Receive SET Remark = @Remark WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					default:
						return false;
				}

			}
			try
			{
				gate.DbHelper.ExecuteNonQuery(docConfCmd, new object[] { remark, voucherID });
				log.Info("SaveDocRemark:" + voucherID);
				return true;
			}
			catch (Exception ex)
			{
				log.Error(ex);
				throw ex;
			}
		}

		public bool DocConfirm(string voucherID)
		{
			Console.WriteLine("DocService-DocConfirm():" + voucherID + "|" + DateTime.Now.ToString());
			bool result = false;

			string profileConfCmd = @"UPDATE Profile SET State = '1' WHERE State = '0' AND VoucherID = @VoucherID";

			//
			string docConfCmd;
			string _DocType = voucherID.Substring(0, 2);
			if (_DocType == "SP")
			{

				docConfCmd = @"UPDATE ReceiveGeneral SET State = '2', Stamp=GETDATE() WHERE State = '1' AND VoucherID = @VoucherID";
			}
			else
			{
				string DocType = voucherID.Substring(0, 1).ToUpper();
				switch (DocType)
				{
					case "R":
						docConfCmd = @"UPDATE Requisition SET State = '2' WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "P":
						docConfCmd = @"UPDATE Plans SET State = '2' WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "S":
						docConfCmd = @"UPDATE Receive SET State = '2', Stamp=GETDATE() WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					default:
						return false;
				}
			}
			//
			try
			{
				gate.DbHelper.ExecuteNonQuery(docConfCmd, new object[] { voucherID });
				gate.DbHelper.ExecuteNonQuery(profileConfCmd, new object[] { voucherID });
				result = true;
				log.Info("DocConfirm:" + voucherID);
			}
			catch (Exception ex)
			{
				log.Error(ex);
				throw ex;
			}
			return result;
		}

		public bool DocUnLock(string voucherID)
		{
			Console.WriteLine("DocService-DocUnLock():" + voucherID + "|" + DateTime.Now.ToString());
			bool result = false;
			//
			string docConfCmd;
			string _DocType = voucherID.Substring(0, 2);
			if (_DocType == "SP")
			{

				docConfCmd = @"UPDATE ReceiveGeneral SET State = '1' WHERE State = '2' AND VoucherID = @VoucherID";
			}
			else
			{
				string DocType = voucherID.Substring(0, 1).ToUpper();
				switch (DocType)
				{
					case "R":
						docConfCmd = @"UPDATE Requisition SET State = '1' WHERE State = '2' AND VoucherID = @VoucherID";
						break;
					case "P":
						docConfCmd = @"UPDATE Plans SET State = '1' WHERE State = '2' AND VoucherID = @VoucherID";
						break;
					case "S":
						docConfCmd = @"UPDATE Receive SET State = '1' WHERE State = '2' AND VoucherID = @VoucherID";
						break;
					default:
						return false;
				}
			}
			//
			try
			{
				gate.DbHelper.ExecuteNonQuery(docConfCmd, new object[] { voucherID });
				result = true;
				log.Info("DocUnLock:" + voucherID);
			}
			catch (Exception ex)
			{
				log.Error(ex);
				Console.WriteLine(ex.Message);
				throw ex;
			}
			return result;
		}

		/// <summary>
		/// 确认
		/// </summary>
		/// <param name="voucherID"></param>
		/// <param name="PropertyName"></param>
		/// <returns></returns>
		public bool DocPropertyConfirm(string voucherID, string PropertyName)
		{
			Console.WriteLine("DocService-DocPropertyConfirm():" + voucherID + "|" + PropertyName + "|" + DateTime.Now.ToString());
			bool result = false;
			string profileConfCmd = @"UPDATE Profile SET State = '1' WHERE State = '0' AND VoucherID = @VoucherID AND PropertyName = @PropertyName ";//AND Result IS NOT NULL  
			try
			{
				gate.DbHelper.ExecuteNonQuery(profileConfCmd, new object[] { voucherID, PropertyName });
				result = true;
				log.Info("DocPropertyConfirm:" + voucherID);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				log.Error(ex);
				throw ex;
			}
			return result;
		}

		/// <summary>
		/// 释放单据
		/// </summary>
		/// <param name="voucherID"></param>
		/// <param name="PropertyName"></param>
		/// <returns></returns>
		public bool DocPropertyUnLock(string voucherID, string PropertyName)
		{
			try
			{
				Console.WriteLine("DocService-DocPropertyUnLock():" + voucherID + "|" + PropertyName + "|" + DateTime.Now.ToString());
				int count = 0;
				object[] outParameters;

				gate.DbHelper.ExecuteStoredProcedure("QC05_Unlock_Doc_Profile",
					new string[] { "VoucherID", "PropertyName" },
					new object[] { voucherID, PropertyName },
					new string[] { "Result" },
					new DbType[] { DbType.Int32 },
					out outParameters);

				count = (int)outParameters.ElementAt(0);
				Console.WriteLine("outParameters:" + count);
				if (count == 1)
					return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				log.Error(ex);
				return false;
			}
			return false;
		}

		public bool SetDocGrade(string voucherID, string Grade)
		{
			Console.WriteLine("DocService-SetDocGrade():" + voucherID + "|" + Grade + "|" + DateTime.Now.ToString());
			string docConfCmd;
			string _DocType = voucherID.Substring(0, 2);
			if (_DocType == "SP")
			{
				docConfCmd = @"UPDATE ReceiveGeneral SET Grade = @Grade WHERE State = '1' AND VoucherID = @VoucherID";
			}
			else
			{
				string DocType = voucherID.Substring(0, 1);
				switch (DocType)
				{
					case "R":
						return true;
					case "P":
						docConfCmd = @"UPDATE Plans SET Grade = @Grade WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "S":
						docConfCmd = @"UPDATE Receive SET Grade = @Grade WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					default:
						return false;
				}
			}

			try
			{
				gate.DbHelper.ExecuteNonQuery(docConfCmd, new object[] { Grade, voucherID });
				Console.WriteLine("docConfCmd:" + docConfCmd + "|" + voucherID + "|" + Grade);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				log.Error(ex);
				return false;
			}
		}

		public bool ChangeCreateType(string voucherID, string createType)
		{
			Console.WriteLine("DocService-ChangeCreateType():" + voucherID + "|" + createType + "|" + DateTime.Now.ToString());
			bool result = false;
			string profileConfCmd = @"UPDATE Plans SET CreateType = @CreateType WHERE State = '1' AND VoucherID = @VoucherID AND CreateType<>1";
			try
			{
				gate.DbHelper.ExecuteNonQuery(profileConfCmd, new object[] { createType, voucherID });
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				log.Error(ex);
				throw ex;
			}
			return result;
		}

		/// <summary>
		/// 删除委托，例行，进料的建档
		/// </summary>
		/// <param name="voucherID"></param>
		/// <returns></returns>
		public bool DocDelete(string voucherID)
		{
			Console.WriteLine("DocService-DocDelete():" + voucherID + DateTime.Now.ToString());
			string docConfCmd;
			string _DocType = voucherID.Substring(0, 2);
			if (_DocType == "SP")
			{
				docConfCmd = @"UPDATE ReceiveGeneral SET State = 'X' WHERE State = '1' AND VoucherID = @VoucherID";
			}
			else
			{
				string DocType = voucherID.Substring(0, 1);
				switch (DocType)
				{
					case "R":
						docConfCmd = @"UPDATE Requisition SET State = 'X' WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "P":
						docConfCmd = @"UPDATE Plans SET State = 'X' WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					case "S":
						docConfCmd = @"UPDATE Receive SET State = 'X' WHERE State = '1' AND VoucherID = @VoucherID";
						break;
					default:
						return false;
				}
			}

			try
			{
				gate.DbHelper.ExecuteNonQuery(docConfCmd, new object[] { voucherID });
				return true;
			}
			catch (Exception ex)
			{
				log.Error(ex);
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
		public bool UpLoadFile(string voucherID, byte[] fileData, string fileType, string DocType, string PropertyName, out string filePathServer)
		{
			Console.WriteLine("DocService-UpLoadFile():" + fileData.Length.ToString() + "-" + DateTime.Now.ToString());
			string _FilePathQC = (string)(configurationAppSettings.GetValue("FilePathQC", typeof(string)));
			Console.WriteLine("configurationAppSettings.GetValue:" + _FilePathQC);
			filePathServer = "";
			try
			{
				filePathServer = string.Format("{0}-{1}.{2}", voucherID, DateTime.Now.ToString("yyMMddHHmmss"), fileType.ToLower());
				string filepath = string.Format("{0}{1}", _FilePathQC, filePathServer);
				Console.WriteLine("filepath:" + filepath);

				MemoryStream meoeryStream = new MemoryStream(fileData);
				FileStream fileStream = new FileStream(filepath, FileMode.Create);
				meoeryStream.WriteTo(fileStream);
				fileStream.Close();
				meoeryStream.Close();

				gate.DbHelper.ExecuteStoredProcedureNonQuery("QE31_UpLoadFilePath", new string[] { "filePath", "VoucherID", "DocType", "PropertyName" }
					, new object[] { filePathServer, voucherID, DocType, PropertyName });

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Create y Isaac 2019-03-06
		/// Remove File Uploaded in QE31
		/// </summary>
		/// <param name="voucherID"></param>
		/// <returns></returns>
		public bool DeleteFile(string voucherID, string PropertyName)
		{
			Console.WriteLine("DocService-DeleteFile():" + voucherID.ToString() + "-" + DateTime.Now.ToString());
			string cmd = "";			
			string DocType = voucherID.Substring(0, 1);
			string fileName = "";
			bool result = true;
			string _FilePathQC = (string)(configurationAppSettings.GetValue("FilePathQC", typeof(string)));			
			Console.WriteLine("configurationAppSettings.GetValue:" + _FilePathQC);
            if (string.IsNullOrEmpty(voucherID) || string.IsNullOrEmpty(PropertyName)) return false;
			cmd = @"UPDATE PROFILE SET [filePath] = NULL WHERE VoucherID = @VoucherID AND PropertyName=@PropertyName";
            fileName = gate.SelectScalar<string>("Select [filePath] From PROFILE WHERE VoucherID = @VoucherID AND PropertyName= @PropertyName", new object[] { voucherID, PropertyName });
			try
			{
		
				string pathFile = Path.Combine(_FilePathQC, fileName);
				Console.WriteLine("delete-pathFile:"+pathFile);
                Console.WriteLine(File.Exists(pathFile));
				if (File.Exists(pathFile))
				{
					gate.DbHelper.ExecuteNonQuery(cmd, new object[] { voucherID, PropertyName });
					File.Delete(pathFile);
					Console.WriteLine("The file name {0} is deleted!", fileName);					
				}
				else
				{
					Console.WriteLine("File not found.");
					result = false;
				}				
				
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
				result = false;
			}
			return result;
		}
	}
}
