﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Control_Panel.Administration.Update
{
    public class UpdateCompanyDto
    {
        public long CompanyId { get; set; }

        [StringLength(100)]
        public string ComUniqueId { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string CompanyCode { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        [StringLength(100)]
        public string SiteThumbnailPath { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ContractStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ContractExpireDate { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public byte[] CompanyPic { get; set; }

        [StringLength(250)]
        public string CompanyLogoPath { get; set; }

        [StringLength(50)]
        public string CompanyImageFormat { get; set; }

        public byte[] ReportPic { get; set; }

        [StringLength(250)]
        public string ReportLogoPath { get; set; }

        [StringLength(50)]
        public string ReportImageFormat { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(300)]
        public string AttendanceDeviceLocation { get; set; }

        public long OrganizationId { get; set; }
    }
}
