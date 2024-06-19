using AMNEVH.Models.GeoRegionEntities;
using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Helpers;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace AMNEVH.Models.UserEntities
{
    public class UserEntities
    {
    }
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserType
    {
        public string welcomeText { get; set; }

        public string ddlTypeID { get; set; }
        public string ddlType { get; set; }
    }
    public class Employee
    {
        public HighSchool highSchool { get; set; }
        public string EMPID { get; set; }
        public string DOJ { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Sex { get; set; }
        public string MStatus { get; set; }
        public string DOB { get; set; }
        public string Anniversary { get; set; }
        public string Cast { get; set; }
        public string Child { get; set; }
        public string Add1 { get; set; }
        public string City1 { get; set; }
        public string State1 { get; set; }
        public string Pin1 { get; set; }
        public string Add2 { get; set; }
        public string City2 { get; set; }
        public string State2 { get; set; }
        public string PIN2 { get; set; }
        public string mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public string DeptID { get; set; }
        public string DesigID { get; set; }
        public string JoinType { get; set; }
        public string Photo { get; set; }
        public string AccNo { get; set; }
        public string IFSC { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string PFNo { get; set; }
        public string ESINo { get; set; }
        public string PF { get; set; }
        public string ESI { get; set; }
        public string TDs { get; set; }
        public string StatUs { get; set; }
        public string Usr { get; set; }
        public string SSID { get; set; }
        public string EDate { get; set; }
        public string PMode { get; set; }
        public string CV { get; set; }
        public string DOC { get; set; }
        public string IDProof { get; set; }
        public string Trained { get; set; }
        public string Photo1 { get; set; }
        public string o1 { get; set; }
        public string dp1 { get; set; }
        public string e1 { get; set; }
        public string s1 { get; set; }
        public string j1 { get; set; }
        public string l1 { get; set; }
        public string o2 { get; set; }
        public string dp2 { get; set; }
        public string e2 { get; set; }
        public string s2 { get; set; }
        public string j2 { get; set; }
        public string l2 { get; set; }
        public string o3 { get; set; }
        public string dp3 { get; set; }
        public string e3 { get; set; }
        public string s3 { get; set; }
        public string j3 { get; set; }
        public string l3 { get; set; }
        public string o4 { get; set; }
        public string dp4 { get; set; }
        public string e4 { get; set; }
        public string s4 { get; set; }
        public string j4 { get; set; }
        public string l4 { get; set; }
        public string o5 { get; set; }
        public string dp5 { get; set; }
        public string e5 { get; set; }
        public string s5 { get; set; }
        public string j5 { get; set; }
        public string l5 { get; set; }
        public string Password { get; set; }
        public string subDesig { get; set; }
        public string PWD { get; set; }
        public string loginForID { get; set; }
        public string spouseName { get; set; }
        public string spouseQuly { get; set; }
        public string spouseWAdd { get; set; }
        public string spousePhone { get; set; }
        public string spouseMob { get; set; }
        public string sonName { get; set; }
        public string sonAge { get; set; }
        public string sonschool { get; set; }
        public string daughName { get; set; }
        public string daughAge { get; set; }
        public string daughSchool { get; set; }
        public string refNameA { get; set; }
        public string refMobA { get; set; }
        public string reAddressA { get; set; }
        public string refKnowA { get; set; }
        public string refNameB { get; set; }
        public string refMobB { get; set; }
        public string reAddressB { get; set; }
        public string refKnowB { get; set; }
        public string subjectSpe1 { get; set; }
        public string subjectSpe2 { get; set; }
        public string expTeacher { get; set; }
        public string expAdmin { get; set; }
        public string expAnyOther { get; set; }
        public string preSalary { get; set; }
        public string expSalary { get; set; }
        public string mentionAreaA { get; set; }
        public string mentionAreaB { get; set; }
        public string mentionAssigA { get; set; }
        public string mentionAssigB { get; set; }
        public string wotkEthic { get; set; }
        public string achAcadmic { get; set; }
        public string achResearch { get; set; }
        public string achSport { get; set; }
        public string achCultural { get; set; }
        public string joinTime { get; set; }
        public string leavestudy { get; set; }
        public string salaryFinal { get; set; }
        public string Mess { get; set; }
        public string Lodging { get; set; }
        public string status1 { get; set; }
        public string status2 { get; set; }
        public string status3 { get; set; }
        public string status4 { get; set; }
        public string status5 { get; set; }
        public string driving { get; set; }
        public string Lstatus { get; set; }
        public string Emstatus { get; set; }
        public string uniqueNo { get; set; }
        public string JIDOJ { get; set; }
        public string JID { get; set; }
        public string AadharNo { get; set; }
        public string Application { get; set; }
        public string Police { get; set; }
        public string DL { get; set; }
        public string gram1 { get; set; }
        public string post1 { get; set; }
        public string tahseel1 { get; set; }
        public string thana1 { get; set; }
        public string gram2 { get; set; }
        public string post2 { get; set; }
        public string tahseel2 { get; set; }
        public string thana2 { get; set; }
        public string OfferL { get; set; }
        public string AppointL { get; set; }
        public string ConfL { get; set; }
        public string BankD { get; set; }
        public string ETime_In { get; set; }
        public string ETime_Out { get; set; }
        public string EMargin { get; set; }
        public string mobileTODO { get; set; }
    }
    public class HighSchool
    {
        public string QID { get; set; }
        public string EMPID { get; set; }
        public string Qual { get; set; }
        public string Board { get; set; }
        public string Year { get; set; }
        public string Subject { get; set; }
        public string Per { get; set; }
        public string midiumInstruct { get; set; }
        public string type { get; set; }
        public string jid { get; set; }
    }
    public class Designation
    {
        public string desigId { get; set; }
        public string desigName { get; set; }
    }
}