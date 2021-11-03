using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMemberList();

        void InsertMember(MemberObject member);

        void UpdateMember(MemberObject member);

        MemberObject GetMemberById(int memberId);

        void DeleteMember(int memberId);

        MemberObject GetMemberByEmail(string email);
        IEnumerable<MemberObject> GetMemberListByEmail(string email);

        
    }
}
