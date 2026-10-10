using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Form
{
    [BsonIgnoreExtraElements]
    public class FormOptions : AuditInfoComplete
    {
        [StringLength(100)]
        [BsonElement("identification")]
        public List<Department>? Identification { get; set; }

        [StringLength(100)]
        [BsonElement("pharmacy_workers")]
        public List<Department>? PharmacyWorkers { get; set; }

        [StringLength(100)]
        [BsonElement("b_standards")]
        public List<Department>? BStandards { get; set; }

        [StringLength(100)]
        [BsonElement("staff_availability")]
        public List<Department>? StaffAvailability { get; set; }

        [StringLength(100)]
        [BsonElement("safety_measure")]
        public List<Department>? SafetyMeasure { get; set; }

        [StringLength(100)]
        [BsonElement("infection_control")]
        public List<Department>? InfectionControl { get; set; }

        [StringLength(100)]
        [BsonElement("sterilization")]
        public List<Department>? Sterilization { get; set; }

        [StringLength(100)]
        [BsonElement("emergency_department_for_men_and_women")]
        public List<Department>? EmergencyDepartment { get; set; }

        [StringLength(100)]
        [BsonElement("emergency_department_men_and_women")]
        public List<Department>? EmergencyDepartment_ { get; set; }

        [StringLength(100)]
        [BsonElement("emergency_department_men_women")]
        public List<Department>? EmergencyDepartmentSingle_ { get; set; }

        [StringLength(100)]
        [BsonElement("outpatient_clinics")]
        public List<Department>? OutpatientClinics { set; get; }

        [StringLength(100)]
        [BsonElement("sleeping_rooms")]
        public List<Department>? SleepingRooms { set; get; }

        [StringLength(100)]
        [BsonElement("lab")]
        public List<Department>? Lab { set; get; }

        [StringLength(100)]
        [BsonElement("emergency")]
        public List<Department>? Emergency { set; get; }

        [StringLength(100)]
        [BsonElement("icu")]
        public List<Department>? ICU { set; get; }

        [StringLength(100)]
        [BsonElement("mortuary")]
        public List<Department>? Mortuary { set; get; }

        [StringLength(100)]
        [BsonElement("dialysis")]
        public List<Department>? Dialysis { set; get; }

        [StringLength(100)]
        [BsonElement("additionalRemarks")]
        public List<Department>? AdditionalRemarks { set; get; }

        [BsonElement("reception")]
        public List<Department>? Reception { get; set; }

        [BsonElement("medical_records")]
        public List<Department>? MedicalRecords { get; set; }

        [BsonElement("administration")]
        public List<Department>? Administration { get; set; }

        [BsonElement("safety_means")]
        public List<Department>? SafetyMeans { get; set; }

        [BsonElement("fighting_the_infection")]
        public List<Department>? FightingInfection { get; set; }

        [BsonElement("x_ray_place")]
        public List<Department>? XRayPlace { get; set; }

        [BsonElement("hypnotizing_wings")]
        public List<Department>? HypnotizingWings { get; set; }

        [BsonElement("laboratory_department")]
        public List<Department>? LaboratoryDepartment { get; set; }

        [BsonElement("operations_department")]
        public List<Department>? OperationsDepartment { get; set; }

        [BsonElement("maternity_ward")]
        public List<Department>? MaternityWard { get; set; }

        [BsonElement("intensive_care_department")]
        public List<Department>? IntensiveCareDepartment { get; set; }

        [BsonElement("pharmacy")]
        public List<Department>? Pharmacy { get; set; }

        [BsonElement("support_services")]
        public List<Department>? SupportServices { get; set; }

        [BsonElement("refrigerator_of_the_dead")]
        public List<Department>? RefrigeratorOfTheDead { get; set; }

        [BsonElement("therapeutic_areas")]
        public List<Department>? TherapeuticAreas { get; set; }

        [BsonElement("basic_conditions_in_the_blood_bank")]
        public List<Department>? BasicConditionsInTheBloodBank { get; set; }

        [BsonElement("laboratory_section")]
        public List<Department>? LaboratorySection { get; set; }

        [BsonElement("day_surgery_center")]
        public List<Department>? DaySurgery { get; set; }

        [BsonElement("NarcoticDrugs")]
        public List<Department>? NarcoticDrugs { get; set; }

        [BsonElement("violation")]
        public List<Department>? violation { get; set; }

        [BsonElement("units")]
        public List<Department>? Units { get; set; }

        [BsonElement("general_requirements")]
        public List<Department>? GeneralRequirements { get; set; }

        [BsonElement("set_up")]
        public List<Department>? SetUp { get; set; }

        [BsonElement("clinics_in_health_centers")]
        public List<Department>? ClinicsHealthCenters { get; set; }

        [BsonElement("maternity_care")]
        public List<Department>? MaternityCare { get; set; }

        [BsonElement("child_care_and_immunizations")]
        public List<Department>? ChildImmunizations { get; set; }

        [BsonElement("non_communicable_diseases")]
        public List<Department>? Noncommunicablediseases { get; set; }

        [BsonElement("communicable_disease_control")]
        public List<Department>? CommunicablediseaseControl { get; set; }

        [BsonElement("oral_and_dental_health_services")]
        public List<Department>? OralandDentalhealth { get; set; }

        [BsonElement("laboratory")]
        public List<Department>? laboratory { get; set; }

        [BsonElement("radiology_services")]
        public List<Department>? RadiologyServices { get; set; }

        [BsonElement("pharmacy_services")]
        public List<Department>? PharmacyServices { get; set; }

        [BsonElement("infection_control_and_sterilization")]
        public List<Department>? InfectionControlSterilization { get; set; }

        [BsonElement("emergency_services")]
        public List<Department>? EmergencyServices { get; set; }

        [BsonElement("sterlization")]
        public List<Department>? Sterlization { get; set; }

        [BsonElement("other_doctors_clinics")]
        public List<Department>? OtherDoctorsClinics { get; set; }

        [BsonElement("labor_examination_department")]
        public List<Department>? LaborExaminationDepartment { get; set; }

        [BsonElement("radiology_department")]
        public List<Department>? RadiologyDepartment { get; set; }

        [BsonElement("management")]
        public List<Department>? Management { get; set; }

        [BsonElement("safety_measures")]
        public List<Department>? SafetyMeasures { get; set; }

        [BsonElement("residence_suites")]
        public List<Department>? ResidenceSuites { get; set; }

        [BsonElement("insulation_department")]
        public List<Department>? InsulationDepartment { get; set; }

        [BsonElement("therapeutic_areas_dialysis_department")]
        public List<Department>? TherapeuticDepartment { get; set; }

        [BsonElement("basic_conditions_in_a_blood_bank")]
        public List<Department>? BloodBank { get; set; }

        [BsonElement("emergency_department")]
        public List<Department>? EmergencydepartmentFurther { get; set; }

        [BsonElement("quality_standards_for_medical_gas_systems")]
        public List<Department>? GasSystem { get; set; }

        [BsonElement("doctors_clinics")]
        public List<Department>? DoctorsClinics { get; set; }

        [BsonElement("surgery_department")]
        public List<Department>? SurgeryDepartment { get; set; }

        [BsonElement("icu_department")]
        public List<Department>? IcuDepartment { get; set; }

        [BsonElement("requirements_surgery_center")]
        public List<Department>? RequirementSurgery { get; set; }

        [BsonElement("other_doctor_clinic")]
        public List<Department>? OtherClinic { get; set; }

        [BsonElement("general_medicine")]
        public List<Department>? GeneralMedicine { get; set; }

        [BsonElement("respiratory_medicine")]
        public List<Department>? RespiratoryMedicine { get; set; }

        [BsonElement("medicine_section")]
        public List<Department>? MedicineSection { get; set; }

        [BsonElement("insulation")]
        public List<Department>? insulation { get; set; }

        [BsonElement("clinic_standards_in_health_centers")]
        public List<Department>? ClinicStandards { get; set; }

        [BsonElement("control_of_communicable_diseases")]
        public List<Department>? ComunicableDisease { get; set; }

        [BsonElement("dental_health_services")]
        public List<Department>? DentalServices { get; set; }

        [BsonElement("standards")]
        public List<Department>? standards { get; set; }

        [BsonElement("medical_mission")]
        public List<Department>? MedicalMission { get; set; }

        [BsonElement("conditions")]
        public List<Department>? conditions { get; set; }

        [BsonElement("general_stats")]
        public List<Department>? GeneralStats { get; set; }

    }
}
