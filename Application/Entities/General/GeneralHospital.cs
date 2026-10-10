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
    public class GeneralHospital : AuditInfoComplete
    {

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;


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

        [BsonElement("and_an_appointment")]
        public string AndAnAppointment { get; set; } = string.Empty;

        [BsonElement("and_an_appointment_other")]
        public string? AndAnAppointmentOther { get; set; }

        [BsonElement("and_an_appointment_file")]
        public string? AndAnAppointmentOtherFile { get; set; }

        [BsonElement("needs_and_facilitate")]
        public string NeedsAndFacilitate { get; set; } = string.Empty;

        [BsonElement("needs_and_facilitate_other")]
        public string? NeedsAndFacilitateOther { get; set; }

        [BsonElement("needs_and_facilitate_file")]
        public string? NeedsAndFacilitateFile { get; set; }

        [BsonElement("men_and_others")]
        public string MenAndOthers { get; set; } = string.Empty;

        [BsonElement("men_and_others_other")]
        public string? MenAndOthersOther { get; set; }

        [BsonElement("men_and_others_file")]
        public string? MenAndOthersFile { get; set; }

        [BsonElement("and_duties_of")]
        public string AndDutiesOf { get; set; } = string.Empty;

        [BsonElement("and_duties_of_other")]
        public string? AndDutiesOfOther { get; set; }

        [BsonElement("and_duties_of_file")]
        public string? AndDutiesOfFile { get; set; }

        [BsonElement("hospital_business_card")]
        public string HospitalBusinessCard { get; set; } = string.Empty;

        [BsonElement("hospital_business_card_other")]
        public string? HospitalBusinessCardOther { get; set; }

        [BsonElement("hospital_business_card_file")]
        public string? HospitalBusinessCardFile { get; set; }

        [BsonElement("list_of_doctors")]
        public string ListOfDoctors { get; set; } = string.Empty;

        [BsonElement("list_of_doctors_other")]
        public string? ListOfDoctorsOther { get; set; }

        [BsonElement("list_of_doctors_file")]
        public string? ListOfDoctorsFile { get; set; }

        [BsonElement("for_receiving_observations")]
        public string ForReceivingObservations { get; set; } = string.Empty;

        [BsonElement("for_receiving_observations_other")]
        public string? ForReceivingObservationsOther { get; set; }

        [BsonElement("for_receiving_observations_file")]
        public string? ForReceivingObservationsFile { get; set; }

        [BsonElement("to_the_specifications")]
        public string ToTheSpecifications { get; set; } = string.Empty;

        [BsonElement("to_the_specifications_other")]
        public string? ToTheSpecificationsOther { get; set; }

        [BsonElement("to_the_specifications_file")]
        public string? ToTheSpecificationsFile { get; set; }

        [BsonElement("basic_safety_requirements")]
        public string? BasicSafetyRequirements { get; set; }

        [BsonElement("basic_safety_requirements_other")]
        public string? BasicSafetyRequirementsOther { get; set; }

        [BsonElement("basic_safety_requirements_file")]
        public string? BasicSafetyRequirementsFile { get; set; }

        [BsonElement("to_facilitate_the")]
        public string? ToFacilitateThe { get; set; }

        [BsonElement("to_facilitate_the_other")]
        public string? ToFacilitateTheOther { get; set; }

        [BsonElement("to_facilitate_the_file")]
        public string? ToFacilitateTheFile { get; set; }

        [BsonElement("is_there_electricity")]
        public string? IsThereElectricity { get; set; }

        [BsonElement("is_there_electricity_other")]
        public string? IsThereElectricityOther { get; set; }

        [BsonElement("is_there_electricity_file")]
        public string? IsThereElectricityFile { get; set; }

        [BsonElement("for_infection_control")]
        public string? ForInfectionControl { get; set; }

        [BsonElement("for_infection_control_other")]
        public string? ForInfectionControlOther { get; set; }

        [BsonElement("for_infection_control_file")]
        public string? ForInfectionControlFile { get; set; }

        [BsonElement("environmental_protection_for")]
        public string? EnvironmentalProtectionFor { get; set; }

        [BsonElement("environmental_protection_for_other")]
        public string? EnvironmentalProtectionForOther { get; set; }

        [BsonElement("environmental_protection_for_file")]
        public string? EnvironmentalProtectionForFile { get; set; }

        [BsonElement("disposal_of_medical")]
        public string? DisposalOfMedical { get; set; }

        [BsonElement("disposal_of_medical_other")]
        public string? DisposalOfMedicalOther { get; set; }

        [BsonElement("disposal_of_medical_file")]
        public string? DisposalOfMedicalFile { get; set; }

        [BsonElement("of_medical_waste")]
        public string? OfMedicalWaste { get; set; }

        [BsonElement("of_medical_waste_other")]
        public string? OfMedicalWasteOther { get; set; }

        [BsonElement("of_medical_waste_file")]
        public string? OfMedicalWasteFile { get; set; }

        [BsonElement("on_the_infection")]
        public string? OnTheInfection { get; set; }

        [BsonElement("on_the_infection_other")]
        public string? OnTheInfectionOther { get; set; }

        [BsonElement("on_the_infection_file")]
        public string? OnTheInfectionFile { get; set; }

        [BsonElement("instructions_and_preventive")]
        public string? InstructionsAndPreventive { get; set; }

        [BsonElement("instructions_and_preventive_other")]
        public string? InstructionsAndPreventiveOther { get; set; }

        [BsonElement("instructions_and_preventive_file")]
        public string? InstructionsAndPreventiveFile { get; set; }

        [BsonElement("infection_control_practitioners")]
        public string? InfectionControlPractitioners { get; set; }

        [BsonElement("infection_control_practitioners_other")]
        public string? InfectionControlPractitionersOther { get; set; }

        [BsonElement("infection_control_practitioners_file")]
        public string? InfectionControlPractitionersFile { get; set; }

        [BsonElement("and_a_ramp")]
        public string? AndARamp { get; set; }

        [BsonElement("and_a_ramp_other")]
        public string? AndARampOther { get; set; }

        [BsonElement("and_a_ramp_file")]
        public string? AndARampFile { get; set; }

        [BsonElement("there_an_area")]
        public string? ThereAnArea { get; set; }

        [BsonElement("there_an_area_other")]
        public string? ThereAnAreaOther { get; set; }

        [BsonElement("there_an_area_file")]
        public string? ThereAnAreaFile { get; set; }

        [BsonElement("isolation_room_dressing")]
        public string? IsolationRoomDressing { get; set; }

        [BsonElement("isolation_room_dressing_other")]
        public string? IsolationRoomDressingOther { get; set; }

        [BsonElement("isolation_room_dressing_file")]
        public string? IsolationRoomDressingFile { get; set; }

        [BsonElement("waiting_areas_for")]
        public string? WaitingAreasFor { get; set; }

        [BsonElement("waiting_areas_for_other")]
        public string? WaitingAreasForOther { get; set; }

        [BsonElement("waiting_areas_for_file")]
        public string? WaitingAreasForFile { get; set; }

        [BsonElement("ambulance_that_meets")]
        public string? AmbulanceThatMeets { get; set; }

        [BsonElement("ambulance_that_meets_other")]
        public string? AmbulanceThatMeetsOther { get; set; }

        [BsonElement("ambulance_that_meets_file")]
        public string? AmbulanceThatMeetsFile { get; set; }

        [BsonElement("is_there_isolation")]
        public string? IsThereIsolation { get; set; }

        [BsonElement("is_there_isolation_other")]
        public string? IsThereIsolationOther { get; set; }

        [BsonElement("is_there_isolation_file")]
        public string? IsThereIsolationFile { get; set; }

        [BsonElement("nurses_available_to")]
        public string? NursesAvailableTo { get; set; }

        [BsonElement("nurses_available_to_other")]
        public string? NursesAvailableToOther { get; set; }

        [BsonElement("nurses_available_to_file")]
        public string? NursesAvailableToFile { get; set; }

        [BsonElement("deputy_doctor_and")]
        public string? DeputyDoctorAnd { get; set; }

        [BsonElement("deputy_doctor_and_other")]
        public string? DeputyDoctorAndOther { get; set; }

        [BsonElement("deputy_doctor_and_file")]
        public string? DeputyDoctorAndFile { get; set; }

        [BsonElement("there_enough_nurses")]
        public string? ThereEnoughNurses { get; set; }

        [BsonElement("there_enough_nurses_other")]
        public string? ThereEnoughNursesOther { get; set; }

        [BsonElement("there_enough_nurses_file")]
        public string? ThereEnoughNursesFile { get; set; }

        [BsonElement("and_in_sufficient")]
        public string? AndInSufficient { get; set; }

        [BsonElement("and_in_sufficient_other")]
        public string? AndInSufficientOther { get; set; }

        [BsonElement("and_in_sufficient_file")]
        public string? AndInSufficientFile { get; set; }

        [BsonElement("transferring_patients_from")]
        public string? TransferringPatientsFrom { get; set; }

        [BsonElement("transferring_patients_from_other")]
        public string? TransferringPatientsFromOther { get; set; }

        [BsonElement("transferring_patients_from_file")]
        public string? TransferringPatientsFromFile { get; set; }

        [BsonElement("obligation_to_report")]
        public string? ObligationToReport { get; set; }

        [BsonElement("obligation_to_report_other")]
        public string? ObligationToReportOther { get; set; }

        [BsonElement("obligation_to_report_file")]
        public string? ObligationToReportFile { get; set; }

        [BsonElement("is_there_commitment")]
        public string? IsThereCommitment { get; set; }

        [BsonElement("is_there_commitment_other")]
        public string? IsThereCommitmentOther { get; set; }

        [BsonElement("is_there_commitment_file")]
        public string? IsThereCommitmentFile { get; set; }

        [BsonElement("emergency_department_registration")]
        public string? EmergencyDepartmentRegistration { get; set; }

        [BsonElement("emergency_department_registration_other")]
        public string? EmergencyDepartmentRegistrationOther { get; set; }

        [BsonElement("emergency_department_registration_file")]
        public string? EmergencyDepartmentRegistrationFile { get; set; }

        [BsonElement("clinics_available_that")]
        public string? ClinicsAvailableThat { get; set; }

        [BsonElement("clinics_available_thatother")]
        public string? ClinicsAvailableThatOther { get; set; }

        [BsonElement("clinics_available_that_file")]
        public string? ClinicsAvailableThatFile { get; set; }

        [BsonElement("doctors_clinics_the")]
        public string? DoctorsClinicsThe { get; set; }

        [BsonElement("doctors_clinics_the_other")]
        public string? DoctorsClinicsTheOther { get; set; }

        [BsonElement("doctors_clinics_the_file")]
        public string? DoctorsClinicsTheFile { get; set; }

        [BsonElement("during_the_committee")]
        public string? DuringTheCommittee { get; set; }

        [BsonElement("during_the_committee_other")]
        public string? DuringTheCommitteeOther { get; set; }

        [BsonElement("during_the_committee_file")]
        public string? DuringTheCommitteeFile { get; set; }

        [BsonElement("of_policies_and")]
        public string? OfPoliciesAnd { get; set; }

        [BsonElement("of_policies_and_other")]
        public string? OfPoliciesAndOther { get; set; }

        [BsonElement("of_policies_and_file")]
        public string? OfPoliciesAndFile { get; set; }

        [BsonElement("institution_who_is")]
        public string? InstitutionWhoIs { get; set; }

        [BsonElement("institution_who_is_other")]
        public string? InstitutionWhoIsother { get; set; }

        [BsonElement("institution_who_is_file")]
        public string? InstitutionWhoIsFile { get; set; }

        [BsonElement("department_have_general")]
        public string? DepartmentHaveGeneral { get; set; }

        [BsonElement("department_have_general_other")]
        public string? DepartmentHaveGeneralOther { get; set; }

        [BsonElement("department_have_general_file")]
        public string? DepartmentHaveGeneralFile { get; set; }

        [BsonElement("reports_to_the")]
        public string? ReportsToThe { get; set; }

        [BsonElement("reports_to_the_other")]
        public string? ReportsToTheOther { get; set; }

        [BsonElement("reports_to_the_file")]
        public string? ReportsToTheFile { get; set; }

        [BsonElement("and_the_safety")]
        public string? AndTheSafety { get; set; }

        [BsonElement("and_the_safety_Other")]
        public string? AndTheSafetyOther { get; set; }

        [BsonElement("and_the_safety_file")]
        public string? AndTheSafetyFile { get; set; }

        [BsonElement("a_specialized_radiation")]
        public string? ASpecializedRadiation { get; set; }

        [BsonElement("a_specialized_radiation_other")]
        public string? ASpecializedRadiationOther { get; set; }

        [BsonElement("a_specialized_radiation_file")]
        public string? ASpecializedRadiationFile { get; set; }

        [BsonElement("is_there_interventional")]
        public string? IsThereInterventional { get; set; }

        [BsonElement("is_there_interventional_other")]
        public string? IsThereInterventionalOther { get; set; }

        [BsonElement("is_there_interventional_file")]
        public string? IsThereInterventionalFile { get; set; }

        [BsonElement("accordance_with_the")]
        public string? AccordanceWithThe { get; set; }

        [BsonElement("accordance_with_the_other")]
        public string? AccordanceWithTheOther { get; set; }

        [BsonElement("accordance_with_the_file")]
        public string? AccordanceWithTheFile { get; set; }

        [BsonElement("and_the_light")]
        public string? AndTheLight { get; set; }

        [BsonElement("and_the_light_other")]
        public string? AndTheLightOther { get; set; }

        [BsonElement("and_the_light_file")]
        public string? AndTheLightFile { get; set; }

        [BsonElement("are_radiology_reports")]
        public string? AreRadiologyReports { get; set; }

        [BsonElement("are_radiology_reports_other")]
        public string? AreRadiologyReportsOther { get; set; }

        [BsonElement("are_radiology_reports_file")]
        public string? AreRadiologyReportsFile { get; set; }

        [BsonElement("is_the_radiation")]
        public string? IsTheRadiation { get; set; }

        [BsonElement("is_the_radiation_other")]
        public string? IsTheRadiationOther { get; set; }

        [BsonElement("is_the_radiation_file")]
        public string? IsTheRadiationFile { get; set; }

        [BsonElement("radiology_department_and")]
        public string? RadiologyDepartmentAnd { get; set; }

        [BsonElement("radiology_department_and_other")]
        public string? RadiologyDepartmentAndOther { get; set; }

        [BsonElement("radiology_department_and_file")]
        public string? RadiologyDepartmentAndFile { get; set; }

        [BsonElement("nursing_care_meet")]
        public string? NursingCareMeet { get; set; }

        [BsonElement("nursing_care_meet_other")]
        public string? NursingCareMeetOther { get; set; }

        [BsonElement("nursing_care_meet_file")]
        public string? NursingCareMeetFile { get; set; }

        [BsonElement("for_every_25")]
        public string? ForEvery25 { get; set; }

        [BsonElement("for_every_25_other")]
        public string? ForEvery25Other { get; set; }

        [BsonElement("for_every_25_file")]
        public string? ForEvery25File { get; set; }

        [BsonElement("in_compliance_with")]
        public string? InComplianceWith { get; set; }

        [BsonElement("in_compliance_with_other")]
        public string? InComplianceWithOther { get; set; }

        [BsonElement("in_compliance_with_file")]
        public string? InComplianceWithFile { get; set; }

        [BsonElement("sleeping_roomsconforming_to")]
        public string? SleepingRoomsconformingTo { get; set; }

        [BsonElement("sleeping_roomsconforming_to_other")]
        public string? SleepingRoomsconformingToOther { get; set; }

        [BsonElement("sleeping_roomsconforming_to_file")]
        public string? SleepingRoomsconformingToFile { get; set; }

        [BsonElement("than_one_patient")]
        public string? ThanOnePatient { get; set; }

        [BsonElement("than_one_patient_other")]
        public string? ThanOnePatientOther { get; set; }

        [BsonElement("than_one_patient_file")]
        public string? ThanOnePatientFile { get; set; }

        [BsonElement("patient_beds_inside")]
        public string? PatientBedsInside { get; set; }

        [BsonElement("patient_beds_inside_other")]
        public string? PatientBedsInsideOther { get; set; }

        [BsonElement("patient_beds_inside_file")]
        public string? PatientBedsInsideFile { get; set; }

        [BsonElement("hospital_beds_commensurate")]
        public string? HospitalBedsCommensurate { get; set; }

        [BsonElement("hospital_beds_commensurate_other")]
        public string? HospitalBedsCommensurateOther { get; set; }

        [BsonElement("hospital_beds_commensurate_file")]
        public string? HospitalBedsCommensurateFile { get; set; }

        [BsonElement("residence_suites_professional")]
        public string? ResidenceSuitesProfessional { get; set; }

        [BsonElement("residence_suites_professional_other")]
        public string? ResidenceSuitesProfessionalOther { get; set; }

        [BsonElement("residence_suites_professional_file")]
        public string? ResidenceSuitesProfessionalFile { get; set; }

        [BsonElement("room_equipmentconforming_to")]
        public string? RoomEquipmentconformingTo { get; set; }

        [BsonElement("room_equipmentconforming_to_other")]
        public string? RoomEquipmentconformingToOther { get; set; }

        [BsonElement("room_equipmentconforming_to_file")]
        public string? RoomEquipmentconformingToFile { get; set; }

        [BsonElement("a_separate_entrance")]
        public string? ASeparateEntrance { get; set; }

        [BsonElement("a_separate_entrance_other")]
        public string? ASeparateEntranceOther { get; set; }

        [BsonElement("a_separate_entrance_file")]
        public string? ASeparateEntranceFile { get; set; }

        [BsonElement("beds_for_each")]
        public string? BedsForEach { get; set; }

        [BsonElement("beds_for_each_other")]
        public string? BedsForEachOther { get; set; }

        [BsonElement("beds_for_each_file")]
        public string? BedsForEachFile { get; set; }

        [BsonElement("of_clean_materials")]
        public string? OfCleanMaterials { get; set; }

        [BsonElement("of_clean_materials_other")]
        public string? OfCleanMaterialsOther { get; set; }

        [BsonElement("of_clean_materials_file")]
        public string? OfCleanMaterialsFile { get; set; }

        [BsonElement("room_solutions_and")]
        public string? RoomSolutionsAnd { get; set; }

        [BsonElement("room_solutions_and_other")]
        public string? RoomSolutionsAndOther { get; set; }

        [BsonElement("room_solutions_and_file")]
        public string? RoomSolutionsAndFile { get; set; }

        [BsonElement("a_room_available")]
        public string? ARoomAvailable { get; set; }

        [BsonElement("a_room_available_other")]
        public string? ARoomAvailableOther { get; set; }

        [BsonElement("a_room_available_file")]
        public string? ARoomAvailableFile { get; set; }

        [BsonElement("medical_staff_that")]
        public string? MedicalStaffThat { get; set; }

        [BsonElement("medical_staff_that_other")]
        public string? MedicalStaffThatOther { get; set; }

        [BsonElement("medical_staff_that_file")]
        public string? MedicalStaffThatFile { get; set; }

        [BsonElement("room_for_the")]
        public string? RoomForThe { get; set; }

        [BsonElement("room_for_the_other")]
        public string? RoomForTheOther { get; set; }

        [BsonElement("room_for_the_file")]
        public string? RoomForTheFile { get; set; }

        [BsonElement("patient_after_the")]
        public string? PatientAfterThe { get; set; }

        [BsonElement("patient_after_the_other")]
        public string? PatientAfterTheOther { get; set; }

        [BsonElement("patient_after_the_file")]
        public string? PatientAfterTheFile { get; set; }

        [BsonElement("first_deputy_in")]
        public string? FirstDeputyIn { get; set; }

        [BsonElement("first_deputy_in_other")]
        public string? FirstDeputyInOther { get; set; }

        [BsonElement("first_deputy_in_file")]
        public string? FirstDeputyInFile { get; set; }

        [BsonElement("diseases_that_conforms")]
        public string? DiseasesThatConforms { get; set; }

        [BsonElement("diseases_that_conforms_other")]
        public string? DiseasesThatConformsOther { get; set; }

        [BsonElement("diseases_that_conforms_file")]
        public string? DiseasesThatConformsFile { get; set; }

        [BsonElement("are_there_work")]
        public string? AreThereWork { get; set; }

        [BsonElement("are_there_work_other")]
        public string? AreThereWorkOther { get; set; }

        [BsonElement("are_there_work_file")]
        public string? AreThereWorkFile { get; set; }

        [BsonElement("practitioners_surgery_department")]
        public string? PractitionersSurgeryDepartment { get; set; }

        [BsonElement("practitioners_surgery_department_other")]
        public string? PractitionersSurgeryDepartmentOther { get; set; }

        [BsonElement("practitioners_surgery_department_file")]
        public string? PractitionersSurgeryDepartmentFile { get; set; }

        [BsonElement("there_two_or")]
        public string? ThereTwoOr { get; set; }

        [BsonElement("there_two_or_other")]
        public string? ThereTwoOrOther { get; set; }

        [BsonElement("there_two_or_file")]
        public string? ThereTwoOrFile { get; set; }

        [BsonElement("is_there_caesarean")]
        public string? IsTherecaesarean { get; set; }

        [BsonElement("is_there_caesarean_other")]
        public string? IsTherecaesareanOther { get; set; }

        [BsonElement("is_there_caesarean_file")]
        public string? IsTherecaesareanFile { get; set; }

        [BsonElement("is_there_babies")]
        public string? IsThereBabies { get; set; }

        [BsonElement("is_there_babies_other")]
        public string? IsThereBabiesOther { get; set; }

        [BsonElement("is_there_babies_file")]
        public string? IsThereBabiesFile { get; set; }

        [BsonElement("two_nurseries_available")]
        public string? TwoNurseriesAvailable { get; set; }

        [BsonElement("two_nurseries_available_other")]
        public string? TwoNurseriesAvailableOther { get; set; }

        [BsonElement("two_nurseries_available_file")]
        public string? TwoNurseriesAvailableFile { get; set; }

        [BsonElement("educator_who_specializes")]
        public string? EducatorWhoSpecializes { get; set; }

        [BsonElement("educator_who_specializes_other")]
        public string? EducatorWhoSpecializesOther { get; set; }

        [BsonElement("educator_who_specializes_file")]
        public string? EducatorWhoSpecializesFile { get; set; }

        [BsonElement("to_the_breast")]
        public string? ToTheBreast { get; set; }

        [BsonElement("to_the_breast_other")]
        public string? ToTheBreastOther { get; set; }

        [BsonElement("to_the_breast_file")]
        public string? ToTheBreastFile { get; set; }

        [BsonElement("rate_of_vaginal")]
        public string? RateOfVaginal { get; set; }

        [BsonElement("rate_of_vaginal_other")]
        public string? RateOfVaginalOther { get; set; }

        [BsonElement("rate_of_vaginal_file")]
        public string? RateOfVaginalFile { get; set; }

        [BsonElement("a_valid_neonatal")]
        public string? AValidNeonatal { get; set; }

        [BsonElement("a_valid_neonatal_other")]
        public string? AValidNeonatalOther { get; set; }

        [BsonElement("a_valid_neonatal_file")]
        public string? AValidNeonatalFile { get; set; }

        [BsonElement("hepatitis_c_virus")]
        public string? HepatitisCVirus { get; set; }

        [BsonElement("hepatitis_c_virus_other")]
        public string? HepatitisCVirusOther { get; set; }

        [BsonElement("hepatitis_c_virus_file")]
        public string? HepatitisCVirusFile { get; set; }

        [BsonElement("neonatal_intensive_care")]
        public string? NeonatalIntensiveCare { get; set; }

        [BsonElement("neonatal_intensive_care_other")]
        public string? NeonatalIntensiveCareOther { get; set; }

        [BsonElement("neonatal_intensive_care_file")]
        public string? NeonatalIntensiveCareFile { get; set; }

        [BsonElement("the_prevention_of")]
        public string? ThePreventionOf { get; set; }

        [BsonElement("the_prevention_of_other")]
        public string? ThePreventionOfOther { get; set; }

        [BsonElement("the_prevention_of_file")]
        public string? ThePreventionOfFile { get; set; }

        [BsonElement("nurseries_available_for")]
        public string? NurseriesAvailableFor { get; set; }

        [BsonElement("nurseries_available_for_other")]
        public string? NurseriesAvailableForOther { get; set; }

        [BsonElement("nurseries_available_for_file")]
        public string? NurseriesAvailableForFile { get; set; }

        [BsonElement("maternity_ward_practitioners")]
        public string? MaternityWardPractitioners { get; set; }

        [BsonElement("maternity_ward_practitioners_other")]
        public string? MaternityWardPractitionersOther { get; set; }

        [BsonElement("maternity_ward_practitioners_file")]
        public string? MaternityWardPractitionersFile { get; set; }

        [BsonElement("ratio_10_of")]
        public string? Ratio10Of { get; set; }

        [BsonElement("ratio_10_of_other")]
        public string? Ratio10OfOther { get; set; }

        [BsonElement("ratio_10_of_file")]
        public string? Ratio10OfFile { get; set; }

        [BsonElement("for_the_entrance")]
        public string? ForTheEntrance { get; set; }

        [BsonElement("for_the_entrance_other")]
        public string? ForTheEntranceOther { get; set; }

        [BsonElement("for_the_entrance_file")]
        public string? ForTheEntranceFile { get; set; }

        [BsonElement("there_a_nurse")]
        public string? ThereANurse { get; set; }

        [BsonElement("there_a_nurse_other")]
        public string? ThereANurseOther { get; set; }

        [BsonElement("there_a_nurse_file")]
        public string? ThereANurseFile { get; set; }

        [BsonElement("of_each_bed")]
        public string? OfEachBed { get; set; }

        [BsonElement("of_each_bed_other")]
        public string? OfEachBedOther { get; set; }

        [BsonElement("of_each_bed_file")]
        public string? OfEachBedFile { get; set; }

        [BsonElement("does_the_level")]
        public string? DoesTheLevel { get; set; }

        [BsonElement("does_the_level_other")]
        public string? DoesTheLevelOther { get; set; }

        [BsonElement("does_the_level_file")]
        public string? DoesTheLevelFile { get; set; }

        [BsonElement("practitioners_icu_department")]
        public string? PractitionersIcuDepartment { get; set; }

        [BsonElement("practitioners_icu_department_other")]
        public string? PractitionersIcuDepartmentOther { get; set; }

        [BsonElement("practitioners_icu_department_file")]
        public string? PractitionersIcuDepartmentFile { get; set; }

        [BsonElement("contract_with_a")]
        public string? ContractWithA { get; set; }

        [BsonElement("contract_with_a_other")]
        public string? ContractWithAOther { get; set; }

        [BsonElement("contract_with_a_file")]
        public string? ContractWithAFile { get; set; }

        [BsonElement("sterilization_room_that")]
        public string? SterilizationRoomThat { get; set; }

        [BsonElement("sterilization_room_that_other")]
        public string? SterilizationRoomThatOther { get; set; }

        [BsonElement("sterilization_room_that_file")]
        public string? SterilizationRoomThatFile { get; set; }

        [BsonElement("and_the_saudi")]
        public string? AndTheSaudi { get; set; }

        [BsonElement("and_the_saudi_other")]
        public string? AndTheSaudiOther { get; set; }

        [BsonElement("and_the_saudi_file")]
        public string? AndTheSaudiFile { get; set; }

        [BsonElement("sufficient_number_of")]
        public string? SufficientNumberOf { get; set; }

        [BsonElement("sufficient_number_of_other")]
        public string? SufficientNumberOfOther { get; set; }

        [BsonElement("sufficient_number_of_file")]
        public string? SufficientNumberOfFile { get; set; }

        [BsonElement("health_certificate_for")]
        public string? HealthCertificateFor { get; set; }

        [BsonElement("health_certificate_for_other")]
        public string? HealthCertificateForOther { get; set; }

        [BsonElement("health_certificate_for_file")]
        public string? HealthCertificateForFile { get; set; }

        [BsonElement("sterilization_department_practitioners_hold")]
        public string? SterilizationDepartmentPractitionersHold { get; set; }

        [BsonElement("sterilization_department_practitioners_hold_other")]
        public string? SterilizationDepartmentPractitionersHoldOther { get; set; }

        [BsonElement("sterilization_department_practitioners_hold_file")]
        public string? SterilizationDepartmentPractitionersHoldFile { get; set; }

        [BsonElement("allows_the_passage")]
        public string? AllowsThePassage { get; set; }

        [BsonElement("allows_the_passage_other")]
        public string? AllowsThePassageOther { get; set; }

        [BsonElement("allows_the_passage_file")]
        public string? AllowsThePassageFile { get; set; }

        [BsonElement("is_there_separating")]
        public string? IsThereSeparating { get; set; }

        [BsonElement("is_there_separating_other")]
        public string? IsThereSeparatingOther { get; set; }

        [BsonElement("is_there_separating_file")]
        public string? IsThereSeparatingFile { get; set; }

        [BsonElement("the_requirements_for")]
        public string? TheRequirementsFor { get; set; }

        [BsonElement("the_requirements_for_other")]
        public string? TheRequirementsForOther { get; set; }

        [BsonElement("the_requirements_for_file")]
        public string? TheRequirementsForFile { get; set; }

        [BsonElement("is_there_toilet")]
        public string? IsThereToilet { get; set; }

        [BsonElement("is_there_toilet_other")]
        public string? IsThereToiletOther { get; set; }

        [BsonElement("is_there_toilet_file")]
        public string? IsThereToiletFile { get; set; }

        [BsonElement("insulation_department_from_the_saudi")]
        public string? InsulationDepartmentFromTheSaudi { get; set; }

        [BsonElement("insulation_department_from_the_saudi_other")]
        public string? InsulationDepartmentFromTheSaudiOther { get; set; }

        [BsonElement("insulation_department_from_the_saudi_file")]
        public string? InsulationDepartmentFromTheSaudiFile { get; set; }

        [BsonElement("sufficient_and_in")]
        public string? SufficientAndIn { get; set; }

        [BsonElement("sufficient_and_in_other")]
        public string? SufficientAndInOther { get; set; }

        [BsonElement("sufficient_and_in_file")]
        public string? SufficientAndInFile { get; set; }

        [BsonElement("air_conditioning_that")]
        public string? AirConditioningThat { get; set; }

        [BsonElement("air_conditioning_that_other")]
        public string? AirConditioningThatOther { get; set; }

        [BsonElement("air_conditioning_that_file")]
        public string? AirConditioningThatFile { get; set; }

        [BsonElement("there_a_reception")]
        public string? ThereAReception { get; set; }

        [BsonElement("there_a_reception_other")]
        public string? ThereAReceptionOther { get; set; }

        [BsonElement("there_a_reception_file")]
        public string? ThereAReceptionFile { get; set; }

        [BsonElement("a_separate_room")]
        public string? ASeparateRoom { get; set; }

        [BsonElement("a_separate_room_other")]
        public string? ASeparateRoomOther { get; set; }

        [BsonElement("a_separate_room_file")]
        public string? ASeparateRoomFile { get; set; }

        [BsonElement("the_devices_of")]
        public string? TheDevicesOf { get; set; }

        [BsonElement("the_devices_of_other")]
        public string? TheDevicesOfOther { get; set; }

        [BsonElement("the_devices_of_file")]
        public string? TheDevicesOfFile { get; set; }

        [BsonElement("of_the_immunology")]
        public string? OfTheImmunology { get; set; }

        [BsonElement("of_the_immunology_other")]
        public string? OfTheImmunologyOther { get; set; }

        [BsonElement("of_the_immunology_file")]
        public string? OfTheImmunologyFile { get; set; }

        [BsonElement("drug_testing_department")]
        public string? DrugTestingDepartment { get; set; }

        [BsonElement("drug_testing_department_other")]
        public string? DrugTestingDepartmentOther { get; set; }

        [BsonElement("drug_testing_department_file")]
        public string? DrugTestingDepartmentFile { get; set; }

        [BsonElement("pcr_conforming_to")]
        public string? PcrConformingTo { get; set; }

        [BsonElement("pcr_conforming_to_other")]
        public string? PcrConformingToOther { get; set; }

        [BsonElement("pcr_conforming_to_file")]
        public string? PcrConformingToFile { get; set; }

        [BsonElement("each_section_of")]
        public string? EachSectionOf { get; set; }

        [BsonElement("each_section_of_other")]
        public string? EachSectionOfOther { get; set; }

        [BsonElement("each_section_of_file")]
        public string? EachSectionOfFile { get; set; }

        [BsonElement("institutions_compliant_with")]
        public string? InstitutionsCompliantWith { get; set; }

        [BsonElement("institutions_compliant_with_other")]
        public string? InstitutionsCompliantWithOther { get; set; }

        [BsonElement("institutions_compliant_with_file")]
        public string? InstitutionsCompliantWithFile { get; set; }

        [BsonElement("room_conforming_to")]
        public string? RoomConformingTo { get; set; }

        [BsonElement("room_conforming_to_other")]
        public string? RoomConformingToOther { get; set; }

        [BsonElement("room_conforming_to_file")]
        public string? RoomConformingToFile { get; set; }

        [BsonElement("to_in_the")]
        public string? ToInThe { get; set; }

        [BsonElement("to_in_the_other")]
        public string? ToInTheOther { get; set; }

        [BsonElement("to_in_the_file")]
        public string? ToInTheFile { get; set; }

        [BsonElement("of_the_sorting")]
        public string? OfTheSorting { get; set; }

        [BsonElement("of_the_sorting_other")]
        public string? OfTheSortingOther { get; set; }

        [BsonElement("of_the_sorting_file")]
        public string? OfTheSortingFile { get; set; }

        [BsonElement("professional_laboratory_department")]
        public string? ProfessionalLaboratoryDepartment { get; set; }

        [BsonElement("professional_laboratory_department_other")]
        public string? ProfessionalLaboratoryDepartmentOther { get; set; }

        [BsonElement("professional_laboratory_department_file")]
        public string? ProfessionalLaboratoryDepartmentFile { get; set; }

        [BsonElement("organizing_the_supply")]
        public string? OrganizingTheSupply { get; set; }

        [BsonElement("organizing_the_supply_other")]
        public string? OrganizingTheSupplyOther { get; set; }

        [BsonElement("organizing_the_supply_file")]
        public string? OrganizingTheSupplyFile { get; set; }

        [BsonElement("for_the_custody")]
        public string? ForTheCustody { get; set; }

        [BsonElement("for_the_custody_other")]
        public string? ForTheCustodyOther { get; set; }

        [BsonElement("for_the_custody_file")]
        public string? ForTheCustodyFile { get; set; }

        [BsonElement("good_thermal_and")]
        public string? GoodThermalAnd { get; set; }

        [BsonElement("good_thermal_and_other")]
        public string? GoodThermalAndOther { get; set; }

        [BsonElement("good_thermal_and_file")]
        public string? GoodThermalAndFile { get; set; }

        [BsonElement("thermometer_available_from")]
        public string? ThermometerAvailableFrom { get; set; }

        [BsonElement("thermometer_available_from_other")]
        public string? ThermometerAvailableFromOther { get; set; }

        [BsonElement("thermometer_available_from_file")]
        public string? ThermometerAvailableFromFile { get; set; }

        [BsonElement("store_medicines_that")]
        public string? StoreMedicinesThat { get; set; }

        [BsonElement("store_medicines_that_other")]
        public string? StoreMedicinesThatOther { get; set; }

        [BsonElement("store_medicines_that_file")]
        public string? StoreMedicinesThatFile { get; set; }

        [BsonElement("available_for_the")]
        public string? AvailableForThe { get; set; }

        [BsonElement("available_for_the_other")]
        public string? AvailableForTheOther { get; set; }

        [BsonElement("available_for_the_file")]
        public string? AvailableForTheFile { get; set; }

        [BsonElement("measures_available_conforming")]
        public string? MeasuresAvailableConforming { get; set; }

        [BsonElement("measures_available_conforming_other")]
        public string? MeasuresAvailableConforming_other { get; set; }

        [BsonElement("measures_available_conforming_file")]
        public string? MeasuresAvailableConformingFile { get; set; }

        [BsonElement("licensed_to_sell")]
        public string? LicensedToSell { get; set; }

        [BsonElement("licensed_to_sell_other")]
        public string? LicensedToSellOther { get; set; }

        [BsonElement("licensed_to_sell_file")]
        public string? LicensedToSellFile { get; set; }

        [BsonElement("memorize_and_print")]
        public string? MemorizeAndPrint { get; set; }

        [BsonElement("memorize_and_print_other")]
        public string? MemorizeAndPrintOther { get; set; }

        [BsonElement("memorize_and_print_file")]
        public string? MemorizeAndPrintFile { get; set; }

        [BsonElement("approved_pharmacy_in")]
        public string? ApprovedPharmacyIn { get; set; }

        [BsonElement("approved_pharmacy_in_other")]
        public string? ApprovedPharmacyInOther { get; set; }

        [BsonElement("approved_pharmacy_in_file")]
        public string? ApprovedPharmacyInFile { get; set; }

        [BsonElement("preparations_laboratory_that")]
        public string? PreparationsLaboratoryThat { get; set; }

        [BsonElement("preparations_laboratory_that_other")]
        public string? PreparationsLaboratoryThatOther { get; set; }

        [BsonElement("preparations_laboratory_that_file")]
        public string? PreparationsLaboratoryThatFile { get; set; }

        [BsonElement("available_pharmaceutical_preparations")]
        public string? AvailablePharmaceuticalPreparations { get; set; }

        [BsonElement("available_pharmaceutical_preparations_other")]
        public string? AvailablePharmaceuticalPreparationsOther { get; set; }

        [BsonElement("available_pharmaceutical_preparations_file")]
        public string? AvailablePharmaceuticalPreparationsFile { get; set; }

        [BsonElement("separate_section_for")]
        public string? SeparateSectionFor { get; set; }

        [BsonElement("separate_section_for_other")]
        public string? SeparateSectionForOther { get; set; }

        [BsonElement("separate_section_for_file")]
        public string? SeparateSectionForFile { get; set; }

        [BsonElement("and_poisons_that")]
        public string? AndPoisonsThat { get; set; }

        [BsonElement("and_poisons_that_other")]
        public string? AndPoisonsThatOther { get; set; }

        [BsonElement("and_poisons_that_file")]
        public string? AndPoisonsThatFile { get; set; }

        [BsonElement("section_for_damaged")]
        public string? SectionForDamaged { get; set; }

        [BsonElement("section_for_damaged_other")]
        public string? SectionForDamagedOther { get; set; }

        [BsonElement("section_for_damaged_file")]
        public string? SectionForDamagedFile { get; set; }

        [BsonElement("to_that_and")]
        public string? ToThatAnd { get; set; }

        [BsonElement("to_that_and_other")]
        public string? ToThatAndOther { get; set; }

        [BsonElement("to_that_and_file")]
        public string? ToThatAndFile { get; set; }

        [BsonElement("saudi_pharmacy")]
        public string? SaudiPharmacy { get; set; }

        [BsonElement("saudi_pharmacy_other")]
        public string? SaudiPharmacyOther { get; set; }

        [BsonElement("saudi_pharmacy_file")]
        public string? SaudiPharmacyFile { get; set; }

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

        [BsonElement("rooms_available_that")]
        public string? RoomsAvailableThat { get; set; }

        [BsonElement("rooms_available_that_other")]
        public string? RoomsAvailableThatOther { get; set; }

        [BsonElement("rooms_available_that_file")]
        public string? RoomsAvailableThatFile { get; set; }

        [BsonElement("re_hand_washing")]
        public string? ReHandWashing { get; set; }

        [BsonElement("re_hand_washing_other")]
        public string? ReHandWashingOther { get; set; }

        [BsonElement("re_hand_washing_file")]
        public string? ReHandWashingFile { get; set; }

        [BsonElement("does_not_lead")]
        public string? DoesNotLead { get; set; }

        [BsonElement("does_not_lead_other")]
        public string? DoesNotLeadOther { get; set; }

        [BsonElement("does_not_lead_file")]
        public string? DoesNotLeadFile { get; set; }

        [BsonElement("does_the_pathology")]
        public string? DoesThePathology { get; set; }

        [BsonElement("does_the_pathology_other")]
        public string? DoesThePathologyOther { get; set; }

        [BsonElement("does_the_pathology_file")]
        public string? DoesThePathologyFile { get; set; }

        [BsonElement("specifications_of_the")]
        public string? SpecificationsOfThe { get; set; }

        [BsonElement("specifications_of_the_other")]
        public string? SpecificationsOfTheOther { get; set; }

        [BsonElement("specifications_of_the_file")]
        public string? SpecificationsOfTheFile { get; set; }

        [BsonElement("the_water_purification")]
        public string? TheWaterPurification { get; set; }

        [BsonElement("the_water_purification_other")]
        public string? TheWaterPurificationOther { get; set; }

        [BsonElement("the_water_purification_file")]
        public string? TheWaterPurificationFile { get; set; }

        [BsonElement("the_elements_and")]
        public string? TheElementsAnd { get; set; }

        [BsonElement("the_elements_and_other")]
        public string? TheElementsAndOther { get; set; }

        [BsonElement("the_elements_and_file")]
        public string? TheElementsAndFile { get; set; }

        [BsonElement("of_treated_water")]
        public string? OfTreatedWater { get; set; }

        [BsonElement("of_treated_water_other")]
        public string? OfTreatedWaterOther { get; set; }

        [BsonElement("of_treated_water_file")]
        public string? OfTreatedWaterFile { get; set; }

        [BsonElement("therapeutic_areas_dialysis_department_registration")]
        public string? TherapeuticAreasDialysisDepartmentRegistration { get; set; }

        [BsonElement("therapeutic_areas_dialysis_department_registration_other")]
        public string? TherapeuticAreasDialysisDepartmentRegistrationOther { get; set; }

        [BsonElement("therapeutic_areas_dialysis_department_registration_file")]
        public string? TherapeuticAreasDialysisDepartmentRegistrationFile { get; set; }

        [BsonElement("the_blood_bank")]
        public string? TheBloodBank { get; set; }

        [BsonElement("the_blood_bank_other")]
        public string? TheBloodBankOther { get; set; }

        [BsonElement("the_blood_bank_file")]
        public string? TheBloodBankFile { get; set; }

        [BsonElement("medical_examinations_taking")]
        public string? MedicalExaminationsTaking { get; set; }

        [BsonElement("medical_examinations_taking_other")]
        public string? MedicalExaminationsTakingOther { get; set; }

        [BsonElement("medical_examinations_taking_file")]
        public string? MedicalExaminationsTakingFile { get; set; }

        [BsonElement("department_available_to")]
        public string? DepartmentAvailableTo { get; set; }

        [BsonElement("department_available_to_other")]
        public string? DepartmentAvailableToOther { get; set; }

        [BsonElement("department_available_to_file")]
        public string? DepartmentAvailableToFile { get; set; }

        [BsonElement("refrigerator_deep_cooling")]
        public string? RefrigeratorDeepCooling { get; set; }

        [BsonElement("refrigerator_deep_cooling_other")]
        public string? RefrigeratorDeepCoolingOther { get; set; }

        [BsonElement("refrigerator_deep_cooling_file")]
        public string? RefrigeratorDeepCoolingFile { get; set; }

        [BsonElement("there_a_section")]
        public string? ThereASection { get; set; }

        [BsonElement("there_a_section_other")]
        public string? ThereASectionOther { get; set; }

        [BsonElement("there_a_section_file")]
        public string? ThereASectionFile { get; set; }

        [BsonElement("by_the_ministry")]
        public string? ByTheMinistry { get; set; }

        [BsonElement("by_the_ministry_other")]
        public string? ByTheMinistryOther { get; set; }

        [BsonElement("by_the_ministry_file")]
        public string? ByTheMinistryFile { get; set; }

        [BsonElement("available_to_conduct")]
        public string? AvailableToConduct { get; set; }

        [BsonElement("available_to_conduct_other")]
        public string? AvailableToConductOther { get; set; }

        [BsonElement("available_to_conduct_file")]
        public string? AvailableToConductFile { get; set; }

        [BsonElement("warehouse_available_to")]
        public string? WarehouseAvailableTo { get; set; }

        [BsonElement("warehouse_available_to_other")]
        public string? WarehouseAvailableToOther { get; set; }

        [BsonElement("warehouse_available_to_file")]
        public string? WarehouseAvailableToFile { get; set; }

        [BsonElement("blood_transfusion_and")]
        public string? BloodTransfusionAnd { get; set; }

        [BsonElement("blood_transfusion_and_other")]
        public string? BloodTransfusionAndOther { get; set; }

        [BsonElement("blood_transfusion_and_file")]
        public string? BloodTransfusionAndFile { get; set; }

        [BsonElement("is_there_recording")]
        public string? IsThereRecording { get; set; }

        [BsonElement("is_there_recording_other")]
        public string? IsThereRecordingOther { get; set; }

        [BsonElement("is_there_recording_file")]
        public string? IsThereRecordingFile { get; set; }

        [BsonElement("safety_and_security")]
        public string? SafetyAndSecurity { get; set; }

        [BsonElement("safety_and_security_other")]
        public string? SafetyAndSecurityOther { get; set; }

        [BsonElement("safety_and_security_file")]
        public string? SafetyAndSecurityFile { get; set; }

        [BsonElement("elimination_of_hepatitis")]
        public string? EliminationOfHepatitis { get; set; }

        [BsonElement("elimination_of_hepatitis_other")]
        public string? EliminationOfHepatitisOther { get; set; }

        [BsonElement("elimination_of_hepatitis_file")]
        public string? EliminationOfHepatitisFile { get; set; }

        [BsonElement("plasma_for_the")]
        public string? PlasmaForThe { get; set; }

        [BsonElement("plasma_for_the_other")]
        public string? PlasmaForTheOther { get; set; }

        [BsonElement("plasma_for_the_file")]
        public string? PlasmaForTheFile { get; set; }

        [BsonElement("withdraw_blood_from")]
        public string? WithdrawBloodFrom { get; set; }

        [BsonElement("withdraw_blood_from_other")]
        public string? WithdrawBloodFromOther { get; set; }

        [BsonElement("withdraw_blood_from_file")]
        public string? WithdrawBloodFromFile { get; set; }

        [BsonElement("the_guide_to")]
        public string? TheGuideTo { get; set; }

        [BsonElement("the_guide_to_other")]
        public string? TheGuideToOther { get; set; }

        [BsonElement("the_guide_to_file")]
        public string? TheGuideToFile { get; set; }

        [BsonElement("conduct_blood_type")]
        public string? ConductBloodType { get; set; }

        [BsonElement("conduct_blood_type_other")]
        public string? ConductBloodTypeOther { get; set; }

        [BsonElement("conduct_blood_type_file")]
        public string? ConductBloodTypeFile { get; set; }

        [BsonElement("counting_the_inventory")]
        public string? CountingtheInventory { get; set; }

        [BsonElement("counting_the_inventory_other")]
        public string? CountingtheInventoryOther { get; set; }

        [BsonElement("counting_the_inventory_file")]
        public string? CountingtheInventoryFile { get; set; }

        [BsonElement("does_the_area")]
        public string? DoesTheArea { get; set; }

        [BsonElement("does_the_area_other")]
        public string? DoesTheAreaOther { get; set; }

        [BsonElement("does_the_area_file")]
        public string? DoesTheAreaFile { get; set; }

        [BsonElement("basic_conditions_commission_for")]
        public string? BasicConditionsCommissionFor { get; set; }

        [BsonElement("basic_conditions_commission_for_other")]
        public string? BasicConditionsCommissionForOther { get; set; }

        [BsonElement("basic_conditions_commission_for_file")]
        public string? BasicConditionsCommissionForFile { get; set; }


        [BsonElement("owner_name")]
        public string OwnerName { get; set; } = string.Empty;


        [BsonElement("name_of_the_institution")]
        public string NameOfTheInstitution { get; set; } = string.Empty;


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


        [BsonElement("note")]
        public string Note { get; set; } = string.Empty;


        [BsonElement("image")]
        public string? Image { get; set; }


        [BsonElement("inspectors")]
        public string[]? Inspectors { get; set; }


        [BsonElement("class_name")]
        public string ClassName { get; set; } = string.Empty;


        [BsonElement("identification_file")]
        public string? IdentificationFile { get; set; }


        [BsonElement("identification_notes")]
        public string? IdentificationNotes { get; set; }

        [BsonElement("reportNumber")]
        public string ReportNumber { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
