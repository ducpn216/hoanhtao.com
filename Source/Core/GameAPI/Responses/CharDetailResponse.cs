using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameAPI.Responses
{
    public class CharDetailResponse
    {
        public CharDetailResponse()
        {
            Base = new BaseCharDetailResponse();
            Prop = new PropertyCharDetailResponse();
            //Mount = new MountCharDetailResponse();
            Sword = new SwordCharDetailResponse();
            Amulet = new AmuletCharDetailResponse();
            Bourn = new BournCharDetailResponse();
            //Pulse = new PulseCharDetailResponse();
            Skill = new List<SkillCharDetailResponse>();
        }

        public BaseCharDetailResponse Base { get; set; }
        public PropertyCharDetailResponse Prop { get; set; }
        //public MountCharDetailResponse Mount { get; set; }
        public SwordCharDetailResponse Sword { get; set; } 
        public AmuletCharDetailResponse Amulet { get; set; }
        public BournCharDetailResponse Bourn { get; set; }  
        //public PulseCharDetailResponse Pulse { get; set; } 
        public List<SkillCharDetailResponse> Skill { get; set; }
    }

    public class BaseCharDetailResponse
    {
        public string dwRoleID { get; set; }
        public string dwAccountID { get; set; }
        public string szName { get; set; }
        public string dwIPAddr { get; set; }
        public string dwProf { get; set; }
        public string dwExp { get; set; }
        public string dwLevel { get; set; }
        public string dwUpExp { get; set; }
        public string dwGold { get; set; }
        public string dwBindGold { get; set; }
        public string dwPacketMoney { get; set; }
        public string dwStorageMoney { get; set; }
        public string szGuildName { get; set; }
        public string szLover { get; set; }
        public string bIsVip { get; set; }
        public string dwVipTime { get; set; }
        public string dwWeiwangLevel { get; set; }
        public string dwActivePoint { get; set; }
        public string bChatBan { get; set; }
        public string bLoginBan { get; set; }
        public string MapID { get; set; }
        public string fPosX { get; set; }
        public string fPosY { get; set; }
        public string ip { get; set; }
        public string login { get; set; }
        public string create { get; set; }
        public string online { get; set; }
    }

    public class PropertyCharDetailResponse
    {
        public string dwAtk { get; set; }
        public string dwDef { get; set; }
        public string dwCri { get; set; }
        public string dwFle { get; set; }
        public string dwASD { get; set; }
        public string dwSpd { get; set; }
        public string dwMuscle { get; set; }
        public string dwPhysique { get; set; }
        public string dwOrgan { get; set; }
        public string dwTechnique { get; set; }
        public string dwDander { get; set; }
    }

    public class MountCharDetailResponse
    {
        public string dwMountUpGrade { get; set; }
        //public new List<>(){
    }

    public class SwordCharDetailResponse
    {
        public string szName { get; set; }
        public string dwSwordId { get; set; }
        public string dwSwordLadder { get; set; }
        public string dwLevel { get; set; }
        public string dwSwordGas { get; set; }
    }

    public class AmuletCharDetailResponse
    {
        public string szName { get; set; }
        public string dwAmuletLevel { get; set; }
        public string dwAmuletRank { get; set; }
        public string dwGrowthValue { get; set; }
    }

    public class BournCharDetailResponse
    {
        public string index { get; set; }
        public string dwBornLv { get; set; }
        public string dwBournExp { get; set; }
    }

    public class PulseCharDetailResponse
    {
         
    }

    public class SkillCharDetailResponse
    {
        public string dwSkillID { get; set; }
        public string dwSkillLevel { get; set; }
        public string szName { get; set; }
    }
}
