using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
   public class SearchedPersonLanguage
    {

        // One of these two fields should be non nullable at runtime.
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }

       
        public short LanguageId { get; set; }
        public short? ReadingExpertise { get; set; }
        public short? UnderstandingExpertise { get; set; }
        public short? WritingExpertise { get; set; }
        public short? SpeakingExpertise { get; set; }
        

        public string LanguageText { get; set; }
        public string ReadingExpertiseText { get; set; }
        public string UnderstandingExpertiseText { get; set; }
        public string WritingExpertiseText { get; set; }
        public string SpeakingExpertiseText { get; set; }



    }
}
