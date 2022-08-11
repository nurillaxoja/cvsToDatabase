using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Task.Models
{
    public class Personnel_Records
    {

        [Key]
        [Ignore]
        public int Id { get; set; }

        [Index(0)]
        public string Payroll_Number { get; set; }

        [Index(1)]
        public string Forenames { get; set; }

        [Index(2)]
        public string Surenames { get; set; }

        [Index(3)]
        public string Date_of_Birth { get; set; }
      

        [Index(4)]
        public int Telephone { get; set; }

        [Index(5)]
        public int Mobile { get; set; }

        [Index(6)]
        public string Address { get; set; }

        [Index(7)]
        public string Address_2 { get; set; }

        [Index(8)]
        public string Postcode { get; set; }

        [Index(9)]
        public string EMail_Home { get; set; }


        [Index(10)]
        [Format("dd-MM-yyyy")]
        public string Start_Date { get; set; }

    }
}
