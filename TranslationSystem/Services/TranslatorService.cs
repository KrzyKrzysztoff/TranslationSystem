using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TranslationSystem.Models;
using TranslationSystemMVC.Database;

namespace TranslationSystem.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly TranslationDbContext _translationDbContext;

        public TranslatorService(TranslationDbContext translationDbContext)
        {
            _translationDbContext = translationDbContext;
        }

        public async Task<TranslatorViewModel> Translate(TranslatorViewModel translatorViewModel)
        {

                if (String.IsNullOrWhiteSpace(translatorViewModel.Contents.Text))
                {
                    return null;
                }

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var content = new StringContent(JsonConvert.SerializeObject(translatorViewModel.Contents, settings), Encoding.UTF8, "application/json");

                //consumption of api
                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://api.funtranslations.com/translate/leetspeak", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();

                            translatorViewModel = JsonConvert.DeserializeObject<TranslatorViewModel>(apiResponse);

                            translatorViewModel.Contents.CurrentDate = DateTime.Now;

                            await _translationDbContext.Content.AddAsync(translatorViewModel.Contents);

                            await _translationDbContext.SaveChangesAsync();

                            return translatorViewModel;
                        }

                        return null;
                    }
                }
          
        }

    }
}
