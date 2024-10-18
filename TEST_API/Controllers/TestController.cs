using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TEST_API.Models;
using TEST_API.Repositories;

namespace TEST_API.Controllers
{
    public class TestController : ApiController
    {
        // GET: Test
        public IEnumerable<GeneralBranchPerformanceRank> Get()
        {
            var _repository = new TestRepository(System.Configuration.ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString);
            var performanceData = _repository.CallStoredProcedure(9, "Southern 1");
            return performanceData;
        }
    }
}