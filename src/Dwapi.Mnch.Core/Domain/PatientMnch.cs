﻿using System;
using Dwapi.Mnch.SharedKernel.Model;

namespace Dwapi.Mnch.Core.Domain
{
    public class PatientMnch : Entity<Guid>
    {

        public virtual string MnchNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }


        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public string Serial { get; set; }
        public DateTime DateExtracted { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string QueueId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? StatusDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int? EncounterId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string TestedBefore { get; set; }
        public int? MonthsLastTested { get; set; }
        public string ClientTestedAs { get; set; }
        public string StrategyHTS { get; set; }
        public string TestKitName1 { get; set; }
        public string TestKitLotNumber1 { get; set; }
        public DateTime? TestKitExpiryDate1 { get; set; }
        public string TestResultsHTS1 { get; set; }
        public string TestKitName2 { get; set; }
        public string TestKitLotNumber2 { get; set; }
        public string TestKitExpiryDate2 { get; set; }
        public string TestResultsHTS2 { get; set; }
        public string FinalResultHTS { get; set; }
        public string FinalResultsGiven { get; set; }
        public string TBScreeningHTS { get; set; }
        public string ClientSelfTested { get; set; }
        public string CoupleDiscordant { get; set; }
        public string TestType { get; set; }

        public string KeyPopulationType { get; set; }
        public string PopulationType{ get; set; }
        public string PatientDisabled{ get; set; }
        public string DisabilityType { get; set; }
        public string PatientConsented{ get; set; }
        public  string County	 { get; set; }
        public  string  SubCounty	 { get; set; }
        public  string Ward	 { get; set; }

        public Guid FacilityId { get; set; }



        public PatientMnch()
        {
        }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
