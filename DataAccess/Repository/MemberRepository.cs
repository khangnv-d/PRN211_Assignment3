using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<MemberObject> GetMemberList()
            => MemberDAO.Instance.GetMemberList();

        public void InsertMember(MemberObject member) => MemberDAO.Instance.InsertMember(member);

        public void UpdateMember(MemberObject member) => MemberDAO.Instance.UpdateMember(member);

        public MemberObject GetMemberById(int memberId) => MemberDAO.Instance.GetMemberByID(memberId);

        public void DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);

        public IEnumerable<MemberObject> GetMemberListByEmail(string email) => MemberDAO.Instance.GetMemberListByEmail(email);

        public MemberObject GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);
    }
}
