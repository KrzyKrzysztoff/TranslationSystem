using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationSystem.Models;

namespace TranslationSystem.Services
{
    public interface ITranslatorService
    {
        Task<TranslatorViewModel> Translate(TranslatorViewModel translatorViewModel);
    }
}
