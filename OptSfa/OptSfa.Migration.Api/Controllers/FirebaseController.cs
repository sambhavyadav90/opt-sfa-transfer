using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirebaseController : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;

        public FirebaseController(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpGet("messages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessages()
        {
            var messages = await _firebaseService.GetMessagesAsync("messages");
            return Ok(messages);
        }
    }
}