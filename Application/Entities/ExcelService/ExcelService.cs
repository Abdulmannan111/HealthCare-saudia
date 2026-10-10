using System.Data;
using System.Globalization;
using Application.IRepositories;
using OfficeOpenXml;

namespace Application.Entities.ExcelService
{
    public class ExcelService
    {
        private readonly string _formId;
        private readonly IUserRepository _userRepository;
        private readonly List<Dictionary<string, object>> _data;
        private readonly Dictionary<string, object> _form;
        private readonly bool _isOtherExcel;
        private List<Dictionary<string, object>> _extractedData;
        private List<Dictionary<string, object>> _stats;
        private List<Dictionary<string, object>> _totals;

        public ExcelService(string formId, List<Dictionary<string, object>> schema, List<Dictionary<string, object>> data, Dictionary<string, object> form, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _formId = formId ?? throw new ArgumentNullException(nameof(formId));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _form = form ?? throw new ArgumentNullException(nameof(form));
            // might need to implement 
            //_isOtherExcel = form.ContainsKey("meta_data") && form["meta_data"] is Dictionary<string, object> metaData && metaData.ContainsKey("dynamic_excel");

            //if (_isOtherExcel)
            //{
            //    // Handle dynamic Excel logic if needed
            //    return;
            //}

            //if (_formId == "66404a888fc238b1660689cc") // Example form ID (e.g., Medicine availability)
            //{
            //    // Add specific handling for a specific form (e.g., Medicine availability)
            //    return;
            //}

            _extractedData = ProcessExtractedData();
            _stats =  GenerateStats();
            _totals = GenerateTotals();
        }

        // Process extracted data and return as a list of dictionaries
        private List<Dictionary<string, object>> ProcessExtractedData()
        {
            var extractedData = new List<object>();
            foreach (var item in _data)
            {
                var extractedData_ = ExtractData(item);
                foreach (var row in extractedData_)
                {
                    if (row.Region != null)
                    {
                        extractedData.Add(row);
                    }
                }
            }
            var data = ConvertToDictionaryList(extractedData);
            return data;
        }

        private List<Dictionary<string, object>> ConvertToDictionaryList(dynamic data)
        {
            var list = new List<Dictionary<string, object>>();

            if (data is IEnumerable<dynamic> collection)
            {
                // If the data is a collection (like a list or array)
                foreach (var item in collection)
                {
                    var dict = new Dictionary<string, object>();
                    if (item.GetType().IsClass)
                    {
                        foreach (var property in item.GetType().GetProperties())
                        {
                            dict[property.Name] = property.GetValue(item);
                        }
                    }
                    else
                    {
                        foreach (var property in item)
                        {
                            dict[property.Name] = property.Value;
                        }
                    }

                    list.Add(dict);
                }
            }
            else if (data is IDictionary<string, object> dictionary)
            {
                // If the data is a single dictionary
                var dict = new Dictionary<string, object>();
                foreach (var key in dictionary.Keys)
                {
                    dict[key] = dictionary[key];
                }
                list.Add(dict);
            }
            else if (data is object obj)
            {
                // If the data is a single object, we can create a dictionary with its properties
                var dict = new Dictionary<string, object>();
                foreach (var property in obj.GetType().GetProperties())
                {
                    dict[property.Name] = property.GetValue(obj);
                }
                list.Add(dict);
            }

            return list;
        }

        // Generate statistics for regions

