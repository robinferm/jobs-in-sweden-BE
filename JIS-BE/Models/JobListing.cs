using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Models
{
    [BsonIgnoreExtraElements]
    public class JobListing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public object external_id { get; set; }
        public string webpage_url { get; set; }
        public object logo_url { get; set; }
        [BsonElement("headline")]
        public string Headline { get; set; }
        [BsonElement("application_deadline")]
        public DateTime Deadline { get; set; }
        //public int number_of_vacancies { get; set; }
        [BsonElement("description")]
        public Description description { get; set; }
        //    public Employment_Type employment_type { get; set; }
        public Salary_Type salary_type { get; set; }
        public object salary_description { get; set; }
        public Duration duration { get; set; }
        public Working_Hours_Type working_hours_type { get; set; }

        public Scope_Of_Work? scope_of_work { get; set; }
        public object access { get; set; }
        public Employer employer { get; set; }
        public Application_Details application_details { get; set; }
        public bool experience_required { get; set; }
        public bool access_to_own_car { get; set; }
        public bool driving_license_required { get; set; }
        public object driving_license { get; set; }
        public Occupation occupation { get; set; }
        public Occupation_Group occupation_group { get; set; }
        public Occupation_Field occupation_field { get; set; }
        //public Workplace_Address workplace_address { get; set; }
        public Must_Have must_have { get; set; }
        public Nice_To_Have nice_to_have { get; set; }
        public DateTime publication_date { get; set; }
        public DateTime last_publication_date { get; set; }
        public bool removed { get; set; }
        public object removed_date { get; set; }
        public string source_type { get; set; }
        //[BsonRepresentation(BsonType.Timestamp, AllowTruncation = true)]
        //[BsonElement]
        public object timestamp { get; set; }


        public class Description
        {
            public string text { get; set; }
            public object text_formatted { get; set; }
            public object company_information { get; set; }
            public object needs { get; set; }
            public object requirements { get; set; }
            public object conditions { get; set; }
        }

        public class Employment_Type
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Salary_Type
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Duration
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Working_Hours_Type
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Scope_Of_Work
        {
            public int? min { get; set; }
            public int? max { get; set; }
        }

        public class Employer
        {
            public object phone_number { get; set; }
            public object email { get; set; }
            public object url { get; set; }
            public object organization_number { get; set; }
            public string name { get; set; }
            public string workplace { get; set; }
        }

        public class Application_Details
        {
            public object information { get; set; }
            public object reference { get; set; }
            public object email { get; set; }
            public bool via_af { get; set; }
            public string url { get; set; }
            public object other { get; set; }
        }

        public class Occupation
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Occupation_Group
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Occupation_Field
        {
            public string concept_id { get; set; }
            public string label { get; set; }
            public string legacy_ams_taxonomy_id { get; set; }
        }

        public class Workplace_Address
        {
            public string municipality { get; set; }
            public string municipality_code { get; set; }
            public string municipality_concept_id { get; set; }
            public string region { get; set; }
            public string region_code { get; set; }
            public string region_concept_id { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
            public string country_concept_id { get; set; }
            public object street_address { get; set; }
            public string postcode { get; set; }
            public string city { get; set; }
            //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
            public float[] coordinates { get; set; }
        }

        public class Must_Have
        {
            public object[] skills { get; set; }
            public object[] languages { get; set; }
            public object[] work_experiences { get; set; }
        }

        public class Nice_To_Have
        {
            public object[] skills { get; set; }
            public object[] languages { get; set; }
            public object[] work_experiences { get; set; }
        }

        public class Timestamp
        {
            public string numberLong { get; set; }
        }
    }
}
