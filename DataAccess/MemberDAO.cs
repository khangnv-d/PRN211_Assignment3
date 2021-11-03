using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class MemberDAO
    {

        private static MemberDAO instance = null;
        private static readonly object instanceLook = new object();
       

        private MemberDAO()
        {
        }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MemberObject> GetMemberList()
        {
            IEnumerable<MemberObject> memList;
            try
            {
                using var context = new ApplicationDbContext();
                memList = context.MemberObject.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return memList;
        }

        public MemberObject GetMemberByEmail(string email)
        {
            using var context = new ApplicationDbContext();

            return context.MemberObject.SingleOrDefault(mem => mem.Email.Equals(email));
        }

        public MemberObject GetMemberByID(int memId)
        {
            MemberObject member = null;
            try
            {
                using var context = new ApplicationDbContext();
                member = context.MemberObject.SingleOrDefault(m => m.MemberId == memId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void InsertMember(MemberObject mem)
        {
            using var context = new ApplicationDbContext();
            context.MemberObject.Add(mem);
            context.SaveChanges();
        }

        public void UpdateMember(MemberObject mem)
        {
            try
            {              
                using var context = new ApplicationDbContext();
                context.MemberObject.Update(mem);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void DeleteMember(int memberId)
        {
            try
            {
                MemberObject mem = GetMemberByID(memberId);
                using var context = new ApplicationDbContext();
                context.MemberObject.Remove(mem);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MemberObject> GetMemberListByEmail(string email)
        {
            IEnumerable<MemberObject> memList = null;
            try
            {
                using var context = new ApplicationDbContext();
                if (email is null || email == "")
                {
                    memList = context.MemberObject.ToList();
                }
                else
                {
                    memList = context.MemberObject.Where(mem => mem.Email.ToUpper().Contains(email.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return memList;
        }

    }
}
