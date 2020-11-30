using Converto;
using HamnAdministration.Bestallningar;
using HamnAdministration.BestallningarQuery;
using HamnAdministration.Meddelanden.Requests;
using HamnAdministration.Meddelanden.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HamnAdministration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkeringController : Controller
    {
        private readonly ParkeringBestallHelper _parkeringBestallHelper;
        private readonly ParkeringQueryHelper _parkeringQueryHelper;

        public ParkeringController(ParkeringBestallHelper parkeringBestallHelper, ParkeringQueryHelper parkeringQueryHelper)
        {
            _parkeringBestallHelper = parkeringBestallHelper;
            _parkeringQueryHelper = parkeringQueryHelper;
        }

        /// <summary>
        /// Skapa en ny parkeringsplats med ett totalt antal parkeringsplatser
        /// </summary>
        /// <returns>A string status</returns>
        [HttpPost]
        public void SkapaParkering([FromBody] SkapaParkeringRequest request)
        {
            var command = request.ConvertTo<SkapaParkeringBestall>();
            _parkeringBestallHelper.Helper(command);
        }

        /// <summary>
        /// Få alla tillgängliga parkeringsområden
        /// </summary>
        /// <returns>A string status</returns>
        [HttpGet]
        public IEnumerable<ParkeringInformation> GetAllParkeringInformations()
        {
            var query = new GetAllParkeringInformationQuery();
            return _parkeringQueryHelper.Helper(query);
        }


        /// <summary>
        /// Få totalt tillgängliga lediga parkeringsplatser
        /// </summary>
        /// <returns>A string status</returns>
        [HttpGet("availablePlaces/count")]
        public int GetTotalLedigaPlatserQuery()
        {
            var query = new GetTotalLedigaPlatserQuery();
            return _parkeringQueryHelper.Helper(query);
        }

        // NOT WORKING ???
        /*
        [HttpGet("availablePlaces/random")]
        public ParkeringPlatsInformation GetRandomLedigaPlatser()
        {
            var query = new GetRandomLedigaPlatser();
            return _parkeringQueryHelper.Helper(query);
        }
        */

        /// <summary>
        /// Kontrollera all information för en specifik parkeringsplats
        /// </summary>
        /// <returns>A string status</returns>
        [HttpGet("{parkeringsNamn}")]
        public ParkeringInformation GetParkeringInformation(string parkeringsNamn)
        {
            var query = new GetParkeringInformationQuery { ParkeringsNamn = parkeringsNamn };
            return _parkeringQueryHelper.Helper(query);
        }


        /// <summary>
        /// Öppna en hel parkeringsplats
        /// </summary>
        /// <returns>A string status</returns>
        [HttpPost("{parkeringsNamn}/open")]
        public void OpenParkering(string parkeringsNamn)
        {
            var command = new OppenParkeringBestall { ParkeringsNamn = parkeringsNamn };
            _parkeringBestallHelper.Helper(command);
        }


        /// <summary>
        /// Stäng en hel parkeringsplats
        /// </summary>
        /// <returns>A string status</returns>
        [HttpPost("{parkeringsNamn}/close")]
        public void SlutParkering(string parkeringsNamn)
        {
            var command = new SlutParkeringBestall { ParkeringsNamn = parkeringsNamn };
            _parkeringBestallHelper.Helper(command);
        }


        /// <summary>
        /// Boka en parkeringsplats och välj var
        /// </summary>
        /// <returns>A string status</returns>
        [HttpPost("{parkeringsNamn}/{platsNummer}/take")]
        public void TaParkeringsPlatsen(string parkeringsNamn, int platsNummer, ParkeringType type)
        {
            var command = new TaParkeringsPlatsenBestall
            {
                ParkeringsNamn = parkeringsNamn,
                PlatsNummer = platsNummer,
                ParkeringType = (int)type
            };
            _parkeringBestallHelper.Helper(command);
        }

        public enum ParkeringType
        {
            Lastfartyg = 4,
            Motorbåt = 2,
            Segelbåt =1
        }



        /// <summary>
        /// Lämna en parkeringsplats och välj var
        /// </summary>
        /// <returns>A string status</returns>
        [HttpPost("{parkeringsNamn}/{platsNummer}/leave")]
        public void LamnaParkeringPlats(string parkeringsNamn, int platsNummer)
        {
            var command = new LamnaParkeringPlatsBestall
            {
                ParkeringsNamn = parkeringsNamn,
                PlatsNummer = platsNummer
            };
            _parkeringBestallHelper.Helper(command);
        }
    }
}
