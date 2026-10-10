using Microsoft.AspNetCore.Mvc;
using Application.Entities;
using Application.IRepositories;
using System.Text;
using System.Text.Json;
using Application.DTOs.Form;
using Application.Entities.Form;
using Application.Entities.ExcelService;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Globalization;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class formController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly IFormSubmission _formSubmissionRepository;
        private readonly IUserRepository _userRepository;
        public formController(IConfiguration configuration, IFormSubmission formSubmisson, IUserRepository userRepository)
        {
            _configuration = configuration;
            _formSubmissionRepository = formSubmisson;
            _userRepository = userRepository;
        }
        // Create new form
        [HttpPost("create")]
        public async Task<IActionResult> CreateForm([FromBody] Application.Entities.Form.Form form)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var Url = _configuration["FormSecretKey:BaseUrl"];
            var requestUri = $"{Url}/api/form/?secret={Uri.EscapeDataString(key)}";
            try
            {
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(form);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Use a dedicated HttpClient instance for this request
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(requestUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var createdForm = System.Text.Json.JsonSerializer.Deserialize<Application.Entities.Form.Form>(jsonResponse);
                        return Ok(createdForm);
                    }

                    return StatusCode((int)response.StatusCode, new { message = "Error creating the form" });
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest(new { message = "Error serializing/deserializing JSON", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }
        //formbyId
        [HttpGet("formbyId")]
        public async Task<ActionResult> GetFormsbyId(
            [FromQuery] string user_id)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];


            string id = user_id;
            // Construct the request URI dynamically
            var requestUri = $"{baseUrl}/api/form?id={id}?secret={Uri.EscapeDataString(key)}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
                }

                return StatusCode((int)response.StatusCode, new { message = "Error retrieving forms from the API." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        [HttpPost("change-status")]
        public async Task<ActionResult> FormChangeStatus([FromBody] ChangeStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request payload.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];

            // Construct the base request URI (no query parameters for id and status)
            var requestUri = $"{baseUrl}/api/form/change-status?secret={Uri.EscapeDataString(key)}";

            // Serialize the request body
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                // Use PostAsync to send the POST request with the JSON body
                var response = await _httpClient.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
                }

                return StatusCode((int)response.StatusCode, new { message = "Error changing form status." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        //Allforms
        [HttpGet("all-forms")]
        public async Task<ActionResult> GetAllForms()
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];


            // Construct the request URI dynamically
            var requestUri = $"{baseUrl}/api/formListingAll?secret={Uri.EscapeDataString(key)}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
                }

                return StatusCode((int)response.StatusCode, new { message = "Error retrieving forms from the API." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        //FormSubmission
        [HttpPost("FormSubmission/create")]
        public async Task<IActionResult> CreateFormSubmission([FromBody] FormSubmissionsDto requestData)
        {
            try
            {

                if (requestData == null || string.IsNullOrEmpty(requestData.id))
                {
                    return BadRequest(new { data = "Invalid request data" });
                }
                var res = await _formSubmissionRepository.CreateFormSubmissionAsync(requestData);

                return Ok(new { data = "Submission Successfully created", submission = requestData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { data = "Submission not created", error = ex.Message });
            }
        }


        //formshowbyId
        [HttpGet("show/{id}")]
        public async Task<ActionResult> GetForms(
            [FromRoute] string id,
            [FromQuery] bool isDev = false,
            [FromQuery] string? public_route = null
            )
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];


            // Construct the request URI dynamically
            var requestUri = $"{baseUrl}/api/form/{id}?isDev={isDev}?public_route={public_route}?secret={Uri.EscapeDataString(key)}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
                }

                return StatusCode((int)response.StatusCode, new { message = "Error retrieving forms from the API." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        //form
        [HttpGet("")]
        public async Task<ActionResult> GetForms()
        {
            var query = HttpContext.Request.Query;

            // Manually parse `page` and `per_page` to handle NaN
            int page = TryParseInt(query["page"], 0);
            int per_page = TryParseInt(query["per_page"], 10);

            bool form_status = TryParseBool(query["form_status"], true);
            bool is_scheduled = TryParseBool(query["is_scheduled"], false);
            bool is_support = TryParseBool(query["is_support"], false);

            string tab_type = query["tab_type"].ToString() ?? "All";
            string? search = query["search"].ToString();
            string? fromDate = query["fromDate"].ToString();
            string? toDate = query["toDate"].ToString();

            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];

            var requestParams = new Dictionary<string, string>
    {
        { "page", page.ToString() },
        { "per_page", per_page.ToString() },
        { "form_status", form_status.ToString().ToLower() },
        { "is_scheduled", is_scheduled.ToString().ToLower() },
        { "is_support", is_support.ToString().ToLower() },
        { "tab_type", tab_type },
        { "search", search },
        { "fromDate", fromDate ?? string.Empty },
        { "toDate", toDate ?? string.Empty },
        { "secret", key }
    };

            var queryString = string.Join("&", requestParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            var requestUri = $"{baseUrl}/api/formListingData?{queryString}";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
                }

                return StatusCode((int)response.StatusCode, new { message = "Error retrieving forms from the API." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }



        // Get form by id
        [HttpGet("GetFormById")]
        public async Task<ActionResult<Application.Entities.Form.Form>> GetFormById([FromQuery] string id)
        {
            try
            {
                var form = await _formSubmissionRepository.GetFormByIdAsync(id);
                if (form == null)
                {
                    return NotFound("Form not found");
                }
                return Ok(form);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }


        // Edit form
        [HttpPost("UpdateForm")]
        public async Task<IActionResult> UpdateForm([FromBody] Application.Entities.Form.Form form)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var baseUrl = _configuration["FormSecretKey:BaseUrl"];

            // Construct the request URI dynamically
            var requestUri = $"{baseUrl}/api/form/update-form/?secret={Uri.EscapeDataString(key)}";

            try
            {
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(form);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Use a dedicated HttpClient instance for this request
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(requestUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var updatedForm = System.Text.Json.JsonSerializer.Deserialize<Application.Entities.Form.Form>(jsonResponse);
                        return Ok(updatedForm);
                    }

                    return StatusCode((int)response.StatusCode, new { message = "Error updating the form" });
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest(new { message = "Error serializing/deserializing JSON", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "HTTP request error occurred", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }


        // Delete form
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteForm(string id)
        {
            // Construct the URL with the provided id
            var key = _configuration["FormSecretKey:SecretKey"];
            var Url = _configuration["FormSecretKey:BaseUrl"];
            string url = $"{Url}/api/form/{id}?secret={key}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send DELETE request
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok("Delete request succeeded.");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, $"Delete request failed. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
        }

        // Open Engine
        [HttpGet("open-engine/{id}")]
        public async Task<ActionResult<Application.Entities.Form.Form>> GetOpenEngineForm(string id)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            _httpClient.BaseAddress = new Uri($"https://admin-builder.kakashi.app/api/form{id}?secret={key}");
            try
            {
                // Construct the endpoint for retrieving a form by ID
                var endpoint = $"forms/{id}";

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var form = System.Text.Json.JsonSerializer.Deserialize<Application.Entities.Form.Form>(jsonResponse);
                    return Ok(form);
                }

                return StatusCode((int)response.StatusCode, "Error retrieving the form from Open Engine");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Open form
        [HttpGet("open-form/{id}")]
        public async Task<ActionResult<Application.Entities.Form.Form>> GetOpenForm(string id)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            _httpClient.BaseAddress = new Uri("https://admin-builder.kakashi.app/api/form{id}?secret={key}");

            try
            {
                // Construct the endpoint for retrieving a form by ID
                var endpoint = $"forms/{id}";

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var form = System.Text.Json.JsonSerializer.Deserialize<Application.Entities.Form.Form>(jsonResponse);
                    return Ok(form);
                }

                return StatusCode((int)response.StatusCode, "Error retrieving the form from Open Form");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("export")]
        public async Task<IActionResult> GetAll([FromQuery] string id, [FromQuery] string? submissionId)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "Form ID is required" });
            }

            try
            {
                // Get user from context and fetch form submission data
                var user = await _userRepository.GetUserFromHttpContextAsync();
                dynamic submissions = await _formSubmissionRepository.GetFormSubmissionsAsync(id, submissionId, user);

                var result = await _formSubmissionRepository.GetSchemeFormByIdAsync(id);

                Response apiResponse;
                try
                {
                    apiResponse = JsonConvert.DeserializeObject<Response>(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Error deserializing API response: {ex.Message}" });
                }

                // Convert allSchemas to List<Dictionary<string, object>> for processing
                List<Dictionary<string, object>> allSchemas = ConvertToDictionaryList(apiResponse.data.schema.schema);
                string n = "form export";

                // Convert form to Dictionary<string, object>
                Dictionary<string, object> form = ConvertToDictionary(apiResponse.data);

                if (form.TryGetValue("name", out object nameObj) && nameObj is string name)
        {
                    n = name; // Use the form name if available
                }

                // Convert submissions to List<Dictionary<string, object>> (handles single object or collection)
                List<Dictionary<string, object>> submissionsList = ConvertToDictionaryList(submissions);

                var excelService = new ExcelService(id, allSchemas, submissionsList, form, _userRepository);

                // Generate the file name
                string timestamp = DateTime.Now.ToString("ddd, MMM dd, yyyy h_mm tt", CultureInfo.InvariantCulture);
                string fileName = $"{n}.xlsx";

                // Sanitize the file name to remove invalid characters
                var invalidChars = Path.GetInvalidFileNameChars();
                var cleanFileName = new string(fileName.Where(ch => !invalidChars.Contains(ch)).ToArray());

                // Generate the Excel file in memory
                using (var memoryStream = new MemoryStream())
                {
                    // Generate the Excel file directly into the memory stream
                    excelService.GenerateExcel(memoryStream, apiResponse.data);

                    // Reset the stream position to the beginning
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Encode the file name for the Content-Disposition header
                    var encodedFileName = Uri.EscapeDataString(cleanFileName);

                    // Set the Content-Disposition header with the encoded file name
                    //Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{encodedFileName}";

                    // Return the file for download
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", encodedFileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        //[HttpGet("export")]
        //public async Task<IActionResult> GetAll([FromQuery] string id, [FromQuery] string? submissionId)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return BadRequest(new { message = "Form ID is required" });
        //    }

        //    try
        //    {
        //        // Get user from context and fetch form submission data
        //        var user = await _userRepository.GetUserFromHttpContextAsync();
        //        dynamic submissions = await _formSubmissionRepository.GetFormSubmissionsAsync(id, submissionId, user);

        //        var result = await _formSubmissionRepository.GetSchemeFormByIdAsync(id);
        //        // return Ok(new { data = new { data = submissions, schema = result } });


        //        Response apiResponse;
        //        try
        //        {
        //            apiResponse = JsonConvert.DeserializeObject<Response>(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(new { message = $"Error deserializing API response: {ex.Message}" });
        //        }

        //        // Convert allSchemas to List<Dictionary<string, object>> for processing
        //        List<Dictionary<string, object>> allSchemas = ConvertToDictionaryList(apiResponse.data.schema.schema);
        //        string n = "form export";
        //        // Convert form to Dictionary<string, object>
        //        Dictionary<string, object> form = ConvertToDictionary(apiResponse.data);

        //        if (form.TryGetValue("name", out object nameObj) && nameObj is string name)
        //        {
        //             n = name;
        //        }
        //        // Convert submissions to List<Dictionary<string, object>> (handles single object or collection)
        //        List<Dictionary<string, object>> submissionsList = ConvertToDictionaryList(submissions);

        //        var excelService = new ExcelService(id, allSchemas, submissionsList, form, _userRepository);

        //        // Get the Downloads folder path and prepare the temporary folder to save the files
        //        var downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        //        var tempFolder = Path.Combine(downloadsPath, "GeneratedReports");

        //        // Ensure the folder exists
        //        if (!Directory.Exists(tempFolder))
        //        {
        //            Directory.CreateDirectory(tempFolder);
        //        }

        //        // Prepare the file paths for storing the generated Excel files
        //        var filePaths = new List<string>();
        //        var regionStats = excelService.GenerateStats();


        //        var totalStatsFilePath = Path.Combine(tempFolder, "TotalStats.xlsx");
        //        excelService.GenerateTotalStatsExcel(totalStatsFilePath);

        //        var regionFilePath = Path.Combine(tempFolder, "Region.xlsx");
        //        excelService.GenerateRegionStatsExcel(regionFilePath); // Excel generation method
        //                                                               // Loop over each region stats and generate Excel files accordingly

        //        string timestamp = DateTime.Now.ToString("ddd, MMM dd, yyyy h_mm tt");

        //        string fileName = $"{n} - {timestamp}.xlsx";
        //        var filePath = Path.Combine(tempFolder, fileName);
        //        excelService.GenerateExcel(filePath,apiResponse.data);

        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            return BadRequest(new { message = "Failed to generate Excel files." });
        //        }

        //        // Encode the file name for ASCII-safe headers
        //        var asciiFileName = fileName; // Fallback for Content-Disposition
        //        var utf8FileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(n));


        //        // Read file bytes

        //        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //        // Set Content-Disposition header (ASCII-safe)
        //        Response.Headers["Content-Disposition"] = $"attachment; filename=\"{utf8FileName}\"";



        //        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
        //    }
        //}


        // Helper method to convert dynamic data to List<Dictionary<string, object>>
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
        // Helper method to convert dynamic data to Dictionary<string, object>
        private Dictionary<string, object> ConvertToDictionary(dynamic data)
        {
            var dict = new Dictionary<string, object>();

            if (data is IDictionary<string, object> dictionary)
            {
                // If the data is already a dictionary, cast it to Dictionary<string, object>
                return dictionary as Dictionary<string, object> ?? new Dictionary<string, object>(dictionary);
            }
            else
            {
                // If the data is a dynamic object, use reflection to create a dictionary
                foreach (var property in data.GetType().GetProperties())
                {
                    dict[property.Name] = property.GetValue(data);
                }
            }

            return dict;
        }


        // Helper method to zip the files
        private void ZipFiles(List<string> filePaths, string zipFilePath)
        {
            // Create a temporary directory to store the files to be zipped
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            // Copy each file to the temporary directory
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var destFilePath = Path.Combine(tempDir, fileName);
                //File.Copy(filePath, destFilePath);
            }

            // Now create a zip file from the directory
            ZipFile.CreateFromDirectory(tempDir, zipFilePath);

            // Optionally, delete the temporary directory and its contents after zipping
            Directory.Delete(tempDir, true);
        }


        // Utility method to safely parse integers (handles NaN, null, and invalid values)
        private int TryParseInt(string? value, int defaultValue)
        {
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        // Utility method to safely parse booleans (default is false if invalid)
        private bool TryParseBool(string? value, bool defaultValue)
        {
            return bool.TryParse(value, out bool result) ? result : defaultValue;
        }

    }

}