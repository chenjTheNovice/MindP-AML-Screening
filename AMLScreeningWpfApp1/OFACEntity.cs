using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLScreeningWpfApp1
{
    public class OFACEntity
    {
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public string ProgramList { get; set; }

        public Address AddressInfo { get; set; }
        public DateOfBirthItem DateOfBirthInfo { get; set; }
        public PlaceOfBirthItem PlaceOfBirthInfo { get; set; }
        public Citizenship CitizenshipInfo { get; set; }
        public Nationality NationalityInfo { get; set; }
        public VesselInfo VesselInfoDetails { get; set; }
    }

    public class Address
    {
        public string Uid { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }

    public class DateOfBirthItem
    {
        public string Uid { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class PlaceOfBirthItem
    {
        public string Uid { get; set; }
        public string PlaceOfBirth { get; set; }
    }

    public class Citizenship
    {
        public string Uid { get; set; }
        public string CitizenshipCountry { get; set; } // Changed property name to avoid conflict
    }

    public class Nationality
    {
        public string Uid { get; set; }
        public string NationalityCountry { get; set; } // Changed property name to avoid conflict
    }

    public class VesselInfo
    {
        public string CallSign { get; set; }
        public string VesselType { get; set; }
        public string VesselFlag { get; set; }
        public string VesselOwner { get; set; }
        public string GrossRegisteredTonnage { get; set; }
        public string Tonnage { get; set; }
    }
}
