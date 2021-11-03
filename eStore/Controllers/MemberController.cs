using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository memberRepository = null;
        private readonly ApplicationDbContext _db;

        public MemberController() => memberRepository = new MemberRepository();

        public IActionResult Index(IEnumerable<MemberObject> memberList)
        {
            if (memberList.Count() == 0)
            {
                memberRepository = new MemberRepository();
                memberList = memberRepository.GetMemberList();
            }
            return View(memberList);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberObject obj)
        {
            try
            {
                if (ModelState.IsValid)
                {                 
                    memberRepository.InsertMember(obj);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(obj);
            }
        }

        // GET: MemberController/Update/[id]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = memberRepository.GetMemberById(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);
        }

        // POST: MemberController/Update/[id]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, MemberObject mem)
        {
            try
            {
                if (id != mem.MemberId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    memberRepository.UpdateMember(mem);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Delete/[id]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = memberRepository.GetMemberById(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);
        }

        // POST: ProductController/Delete/[id]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult SearchByMemberEmail(string memEmail)
        {
            var memList = memberRepository.GetMemberListByEmail(memEmail);
            return View("Index", memList);
        }

    }
}
