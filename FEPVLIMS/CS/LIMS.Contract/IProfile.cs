using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IProfile
    {
        bool ProfileInRange(string voucherID, string sampleName, string lot_no, string perpertyName, decimal result);

     

        bool Dell(string voucherID, string PropertyName);

        bool DeleteProfile(Profile profile);

        bool Modify(List<Profile> _ProfileList, out string msg);

        string RecodeResult(Profile profile);

        Profile SelectProfile(string voucherID, string propertyName);
    }
}