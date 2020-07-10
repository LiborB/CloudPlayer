using CloudPlayerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CloudPlayerAPI.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly CloudPlayerContext _context;
        public BaseApiController(CloudPlayerContext dbContext)
        {
            _context = dbContext;
        }

        public BaseApiController()
        {
            
        }
    }
}