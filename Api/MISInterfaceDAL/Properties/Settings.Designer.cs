﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISInterfaceDAL.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.17.215.200:8006/ZFEPV_GetBLInfo.asmx")]
        public string MISInterfaceDAL_VehicleTransfer_ZFEPV_GetBLInfo {
            get {
                return ((string)(this["MISInterfaceDAL_VehicleTransfer_ZFEPV_GetBLInfo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.20.50.128/A27N6Service/WA27N6Connect/WA27N6MIS001.asmx")]
        public string MISInterfaceDAL_ShippingOrderDate_WA27N6MIS001 {
            get {
                return ((string)(this["MISInterfaceDAL_ShippingOrderDate_WA27N6MIS001"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.20.50.128/A27N6Service/WA27N6Connect/WA27N6MIS002.asmx")]
        public string MISInterfaceDAL_VehicleInformationInput_WA27N6MIS002 {
            get {
                return ((string)(this["MISInterfaceDAL_VehicleInformationInput_WA27N6MIS002"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.17.213.167:8007/FEPV_GetSOInfo.asmx")]
        public string MISInterfaceDAL_UploadPickInfo_FEPV_GetSOInfo {
            get {
                return ((string)(this["MISInterfaceDAL_UploadPickInfo_FEPV_GetSOInfo"]));
            }
        }
    }
}