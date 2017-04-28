using CleanCode;
using MvcCms.Data;
using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

using System.Web.Mvc;

namespace MvcCms.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private readonly IPostRepository _posts;
        private readonly IRecordRepository _records;
        private readonly IUserRepository _users;
        private readonly int _pageSize = 2;
        //test 123
        public HomeController() : this(new PostRepository(), new RecordRepository(), new UserRepository()) { }

        public HomeController(IPostRepository postRepository, IRecordRepository recordRepository, IUserRepository userRepository)
        {
            _posts = postRepository;
            _records = recordRepository;
            _users = userRepository;
        }

        // GET: Default
        // root/
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var posts = await _posts.GetPageAsync(1, _pageSize);

            ViewBag.PreviousPage = 0;
            ViewBag.NextPage = (Decimal.Divide(_posts.CountPublished, _pageSize) > 1) ? 2 : -1;
            return View(posts);
        }

        // GET: Default
        // root/
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            MainPage mp = new MainPage();
            mp.Record = new Record();
            mp.RecordList = await _records.GetAllAsync();
            return View(mp);
        }

        // /admin/post/create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecord1(Record model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetLoggedInUser();
            model.AuthorId = user.Id;

            //CreateXml.createXMLFromStaticData(model);
            try
            {
                _records.Create(model);

                MainPage mp = new MainPage();
                mp.Record = model;
                mp.RecordList = await _records.GetAllAsync();
                return View(mp);
            }
            catch (Exception e)
            {
                MainPage mp = new MainPage();
                mp.Record = new Record();
                mp.RecordList = await _records.GetAllAsync();

                ModelState.AddModelError("key", e);
                return View(mp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecord(Record model)
        {
            if (ModelState.IsValid)
            {
                //Save or whatever you want to do 
            }

            CreateXMLFile xmlFile = new CreateXMLFile();
            IList<Record> records = new List<Record>();
            records.Add(model);

            xmlFile.createXMLFile(records);
            //////Barisin fonksiyonu
            MainPage mp = new MainPage();
            mp.Record = new Record();
            mp.RecordList = await _records.GetAllAsync();

            return View(mp);
        }

        [Route("page/{page:int}")]
        public async Task<ActionResult> Page(int page = 1)
        {
            if (page < 2)
            {
                return RedirectToAction("index");
            }

            var posts = await _posts.GetPageAsync(page, _pageSize);

            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = (Decimal.Divide(_posts.CountPublished, _pageSize) > page) ? page + 1 : -1;

            return View("index", posts);
        }

        // root/posts/post-id
        [Route("posts/{postId}")]
        public async Task<ActionResult> Post(string postId)
        {
            var post = _posts.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // root/tags/tag-id
        [Route("tags/{tagId}")]
        public async Task<ActionResult> Tag(string tagId)
        {
            var posts = await _posts.GetPostsByTagAsync(tagId);

            if (!posts.Any())
            {
                return HttpNotFound();
            }

            ViewBag.Tag = tagId;

            return View(posts);
        }

        private async Task<CmsUser> GetLoggedInUser()
        {
            return await _users.GetUserByNameAsync(User.Identity.Name);
        }

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }
    }
}