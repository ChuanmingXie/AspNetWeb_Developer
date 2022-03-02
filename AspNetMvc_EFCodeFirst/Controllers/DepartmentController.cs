using AspNetMvc_EFCodeFirst.DAL;
using AspNetMvc_EFCodeFirst.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AspNetMvc_EFCodeFirst.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Department
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Administrator);
            return View(await departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //注释掉原先的代码,使用原生的SQL语句测试
            //Department department = await db.Departments.FindAsync(id);
            string query = "SELECT * FROM Department WHERE DepartmentID=@p0";
            Department department = await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync();
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName");
            return View();
        }

        // POST: Department/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // POST: Department/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id,byte[] rowVersion)
        {
            string[] fieldToBind = new string[] { "Name", "Budget", "StartDate", "InstructorID", "RowVersion" };
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var departmentToUpdate = await db.Departments.FindAsync(id);
            if (departmentToUpdate == null)
            {
                Department deletedDepartment = new Department();
                TryUpdateModel(deletedDepartment, fieldToBind);
                ModelState.AddModelError(string.Empty, "Unable to save changes. The department was deleted by another user.");
                ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", deletedDepartment.InstructorID);
                return View(deletedDepartment);
            }
            if (TryUpdateModel(departmentToUpdate, fieldToBind))
            {
                try
                {
                    db.Entry(departmentToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Department)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("", "不行");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();
                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("院系名称", "Current value:" + databaseValues.Name);
                        if (databaseValues.Budget != clientValues.Budget)
                            ModelState.AddModelError("研发预算", "Current value:" + string.Format("{0:c}", databaseValues.Budget));
                        if (databaseValues.StartDate != clientValues.StartDate)
                            ModelState.AddModelError("建立时间", "Current value:" + string.Format("{0:d}", databaseValues.StartDate));
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                            ModelState.AddModelError("系主任", "Current value:" + db.Instructors.Find(databaseValues.InstructorID).FullName);
                        ModelState.AddModelError(string.Empty, "您试图编辑的记录在您获得原始值后被另一个用户修改。" +
                            "编辑操作已取消，数据库中的当前值已显示。如果仍要编辑此记录，请再次单击“保存”按钮。否则，请单击“返回列表”超链接");
                    }
                }
                catch(RetryLimitExceededException /*dex*/)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }

        // GET: Department/Delete/5
        public async Task<ActionResult> Delete(int? id,bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage= "您试图编辑的记录在您获得原始值后被另一个用户修改。" +
                    "编辑操作已取消，数据库中的当前值已显示。如果仍要编辑此记录，请再次单击“保存”按钮。否则，请单击“返回列表”超链接";
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Department department)
        {
            try
            {
                //Department department = await db.Departments.FindAsync(id);
                //db.Departments.Remove(department);
                db.Entry(department).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete",new { concurrencyError=true,id=department.DepartmentID});
            }
            catch(DataException /*dex*/)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(department);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
