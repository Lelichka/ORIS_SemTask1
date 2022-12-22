using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using SemTask1.Attributes;
using SemTask1.Models;
using SemTask1.MyORM;
using SemTask1.Results;

namespace SemTask1.Controller
{
    [ApiController("/")]
    public class MainController
    {
        private readonly string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfSemTask1;Integrated Security=True";
        [HttpGet("")]
        [AuthCookieRequired]
        public MainResult GetMainPage(int userId)
        {
            var comments = new CommentsDAO(strConnection).FindAll().ToList();
            var styles = new DanceStylesDAO(strConnection).FindAll().ToList();
            return new MainResult(styles,comments, userId != 0);
        }
        
    }
}
