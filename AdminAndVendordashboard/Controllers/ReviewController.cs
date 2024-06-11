using Application.Service.ItemReviewF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ReviewController : ControllerBase
    {
        private readonly IItemReviewService  _itemReviewService;
    }
}