        public  List<Dictionary<string, object>> GenerateStats()
        {
            var userIds = _extractedData.Select(item => item["UserId"]?.ToString()).Distinct().ToList();

            var userDetails = _userRepository.GetUsersByIdsAsync(userIds);
            var regionCounts = new Dictionary<string, RegionStats>();

            // Step 1: Aggregate Data
            foreach (var item in _extractedData)
            {
                //var fieldLists = ExtractFieldLists(item);
                //var totalCount = fieldLists.Values.FirstOrDefault()?.Count ?? 0;

                //if (totalCount > 1)
                //{
                    var region = item["Region"].ToString();
                    if (!regionCounts.ContainsKey(region))
                    {
                        regionCounts[region] = new RegionStats
                        {
                            Users = new HashSet<string>(),
                            Comply = 0,
                            NotComply = 0,
                            Na = 0
                        };
                    }

                    var userId = item["UserId"]?.ToString();
                    var answerList = item["Answer"] as List<string>;

                    if (userId != null && answerList.Count > 1)
                    {
                    regionCounts[region].Users.Add(userId);
                            if (userDetails.TryGetValue(userId, out var userInfo))
                            {
                                item["UserEmail"] = userInfo.UserEmail;
                                item["UserName"] = userInfo.UserName;
                            }
                        }

                    for (int i = 1; i < answerList.Count; i++)
                    {
                        switch (answerList[i])
                        {
                            case "Comply":
                                regionCounts[region].Comply++;
                                break;
                            case "Not Comply":
                                regionCounts[region].NotComply++;
                                break;
                            case "N/A":
                                regionCounts[region].Na++;
                                break;
                        }
                    }
                }
            //}


            string s = null;
            // Step 4: Generate Output
            return regionCounts.Select(r => new Dictionary<string, object>
            {
                { "Region", r.Key },
                { "Total Users", r.Value.Users.Count }, // Only existing users
                { "Comply", r.Value.Comply },
                { "Not Comply", r.Value.NotComply },
                { "Not Applicable", r.Value.Na }
            }).ToList();
                }


