using AlertaSolo.DTO;
using AlertaSolo.Model;
using AlertaSolo.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlertaSolo.Pages.Sensores
{
    public class IndexModel : PageModel
    {
        private readonly ISensorService _sensorService;
        private readonly ILocalRiscoService _localRiscoService;

        public IndexModel(ISensorService sensorService, ILocalRiscoService localRiscoService)
        {
            _sensorService = sensorService;
            _localRiscoService = localRiscoService;
        }

        public List<SensorDTO> Sensores { get; set; } = new();
        public SelectList LocaisSelectList { get; set; }

        [BindProperty]
        public Sensor NovoSensor { get; set; }

        public async Task OnGetAsync()
        {
            var lista = await _sensorService.GetAllAsync();
            Sensores = lista.ToList();

            var locais = await _localRiscoService.GetAllAsync();
            LocaisSelectList = new SelectList(locais, "Id", "NomeLocal");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            NovoSensor.DataInstalacao = DateTime.Now;
            NovoSensor.QntdAlertas = 0;

            await _sensorService.CreateAsync(new SensorCreateDto
            {
                CodigoEsp32 = NovoSensor.CodigoEsp32,
                Status = NovoSensor.Status,
                TipoSensor = NovoSensor.TipoSensor,
                QntdAlertas = NovoSensor.QntdAlertas,
                LocalRiscoId = NovoSensor.LocalRiscoId
            });

            return RedirectToPage();
        }
    }
}
