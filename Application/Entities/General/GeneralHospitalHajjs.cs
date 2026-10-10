using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.General
{

    [BsonIgnoreExtraElements]
    public class GeneralHospitalHajjs : AuditInfoComplete
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
        public string? StaffAvailability { get; set; }


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


        [BsonElement("the_basic_safety")]
        public string TheBasicSafety { get; set; } = string.Empty;

        [BsonElement("the_basic_safety_other")]
        public string TheBasicSafetyOther { get; set; } = string.Empty;

        [BsonElement("the_basic_safety_other_file")]
        public string[]? TheBasicSafetyFile { get; set; }


        [BsonElement("wide_with_the")]
        public string WideWithThe { get; set; } = string.Empty;

        [BsonElement("wide_with_the_other")]
        public string WideWithTheOther { get; set; } = string.Empty;

        [BsonElement("wide_with_the_file")]
        public string[]? WideWithTheFile { get; set; }

        [BsonElement("outage_and_it")]
        public string OutageAndIt { get; set; } = string.Empty;

        [BsonElement("outage_and_it_other")]
        public string OutageAndItOther { get; set; } = string.Empty;

        [BsonElement("outage_and_it_file")]
        public string[]? OutageAndItFile { get; set; }

        [BsonElement("evaluation_program_for")]
        public string? EvaluationProgramFor { get; set; }

        [BsonElement("evaluation_program_for_other")]
        public string? EvaluationProgramForOther { get; set; }

        [BsonElement("evaluation_program_for")]
        public string? EvaluationProgramForFile { get; set; }

        [BsonElement("for_the_safe")]
        public string? ForTheSafe { get; set; }

        [BsonElement("for_the_safe_other")]
        public string? ForTheSafeOther { get; set; }

        [BsonElement("for_the_safe_file")]
        public string? ForTheSafeFile { get; set; }

        [BsonElement("waste_that_meets")]
        public string? WasteThatMeets { get; set; }

        [BsonElement("waste_that_meets_other")]
        public string? WasteThatMeetsOther { get; set; }

        [BsonElement("waste_that_meets_file")]
        public string? WasteThatMeetsFile { get; set; }

        [BsonElement("waste_conforms_to")]
        public string? WasteConformsTo { get; set; }

        [BsonElement("waste_conforms_to_other")]
        public string? WasteConformsToOther { get; set; }

        [BsonElement("waste_conforms_to_file")]
        public string? WasteConformsToFile { get; set; }

        [BsonElement("trained_on_the")]
        public string? TrainedOnThe { get; set; }

        [BsonElement("trained_on_the_other")]
        public string? TrainedOnTheOther { get; set; }

        [BsonElement("trained_on_the_file")]
        public string? TrainedOnTheFile { get; set; }

        [BsonElement("infection_case_immediately")]
        public string? InfectionCaseImmediately { get; set; }

        [BsonElement("infection_case_immediately_other")]
        public string? InfectionCaseImmediatelyOther { get; set; }

        [BsonElement("infection_case_immediately_file")]
        public string? InfectionCaseImmediatelyFile { get; set; }

        [BsonElement("classification_from_the")]
        public string? ClassificationFromThe { get; set; }

        [BsonElement("classification_from_the_other")]
        public string? ClassificationFromTheOther { get; set; }

        [BsonElement("classification_from_the_file")]
        public string? ClassificationFromTheFile { get; set; }

        [BsonElement("air_isolation_and")]
        public string? AirIsolationAnd { get; set; }

        [BsonElement("air_isolation_and_other")]
        public string? AirIsolationAndOther { get; set; }

        [BsonElement("air_isolation_and_file")]
        public string? AirIsolationAndFile { get; set; }

        [BsonElement("is_an_area")]
        public string? IsAnArea { get; set; }

        [BsonElement("is_an_area_other")]
        public string? IsAnAreaOther { get; set; }

        [BsonElement("is_an_area_file")]
        public string? IsAnAreaFile { get; set; }

        [BsonElement("files_and_service")]
        public string? FilesAndService { get; set; }

        [BsonElement("files_and_service_other")]
        public string? FilesAndServiceOther { get; set; }

        [BsonElement("files_and_service_file")]
        public string? FilesAndServiceFile { get; set; }

        [BsonElement("others_for_women")]
        public string? OthersForWomen { get; set; }

        [BsonElement("others_for_women_other")]
        public string? OthersForWomenOther { get; set; }

        [BsonElement("others_for_women_file")]
        public string? OthersForWomenFile { get; set; }

        [BsonElement("ambulance_that_meets")]
        public string? AmbulanceThatMeets { get; set; }

        [BsonElement("ambulance_that_meets_other")]
        public string? AmbulanceThatMeetsOther { get; set; }

        [BsonElement("ambulance_that_meets_file")]
        public string? AmbulanceThatMeetsFile { get; set; }

        [BsonElement("isolation_room_is")]
        public string? IsolationRoomIs { get; set; }

        [BsonElement("isolation_room_is")]
        public string? IsolationRoomIsOther { get; set; }

        [BsonElement("isolation_room_is_file")]
        public string? IsolationRoomIsFile { get; set; }

        [BsonElement("available_to_deal")]
        public string? AvailableToDeal { get; set; }

        [BsonElement("available_to_deal_other")]
        public string? AvailableToDealOther { get; set; }

        [BsonElement("available_to_deal_file")]
        public string? AvailableToDealFile { get; set; }

        [BsonElement("every_ten_families")]
        public string? EveryTenFamilies { get; set; }

        [BsonElement("every_ten_families_other")]
        public string? EveryTenFamiliesOther { get; set; }

        [BsonElement("every_ten_families_file")]
        public string? EveryTenFamiliesFile { get; set; }

        [BsonElement("are_available_to")]
        public string? AreAvailableTo { get; set; }

        [BsonElement("are_available_to_other")]
        public string? AreAvailableToOther { get; set; }

        [BsonElement("are_available_to_file")]
        public string? AreAvailableToFile { get; set; }

        [BsonElement("listed_in_the")]
        public string? ListedInThe { get; set; }

        [BsonElement("listed_in_the_other")]
        public string? ListedInTheOther { get; set; }

        [BsonElement("listed_in_the_file")]
        public string? ListedInTheFile { get; set; }

        [BsonElement("to_hospital_departments")]
        public string? ToHospitalDepartments { get; set; }

        [BsonElement("to_hospital_departments_other")]
        public string? ToHospitalDepartmentsOther { get; set; }

        [BsonElement("to_hospital_departments_file")]
        public string? ToHospitalDepartmentsFile { get; set; }

        [BsonElement("accident_or_death")]
        public string? AccidentOrDeath { get; set; }

        [BsonElement("accident_or_death_other")]
        public string? AccidentOrDeathOther { get; set; }

        [BsonElement("accident_or_death_file")]
        public string? AccidentOrDeathFile { get; set; }

        [BsonElement("record_of_traffic")]
        public string? RecordOfTraffic { get; set; }

        [BsonElement("record_of_traffic_other")]
        public string? RecordOfTrafficOther { get; set; }

        [BsonElement("record_of_traffic_file")]
        public string? RecordOfTrafficFile { get; set; }

        [BsonElement("saudi_commission_hold")]
        public string? SaudiCommissionHold { get; set; }

        [BsonElement("saudi_commission_hold_other")]
        public string? SaudiCommissionHoldOther { get; set; }

        [BsonElement("saudi_commission_hold_file")]
        public string? SaudiCommissionHoldFile { get; set; }

        [BsonElement("the_door_works")]
        public string? TheDoorWorks { get; set; }

        [BsonElement("the_door_works_other")]
        public string? TheDoorWorksOther { get; set; }

        [BsonElement("the_door_works_file")]
        public string? TheDoorWorksFile { get; set; }

        [BsonElement("are_radiology_reports")]
        public string? AreRadiologyReports { get; set; }

        [BsonElement("are_radiology_reports_other")]
        public string? AreRadiologyReportsOther { get; set; }

        [BsonElement("are_radiology_reports_file")]
        public string? AreRadiologyReportsFile { get; set; }

        [BsonElement("radiation_protection_program")]
        public string? RadiationProtectionProgram { get; set; }

        [BsonElement("radiation_protection_program_other")]
        public string? RadiationProtectionProgramOther { get; set; }

        [BsonElement("radiation_protection_program_file")]
        public string? RadiationProtectionProgramFile { get; set; }

        [BsonElement("registration_and_classification")]
        public string? RegistrationAndClassification { get; set; }

        [BsonElement("registration_and_classification_other")]
        public string? RegistrationAndClassificationOther { get; set; }

        [BsonElement("registration_and_classification_file")]
        public string? RegistrationAndClassificationFile { get; set; }

        [BsonElement("and_nursing_care")]
        public string? AndNursingCare { get; set; }

        [BsonElement("and_nursing_care_other")]
        public string? AndNursingCareother { get; set; }

        [BsonElement("and_nursing_care_file")]
        public string? AndNursingCareFile { get; set; }

        [BsonElement("available_for_rooms")]
        public string? AvailableForRooms { get; set; }

        [BsonElement("available_for_rooms_other")]
        public string? AvailableForRoomsOther { get; set; }

        [BsonElement("available_for_rooms_file")]
        public string? AvailableForRoomsFile { get; set; }

        [BsonElement("passage_of_beds")]
        public string? PassageOfBeds { get; set; }

        [BsonElement("passage_of_beds_other")]
        public string? PassageOfBedsOther { get; set; }

        [BsonElement("passage_of_beds_file")]
        public string? PassageOfBedsFile { get; set; }

        [BsonElement("sleeping_rooms_conforming")]
        public string? SleepingRoomsConforming { get; set; }

        [BsonElement("sleeping_rooms_conforming_other")]
        public string? SleepingRoomsConformingOther { get; set; }

        [BsonElement("sleeping_rooms_conforming_file")]
        public string? SleepingRoomsConformingFile { get; set; }

        [BsonElement("privacy_with_curtains")]
        public string? PrivacyWithCurtains { get; set; }

        [BsonElement("privacy_with_curtains_other")]
        public string? PrivacyWithCurtainsOther { get; set; }

        [BsonElement("privacy_with_curtains_file")]
        public string? PrivacyWithCurtainsFile { get; set; }

        [BsonElement("wards_are_easy")]
        public string? WardsAreEasy { get; set; }

        [BsonElement("wards_are_easy_other")]
        public string? WardsAreEasyOther { get; set; }

        [BsonElement("wards_are_easy_file")]
        public string? WardsAreEasyFile { get; set; }

        [BsonElement("is_commensurate_with")]
        public string? IsCommensurateWith { get; set; }

        [BsonElement("is_commensurate_with_other")]
        public string? IsCommensurateWithOther { get; set; }

        [BsonElement("is_commensurate_with_file")]
        public string? IsCommensurateWithFile { get; set; }

        [BsonElement("saudi_commission_for")]
        public string? SaudiCommissionFor { get; set; }

        [BsonElement("saudi_commission_for_other")]
        public string? SaudiCommissionForOther { get; set; }

        [BsonElement("saudi_commission_for_file")]
        public string? SaudiCommissionForFile { get; set; }

        [BsonElement("operating_room_equipment")]
        public string? OperatingRoomEquipment { get; set; }

        [BsonElement("operating_room_equipment_other")]
        public string? OperatingRoomEquipmentOther { get; set; }

        [BsonElement("operating_room_equipment_file")]
        public string? OperatingRoomEquipmentFile { get; set; }

        [BsonElement("there_is_entrance")]
        public string? ThereIsEntrance { get; set; }

        [BsonElement("there_is_entrance_other")]
        public string? ThereIsEntranceOther { get; set; }

        [BsonElement("there_is_entrance_file")]
        public string? ThereIsEntranceFile { get; set; }

        [BsonElement("room_with_a")]
        public string? RoomWithA { get; set; }

        [BsonElement("room_with_a_other")]
        public string? RoomWithAOther { get; set; }

        [BsonElement("room_with_a_file")]
        public string? RoomWithAFile { get; set; }

        [BsonElement("paths_for_the")]
        public string? PathsForThe { get; set; }

        [BsonElement("paths_for_the_other")]
        public string? PathsForTheOther { get; set; }

        [BsonElement("paths_for_the_file")]
        public string? PathsForTheFile { get; set; }

        [BsonElement("room_solutions_and")]
        public string? RoomSolutionsAnd { get; set; }

        [BsonElement("room_solutions_and_other")]
        public string? RoomSolutionsAndOther { get; set; }

        [BsonElement("room_solutions_and_file")]
        public string? RoomSolutionsAndFile { get; set; }

        [BsonElement("anesthesiologist_in_a")]
        public string? AnesthesiologistInA { get; set; }

        [BsonElement("anesthesiologist_in_a_other")]
        public string? AnesthesiologistInAOther { get; set; }

        [BsonElement("anesthesiologist_in_a_file")]
        public string? AnesthesiologistInAFile { get; set; }

        [BsonElement("for_the_surgical")]
        public string? ForTheSurgical { get; set; }

        [BsonElement("for_the_surgical_other")]
        public string? ForTheSurgicalOther { get; set; }

        [BsonElement("for_the_surgical_file")]
        public string? ForTheSurgicalFile { get; set; }

        [BsonElement("a_sterilization_room")]
        public string? ASterilizationRoom { get; set; }

        [BsonElement("a_sterilization_room_other")]
        public string? ASterilizationRoomOther { get; set; }

        [BsonElement("a_sterilization_room_file")]
        public string? ASterilizationRoomFile { get; set; }

        [BsonElement("the_patient_after")]
        public string? ThePatientAfter { get; set; }

        [BsonElement("the_patient_after_other")]
        public string? ThePatientAfterOther { get; set; }

        [BsonElement("the_patient_after_file")]
        public string? ThePatientAfterFile { get; set; }

        [BsonElement("is_a_department")]
        public string? IsADepartment { get; set; }

        [BsonElement("is_a_department_other")]
        public string? IsADepartmentOther { get; set; }

        [BsonElement("is_a_department_file")]
        public string? IsADepartmentFile { get; set; }

        [BsonElement("diseases_in_accordance")]
        public string? DiseasesInAccordance { get; set; }

        [BsonElement("diseases_in_accordance_other")]
        public string? DiseasesInAccordanceOther { get; set; }

        [BsonElement("diseases_in_accordance_file")]
        public string? DiseasesInAccordanceFile { get; set; }

        [BsonElement("work_policies_and")]
        public string? WorkPoliciesAnd { get; set; }

        [BsonElement("work_policies_and_other")]
        public string? WorkPoliciesAndOther { get; set; }

        [BsonElement("work_policies_and_file")]
        public string? WorkPoliciesAndFile { get; set; }

        [BsonElement("hold_a_professional")]
        public string? HoldAProfessional { get; set; }

        [BsonElement("hold_a_professional_other")]
        public string? HoldAProfessionalOther { get; set; }

        [BsonElement("hold_a_professional_file")]
        public string? HoldAProfessionalFile { get; set; }

        [BsonElement("are_10_of")]
        public string? Are10Of { get; set; }

        [BsonElement("are_10_of_other")]
        public string? Are10OfOther { get; set; }

        [BsonElement("are_10_of_file")]
        public string? Are10OfFile { get; set; }

        [BsonElement("is_available_for")]
        public string? IsAvailableFor { get; set; }

        [BsonElement("is_available_for_other")]
        public string? IsAvailableForOther { get; set; }

        [BsonElement("is_available_for_file")]
        public string? IsAvailableForFile { get; set; }

        [BsonElement("available_for_every")]
        public string? AvailableForEvery { get; set; }

        [BsonElement("available_for_every_other")]
        public string? AvailableForEveryOther { get; set; }

        [BsonElement("available_for_every_file")]
        public string? AvailableForEveryFile { get; set; }

        [BsonElement("three_hand_wash")]
        public string? ThreeHandWash { get; set; }

        [BsonElement("three_hand_wash_other")]
        public string? ThreeHandWashOther { get; set; }

        [BsonElement("three_hand_wash_file")]
        public string? ThreeHandWashFile { get; set; }

        [BsonElement("the_level_of")]
        public string? TheLevelOf { get; set; }

        [BsonElement("the_level_of_other")]
        public string? TheLevelOfOther { get; set; }

        [BsonElement("the_level_of_file")]
        public string? TheLevelOfFile { get; set; }

        [BsonElement("the_saudi_center")]
        public string? TheSaudiCenter { get; set; }

        [BsonElement("the_saudi_center_other")]
        public string? TheSaudiCenterOther { get; set; }

        [BsonElement("the_saudi_center_file")]
        public string? TheSaudiCenterFile { get; set; }

        [BsonElement("sterilization_room_that")]
        public string? SterilizationRoomThat { get; set; }

        [BsonElement("sterilization_room_that_other")]
        public string? SterilizationRoomThatOther { get; set; }

        [BsonElement("sterilization_room_that_file")]
        public string? SterilizationRoomThatFile { get; set; }

        [BsonElement("specifications_of_the")]
        public string? SpecificationsOfThe { get; set; }

        [BsonElement("specifications_of_the_other")]
        public string? SpecificationsOfTheOther { get; set; }

        [BsonElement("specifications_of_the_file")]
        public string? SpecificationsOfTheFile { get; set; }

        [BsonElement("sufficient_number_of")]
        public string? SufficientNumberOf { get; set; }

        [BsonElement("sufficient_number_of_other")]
        public string? SufficientNumberOfOther { get; set; }

        [BsonElement("sufficient_number_of_file")]
        public string? SufficientNumberOfFile { get; set; }

        [BsonElement("a_health_certificate")]
        public string? AHealthCertificate { get; set; }

        [BsonElement("a_health_certificate_other")]
        public string? AHealthCertificateOther { get; set; }

        [BsonElement("a_health_certificate_file")]
        public string? AHealthCertificateFile { get; set; }

        [BsonElement("and_classification_from")]
        public string? AndClassificationFrom { get; set; }

        [BsonElement("and_classification_from_other")]
        public string? AndClassificationFromOther { get; set; }

        [BsonElement("and_classification_from_file")]
        public string? AndClassificationFromFile { get; set; }

        [BsonElement("allows_air_to")]
        public string? AllowsAirTo { get; set; }

        [BsonElement("allows_air_to_other")]
        public string? AllowsAirToOther { get; set; }

        [BsonElement("allows_air_to_file")]
        public string? AllowsAirToFile { get; set; }

        [BsonElement("pressure_comply_with")]
        public string? PressureComplyWith { get; set; }

        [BsonElement("pressure_comply_with_other")]
        public string? PressureComplyWithOther { get; set; }

        [BsonElement("pressure_comply_with_file")]
        public string? PressureComplyWithFile { get; set; }

        [BsonElement("the_rest_of")]
        public string? TheRestOf { get; set; }

        [BsonElement("the_rest_of_other")]
        public string? TheRestOfOther { get; set; }

        [BsonElement("the_rest_of_file")]
        public string? TheRestOfFile { get; set; }

        [BsonElement("apron_head_cover")]
        public string? ApronHeadCover { get; set; }

        [BsonElement("apron_head_cover_other")]
        public string? ApronHeadCoverOther { get; set; }

        [BsonElement("apron_head_cover_file")]
        public string? ApronHeadCoverFile { get; set; }

        [BsonElement("there_is_isolation")]
        public string? ThereIsIsolation { get; set; }

        [BsonElement("there_is_isolation_other")]
        public string? ThereIsIsolationOther { get; set; }

        [BsonElement("there_is_isolation_file")]
        public string? ThereIsIsolationFile { get; set; }

        [BsonElement("professional_registration_and")]
        public string? ProfessionalRegistrationAnd { get; set; }

        [BsonElement("professional_registration_and_other")]
        public string? ProfessionalRegistrationAndOther { get; set; }

        [BsonElement("professional_registration_and_file")]
        public string? ProfessionalRegistrationAndFile { get; set; }

        [BsonElement("laboratory_space_is")]
        public string? LaboratorySpaceIs { get; set; }

        [BsonElement("laboratory_space_is_other")]
        public string? LaboratorySpaceIsOther { get; set; }

        [BsonElement("laboratory_space_is_file")]
        public string? LaboratorySpaceIsFile { get; set; }

        [BsonElement("air_conditioning_is")]
        public string? AirConditioningIs { get; set; }

        [BsonElement("air_conditioning_is_other")]
        public string? AirConditioningIsOther { get; set; }

        [BsonElement("air_conditioning_is_file")]
        public string? AirConditioningIsFile { get; set; }

        [BsonElement("reception_place_is")]
        public string? ReceptionPlaceIs { get; set; }

        [BsonElement("reception_place_is_other")]
        public string? ReceptionPlaceIsOther { get; set; }

        [BsonElement("reception_place_is_file")]
        public string? ReceptionPlaceIsFile { get; set; }

        [BsonElement("there_is_a")]
        public string? ThereIsA { get; set; }

        [BsonElement("there_is_a_other")]
        public string? ThereIsAOther { get; set; }

        [BsonElement("there_is_a_file")]
        public string? ThereIsAFile { get; set; }

        [BsonElement("anatomy_department_conforms")]
        public string? AnatomyDepartmentConforms { get; set; }

        [BsonElement("anatomy_department_conforms_other")]
        public string? AnatomyDepartmentConformsOther { get; set; }

        [BsonElement("anatomy_department_conforms_file")]
        public string? AnatomyDepartmentConformsFile { get; set; }

        [BsonElement("department_conforms_to")]
        public string? DepartmentconformsTo { get; set; }

        [BsonElement("department_conforms_to_other")]
        public string? DepartmentconformsToOther { get; set; }

        [BsonElement("department_conforms_to_file")]
        public string? DepartmentconformsToFile { get; set; }

        [BsonElement("drug_testing_department")]
        public string? DrugTestingDepartment { get; set; }

        [BsonElement("drug_testing_department_other")]
        public string? DrugTestingDepartmentOther { get; set; }

        [BsonElement("drug_testing_department_file")]
        public string? DrugTestingDepartmentFile { get; set; }

        [BsonElement("department_of_biomolecules")]
        public string? DepartmentOfBiomolecules { get; set; }

        [BsonElement("department_of_biomolecules_other")]
        public string? DepartmentOfBiomoleculesOther { get; set; }

        [BsonElement("department_of_biomolecules_file")]
        public string? DepartmentOfBiomoleculesFile { get; set; }

        [BsonElement("forms_for_each")]
        public string? FormsForEach { get; set; }

        [BsonElement("forms_for_each_other")]
        public string? FormsForEachOther { get; set; }

        [BsonElement("forms_for_each_file")]
        public string? FormsForEachFile { get; set; }

        [BsonElement("the_mechanism_for")]
        public string? TheMechanismFor { get; set; }

        [BsonElement("the_mechanism_for_other")]
        public string? TheMechanismForOther { get; set; }

        [BsonElement("the_mechanism_for_file")]
        public string? TheMechanismForFile { get; set; }

        [BsonElement("the_facilities_equipment")]
        public string? TheFacilitiesEquipment { get; set; }

        [BsonElement("the_facilities_equipment_other")]
        public string? TheFacilitiesEquipmentOther { get; set; }

        [BsonElement("the_facilities_equipment_file")]
        public string? TheFacilitiesEquipmentFile { get; set; }

        [BsonElement("the_devices_is")]
        public string? TheDevicesIs { get; set; }

        [BsonElement("the_devices_is_other")]
        public string? TheDevicesIsOther { get; set; }

        [BsonElement("the_devices_is_file")]
        public string? TheDevicesIsFile { get; set; }

        [BsonElement("the_facilities_and")]
        public string? TheFacilitiesAnd { get; set; }

        [BsonElement("the_facilities_and_other")]
        public string? TheFacilitiesAndOther { get; set; }

        [BsonElement("the_facilities_and_file")]
        public string? TheFacilitiesAndFile { get; set; }

        [BsonElement("hold_a_registration")]
        public string? HoldARegistration { get; set; }

        [BsonElement("hold_a_registration_other")]
        public string? HoldARegistrationOther { get; set; }

        [BsonElement("hold_a_registration_file")]
        public string? HoldARegistrationFile { get; set; }

        [BsonElement("a_section_is")]
        public string? ASectionIs { get; set; }

        [BsonElement("a_section_is_other")]
        public string? ASectionIsOther { get; set; }

        [BsonElement("a_section_is_file")]
        public string? ASectionIsFile { get; set; }

        [BsonElement("preparations_are_valid")]
        public string? PreparationsAreValid { get; set; }

        [BsonElement("preparations_are_valid_other")]
        public string? PreparationsAreValidOther { get; set; }

        [BsonElement("preparations_are_valid_file")]
        public string? PreparationsAreValidFile { get; set; }

        [BsonElement("separate_section_for")]
        public string? SeparateSectionFor { get; set; }

        [BsonElement("separate_section_for_other")]
        public string? SeparateSectionForOther { get; set; }

        [BsonElement("separate_section_for_file")]
        public string? SeparateSectionForFile { get; set; }

        [BsonElement("is_a_register")]
        public string? IsARegister { get; set; }

        [BsonElement("is_a_register_other")]
        public string? IsARegisterOther { get; set; }

        [BsonElement("is_a_register_file")]
        public string? IsARegisterFile { get; set; }

        [BsonElement("the_requirements_for")]
        public string? TheRequirementsFor { get; set; }

        [BsonElement("the_requirements_for_other")]
        public string? TheRequirementsForOther { get; set; }

        [BsonElement("the_requirements_for_file")]
        public string? TheRequirementsForFile { get; set; }

        [BsonElement("sticker_and_meet")]
        public string? StickerAndMeet { get; set; }

        [BsonElement("sticker_and_meet_other")]
        public string? StickerAndMeetOther { get; set; }

        [BsonElement("sticker_and_meet_file")]
        public string? StickerAndMeetFile { get; set; }

        [BsonElement("and_classification_professional")]
        public string? AndClassificationProfessional { get; set; }

        [BsonElement("and_classification_professional_other")]
        public string? AndClassificationProfessionalOther { get; set; }

        [BsonElement("and_classification_professional_file")]
        public string? AndClassificationProfessionalFile { get; set; }

        [BsonElement("available_that_meet")]
        public string? AvailableThatMeet { get; set; }

        [BsonElement("available_that_meet_other")]
        public string? AvailableThatMeetOther { get; set; }

        [BsonElement("available_that_meet_file")]
        public string? AvailableThatMeetFile { get; set; }

        [BsonElement("a_nursing_station")]
        public string? ANursingStation { get; set; }

        [BsonElement("a_nursing_station_other")]
        public string? ANursingStationOther { get; set; }

        [BsonElement("a_nursing_station_file")]
        public string? ANursingStationFile { get; set; }

        [BsonElement("are_available_that")]
        public string? AreAvailableThat { get; set; }

        [BsonElement("are_available_that_other")]
        public string? AreAvailableThatOther { get; set; }

        [BsonElement("are_available_that_file")]
        public string? AreAvailableThatFile { get; set; }

        [BsonElement("available_for_treatment")]
        public string? AvailableForTreatment { get; set; }

        [BsonElement("available_for_treatment")]
        public string? AvailableForTreatmentOther { get; set; }

        [BsonElement("available_for_treatment_file")]
        public string? AvailableForTreatmentFile { get; set; }

        [BsonElement("available_to_store")]
        public string? AvailableToStore { get; set; }

        [BsonElement("available_to_store_other")]
        public string? AvailableToStoreOther { get; set; }

        [BsonElement("available_to_store_file")]
        public string? AvailableToStoreFile { get; set; }

        [BsonElement("pathology_laboratory_comply")]
        public string? PathologyLaboratoryComply { get; set; }

        [BsonElement("pathology_laboratory_comply_other")]
        public string? PathologyLaboratoryComplyOther { get; set; }

        [BsonElement("pathology_laboratory_comply_file")]
        public string? PathologyLaboratoryComplyFile { get; set; }

        [BsonElement("and_specifications_elements")]
        public string? AndSpecificationsElements { get; set; }

        [BsonElement("and_specifications_elements_other")]
        public string? AndSpecificationsElementsOther { get; set; }

        [BsonElement("and_specifications_elements_file")]
        public string? AndSpecificationsElementsFile { get; set; }

        [BsonElement("the_water_purification")]
        public string? TheWaterPurification { get; set; }

        [BsonElement("the_water_purification_other")]
        public string? TheWaterPurificationOther { get; set; }

        [BsonElement("the_water_purification_file")]
        public string? TheWaterPurificationFile { get; set; }

        [BsonElement("and_specifications_of")]
        public string? AndSpecificationsOf { get; set; }

        [BsonElement("and_specifications_of_other")]
        public string? AndSpecificationsOfOther { get; set; }

        [BsonElement("and_specifications_of_file")]
        public string? AndSpecificationsOfFile { get; set; }

        [BsonElement("is_available_in")]
        public string? IsAvailableIn { get; set; }

        [BsonElement("is_available_in_other")]
        public string? IsAvailableInOther { get; set; }

        [BsonElement("is_available_in_file")]
        public string? IsAvailableInFile { get; set; }

        [BsonElement("all_practitioners_hold")]
        public string AllPractitionersHold { get; set; } = string.Empty;

        [BsonElement("all_practitioners_hold_other")]
        public string AllPractitionersHoldOther { get; set; } = string.Empty;

        [BsonElement("all_practitioners_hold_file")]
        public string[]? AllPractitionersHoldFile { get; set; }


        [BsonElement("reportNumber")]
        public string ReportNumber { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