        // Generate totals for submissions
        private List<Dictionary<string, object>> GenerateTotals()
        {
            int totalSubmissions = _data.Count;
            int totalSelfAssessments = totalSubmissions;

            return new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Total Submissions", totalSubmissions },
                    { "Total Inspections", 0 },
                    { "Total Self Assessments", totalSelfAssessments }
                }
            };
        }

        // Generate Excel for region stats
        public void GenerateRegionStatsExcel(string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var statsWorksheet = package.Workbook.Worksheets.Add("Region Stats");
                var regionStats = GenerateStats(); // Get region statistics

                // Write headers for region stats
                statsWorksheet.Cells[1, 1].Value = "Region";
                statsWorksheet.Cells[1, 2].Value = "Total Users";
                statsWorksheet.Cells[1, 3].Value = "Comply";
                statsWorksheet.Cells[1, 4].Value = "Not Comply";
                statsWorksheet.Cells[1, 5].Value = "Not Applicable";

                // Fill data for region stats
                for (int i = 0; i < regionStats.Count; i++)
                {
                    var row = regionStats[i];
                    statsWorksheet.Cells[i + 2, 1].Value = row["Region"];
                    statsWorksheet.Cells[i + 2, 2].Value = row["Total Users"];
                    statsWorksheet.Cells[i + 2, 3].Value = row["Comply"];
                    statsWorksheet.Cells[i + 2, 4].Value = row["Not Comply"];
                    statsWorksheet.Cells[i + 2, 5].Value = row["Not Applicable"];
                }

                // Save the region stats Excel file
                package.SaveAs(new FileInfo(filePath));
            }
        }

        // Generate Excel for total stats
        public void GenerateTotalStatsExcel(string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var totalsWorksheet = package.Workbook.Worksheets.Add("Total Stats");
                var totalStats = GenerateTotals(); // Get total statistics

                // Write headers for total stats
                totalsWorksheet.Cells[1, 1].Value = "Total Submissions";
                totalsWorksheet.Cells[1, 2].Value = "Total Inspections";
                totalsWorksheet.Cells[1, 3].Value = "Total Self Assessments";

                // Fill data for total stats
                for (int i = 0; i < totalStats.Count; i++)
                {
                    var row = totalStats[i];
                    totalsWorksheet.Cells[i + 2, 1].Value = row["Total Submissions"];
                    totalsWorksheet.Cells[i + 2, 2].Value = row["Total Inspections"];
                    totalsWorksheet.Cells[i + 2, 3].Value = row["Total Self Assessments"];
                }

                // Save the total stats Excel file
                package.SaveAs(new FileInfo(filePath));
            }
        }
        

        private List<string> GetListFromObject(object obj)
        {
            return obj is IEnumerable<string> list ? list.ToList() : new List<string> { obj?.ToString() ?? "N/A" };
        }

        public void GenerateExcel(Stream filePath, Data data)
        {
            try
            {
                using (var package = new ExcelPackage(filePath))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Report");

                    // Set headers dynamically
                    var headers = new[] { "#No", "Report No", "Region", "Cluster", "Site", "Inspection Type", "Step Name", "Question Number", "Standard", "Category", "Healthcare Quality", "Risk (Likelihood x Impact)", "Likelihood", "Impact", "Risk Value", "Answer", "Not Comply Reason", "User  Name", "User  Email", "Submission Date" };
                    
                    for (int col = 0; col < headers.Length; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = headers[col];
                    }
                    int no = 0;
                    // Fill data rows
                    for (int row = 0; row < _extractedData.Count; row++)
                    {

                        var dataRow = _extractedData[row];
                        var fieldLists = ExtractFieldLists(dataRow);
                        var totalCount = fieldLists.Values.FirstOrDefault().Count;
                        for (int col = 1; col < totalCount; col++)
                        {
                            // Set values for each column
                            worksheet.Cells[no + 2, 1].Value = row + 1; // #No (1-based index)
                            worksheet.Cells[no + 2, 2].Value = dataRow["ReportNo"]; // Report No
                            worksheet.Cells[no + 2, 3].Value = dataRow["Region"];
                            worksheet.Cells[no + 2, 4].Value = dataRow["Cluster"];
                            worksheet.Cells[no + 2, 5].Value = dataRow["Site"];
                            worksheet.Cells[no + 2, 6].Value = dataRow["InspectionType"]; // Assuming you have this field

                            // Extract Likelihood & Impact for risk calculation
                            double likelihood = col < fieldLists[13].Count ? GetDoubleValue(fieldLists[13][col]) : 0;
                            double impact = col < fieldLists[14].Count ? GetDoubleValue(fieldLists[14][col]) : 0;

                            // Invoke delegate to calculate risk
                            (double riskValue, string riskCategory) = calculateRisk(likelihood, impact);

                            // Store calculated risk value and category in the worksheet
                            worksheet.Cells[no + 2, 12].Value = riskCategory;
                                                                              // Populate list-based fields dynamically
                            foreach (var field in fieldLists)
                            {
                                worksheet.Cells[no + 2, field.Key].Value = col < field.Value.Count ? field.Value[col] : "N/A";
                            }

                            worksheet.Cells[no + 2, 18].Value = dataRow["UserName"]; // Ensure this field exists
                            worksheet.Cells[no + 2, 19].Value = dataRow["UserEmail"]; // Ensure this field exists
                            worksheet.Cells[no + 2, 20].Value = dataRow["created_at"]; // Ensure this field exists
                            no++;
                        }
                    }

                    // Add stats and totals as separate sheets
                    var statsWorksheet = package.Workbook.Worksheets.Add("Stats");
                    statsWorksheet.Cells["A1"].LoadFromCollection(_stats, true);

                    var totalsWorksheet = package.Workbook.Worksheets.Add("Totals");
                    totalsWorksheet.Cells["A1"].LoadFromCollection(_totals, true);

                    // Save the generated Excel file
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                throw; // Consider logging the exception for debugging
            }
        }
        public List<ItemData> ExtractData(Dictionary<string, object> data, string reportNo = null, string schema_version = null, string status = null, string inspection_date = null, string user_id = null, string created_at = null)
        {
            var extractedData = new List<ItemData>();

            // Check if data is not null
            if (data != null)
            {
                // Initialize index starting at 1 (or adjust as needed)
                int index = 1;

                // Iterate through each key-value pair in the dictionary
                foreach (var item in data)
                {
                    reportNo = data.ContainsKey("report_no") ? data["report_no"]?.ToString() : null;
                    status = data.ContainsKey("status") ? data["status"]?.ToString() : null;
                    schema_version = data.ContainsKey("schema_version") ? data["schema_version"]?.ToString() : null;
                    inspection_date = data.ContainsKey("inspection_date") ? data["inspection_date"]?.ToString() : null;
                    user_id = data.ContainsKey("user_id") ? data["user_id"]?.ToString() : null;
                    created_at = data.ContainsKey("CreatedDate") && DateTime.TryParse(data["CreatedDate"]?.ToString(), out DateTime dt)
    ? dt.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture)
    : null;

                    // Check if item.Value is of a dictionary type
                    if (item.Value is Dictionary<string, object> valueDict)
                    {
                        // Create an instance of ItemData to hold extracted values
                        var itemData = new ItemData();

                        // Extract values based on the key, passing index by reference
                        ExtractValues(valueDict, itemData, ref index, reportNo, schema_version, status, inspection_date, user_id);
                        itemData.ReportNo = reportNo;
                        itemData.Status = status;
                        itemData.SchemaVersion = schema_version;
                        itemData.created_at = created_at;
                        itemData.UserId = user_id;
                        // Add the ItemData object to the result list
                        extractedData.Add(itemData);
                    }
                    else
                    {
                        // Handle case where item.Value is not a Dictionary<string, object>
                        // Maybe log an error or skip the item
                        Console.WriteLine("item.Value is not of the expected type.");
                    }
                }
            }

            return extractedData;
        }

        // Recursively extracts values and stores them in ItemData
        private void ExtractValues(object value, ItemData itemData, ref int index, string reportNo, string schema_version, string status, string inspection_date, string user_id)
        {
            if (value is Dictionary<string, object> itemDict)
            {
                // If the value is a dictionary, go through its keys
                foreach (var kvp in itemDict)
                {
                    if (kvp.Key.Equals("site"))
                    {
                        itemData.Site = itemDict.ContainsKey("site") &&
                                          itemDict["site"] is Dictionary<string, object> regionDict &&
                                          regionDict.ContainsKey("label") ?
                                          regionDict["label"]?.ToString() : "N/A";
                    }
                    else if (kvp.Key.Equals("cluster"))
                    {
                        itemData.Cluster = itemDict.ContainsKey("cluster") &&
                                          itemDict["cluster"] is Dictionary<string, object> regionDict &&
                                          regionDict.ContainsKey("label") ?
                                          regionDict["label"]?.ToString() : "N/A";
                    }
                    else if (kvp.Key.Equals("region"))
                    {
                        itemData.Region = itemDict.ContainsKey("region") &&
                                          itemDict["region"] is Dictionary<string, object> regionDict &&
                                          regionDict.ContainsKey("label") ?
                                          regionDict["label"]?.ToString() : "N/A";
                    }
                    else if (kvp.Key.Equals("Inspection Type"))
                    {
                        itemData.InspectionType = inspection_date ?? "N/A";
                    }
                    else if (kvp.Key.Equals("step"))
                    {
                        if (itemDict.ContainsKey("step") && itemDict["step"] != null)
                        {
                            itemData.StepName.AddRange(itemDict["step"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("no"))
                    {
                        if (itemDict.ContainsKey("no") && itemDict["no"] != null)
                        {
                            itemData.QuestionNumber.AddRange(itemDict["no"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("title"))
                    {
                        if (itemDict.ContainsKey("title") && itemDict["title"] != null)
                        {
                            itemData.Standard.AddRange(itemDict["title"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("Category"))
                    {
                        if (itemDict.ContainsKey("Category") && itemDict["Category"] != null)
                        {
                            itemData.Category.AddRange(itemDict["Category"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("Healthcare Quality"))
                    {
                        if (itemDict.ContainsKey("Healthcare Quality") && itemDict["Healthcare Quality"] != null)
                        {
                            itemData.HealthcareQuality.AddRange(itemDict["Healthcare Quality"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("Likelihood"))
                    {
                        if (itemDict.ContainsKey("Likelihood") && itemDict["Likelihood"] != null)
                        {
                            itemData.Likelihood.AddRange(itemDict["Likelihood"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("Impact"))
                    {
                        if (itemDict.ContainsKey("Impact") && itemDict["Impact"] != null)
                        {
                            itemData.Impact.AddRange(itemDict["Impact"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("risk"))
                    {
                        if (itemDict.ContainsKey("risk") && itemDict["risk"] != null)
                        {
                            itemData.RiskValue.AddRange(itemDict["risk"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("question"))
                    {
                        if (itemDict.ContainsKey("question") && itemDict["question"] != null)
                        {
                            itemData.Answer.AddRange(itemDict["question"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("reason"))
                    {
                        if (itemDict.ContainsKey("reason") && itemDict["reason"] != null)
                        {
                            itemData.NotComplyReason.AddRange(itemDict["reason"].ToString().Split(',').Select(s => s.Trim()));
                        }
                    }
                    else if (kvp.Key.Equals("report_no"))
                    {
                        itemData.ReportNo = itemDict.ContainsKey("report_no") ? itemDict["report_no"]?.ToString() : "N/A";
                    }
                    else
                    {
                        Console.WriteLine("inspectors_data key not found in itemDict.");
                    }

                    // Recursively check nested dictionaries
                    ExtractValues(kvp.Value, itemData, ref index, reportNo, schema_version, status, inspection_date, user_id);
                }
            }
            else if (value is List<object> itemList)
            {
                // If the value is a list, iterate over each item
                foreach (var item in itemList)
                {
                    // Recursively check each item in the list
                    ExtractValues(item, itemData, ref index, reportNo, schema_version, status, inspection_date, user_id);
                }
            }
            else
            {
                // If it's a simple value, assign it directly to the corresponding property
                itemData.OtherData = value?.ToString() ?? "N/A"; // General fallback if no specific key match              
            }
        }
        Func<double, double, (double, string)> calculateRisk = (likelihood, impact) =>
        {
            double riskValue = likelihood * impact;
            string riskCategory = riskValue switch
            {
                <= 2 => "Minor",
                >= 3 and <= 9 => "Moderate",
                >= 10 and <= 14 => "Major",
                _ => "Catastrophic"
            };

            return (riskValue, riskCategory);
        };

        private Dictionary<int, List<string>> ExtractFieldLists(Dictionary<string, object> dataRow)
        {
            return new Dictionary<int, List<string>>
    {
        { 7, GetListFromObject(dataRow["StepName"]) },
        { 8, GetListFromObject(dataRow["QuestionNumber"]) },
        { 9, GetListFromObject(dataRow["Standard"]) },
        { 10, GetListFromObject(dataRow["Category"]) },
        { 11, GetListFromObject(dataRow["HealthcareQuality"]) },
        { 15, GetListFromObject(dataRow["RiskValue"]) },
        { 13, GetListFromObject(dataRow["Likelihood"]) },
        { 14, GetListFromObject(dataRow["Impact"]) },
        { 16, GetListFromObject(dataRow["Answer"]) },
        { 17, GetListFromObject(dataRow["NotComplyReason"]) }
    };
        }
        double GetDoubleValue(object value)
        {
            if (value is List<string> list)
                return double.TryParse(list.FirstOrDefault(), out double result) ? result : 0;
            if (value is string str)
                return double.TryParse(str, out double result) ? result : 0;
            if (value is double dbl)
                return dbl;
            if (value is int intVal)
                return intVal;
            return 0; // Default if unconvertible
        }
    }
}