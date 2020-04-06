using Microsoft.AspNetCore.Mvc;
using TriviaBoxServer.Managers;
using TriviaBoxServer.Models.Request;
using TriviaBoxServer.Models.Response;
using System;
using System.Threading.Tasks;

namespace TriviaBoxServer.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private IRoomManager _manager;
        public RoomController(IRoomManager manager)
        {
            _manager = manager;
        }

        // GET api/room/{roomCode}
        [HttpGet]
        [Route("{roomCode}")]
        public async Task<IActionResult> GetRoom(string roomCode)
        {
            var result = await _manager.GetRoom(roomCode);
            return ApiResult(result, () => Ok(result.Item));
        }

        // POST api/room
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody]AddRoomRequest request)
        {
            var result = await _manager.AddRoom(request);
            return ApiResult(result, () => Ok(result.Item));
        }

        public IActionResult ApiResult<T>(Result<T> result, Func<IActionResult> successFn)
        {
            var (_, error) = result;

            if (error == null)
            {
                return successFn(); 
            }

            return StatusCode((int)error.Code, Json(error));
        }
    }
}
