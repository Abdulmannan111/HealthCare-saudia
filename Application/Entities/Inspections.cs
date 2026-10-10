using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class Inspections : AuditInfoComplete
    {

        [BsonElement("auditMethod")]
        public string AuditMethod { get; set; } = string.Empty;

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;

        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;

        [BsonElement("city")]
        public string City { get; set; } = string.Empty;

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("inspectorPhone")]
        public string InspectorPhone { get; set; } = string.Empty;

        [BsonElement("nameNumberWritten")]
        public string NameNumberWritten { get; set; } = string.Empty;

        [BsonElement("licenseInClearPlace")]
        public string LicenseInClearPlace { get; set; } = string.Empty;

        [BsonElement("sectionPrescriptionDrugs")]
        public string SectionPrescriptionDrugs { get; set; } = string.Empty;

        [BsonElement("medicalSpecialSection")]
        public string MedicalSpecialSection { get; set; } = string.Empty;

        [BsonElement("separatePreparationLab")]
        public string SeparatePreparationLab { get; set; } = string.Empty;

        [BsonElement("storageRequirementSection")]
        public string StorageRequirementSection { get; set; } = string.Empty;

        [BsonElement("systemInCompliance")]
        public string SystemInCompliance { get; set; } = string.Empty;

        [BsonElement("RecordsAvailable")]
        public string RecordsAvailable { get; set; } = string.Empty;

        [BsonElement("psychiatricControlledRegister")]
        public string PsychiatricControlledRegister { get; set; } = string.Empty;

        [BsonElement("dispensedPrescription")]
        public string DispensedPrescription { get; set; } = string.Empty;

        [BsonElement("humidityInPharmacy")]
        public string HumidityInPharmacy { get; set; } = string.Empty;

        [BsonElement("pharmacyThermometer")]
        public string PharmacyThermometer { get; set; } = string.Empty;

        [BsonElement("refrigeratorThermometer2_8")]
        public string RefrigeratorThermometer2_8 { get; set; } = string.Empty;

        [BsonElement("refrigeratorThermometer10_25")]
        public string RefrigeratorThermometer10_25 { get; set; } = string.Empty;

        [BsonElement("measuringCalibration")]
        public string MeasuringCalibration { get; set; } = string.Empty;

        [BsonElement("safetyMeans")]
        public string SafetyMeans { get; set; } = string.Empty;

        [BsonElement("pharmaceuticalServices")]
        public string PharmaceuticalServices { get; set; } = string.Empty;

        [BsonElement("reportSideEffects")]
        public string ReportSideEffects { get; set; } = string.Empty;

        [BsonElement("approvedPharmacySeal")]
        public string ApprovedPharmacySeal { get; set; } = string.Empty;

        [BsonElement("affectedMaterials")]
        public string AffectedMaterials { get; set; } = string.Empty;

        [BsonElement("medicineStorageAvailable")]
        public string MedicineStorageAvailable { get; set; } = string.Empty;

        [BsonElement("guidanceBoardsAvailable")]
        public string GuidanceBoardsAvailable { get; set; } = string.Empty;

        [BsonElement("computerAvailable")]
        public string ComputerAvailable { get; set; } = string.Empty;

        [BsonElement("electronicSystemAvailable")]
        public string ElectronicSystemAvailable { get; set; } = string.Empty;

        [BsonElement("receiptDisbursementRecord")]
        public string ReceiptDisbursementRecord { get; set; } = string.Empty;

        [BsonElement("InsectControlSystem")]
        public string InsectControlSystem { get; set; } = string.Empty;

        [BsonElement("NarcoticPsychotropicSubstances")]
        public string NarcoticPsychotropicSubstances { get; set; } = string.Empty;

        [BsonElement("pharmaceuticalAccordingToList")]
        public string PharmaceuticalAccordingToList { get; set; } = string.Empty;

        [BsonElement("expiredProductsAvailable")]
        public string ExpiredProductsAvailable { get; set; } = string.Empty;

        [BsonElement("nonRegisteredProductsAvailable")]
        public string NonRegisteredProductsAvailable { get; set; } = string.Empty;

        [BsonElement("nonRegisteredDevicesAvailable")]
        public string NonRegisteredDevicesAvailable { get; set; } = string.Empty;

        [BsonElement("careCenterPerscription")]
        public string CareCenterPerscription { get; set; } = string.Empty;

        [BsonElement("scientificNameDrugs")]
        public string ScientificNameDrugs { get; set; } = string.Empty;

        [BsonElement("practitionersLicensed")]
        public string PractitionersLicensed { get; set; } = string.Empty;

        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
