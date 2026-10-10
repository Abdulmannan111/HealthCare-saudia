using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities.General
{
    [BsonIgnoreExtraElements]
    public class GeneralComplexes : AuditInfoComplete
    {


        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;


        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;


        [BsonElement("city")]
        public string City { get; set; } = string.Empty;

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("auditor_email")]
        public string AuditorEmail { get; set; } = string.Empty;

        [BsonElement("auditor_phone")]
        public string AuditorPhone { get; set; } = string.Empty;


        [BsonElement("street_name")]
        public string StreetName { get; set; } = string.Empty;


        [BsonElement("neighborhood")]
        public string Neighborhood { get; set; } = string.Empty;


        [BsonElement("reason_for_visiting")]
        public string ReasonForVisiting { get; set; } = string.Empty;


        [BsonElement("available_on_ground")]
        public string AvailableOnGround { get; set; } = string.Empty;


        [BsonElement("note")]
        public string Note { get; set; } = string.Empty;


        [BsonElement("image")]
        public string[]? Image { get; set; }



        [BsonElement("owner_name")]
        public string OwnerName { get; set; } = string.Empty;


        [BsonElement("institution_email")]
        public string InstitutionEmail { get; set; } = string.Empty;


        [BsonElement("cbahi_certified")]
        public string CbahiCertified { get; set; } = string.Empty;


        [BsonElement("facility_license_number")]
        public string FacilityLicenseNumber { get; set; } = string.Empty;


        [BsonElement("license_issue_date")]
        public string LicenseIssueDate { get; set; } = string.Empty;


        [BsonElement("license_expiry_date")]
        public string LicenseExpiryDate { get; set; } = string.Empty;


        [BsonElement("name_of_director")]
        public string NameOfDirector { get; set; } = string.Empty;


        [BsonElement("director_qualification")]
        public string DirectorQualification { get; set; } = string.Empty;


        [BsonElement("name_of_technical_supervisor")]
        public string NameOfTechnicalSupervisor { get; set; } = string.Empty;


        [BsonElement("technical_supervisor_specialty")]
        public string TechnicalSupervisorSpecialty { get; set; } = string.Empty;


        [BsonElement("is_specialization_classified")]
        public string IsSpecializationClassified { get; set; } = string.Empty;


        [BsonElement("inspectors")]
        public string[]? Inspectors { get; set; }



        [BsonElement("staff_availability")]
        public string[]? StaffAvailability { get; set; }


        [BsonElement("class_name")]
        public string ClassName { get; set; } = string.Empty;


        [BsonElement("cbahi_certified_other")]
        public string? CbahiCertifiedOther { get; set; }


        [BsonElement("identification_file")]
        public string? IdentificationFile { get; set; }


        [BsonElement("identification_notes")]
        public string? IdentificationNotes { get; set; }


        [BsonElement("is_specialization_classified_other")]
        public string? IsSpecializationClassifiedOther { get; set; }


        [BsonElement("medical_waste_that")]
        public string Medicalwastethat { get; set; } = string.Empty;


        [BsonElement("medical_waste_that_other")]
        public string? MedicalWasteThatOther { get; set; }


        [BsonElement("medical_waste_that_file")]
        public string? MedicalWasteThatFile { get; set; }


        [BsonElement("for_the_safe")]
        public string ForTheSafe { get; set; } = string.Empty;


        [BsonElement("for_the_safe_other")]
        public string? ForTheSafeOther { get; set; }


        [BsonElement("for_the_safe_file")]
        public string? ForTheSafeFile { get; set; }


        [BsonElement("infection_control_and")]
        public string InfectionControlAnd { get; set; } = string.Empty;


        [BsonElement("infection_control_and_other")]
        public string? InfectionControlAndOther { get; set; }


        [BsonElement("infection_control_and_file")]
        public string? InfectionControlAndFile { get; set; }


        [BsonElement("implementation_of_the")]
        public string ImplementationOfThe { get; set; } = string.Empty;


        [BsonElement("implementation_of_the_other")]
        public string? ImplementationOfTheOther { get; set; }


        [BsonElement("implementation_of_the_file")]
        public string? ImplementationOfTheFile { get; set; }

        [BsonElement("with_the_statistical")]
        public string WithTheStatistical { get; set; } = string.Empty;


        [BsonElement("with_the_statistical_other")]
        public string? WithTheStatisticalOther { get; set; }


        [BsonElement("with_the_statistical_file")]
        public string? WithTheStatisticalFile { get; set; }

        [BsonElement("all_instructions_and")]
        public string AllInstructionsAnd { get; set; } = string.Empty;


        [BsonElement("all_instructions_and_other")]
        public string? AllInstructionsAndOther { get; set; }


        [BsonElement("all_instructions_and_file")]
        public string? AllInstructionsAndFile { get; set; }

        [BsonElement("to_report_the")]
        public string ToReportThe { get; set; } = string.Empty;


        [BsonElement("to_report_the_other")]
        public string? ToReportTheOther { get; set; }


        [BsonElement("to_report_the_file")]
        public string? ToReportTheFile { get; set; }

        [BsonElement("a_sterilization_room")]
        public string ASterilizationRoom { get; set; } = string.Empty;


        [BsonElement("a_sterilization_room_other")]
        public string? ASterilizationRoomOther { get; set; }


        [BsonElement("a_sterilization_room_file")]
        public string? ASterilizationRoomFile { get; set; }

        [BsonElement("a_health_certificate")]
        public string AHealthCertificate { get; set; } = string.Empty;


        [BsonElement("a_health_certificate_other")]
        public string? AHealthCertificateOther { get; set; }


        [BsonElement("a_health_certificate_file")]
        public string? AHealthCertificateFile { get; set; }


        [BsonElement("the_saudi_center")]
        public string TheSaudiCenter { get; set; } = string.Empty;


        [BsonElement("the_saudi_center_other")]
        public string? TheSaudiCenterOther { get; set; }


        [BsonElement("the_saudi_center_file")]
        public string? TheSaudiCenterFile { get; set; }

        [BsonElement("there_are_sufficient")]
        public string ThereAreSufficient { get; set; } = string.Empty;


        [BsonElement("there_are_sufficient_other")]
        public string? ThereAreSufficientOther { get; set; }


        [BsonElement("there_are_sufficient_file")]
        public string? ThereAreSufficientFile { get; set; }

        [BsonElement("stages_conform_to")]
        public string StagesConformTo { get; set; } = string.Empty;


        [BsonElement("stages_conform_to_other")]
        public string? StagesConformToOther { get; set; }


        [BsonElement("stages_conform_to_file")]
        public string? StagesConformToFile { get; set; }

        [BsonElement("floor_or_on")]
        public string FloorOrOn { get; set; } = string.Empty;


        [BsonElement("floor_or_on_other")]
        public string? FloorOrOnOther { get; set; }


        [BsonElement("floor_or_on_file")]
        public string? FloorOrOnFile { get; set; }

        [BsonElement("that_can_accommodate")]
        public string ThatCanAccommodate { get; set; } = string.Empty;


        [BsonElement("that_can_accommodate_other")]
        public string? ThatCanAccommodateOther { get; set; }


        [BsonElement("that_can_accommodate_file")]
        public string? ThatCanAccommodateFile { get; set; }

        [BsonElement("modern")]
        public string Modern { get; set; } = string.Empty;


        [BsonElement("modern_other")]
        public string? ModernOther { get; set; }


        [BsonElement("modern_file")]
        public string? ModernFile { get; set; }

        [BsonElement("accident_or_death")]
        public string AccidentOrDeath { get; set; } = string.Empty;


        [BsonElement("accident_or_death_other")]
        public string? AccidentOrDeathOther { get; set; }


        [BsonElement("accident_or_death_file")]
        public string? AccidentOrDeathFile { get; set; }

        [BsonElement("all_serious_emergency")]
        public string AllSeriousEmergency { get; set; } = string.Empty;


        [BsonElement("all_serious_emergency_other")]
        public string? AllSeriousEmergencyOther { get; set; }


        [BsonElement("all_serious_emergency_file")]
        public string? AllSeriousEmergencyFile { get; set; }

        [BsonElement("availability_of_emergency")]
        public string AvailabilityOfEmergency { get; set; } = string.Empty;


        [BsonElement("availability_of_emergency_other")]
        public string? AvailabilityOfEmergencyOther { get; set; }


        [BsonElement("availability_of_emergency_file")]
        public string? AvailabilityOfEmergencyFile { get; set; }

        [BsonElement("of_a_doctor")]
        public string OfADoctor { get; set; } = string.Empty;


        [BsonElement("of_a_doctor_other")]
        public string? OfADoctorOther { get; set; }


        [BsonElement("of_a_doctor_file")]
        public string? OfADoctorFile { get; set; }

        [BsonElement("all_requirements_apply")]
        public string AllRequirementsApply { get; set; } = string.Empty;


        [BsonElement("all_requirements_apply_other")]
        public string? AllRequirementsApplyOther { get; set; }


        [BsonElement("all_requirements_apply_file")]
        public string? AllRequirementsApplyFile { get; set; }

        [BsonElement("entrance_conforming_to")]
        public string EntranceConformingTo { get; set; } = string.Empty;


        [BsonElement("entrance_conforming_to_other")]
        public string? EntranceConformingToOther { get; set; }


        [BsonElement("entrance_conforming_to_file")]
        public string? EntranceConformingToFile { get; set; }

        [BsonElement("out_in_the")]
        public string OutInThe { get; set; } = string.Empty;


        [BsonElement("out_in_the_other")]
        public string? OutInTheOther { get; set; }


        [BsonElement("out_in_the_file")]
        public string? OutInTheFile { get; set; }

        [BsonElement("patients_conforming_to")]
        public string PatientsConformingTo { get; set; } = string.Empty;


        [BsonElement("patients_conforming_to_other")]
        public string? PatientsConformingToOther { get; set; }


        [BsonElement("patients_conforming_to_file")]
        public string? PatientsConformingToFile { get; set; }

        [BsonElement("necessary_equipment_conforming")]
        public string NecessaryEquipmentConforming { get; set; } = string.Empty;


        [BsonElement("necessary_equipment_conforming_other")]
        public string? NecessaryEquipmentConformingOther { get; set; }


        [BsonElement("necessary_equipment_conforming_file")]
        public string? NecessaryEquipmentConformingFile { get; set; }

        [BsonElement("operating_rooms_requirements")]
        public string OperatingRoomsRequirements { get; set; } = string.Empty;


        [BsonElement("operating_rooms_requirements_other")]
        public string? OperatingRoomsRequirementsOther { get; set; }


        [BsonElement("operating_rooms_requirements_file")]
        public string? OperatingRoomsRequirementsFile { get; set; }

        [BsonElement("operating_rooms_comply")]
        public string OperatingRoomsComply { get; set; } = string.Empty;


        [BsonElement("operating_rooms_comply_other")]
        public string? OperatingRoomsComplyOther { get; set; }


        [BsonElement("operating_rooms_comply_file")]
        public string? OperatingRoomsComplyFile { get; set; }

        [BsonElement("ophthalmology_comply_with")]
        public string OphthalmologyComplyWith { get; set; } = string.Empty;


        [BsonElement("ophthalmology_comply_with_other")]
        public string? OphthalmologyComplyWithOther { get; set; }


        [BsonElement("ophthalmology_comply_with_file")]
        public string? OphthalmologyComplyWithFile { get; set; }

        [BsonElement("areas_conform_to")]
        public string AreasConformTo { get; set; } = string.Empty;


        [BsonElement("areas_conform_to_other")]
        public string? AreasConformToOther { get; set; }


        [BsonElement("areas_conform_to_file")]
        public string? AreasConformToFile { get; set; }

        [BsonElement("rooms_conform_to")]
        public string RoomsConformTo { get; set; } = string.Empty;


        [BsonElement("rooms_conform_to_other")]
        public string? RoomsConformToOther { get; set; }


        [BsonElement("rooms_conform_to_file")]
        public string? RoomsConformToFile { get; set; }

        [BsonElement("anesthesia_of_the")]
        public string AnesthesiaOfThe { get; set; } = string.Empty;


        [BsonElement("anesthesia_of_the_other")]
        public string? AnesthesiaOfTheOther { get; set; }


        [BsonElement("anesthesia_of_the_file")]
        public string? AnesthesiaOfTheFile { get; set; }

        [BsonElement("and_procedures_conform")]
        public string AndProceduresConform { get; set; } = string.Empty;


        [BsonElement("and_procedures_conform_other")]
        public string? AndProceduresConformOther { get; set; }


        [BsonElement("and_procedures_conform_file")]
        public string? AndProceduresConformFile { get; set; }

        [BsonElement("policies_and_procedures")]
        public string PoliciesAndProcedures { get; set; } = string.Empty;


        [BsonElement("policies_and_procedures_other")]
        public string? PoliciesAndProceduresOther { get; set; }


        [BsonElement("policies_and_procedures_file")]
        public string? PoliciesAndProceduresFile { get; set; }

        [BsonElement("and_the_intensive")]
        public string AndTheIntensive { get; set; } = string.Empty;


        [BsonElement("and_the_intensive_other")]
        public string? AndTheIntensiveOther { get; set; }


        [BsonElement("and_the_intensive_file")]
        public string? AndTheIntensiveFile { get; set; }

        [BsonElement("needs_for_continuous")]
        public string NeedsForContinuous { get; set; } = string.Empty;


        [BsonElement("needs_for_continuous_other")]
        public string? NeedsForContinuousOther { get; set; }


        [BsonElement("needs_for_continuous_file")]
        public string? NeedsForContinuousFile { get; set; }

        [BsonElement("in_a_day")]
        public string InADay { get; set; } = string.Empty;


        [BsonElement("in_a_day_other")]
        public string? InADayOther { get; set; }


        [BsonElement("in_a_day_file")]
        public string? InADayFile { get; set; }

        [BsonElement("for_reporting_sentinel")]
        public string ForReportingSentinel { get; set; } = string.Empty;


        [BsonElement("for_reporting_sentinel_other")]
        public string? ForReportingSentinelOther { get; set; }


        [BsonElement("for_reporting_sentinel_file")]
        public string? ForReportingSentinelFile { get; set; }

        [BsonElement("licensed_and_all")]
        public string LicensedAndAll { get; set; } = string.Empty;


        [BsonElement("licensed_and_all_other")]
        public string? LicensedAndAllOther { get; set; }


        [BsonElement("licensed_and_all_file")]
        public string? LicensedAndAllFile { get; set; }


        [BsonElement("reportNumber")]
        public string ReportNumber { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }



    }
}
