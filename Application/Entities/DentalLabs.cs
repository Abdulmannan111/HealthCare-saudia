using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class DentalLabs : AuditInfoComplete
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

        
        [BsonElement("reception")]
        public string Reception { get; set; } = string.Empty;

        
        [BsonElement("reception_other")]
        public string? ReceptionOther { get; set; }

        
        [BsonElement("reception_file")]
        public string? ReceptionFile { get; set; }

        
        [BsonElement("of_the_institution")]
        public string OfTheInstitution { get; set; } = string.Empty;

        
        [BsonElement("of_the_institution_other")]
        public string OfTheInstitutionOther { get; set; } = string.Empty;

        
        [BsonElement("of_the_institution_file")]
        public string[]? OfTheInstitutionFile { get; set; }

        
        [BsonElement("for_receiving")]
        public string ForReceiving { get; set; } = string.Empty;

        
        [BsonElement("for_receiving_other")]
        public string ForReceivingOther { get; set; } = string.Empty;


        
        [BsonElement("for_receiving_file")]
        public string[]? ForReceivingFile { get; set; }

       
        [BsonElement("that_explain_the")]
        public string ThatExplainThe { get; set; } = string.Empty;


        
        [BsonElement("that_explain_the_other")]
        public string ThatExplainTheOther { get; set; } = string.Empty;


       
        [BsonElement("that_explain_the_file")]
        public string[]? ThatExplainTheFile { get; set; }


        [BsonElement("list_of_doctors")]
        public string ListOfDoctors { get; set; } = string.Empty;


        [BsonElement("list_of_doctors_other")]
        public string ListOfDoctorsOther { get; set; } = string.Empty;


        
        [BsonElement("list_of_doctors_file")]
        public string[]? ListOfDotorsFile { get; set; }

       
        [BsonElement("are_sufficient_and")]
        public string AreSufficientAnd { get; set; } = string.Empty;

        
        [BsonElement("are_sufficient_and_other")]
        public string AreSufficientAndOther { get; set; } = string.Empty;


        
        [BsonElement("are_sufficient_and_file")]
        public string[]? AreSufficientAndFile { get; set; }

        
        [BsonElement("for_people_with")]
        public string ForPeopleWith { get; set; } = string.Empty;

        

        [BsonElement("for_people_with_other")]
        public string ForPeopleWithOther { get; set; } = string.Empty;


        
        [BsonElement("for_people_with_file")]
        public string[]? ForPeopleWithFile { get; set; }

       
        [BsonElement("free_review_of")]
        public string FreeReviewOf { get; set; } = string.Empty;

        
        [BsonElement("free_review_of_other")]
        public string FreeReviewOfOther { get; set; } = string.Empty;


        
        [BsonElement("free_review_of_file")]
        public string[]? FreeReviewOfFile { get; set; }

       
        [BsonElement("a_prominent_place")]
        public string AProminentPlace { get; set; } = string.Empty;

       
        [BsonElement("a_prominent_place_other")]
        public string AProminentPlaceOther { get; set; } = string.Empty;


        
        [BsonElement("a_prominent_place_file")]
        public string[]? AProminentPlaceFile { get; set; }

       
        [BsonElement("up_guiding_boards")]
        public string UpGuidingBoards { get; set; } = string.Empty;

       
        [BsonElement("up_guiding_boards_other")]
        public string UpGuidingBoardsOther { get; set; } = string.Empty;


       
        [BsonElement("up_guiding_boards_file")]
        public string[]? UpGuidingBoardsFile { get; set; }


        
        [BsonElement("medical_records")]
        public string MedicalRecords { get; set; } = string.Empty;


        [BsonElement("medical_records_other")]
        public string? MedicalRecordsOther { get; set; }

        [BsonElement("medical_records_file")]
        public string? MedicalRecordsFile { get; set; }


        [BsonElement("there_is_an")]
        public string ThereIsAn { get; set; } = string.Empty;


        
        [BsonElement("there_is_an_other")]
        public string ThereIsAnOther { get; set; } = string.Empty;


        
        [BsonElement("there_is_an_file")]
        public string[]? ThereIsAnFile { get; set; }


        [BsonElement("be_closed_and")]
        public string BeClosedAnd { get; set; } = string.Empty;


        
        [BsonElement("be_closed_and_other")]
        public string BeClosedAndOther { get; set; } = string.Empty;



        [BsonElement("be_closed_and_file")]
        public string[]? BeClosedAndFile { get; set; }


        [BsonElement("the_data_and")]
        public string TheDataAnd { get; set; } = string.Empty;


       
        [BsonElement("the_data_and_other")]
        public string TheDataAndOther { get; set; } = string.Empty;


       
        [BsonElement("the_data_and_file")]
        public string[]? TheDataAndFile { get; set; }


        [BsonElement("each_patient_s")]
        public string EachPatientsS { get; set; } = string.Empty;



        [BsonElement("each_patient_s_other")]
        public string EachPatientsSOther { get; set; } = string.Empty;


        
        [BsonElement("each_patient_s_file")]
        public string[]? EachPatientsSFile { get; set; }


        [BsonElement("reviews_are_included")]
        public string ReviewsAreIncluded { get; set; } = string.Empty;


        
        [BsonElement("reviews_are_included_other")]
        public string ReviewsAreIncludedOther { get; set; } = string.Empty;


        
        [BsonElement("reviews_are_included_file")]
        public string[]? ReviewsAreIncludedFile { get; set; }


        [BsonElement("administration")]
        public string Administration { get; set; } = string.Empty;


        
        [BsonElement("administration_other")]
        public string? AdministrationOther { get; set; }


       
        [BsonElement("administration_file")]
        public string? AdministrationFile { get; set; }


        [BsonElement("others_for_women")]
        public string OthersForWomen { get; set; } = string.Empty;


        
        [BsonElement("others_for_women_other")]
        public string OthersForWomenOther { get; set; } = string.Empty;


       
        [BsonElement("others_for_women_file")]
        public string[]? OthersForWomenFile { get; set; }


        [BsonElement("halls_comply_with")]
        public string HallsComplyWith { get; set; } = string.Empty;


        
        [BsonElement("halls_comply_with_other")]
        public string HallsComplyWithOther { get; set; } = string.Empty;


        
        [BsonElement("halls_comply_with_file")]
        public string[]? HallsComplyWithFile { get; set; }


        [BsonElement("specifications_of_the")]
        public string SpecificationsOfThe { get; set; } = string.Empty;


        
        [BsonElement("specifications_of_the_other")]
        public string SpecificationsOfTheOther { get; set; } = string.Empty;


      
        [BsonElement("specifications_of_the_file")]
        public string[]? SpecificationsOfTheFile { get; set; }


        [BsonElement("content_in_the")]
        public string ContentInThe { get; set; } = string.Empty;



        [BsonElement("content_in_the_other")]
        public string ContentInTheOther { get; set; } = string.Empty;


        
        [BsonElement("content_in_the_file")]
        public string[]? ContentInTheFile { get; set; }


        [BsonElement("are_not_kept")]
        public string AreNotKept { get; set; } = string.Empty;


        
        [BsonElement("are_not_kept_other")]
        public string AreNotKeptOther { get; set; } = string.Empty;


       
        [BsonElement("are_not_kept_file")]
        public string[]? AreNotKeptFile { get; set; }


        [BsonElement("basic_scientific_periodicals")]
        public string BasicScientificPeriodicals { get; set; } = string.Empty;


        
        [BsonElement("basic_scientific_periodicals_other")]
        public string BasicScientificPeriodicalsOther { get; set; } = string.Empty;


        
        [BsonElement("basic_scientific_periodicals_file")]
        public string[]? BasicScientificPeriodicalsFile { get; set; }


        [BsonElement("health_practitioners_to")]
        public string HealthPractitionersTo { get; set; } = string.Empty;


        
        [BsonElement("health_practitioners_to_other")]
        public string HealthPractitionersToOther { get; set; } = string.Empty;


        
        [BsonElement("health_practitioners_to_file")]
        public string[]? HealthPractitionersToFile { get; set; }


        [BsonElement("level_of_hygiene")]
        public string LevelOfHygiene { get; set; } = string.Empty;


     
        [BsonElement("level_of_hygiene_other")]
        public string LevelOfHygieneOther { get; set; } = string.Empty;


       
        [BsonElement("level_of_hygiene_file")]
        public string[]? LevelOfHygieneFile { get; set; }


        [BsonElement("keep_expired_medicines")]
        public string KeepExpiredMedicines { get; set; } = string.Empty;


        
        [BsonElement("keep_expired_medicines_other")]
        public string KeepExpiredMedicinesOther { get; set; } = string.Empty;


     
        [BsonElement("keep_expired_medicines_file")]
        public string[]? KeepExpiredMedicinesFile { get; set; }


        [BsonElement("or_regulatory_decisions")]
        public string OrRegulatoryDecisions { get; set; } = string.Empty;



        [BsonElement("or_regulatory_decisions_other")]
        public string OrRegulatoryDecisionsOther { get; set; } = string.Empty;


        
        [BsonElement("or_regulatory_decisions_file")]
        public string[]? OrRegulatoryDecisionsFile { get; set; }


        [BsonElement("card_in_a")]
        public string CardInA { get; set; } = string.Empty;



        [BsonElement("card_in_a_other")]
        public string CardInAOther { get; set; } = string.Empty;


       
        [BsonElement("card_in_a_file")]
        public string[]? CardInAFile { get; set; }


        [BsonElement("safety_means")]
        public string SafetyMeans { get; set; } = string.Empty;


        
        [BsonElement("safety_means_other")]
        public string? SafetyMeansOther { get; set; }


        
        [BsonElement("safety_means_file")]
        public string? SafetyMeansFile { get; set; }


        [BsonElement("fire_extinguishing")]
        public string FireExtinguishing { get; set; } = string.Empty;


      
        [BsonElement("fire_extinguishing_other")]
        public string FireExtinguishingOther { get; set; } = string.Empty;


       
        [BsonElement("fire_extinguishing_file")]
        public string[]? FireExtinguishingFile { get; set; }


        [BsonElement("good_ventilation_with")]
        public string GoodVentilationWith { get; set; } = string.Empty;


        [Required]
        [BsonElement("good_ventilation_with_other")]
        public string GoodVentilationWithOther { get; set; } = string.Empty;


        
        [BsonElement("good_ventilation_with_file")]
        public string[]? GoodVentilationWithFile { get; set; }


        [BsonElement("central_suction_in")]
        public string CentralSuctionIn { get; set; } = string.Empty;


        
        [BsonElement("central_suction_in_other")]
        public string CentralSuctionInOther { get; set; } = string.Empty;


       
        [BsonElement("central_suction_in_file")]
        public string[]? CentralSuctionInFile { get; set; }


        [BsonElement("front_of_the")]
        public string FrontOfThe { get; set; } = string.Empty;


        
        [BsonElement("front_of_the_other")]
        public string FrontOfTheOther { get; set; } = string.Empty;


       
        [BsonElement("front_of_the_file")]
        public string[]? FrontOfTheFile { get; set; }


        [BsonElement("and_work_in")]
        public string AndWorkIn { get; set; } = string.Empty;


      
        [BsonElement("and_work_in_other")]
        public string? AndWorkInOther { get; set; }


       
        [BsonElement("and_work_in_file")]
        public string? AndWorkInFile { get; set; }


        [BsonElement("aid_kit_for")]
        public string AidKitFor { get; set; } = string.Empty;


        
        [BsonElement("aid_kit_for_other")]
        public string? AidKitForOther { get; set; }


        
        [BsonElement("aid_kit_for_file")]
        public string? AidKitForFile { get; set; }


        [BsonElement("eye")]
        public string Eye { get; set; } = string.Empty;


        
        [BsonElement("eye_other")]
        public string? Eye_other { get; set; }


       
        [BsonElement("eye_file")]
        public string? Eye_File { get; set; }


        [BsonElement("every_technician_in")]
        public string EveryTechnicianIn { get; set; } = string.Empty;


      
        [BsonElement("every_technician_in_other")]
        public string? EveryTechnicianInOther { get; set; }


        
        [BsonElement("every_technician_in_file")]
        public string? EveryTechnicianInFile { get; set; }


        [BsonElement("to_protect_against")]
        public string ToProtectAgainst { get; set; } = string.Empty;


       
        [BsonElement("to_protect_against_other")]
        public string? ToProtectAgainstOther { get; set; }


        
        [BsonElement("to_protect_against_file")]
        public string? ToProtectAgainstFile { get; set; }


        [BsonElement("for_the_incoming")]
        public string ForTheIncoming { get; set; } = string.Empty;


        
        [BsonElement("for_the_incoming_other")]
        public string? ForTheIncomingOther { get; set; }


        
        [BsonElement("for_the_incoming_file")]
        public string? ForTheIncomingFile { get; set; }


        [BsonElement("the_approved_regulations")]
        public string TheApprovedRegulations { get; set; } = string.Empty;


        
        [BsonElement("the_approved_regulations_other")]
        public string? TheApprovedRegulationsOther { get; set; }



        [BsonElement("the_approved_regulations_file")]
        public string? TheApprovedRegulationsFile { get; set; }


        [BsonElement("all_requirements_apply")]
        public string AllRequirementsApply { get; set; } = string.Empty;


        
        [BsonElement("all_requirements_apply_other")]
        public string? AllRequirementsApplyOther { get; set; }



        [BsonElement("all_requirements_apply_file")]
        public string? AllRequirementsApplyFile { get; set; }


        [BsonElement("units")]
        public string Units { get; set; } = string.Empty;


        
        [BsonElement("units_other")]
        public string? UnitsOther { get; set; }


       
        [BsonElement("units_file")]
        public string? UnitsFile { get; set; }


        [BsonElement("of_a_ceramic")]
        public string OfACeramic { get; set; } = string.Empty;


        
        [BsonElement("of_a_ceramic_other")]
        public string? OfACeramicOther { get; set; }


       
        [BsonElement("of_a_ceramic_file")]
        public string? OfACeramicFile { get; set; }


        [BsonElement("devices_and_tools")]
        public string DevicesAndTools { get; set; } = string.Empty;


        
        [BsonElement("devices_and_tools_other")]
        public string? DevicesAndToolsOther { get; set; }


        
        [BsonElement("devices_and_tools_file")]
        public string? DevicesAndToolsFile { get; set; }


        [BsonElement("ceramic_furnaces_inside")]
        public string CeramicFurnacesInside { get; set; } = string.Empty;


        
        [BsonElement("ceramic_furnaces_inside_other")]
        public string? CeramicFurnacesInsideOther { get; set; }


        
        [BsonElement("ceramic_furnaces_inside_file")]
        public string? CeramicFurnacesInsideFile { get; set; }


        [BsonElement("well_isolated_from")]
        public string WellIsolatedFrom { get; set; } = string.Empty;


       
        [BsonElement("well_isolated_from_other")]
        public string? WellIsolatedFromOther { get; set; }


     
        [BsonElement("well_isolated_from_file")]
        public string? WellIsolatedFromFile { get; set; }


        [BsonElement("a_casting_unit")]
        public string ACastingUnit { get; set; } = string.Empty;



        [BsonElement("a_casting_unit_other")]
        public string? ACastingUnitOther { get; set; }


        
        [BsonElement("a_casting_unit_file")]
        public string? ACastingUnitFile { get; set; }


        [BsonElement("to_expel_the")]
        public string ToExpelThe { get; set; } = string.Empty;


        
        [BsonElement("to_expel_the_other")]
        public string? ToExpelTheOther { get; set; }



        [BsonElement("to_expel_the_file")]
        public string? ToExpelTheFile { get; set; }


        [BsonElement("and_tools_necessary")]
        public string AndToolsNecessary { get; set; } = string.Empty;


        
        [BsonElement("and_tools_necessary_other")]
        public string? AndToolsNecessaryOther { get; set; }


        
        [BsonElement("and_tools_necessary_file")]
        public string? AndToolsNecessaryFile { get; set; }


        [BsonElement("necessary_for_waxing")]
        public string NecessaryForWaxing { get; set; } = string.Empty;


        
        [BsonElement("necessary_for_waxing_other")]
        public string? NecessaryForWaxingOther { get; set; }


        
        [BsonElement("necessary_for_waxing_file")]
        public string? NecessaryForWaxingFile { get; set; }

        
        [BsonElement("and_bowl_for")]
        public string AndBowlFor { get; set; } = string.Empty;


        
        [BsonElement("and_bowl_for_other")]
        public string? AndBowlForOther { get; set; }


        
        [BsonElement("and_bowl_for_File")]
        public string? AndBowlForFile { get; set; }

        
        [BsonElement("with_gas_electricity")]
        public string WithGasElectricity { get; set; } = string.Empty;


       
        [BsonElement("with_gas_electricity_other")]
        public string? WithGasElectricityOther { get; set; }


      
        [BsonElement("with_gas_electricity_file")]
        public string? WithGasElectricityFile { get; set; }

        
        [BsonElement("device_ventilation_and")]
        public string DeviceVentilationAnd { get; set; } = string.Empty;


       
        [BsonElement("device_ventilation_and_other")]
        public string? DeviceVentilationAndOther { get; set; }


        
        [BsonElement("device_ventilation_and_file")]
        public string? DeviceVentilationAndFile { get; set; }

        
        [BsonElement("an_integrated_unit")]
        public string AnIntegratedUnit { get; set; } = string.Empty;



        [BsonElement("an_integrated_unit_other")]
        public string? AnIntegratedUnitOther { get; set; }


        
        [BsonElement("an_integrated_unit_file")]
        public string? AnIntegratedUnitFile { get; set; }

       
        [BsonElement("waxing_and_shaping")]
        public string WaxingAndShaping { get; set; } = string.Empty;


        
        [BsonElement("waxing_and_shaping_other")]
        public string? WaxingAndShapingOther { get; set; }


      
        [BsonElement("waxing_and_shaping_file")]
        public string? WaxingAndShapingFile { get; set; }


        [BsonElement("gypsum_equipment_and")]
        public string GypsumEquipmentAnd { get; set; } = string.Empty;


        
        [BsonElement("gypsum_equipment_and_other")]
        public string? GypsumEquipmentAndOther { get; set; }


    
        [BsonElement("gypsum_equipment_and_file")]
        public string? GypsumEquipmentAndFile { get; set; }


        [BsonElement("lighting_and_a")]
        public string LightingAndA { get; set; } = string.Empty;


        
        [BsonElement("lighting_and_a_other")]
        public string? LightingAndAOther { get; set; }


        [Required]
        [BsonElement("lighting_and_a_file")]
        public string? LightingAndAFile { get; set; }


        [BsonElement("a_hole_for")]
        public string AHoleFor { get; set; } = string.Empty;


      
        [BsonElement("a_hole_for_other")]
        public string? AHoleForOther { get; set; }


      
        [BsonElement("a_hole_for_file")]
        public string? AHoleForFile { get; set; }


        [BsonElement("casting_gypsum_models")]
        public string CastingGypsumModels { get; set; } = string.Empty;


     
        [BsonElement("casting_gypsum_models_other")]
        public string? CastingGypsumModelsOther { get; set; }


       
        [BsonElement("casting_gypsum_models_file")]
        public string? CastingGypsumModelsFile { get; set; }


        [BsonElement("new_cases_and")]
        public string NewCasesAnd { get; set; } = string.Empty;


        
        [BsonElement("new_cases_and_other")]
        public string? NewCasesAndOther { get; set; }


       
        [BsonElement("new_cases_and_file")]
        public string? NewCasesAndFile { get; set; }


        [BsonElement("reportNumber")]
        public string ReportNumber { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
