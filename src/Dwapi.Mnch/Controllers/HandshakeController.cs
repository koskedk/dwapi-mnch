using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.Mnch.Core.Interfaces.Repository;
using Dwapi.Mnch.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Mnch.Controllers
{
    [Route("api/mnch/[controller]")]
    [ApiController]
    public class HandshakeController : ControllerBase
    {
        private readonly IManifestRepository _manifestRepository;
        private readonly ILiveSyncService _liveSyncService;

        public HandshakeController(IManifestRepository manifestRepository, ILiveSyncService liveSyncService)
        {
            _manifestRepository = manifestRepository;
            _liveSyncService = liveSyncService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid session)
        {
            try
            {
                await _manifestRepository.EndSession(session);
                var handshakes = _manifestRepository
                    .GetSessionHandshakes(session)
                    .ToList();
                await _liveSyncService.SyncHandshake(handshakes);
                return Ok(session);
            }
            catch (Exception e)
            {
                Log.Error(e, "handshake error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
