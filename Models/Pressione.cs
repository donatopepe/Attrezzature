using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class Pressione : PressValv
    {
        [Display(Name = "Numero seriale", Prompt = "Inserire numero seriale di fabbrica", Description = "Numero seriale di fabbrica")]
        public string SerialNoFabbrica { get; set; }
        [Display(Name = "Tecnico di riferimento", Prompt = "Inserire tecnico di riferimento", Description = "Tecnico di riferimento")]
        public ApplicationUser TecnicoReferente { get; set; }
        [Display(Name = "Volume in l", Prompt = "Volume in l", Description = "Volume in l")]
        public int? Volume { get; set; }
        [Display(Name = "Pressione massima [bar]", Prompt = "Pressione massima [bar]", Description = "Pressione massima [bar]")]
        public int? PressioneMassima { get; set; }
        [Display(Name = "Fluido", Prompt = "Fluido", Description = "Fluido")]
        public string Fluido { get; set; }
        [Display(Name = "Tipo attrezzatura", Prompt = "Tipo attrezzatura", Description = "Tipo attrezzatura")]
        public string Descrizione { get; set; }
        [Display(Name = "Ubicazione", Prompt = "Ubicazione", Description = "Ubicazione")]
        public string Ubicazione { get; set; }
        [Display(Name = "Norma", Prompt = "Norma", Description = "Norma")]
        public string Norma { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Verifica primo impianto", Prompt = "Verifica primo impianto", Description = "Verifica primo impianto")]
        public DateTime? VerificaPrimoImpianto { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Prima verifica funzionamento", Prompt = "Prima verifica funzionamento", Description = "Prima verifica funzionamento")]
        public DateTime? PrimaVerificaFunzionalita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Verifica funzionamento", Prompt = "Verifica funzionamento", Description = "Verifica funzionamento")]
        public DateTime? VerificaFunzionalita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Verifica integrità", Prompt = "Verifica integrità", Description = "Verifica integrità")]
        public DateTime? VerificaIntegrita { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scadenza funzionamento", Prompt = "Scadenza funzionamento", Description = "Scadenza funzionamento")]
        public DateTime? ScadenzaFunzionalita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Scadenza integrità", Prompt = "Scadenza integrità", Description = "Scadenza integrità")]
        public DateTime? ScadenzaIntegrita { get; set; }





        [Display(Name = "Disco di rottura montato", Prompt = "Disco di rottura montato", Description = "Disco di rottura montato")]
        public Disco DiscoMontato { get; set; }



        [Display(Name = "Valvola montata", Prompt = "Valvola montata", Description = "Valvola montata")]
        public List<PressioneValvola> ValvolaMontata { get; set; }


    }
}
